using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace ImgImporter
{
    /// <summary>
    /// Use this asset to create the scene part. Made based on the info in the psd file.
    /// </summary>

    /// <summary>
    /// Parses the name of the file to expose the metadata info for further use
    /// Can by implicitly cast to bool for easy use with if clause
    /// </summary>
    public class ParsedName
    {
        private string nameToParse;
        private string uniqueName;
        private string mirrorName;
        private bool main;

        private bool successParsing;

        public bool SuccessParsing { get { return successParsing; } }

        // Now this is a real issue. How to parse the existing images
        // So they are working well with the parsed PSD data
        // As we have a lot existing files, we need the way to formalize the new ones to fit patterns
        public ParsedName(string nameToParse)
        {
            this.nameToParse = nameToParse;
            var split = nameToParse.ToLower().Replace(' ', '_').Split('_');
            if (split.Length > 0)
            {
                uniqueName = split[0];
                successParsing = true;
            }
            else
            {
                successParsing = false;
            }
            main = split.Any(x => x == "main");
            mirrorName = split.FirstOrDefault(x => x.Contains("mirror"));
        }

        public static implicit operator bool(ParsedName n)
        {
            return n.successParsing;
        }
    }

    public class PSDParser
    {
        private PSDFile currentFile;
        private LayerInformation[] layers;
        private List<ImageToObjectData> objectDatas;
        private string assetPath;

        public bool objectsCreated { get { return objectDatas != null && objectDatas.Count > 0; } }

        public LayerInformation[] Layers { get { return layers; } }
        public List<ImageToObjectData> ObjectDatas { get { return objectDatas; } }


        public PSDParser(string path)
        {
            OpenFileAndGetLayers(path);
        }

        private void OpenFileAndGetLayers(string path)
        {
            assetPath = path;
            var file = new PSDFile(path);

            layers = new LayerInformation[file.Layers.Count];
            for (int i = 0; i < file.Layers.Count; i++)
            {
                layers[i] = new LayerInformation(file.Layers[i]);
                layers[i].order = i;
            }
        }

        public void CreateObsDataFromImgs()
        {
            if (layers == null || layers.Length == 0) return;

            objectDatas = new List<ImageToObjectData>();
            for (int i = 0; i < layers.Length; ++i)
            {
                if (layers[i].Visible && layers[i].assetPath != null)
                    objectDatas.Add(CreateObjectsData(layers[i]));
            }
        }


        private ImageToObjectData CreateObjectsData(LayerInformation imgs)
        {
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(imgs.assetPath);
            var importer = AssetImporter.GetAtPath(imgs.assetPath) as TextureImporter;

            return new ImageToObjectData
            {
                ppu = importer.spritePixelsPerUnit,
                spriteName = imgs.Name,
                scale = sprite.rect,
                originalGeometry = imgs.geometry,
                order = imgs.order,
                spritePath = imgs.assetPath
            };
        }
    }
}