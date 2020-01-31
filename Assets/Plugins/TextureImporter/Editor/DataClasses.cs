using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ImgImporter
{
    public class PSDFile
    {
        public PSDFile(string path)
        {
            psd = new PhotoshopFile.PsdFile(path, System.Text.Encoding.Default);
        }

        private PhotoshopFile.PsdFile psd;

        public List<PhotoshopFile.Layer> Layers { get { return psd.Layers; } }
    }

    public class LayerComparer : IComparer
    {
        public int Compare(System.Object x, System.Object y)
        {
            LayerInformation layer1 = x as LayerInformation;
            LayerInformation layer2 = y as LayerInformation;

            string name1 = PSDImportWindow.DiscardSpace(layer1.Name);
            string name2 = PSDImportWindow.DiscardSpace(layer2.Name);

            if (name1.EndsWith("_main") && name2.EndsWith("_main"))
                return string.Compare(name1, name2);
            else if (name1.EndsWith("_main"))
                return -1;
            else if (name2.EndsWith("_main"))
                return 1;

            return string.Compare(name1, name2);
        }
    }

    public class LayerInformation
    {
        public int order;

        public string Name { get { return layer.Name; } }
        public string assetPath;
        public Rect geometry { get { return layer.Rect; } }
        public bool Visible { get { return layer.Visible; } set { layer.Visible = value; } }
        public float ppu;
        private PhotoshopFile.Layer layer;

        public LayerInformation(PhotoshopFile.Layer layer)
        {
            this.layer = layer;
        }

        //TODO: try to speed this up
        public Texture2D CreateTexture()
        {
            if ((int)layer.Rect.width == 0 || (int)layer.Rect.height == 0)
                return null;

            Texture2D tex = new Texture2D((int)layer.Rect.width, (int)layer.Rect.height, TextureFormat.RGBA32, true);
            Color32[] pixels = new Color32[tex.width * tex.height];

            var red = (from l in layer.Channels where l.ID == 0 select l).First();
            var green = (from l in layer.Channels where l.ID == 1 select l).First();
            var blue = (from l in layer.Channels where l.ID == 2 select l).First();
            var alpha = layer.AlphaChannel;

            for (int i = 0; i < pixels.Length; i++)
            {
                byte r = red.ImageData[i];
                byte g = green.ImageData[i];
                byte b = blue.ImageData[i];
                byte a = 255;

                if (alpha != null)
                    a = alpha.ImageData[i];

                int mod = i % tex.width;
                int n = ((tex.width - mod - 1) + i) - mod;
                pixels[pixels.Length - n - 1] = new Color32(r, g, b, a);
            }

            tex.SetPixels32(pixels);
            tex.Apply();
            return tex;
        }

        public Sprite SaveSprite(Texture2D tex, string assetPath, string name, float pixelsToUnitSize)
        {
            if (tex == null)
                return null;

            string path = Path.Combine(assetPath,name + ".png");

            byte[] buf = tex.EncodeToPNG();
            File.WriteAllBytes(path, buf);
            AssetDatabase.Refresh();
            // Load the texture so we can change the type
            AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.maxTextureSize = 8192;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.spritePivot = new Vector2(0.5f, 0.5f);
            textureImporter.spritePixelsPerUnit = pixelsToUnitSize;
            textureImporter.mipmapEnabled = false;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            return (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
        }
    }

    // Container for information about the scene part that is to be constructed from PSD
    // TODO: This should be sored in a metafile in order to construct objects from it without PSD file
    [System.Serializable]
    public struct ImageToObjectData
    {
        public string spriteName;
        public Rect scale;
        public Rect originalGeometry;
        public int order;
        public bool mirrorImage;
        public string spritePath;
        public float ppu;
    }
}

// Files structure:
// -- Configuration files
// \TextureImporter\Configuration\
//
//
// -- Sources that are put in this folder for further conversion. Not added to the build
// \.PSDSources\
//
//
// -- Chashing And Temporaries
// \.temp\
//
//
// -- Files converted from the psd
// \ImportedImageAssets\
//
//