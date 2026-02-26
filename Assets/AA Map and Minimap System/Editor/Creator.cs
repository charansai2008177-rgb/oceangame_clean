// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace AAMAP
{
    public static class Creator
    {
        private static GameObject minimapUIObject = null;
        private static GameObject minimapCamera = null;
        private static GameObject mapUIObject = null;
        private static GameObject mapCamera = null;

        private static readonly string existingMaterialName = "Assets/AA Map and Minimap System/Example Material/Example_Icon_Material.mat";
        private static readonly string materialsPath = "Assets/AA Map and Minimap System/Materials";
        private static readonly string materialsFileNameFormat = "Map_Icon_Material";

        private static readonly string mapRenderTexturePath = "Assets/AA Map and Minimap System/Render Textures/Map Render Textures";
        private static readonly string mapRenderTextureFileFormat = "Map_Render_Texture";
        private static readonly Vector2Int mapRenderTextureSize = new Vector2Int(1000, 1000);

        private static readonly string minimapRenderTexturePath = "Assets/AA Map and Minimap System/Render Textures/Minimap Render Textures";
        private static readonly string minimapRenderTextureFileFormat = "Minimap_Render_Texture";
        private static readonly Vector2Int minimapRenderTextureSize = new Vector2Int(1000, 1000);

        private readonly static string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";

        /// <summary>
        /// Creates a new minimap. Do not call this method outside of the editor mode.
        /// </summary>
        [MenuItem("GameObject/AA Map and Minimap System/Create Minimap", false, 0)]
        public static void CreateMinimap()
        {
            if (Selection.activeGameObject == null)
            {
                Debug.LogWarning(errorMessagePrefix + "Failed to create the minimap. Please right click on the Canvas and select <b>AA Map and Minimap System > Create Minimap</b>.\n");
            }
            else
            {
                if (Selection.activeGameObject.GetComponent<Canvas>() == null)
                {
                    Debug.LogWarning(errorMessagePrefix + "Failed to create the minimap. Selected GameObject does not have a Canvas component on it. Please right click on the Canvas and select <b>AA Map and Minimap System > Create Minimap</b>.\n");
                }
                else
                {
                    minimapUIObject = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AA Map and Minimap System/Prefabs/Minimap.prefab"));
                    
                    if (minimapUIObject != null)
                    {
                        minimapUIObject.transform.SetParent(Selection.activeGameObject.transform);
                        minimapUIObject.name = "Minimap";

                        minimapUIObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                        minimapUIObject.GetComponent<RectTransform>().localScale = Vector3.one;

                        minimapCamera = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AA Map and Minimap System/Prefabs/Minimap Camera.prefab"));

                        if (minimapCamera != null)
                        {
                            minimapCamera.transform.parent = null;
                            minimapCamera.name = "Minimap Camera";

                            minimapUIObject.GetComponent<MinimapManager>().SetCamera(minimapCamera);

                            RenderTexture renderTexture = new RenderTexture(minimapRenderTextureSize.x, minimapRenderTextureSize.y, 0, RenderTextureFormat.ARGB32);
                            renderTexture.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;

                            string renderPipeline = GetCurrentRenderPipeline();

                            if (renderPipeline.Equals("URP"))
                            {
                                renderTexture.antiAliasing = 2;
                            }
                            else if (renderPipeline.Equals("HDRP"))
                            {
                                renderTexture.antiAliasing = 1;
                            }
                            else
                            {
                                // SRP
                                renderTexture.antiAliasing = 8;
                            }

                            renderTexture.wrapMode = TextureWrapMode.Clamp;
                            renderTexture.filterMode = FilterMode.Bilinear;
                            renderTexture.anisoLevel = 1;
                            renderTexture.useDynamicScale = false;
                            renderTexture.useMipMap = false;

                            string renderTexturePath = CreateMinimapRenderTextureName();

                            AssetDatabase.CreateAsset(renderTexture, renderTexturePath);

                            minimapCamera.GetComponent<Camera>().targetTexture = AssetDatabase.LoadAssetAtPath<RenderTexture>(renderTexturePath);
                            minimapCamera.GetComponent<Camera>().forceIntoRenderTexture = true;
                            minimapCamera.GetComponent<Camera>().Render();
                            minimapUIObject.GetComponent<MinimapManager>().SetRenderTexture(AssetDatabase.LoadAssetAtPath<RenderTexture>(renderTexturePath));

                            for (int i = 0; i < minimapUIObject.transform.childCount; i++)
                            {
                                if (minimapUIObject.transform.GetChild(i).gameObject.name.Equals("Minimap Mask"))
                                {
                                    for (int j = 0; j < minimapUIObject.transform.GetChild(i).childCount; j++)
                                    {
                                        if (minimapUIObject.transform.GetChild(i).GetChild(j).gameObject.name.Equals("Minimap Display"))
                                        {
                                            minimapUIObject.transform.GetChild(i).GetChild(j).GetComponent<RawImage>().texture = AssetDatabase.LoadAssetAtPath<RenderTexture>(renderTexturePath);

                                            break;
                                        }
                                    }

                                    break;
                                }
                            }

                            Selection.activeGameObject = minimapUIObject;
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(minimapUIObject.GetComponent<RectTransform>(), true);
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(minimapUIObject.GetComponent<MinimapManager>(), true);
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(minimapCamera.GetComponent<Transform>(), true);
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(minimapCamera.GetComponent<Camera>(), true);

                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                        else
                        {
                            Debug.LogError(errorMessagePrefix + "<b>Assets > AA Map and Minimap System > Minimap Camera.prefab</b> could not be found. Please delete and re-download the asset.\n");
                        }
                    }
                    else
                    {
                        Debug.LogError(errorMessagePrefix + "<b>Assets > AA Map and Minimap System > Minimap.prefab</b> could not be found. Please delete and re-download the asset.\n");
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new map. Do not call this method outside of the editor mode.
        /// </summary>
        [MenuItem("GameObject/AA Map and Minimap System/Create Map", false, 0)]
        public static void CreateMap()
        {
            if (Selection.activeGameObject == null)
            {
                Debug.LogWarning(errorMessagePrefix + "Failed to create the map. Please right click on the Canvas and select <b>AA Map and Minimap System > Create Map</b>.\n");
            }
            else
            {
                if (Selection.activeGameObject.GetComponent<Canvas>() == null)
                {
                    Debug.LogWarning(errorMessagePrefix + "Failed to create the map. Selected GameObject does not have a Canvas component on it. Please right click on the Canvas and select <b>AA Map and Minimap System > Create Map</b>.\n");
                }
                else
                {
                    mapUIObject = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AA Map and Minimap System/Prefabs/Map.prefab"));

                    if (mapUIObject != null)
                    {
                        mapUIObject.transform.SetParent(Selection.activeGameObject.transform);
                        mapUIObject.name = "Map";

                        mapUIObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                        mapUIObject.GetComponent<RectTransform>().localScale = Vector3.one;

                        if (minimapUIObject != null)
                        {
                            mapUIObject.GetComponent<MapManager>().minimapGameObject = minimapUIObject;
                        }
                        else
                        {
                            GameObject temp;
                            temp = GameObject.Find("Minimap");

                            if (temp != null)
                            {
                                if (temp.GetComponent<MinimapManager>() != null)
                                {
                                    minimapUIObject = temp;
                                    mapUIObject.GetComponent<MapManager>().minimapGameObject = temp;
                                }
                            }
                        }

                        mapCamera = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AA Map and Minimap System/Prefabs/Map Camera.prefab"));

                        if (mapCamera != null)
                        {
                            mapCamera.transform.parent = null;
                            mapCamera.name = "Map Camera";

                            mapUIObject.GetComponent<MapManager>().SetMapCamera(mapCamera);

                            RenderTexture renderTexture = new RenderTexture(mapRenderTextureSize.x, mapRenderTextureSize.y, 0, RenderTextureFormat.ARGB32);
                            renderTexture.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;

                            string renderPipeline = GetCurrentRenderPipeline();

                            if (renderPipeline.Equals("URP"))
                            {
                                renderTexture.antiAliasing = 2;
                            }
                            else if (renderPipeline.Equals("HDRP"))
                            {
                                renderTexture.antiAliasing = 1;
                            }
                            else
                            {
                                // SRP
                                renderTexture.antiAliasing = 8;
                            }
                            
                            renderTexture.wrapMode = TextureWrapMode.Clamp;
                            renderTexture.filterMode = FilterMode.Bilinear;
                            renderTexture.anisoLevel = 1;
                            renderTexture.useDynamicScale = false;
                            renderTexture.useMipMap = false;

                            string renderTexturePath = CreateMapRenderTextureName();

                            AssetDatabase.CreateAsset(renderTexture, renderTexturePath);

                            mapCamera.GetComponent<Camera>().targetTexture = AssetDatabase.LoadAssetAtPath<RenderTexture>(renderTexturePath);
                            mapCamera.GetComponent<Camera>().forceIntoRenderTexture = true;
                            mapCamera.GetComponent<Camera>().Render();
                            mapUIObject.GetComponent<MapManager>().SetRenderTexture(AssetDatabase.LoadAssetAtPath<RenderTexture>(renderTexturePath));

                            for (int i = 0; i < mapUIObject.transform.childCount; i++)
                            {
                                if (mapUIObject.transform.GetChild(i).gameObject.name.Equals("Map Mask"))
                                {
                                    for (int j = 0; j < mapUIObject.transform.GetChild(i).childCount; j++)
                                    {
                                        if (mapUIObject.transform.GetChild(i).GetChild(j).gameObject.name.Equals("Map Display"))
                                        {
                                            mapUIObject.transform.GetChild(i).GetChild(j).GetComponent<RawImage>().texture = AssetDatabase.LoadAssetAtPath<RenderTexture>(renderTexturePath);

                                            break;
                                        }
                                    }

                                    break;
                                }
                            }

                            mapUIObject.GetComponent<MapManager>().backgroundImageColor = Color.white;

                            Selection.activeGameObject = mapUIObject;
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(mapUIObject.GetComponent<RectTransform>(), true);
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(mapUIObject.GetComponent<MinimapManager>(), true);
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(mapCamera.GetComponent<Transform>(), true);
                            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(mapCamera.GetComponent<Camera>(), true);

                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                        else
                        {
                            Debug.LogError(errorMessagePrefix + "<b>Assets > AA Map and Minimap System > Map Camera.prefab</b> could not be found. Please delete and re-download the asset.\n");
                        }
                    }
                    else
                    {
                        Debug.LogError(errorMessagePrefix + "<b>Assets > AA Map and Minimap System > Map.prefab</b> could not be found. Please delete and re-download the asset.\n");
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new map icon.
        /// </summary>
        [MenuItem("GameObject/AA Map and Minimap System/Create Map Icon", false, 0)]
        public static void CreateIcon()
        {
            GetCurrentRenderPipeline();
            GameObject createdIcon = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AA Map and Minimap System/Prefabs/Map Icon.prefab"));

            if (createdIcon != null)
            {
                if (Selection.activeGameObject != null)
                {
                    createdIcon.transform.SetParent(Selection.activeGameObject.transform);
                }

                createdIcon.name = "Map Icon";
            }

            if (minimapCamera != null)
            {
                createdIcon.GetComponent<MapIcon>().minimapCamera = minimapCamera;
            }
            else
            {
                GameObject temp;
                temp = GameObject.Find("Minimap Camera");

                if (temp != null)
                {
                    if (temp.GetComponent<Camera>() != null)
                    {
                        minimapCamera = temp;
                        createdIcon.GetComponent<MapIcon>().minimapCamera = temp;
                    }
                }
            }

            if (mapCamera != null)
            {
                createdIcon.GetComponent<MapIcon>().mapCamera = mapCamera;
            }
            else
            {
                GameObject temp;
                temp = GameObject.Find("Map Camera");

                if (temp != null)
                {
                    if (temp.GetComponent<Camera>() != null)
                    {
                        mapCamera = temp;
                        createdIcon.GetComponent<MapIcon>().mapCamera = temp;
                    }
                }
            }

            Selection.activeGameObject = createdIcon;
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(createdIcon.GetComponent<MapIcon>(), true);

            string newMaterialName = CreateIconMaterialName();
            AssetDatabase.CopyAsset(existingMaterialName, newMaterialName);

            Material tempMaterial = AssetDatabase.LoadAssetAtPath<Material>(newMaterialName);

            string renderingPipeline = GetCurrentRenderPipeline();

            if (renderingPipeline.Equals("URP"))
            {
                tempMaterial.shader = Shader.Find("Universal Render Pipeline/Lit");
                tempMaterial.SetFloat("_Surface", 1);       // Transparent
                tempMaterial.SetFloat("_Smoothness", 0);
                tempMaterial.EnableKeyword("_EMISSION");
                tempMaterial.SetColor("_BaseColor", createdIcon.GetComponent<MapIcon>().iconColor);
                tempMaterial.SetTexture("_BaseMap", createdIcon.GetComponent<MapIcon>().iconTexture);
                tempMaterial.SetColor("_EmissionColor", createdIcon.GetComponent<MapIcon>().iconColor);
                tempMaterial.SetTexture("_EmissionMap", createdIcon.GetComponent<MapIcon>().iconTexture);
            }
            else if (renderingPipeline.Equals("HDRP"))
            {
                tempMaterial.shader = Shader.Find("HDRP/Lit");

                tempMaterial.SetFloat("_SurfaceType", 1);       // Transparent
                tempMaterial.SetFloat("_BlendMode", 0);     // Blending mode: Alpha
                tempMaterial.SetFloat("_RefractionModel", 0);
                tempMaterial.SetFloat("_EnableBlendModePreserveSpecularLighting", 0F);
                tempMaterial.SetFloat("_TransparentDepthPrepass", 1F);
                tempMaterial.SetFloat("_Smoothness", 0F);

                tempMaterial.SetTexture("_BaseColorMap", createdIcon.GetComponent<MapIcon>().iconTexture);
                tempMaterial.SetColor("_BaseColor", createdIcon.GetComponent<MapIcon>().iconColor);
                tempMaterial.SetTexture("_EmissiveColorMap", createdIcon.GetComponent<MapIcon>().iconTexture);
                tempMaterial.SetColor("_EmissiveColor", createdIcon.GetComponent<MapIcon>().iconColor);
            }

            EditorUtility.SetDirty(tempMaterial);   // Marks the material as dirty to apply changes.

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //createdIcon.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[1];
            createdIcon.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;

            createdIcon.GetComponent<MapIcon>().iconMaterial = tempMaterial;
        }

        /// <summary>
        /// Creates an asset name for the Material on the map icon and returns it.
        /// </summary>
        private static string CreateIconMaterialName()
        {
            AssetDatabase.Refresh();

            if (!AssetDatabase.IsValidFolder(materialsPath))
            {
                AssetDatabase.CreateFolder("Assets/AA Map and Minimap System", "Materials");
            }

            // Finds all the map icon material assets within the specified folder.
            string[] assetPaths = AssetDatabase.FindAssets("t:Material", new[] { materialsPath });
            string[] assetNames = new string[assetPaths.Length];

            for (int i = 0; i < assetNames.Length; i++)
            {
                assetNames[i] = AssetDatabase.GUIDToAssetPath(assetPaths[i]);
            }

            int biggestNumber = 1;

            for (int i = 0; i < assetNames.Length; i++)
            {
                int newNumber = FindNumberInString(assetNames[i], ".mat");

                if (newNumber > biggestNumber)
                {
                    biggestNumber = newNumber;
                }
            }

            if (AssetDatabase.LoadAssetAtPath(materialsPath + "/" + materialsFileNameFormat + "_" + biggestNumber.ToString() + ".mat", typeof(Material)) != null)
            {
                biggestNumber += 1;
            }

            string fullPath = materialsPath + "/" + materialsFileNameFormat + "_" + biggestNumber.ToString() + ".mat";

            return fullPath;
        }

        private static int FindNumberInString(string input, string fileFormat)
        {
            int lastUnderscoreIndex = input.LastIndexOf('_');
            int extentionIndex = input.IndexOf(fileFormat);

            return Convert.ToInt32(input.Substring(lastUnderscoreIndex + 1, extentionIndex - lastUnderscoreIndex - 1));
        }

        /// <summary>
        /// Creates an asset name for a new Map Render Texture and returns it.
        /// </summary>
        private static string CreateMapRenderTextureName()
        {
            AssetDatabase.Refresh();

            if (!AssetDatabase.IsValidFolder(mapRenderTexturePath))
            {
                AssetDatabase.CreateFolder("Assets/AA Map and Minimap System/Render Textures", "Map Render Textures");
            }

            // Finds all the map render texture assets within the specified folder.
            string[] assetPaths = AssetDatabase.FindAssets("t:RenderTexture", new[] { mapRenderTexturePath });
            string[] assetNames = new string[assetPaths.Length];

            for (int i = 0; i < assetNames.Length; i++)
            {
                assetNames[i] = AssetDatabase.GUIDToAssetPath(assetPaths[i]);
            }

            int biggestNumber = 1;

            for (int i = 0; i < assetNames.Length; i++)
            {
                int newNumber = FindNumberInString(assetNames[i], ".renderTexture");

                if (newNumber > biggestNumber)
                {
                    biggestNumber = newNumber;
                }
            }

            if (AssetDatabase.LoadAssetAtPath(mapRenderTexturePath + "/" + mapRenderTextureFileFormat + "_" + biggestNumber.ToString() + ".renderTexture", typeof(RenderTexture)) != null)
            {
                biggestNumber += 1;
            }

            string fullPath = mapRenderTexturePath + "/" + mapRenderTextureFileFormat + "_" + biggestNumber.ToString() + ".renderTexture";

            return fullPath;
        }

        /// <summary>
        /// Creates an asset name for a new Minimap Render Texture and returns it.
        /// </summary>
        private static string CreateMinimapRenderTextureName()
        {
            AssetDatabase.Refresh();

            if (!AssetDatabase.IsValidFolder(minimapRenderTexturePath))
            {
                AssetDatabase.CreateFolder("Assets/AA Map and Minimap System/Render Textures", "Minimap Render Textures");
            }

            // Finds all the minimap render texture assets within the specified folder.
            string[] assetPaths = AssetDatabase.FindAssets("t:RenderTexture", new[] { minimapRenderTexturePath });
            string[] assetNames = new string[assetPaths.Length];

            for (int i = 0; i < assetNames.Length; i++)
            {
                assetNames[i] = AssetDatabase.GUIDToAssetPath(assetPaths[i]);
            }

            int biggestNumber = 1;

            for (int i = 0; i < assetNames.Length; i++)
            {
                int newNumber = FindNumberInString(assetNames[i], ".renderTexture");

                if (newNumber > biggestNumber)
                {
                    biggestNumber = newNumber;
                }
            }

            if (AssetDatabase.LoadAssetAtPath(minimapRenderTexturePath + "/" + minimapRenderTextureFileFormat + "_" + biggestNumber.ToString() + ".renderTexture", typeof(RenderTexture)) != null)
            {
                biggestNumber += 1;
            }

            string fullPath = minimapRenderTexturePath + "/" + minimapRenderTextureFileFormat + "_" + biggestNumber.ToString() + ".renderTexture";

            return fullPath;
        }

        /// <summary>
        /// Returns the current rendering pipeline as a string.
        /// </summary>
        private static string GetCurrentRenderPipeline()
        {
            RenderPipelineAsset renderPipelineAsset = GraphicsSettings.renderPipelineAsset;

            if (renderPipelineAsset)
            {
                Type assetType = renderPipelineAsset.GetType();

                if (assetType.Name.Contains("UniversalRenderPipelineAsset"))
                {
                    return "URP";
                }
                else if (assetType.Name.Contains("HDRenderPipelineAsset"))
                {
                    return "HDRP";
                }
                else
                {
                    return "SRP";
                }
            }
            else
            {
                return "SRP";
            }
        }

    }
}
