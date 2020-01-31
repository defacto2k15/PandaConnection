using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ImgImporter
{
    public class PSDImportWindow : EditorWindow
    {
        public static PSDImportWindow instance;
        private string psdPath;
        private string outPath = "Assets/";
        private Vector2 scrollPos = new Vector2(0, 0);
        private PSDParser parser;
        private float pixelsToUnitSize = 100.0f;
        private Editor gameObjectEditor;

        [MenuItem("Friends/PSDImportWindow")]
        public static void InvokeWindow()
        {
            if (instance == null)
                instance = GetWindow<PSDImportWindow>();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            if (Event.current.type == EventType.DragExited)
            {
                if (mouseOverWindow == instance)
                {
                    string p = GetDragDropPath();
                    OnChangeTexture(p);
                    return;
                }
            }

            var texName = psdPath == null ? "Drag and drop a PSD file" : ("Input file: " + psdPath);
            EditorGUILayout.LabelField(texName);

            if (GUILayout.Button("Select PSD file"))
            {
                string path = EditorUtility.OpenFilePanel("Open PSD file", "", "");
                OnChangeTexture(path);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Output path: " + outPath);
            if (GUILayout.Button("Select output folder"))
            {
                string path = EditorUtility.OpenFolderPanel("Select output folder", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    outPath = path;
                    outPath = "Assets" + outPath.Substring(Application.dataPath.Length); //make relative path
                }
            }
            EditorGUILayout.EndHorizontal();

            if (parser != null)
            {
                if (parser.Layers != null)
                {
                    parser.Layers.OrderBy(x => x.Name);
                    EditorGUILayout.Separator();
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Select all"))
                    {
                        parser.Layers.ToList().ForEach(x => x.Visible = true);
                    }
                    if (GUILayout.Button("Deselect all"))
                    {
                        parser.Layers.ToList().ForEach(x => x.Visible = false);
                    }
                    EditorGUILayout.EndHorizontal();

                    Array.Sort(parser.Layers, (l1, l2) => l1.Name.CompareTo(l2.Name));
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                    foreach (LayerInformation layer in parser.Layers)
                    {
                        if (IsLayerNameValid(layer.Name))
                        {
                            layer.Visible = EditorGUILayout.ToggleLeft(layer.Name, layer.Visible);
                        }
                    }
                    EditorGUILayout.EndScrollView();
                }

                if (GUILayout.Button("Convert"))
                {
                    Convert(outPath);
                }

                if (GUILayout.Button("Create Scene"))
                {
                    CreateGameObjects();
                }
            }

            EditorGUILayout.EndVertical();
        }

        private void OnChangeTexture(string path)
        {
            if (path != null && (path.EndsWith(".psd") || path.EndsWith(".psb")))
            {
                psdPath = path;
                EditorUtility.DisplayProgressBar("Loading PSD", "", 1.0f);
                parser = new PSDParser(psdPath);
                EditorUtility.ClearProgressBar();
            }
        }

        private void Convert(string outputPath)
        {
            GenerateSprites(outputPath);
            parser.CreateObsDataFromImgs();
        }

        private void CreateGameObjects()
        {
            if (parser.ObjectDatas == null)
                return;

            GameObject root = new GameObject(Path.GetFileNameWithoutExtension(psdPath));

            foreach (var obj in parser.ObjectDatas)
            {
                GameObject go = new GameObject(obj.spriteName);
                go.transform.parent = root.transform;

                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(obj.spritePath);
                var renderer = go.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                renderer.sortingOrder = obj.order * 5;
                renderer.flipX = GetMirrorXVal(obj.spriteName);
                renderer.flipY = GetMirrorYVal(obj.spriteName);

                go.transform.position = GetPosition(obj.originalGeometry, pixelsToUnitSize);
                var scale = GetDifferenceInScale(obj.scale, obj.originalGeometry); //kz: i think the values are reversed - scale should be original
                // As we import on the some ppu. If sprites exists on different ppu we need to adjust for it 
                if (obj.ppu != pixelsToUnitSize)
                {
                    scale /= pixelsToUnitSize / obj.ppu;
                }
                go.transform.localScale = scale;
            }
        }

        private void GenerateSprites(string path)
        {
            if (parser.Layers == null)
                return;

            Array.Sort(parser.Layers, new LayerComparer());

            for (int i = 0; i < parser.Layers.Length; ++i)
            {
                if (parser.Layers[i].Visible && IsLayerNameValid(parser.Layers[i].Name))
                {
                    EditorUtility.DisplayProgressBar("Creating PNG files", parser.Layers[i].Name, ((i + 1) / (float)parser.Layers.Length));
                    AssociateAsset(parser.Layers[i], path);
                }
            }

            EditorUtility.ClearProgressBar();
        }

        //Here we check if sprite with a given name already exists in the database
        private string GetPathForAsset(string layerName)
        {
            string name = DiscardSpace(layerName);

            //discard mirror postfix
            if (name.Contains("_mirror"))
            {
                name = name.Substring(0, name.IndexOf('_'));
            }

            if (!name.EndsWith("_main"))
                name += "_main";

            string[] guids = AssetDatabase.FindAssets(name + " t:Sprite");
            if (guids.Length > 0)
            {
                foreach (string guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var filename = Path.GetFileName(path);
                    if (filename == (name + ".png"))
                        return path;
                }
            }

            return "";
        }

        private void AssociateAsset(LayerInformation layer, string path)
        {
            //Debug.Log("CREATE SPRITE: " + layer.Name);
            string assetPath = GetPathForAsset(layer.Name);
            if (string.IsNullOrEmpty(assetPath))
            {
                layer.ppu = pixelsToUnitSize;
                //Debug.Log("SAVE SPRITE: " + layer.Name);
                if (CreateSprite(layer, path) != null)
                    layer.assetPath = Path.Combine(path, layer.Name + ".png");
            }
            else
            {
                Debug.Log("FOUND SPRITE " + layer.Name + " AT: " + path);
                var asset = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                layer.ppu = asset.spritePixelsPerUnit;
                layer.assetPath = assetPath;
            }
        }

        ///
        //Helpers
        ///
        static public string DiscardSpace(string name)
        {
            var spaceIdx = name.IndexOf(' ');
            return spaceIdx > -1 ? name.Substring(0, spaceIdx) : name;
        }

        private bool GetMirrorXVal(string layerName)
        {
            string name = DiscardSpace(layerName);
            return name.EndsWith("_mirrorX") || name.EndsWith("_mirrorXY");
        }

        private bool GetMirrorYVal(string layerName)
        {
            string name = DiscardSpace(layerName);
            return name.EndsWith("_mirrorY") || name.EndsWith("_mirrorXY");
        }

        private Sprite CreateSprite(LayerInformation layer, string path)
        {
            Texture2D tex = layer.CreateTexture();
            Sprite spr = layer.SaveSprite(tex, path, layer.Name, layer.ppu);
            DestroyImmediate(tex);

            return spr;
        }

        public Vector3 GetDifferenceInScale(Rect geoBase, Rect geoImage)
        {
            var x = geoImage.width / geoBase.width;
            var y = geoImage.height / geoBase.height;

            return new Vector3(x, y, 1f);
        }

        public Vector3 GetPosition(Rect geometry, float pixelsToUnits)
        {
            var x = (geometry.width / 2 + geometry.x) / pixelsToUnits;
            var y = (-geometry.height / 2 - geometry.y) / pixelsToUnits;

            return new Vector3(x, y, 0);
        }

        public bool IsLayerNameValid(string name)
        {
            return name != "</Layer set>" && name != "</Layer group>";
        }

        public string GetDragDropPath()
        {
            string[] paths = DragAndDrop.paths;
            if (paths != null)
                return paths[0];

            return null;
        }
    }
}