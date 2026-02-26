// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;
using UnityEditor;

namespace AAMAP
{
    [CustomEditor(typeof(MapIcon))]
    public class MapIconEditor : Editor
    {
        [Tooltip("Map Icon component on this GameObject.")] private MapIcon mapIcon = null;

        [Tooltip("Mesh Renderer component on the \"Map Icon > Visuals\" GameObject.")] private MeshRenderer meshRenderer = null;
        [Tooltip("Transform component on this GameObject.")] private Transform iconTransform = null;

        [Tooltip("This Rect is used to add Tooltips to the inspector fields.")] private Rect typeRect;
        [Tooltip("This GUI Content is used to add Tooltips to the inspector fields.")] private GUIContent gUIContent;
        [Tooltip("This is the tab horizontal space distance on the sub-fields in the inspector.")] private readonly float spaceDistance = 20F;

        [Tooltip("Material on this Map Icon.")] private Material iconMaterial;

        private readonly string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";

        /// <summary>
        /// Draws the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            GetGameObjectsAndComponents();

            EditorGUILayout.LabelField("Icon Texture", EditorStyles.boldLabel);

            mapIcon.iconTexture = (Texture)EditorGUILayout.ObjectField("Texture", mapIcon.iconTexture, typeof(Texture), true, GUILayout.Height(EditorGUIUtility.singleLineHeight), GUILayout.ExpandWidth(true));
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Texture of the map icon.\n\nYou can locate these textures in \"Assets > AA Map and Minimap System > Sprites > Map Icons\".\n\nMethods:\nGetIconTexture( );\nSetIconTexture(...);");
            GUI.Label(typeRect, gUIContent);

            mapIcon.iconColor = EditorGUILayout.ColorField("Color", mapIcon.iconColor);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Color of the map icon.\n\nMethods:\nGetIconColor( );\nSetIconColor(...);");
            GUI.Label(typeRect, gUIContent);

            GUILayout.Space(20F);

            EditorGUILayout.LabelField("Icon Properties", EditorStyles.boldLabel);

            mapIcon.iconOffset = EditorGUILayout.Vector3Field("Offset", mapIcon.iconOffset);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Local offset position of the map icon, based on its parent GameObject.\n\nMethods:\nGetIconOffset( );\nSetIconOffset(...);");
            GUI.Label(typeRect, gUIContent);

            mapIcon.iconScale = EditorGUILayout.Vector3Field("Scale", mapIcon.iconScale);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Local scale of the map icon.\n\nMethods:\nGetIconScale( );\nSetIconScale(...);");
            GUI.Label(typeRect, gUIContent);

            mapIcon.iconRotation = EditorGUILayout.Slider("Rotation", mapIcon.iconRotation, -360F, 360F);
            typeRect = GUILayoutUtility.GetLastRect();
            GUI.Label(typeRect, new GUIContent("", "Default local Y rotation value of the map icon.\n\nMethods:\nGetIconRotation( );\nSetIconRotation(...);"));

            GUILayout.Space(20F);

            EditorGUILayout.LabelField("Minimap Camera (Optional)", EditorStyles.boldLabel);

            mapIcon.minimapCamera = (GameObject)EditorGUILayout.ObjectField("Minimap Camera", mapIcon.minimapCamera, typeof(GameObject), true);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "\"Minimap Camera\" GameObject.\n\nMethods:\nGetMinimapCamera( );\nSetMinimapCamera(...);");
            GUI.Label(typeRect, gUIContent);

            if (mapIcon.minimapCamera != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapIcon.rotateWithCamera = EditorGUILayout.Toggle("Rotate With Camera", mapIcon.rotateWithCamera);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the map icon is going to rotate with the minimap camera.\n\nMethods:\nDoesIconRotateWithCamera( );\nRotateIconWithCamera( );\nDontRotateIconWithCamera( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Camera (Optional)", EditorStyles.boldLabel);

            mapIcon.mapCamera = (GameObject)EditorGUILayout.ObjectField("Map Camera", mapIcon.mapCamera, typeof(GameObject), true);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "\"Map Camera\" GameObject.\n\nMethods:\nGetMapCamera( );\nSetMapCamera(...);");
            GUI.Label(typeRect, gUIContent);

            if (mapIcon.mapCamera != null)
            {
                mapIcon.haveCustomRotation = EditorGUILayout.Toggle("Have Custom Rotation", mapIcon.haveCustomRotation);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the map icon rotation will change when the map is enabled to display the icon with a much better rotation value. This is recommended if you have set this map icon to rotate with the Minimap Camera.\n\nIf this is false, the icon is not going to change rotation when the map is enabled.\n\nMethods:\nDoesHaveCustomMapRotation( );\nEnableCustomMapRotation( );\nDisableCustomMapRotation( );");
                GUI.Label(typeRect, gUIContent);

                if (mapIcon.haveCustomRotation)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance);
                    mapIcon.customRotation = EditorGUILayout.Slider("Custom Rotation", mapIcon.customRotation, -360F, 360F);
                    typeRect = GUILayoutUtility.GetLastRect();
                    gUIContent = new GUIContent("", "Custom global Y axis rotation of the map icon when the map is enabled. This value is added to the global Y rotation value of the map camera.\n\nMethods:\nGetCustomMapRotation( );\nSetCustomMapRotation(...);");
                    GUI.Label(typeRect, gUIContent);
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(20F);

            EditorGUILayout.LabelField("Icon Material", EditorStyles.boldLabel);

            mapIcon.iconMaterial = (Material)EditorGUILayout.ObjectField("Material", mapIcon.iconMaterial, typeof(Material), true);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Material on this Map Icon.\n\nYou can locate these materials at \"Assets > AA Map and Minimap System > Materials\".\n\nMethods:\nGetIconMaterial( );\nSetIconMaterial(...);");
            GUI.Label(typeRect, gUIContent);

            GUILayout.Space(20F);

            if (GUILayout.Button("Delete Map Icon"))
            {
                if (mapIcon.iconMaterial != null)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(mapIcon.iconMaterial));
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

                DestroyImmediate(mapIcon.gameObject);
            }

            if (GUILayout.Button("Reset Map Icon"))
            {
                Texture tempSprite = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AA Map and Minimap System/Sprites/Map Icons/Map Icon 3.png");

                if (tempSprite != null)
                {
                    mapIcon.iconTexture = tempSprite;
                }

                mapIcon.iconColor = Color.white;
                mapIcon.iconOffset = new Vector3(0F, 100F, 0F);
                mapIcon.iconScale = new Vector3(3F, 1F, 3F);
                mapIcon.iconRotation = 0F;
                mapIcon.rotateWithCamera = true;

                mapIcon.haveCustomRotation = true;
                mapIcon.customRotation = 0F;

                meshRenderer.sharedMaterials[0].SetColor("_Color", Color.white);
                meshRenderer.sharedMaterials[0].SetColor("_EmissionColor", Color.white);
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (mapIcon != null)
                {
                    ApplyIconTexture();
                    ApplyIconProperites();
                    ApplyIconColor();
                    ApplyMaterial();
                }
                
                if (target != null)
                {
                    Undo.RecordObject(target, "Changed Map Icon");
                }
            }
        }

        /// <summary>
        /// Gets all the necessary GameObjects and components on the map icon.
        /// </summary>
        private void GetGameObjectsAndComponents()
        {
            if (mapIcon == null)
            {
                mapIcon = (MapIcon)target;
            }

            if (meshRenderer == null)
            {
                meshRenderer = mapIcon.transform.GetChild(0).GetComponent<MeshRenderer>();

                if (meshRenderer == null)
                {
                    Debug.LogWarning(errorMessagePrefix + "Mesh Renderer component on the \"" + mapIcon.gameObject.name + " > Visuals\" GameObject could not be found.\n");
                }
            }

            if (iconTransform == null)
            {
                iconTransform = mapIcon.transform;
            }

            if (iconMaterial == null)
            {
                iconMaterial = meshRenderer.sharedMaterials[0];

                if (iconMaterial == null)
                {
                    Debug.LogWarning(errorMessagePrefix + "Material on the map icon mesh renderer could not be found.\n");
                }
            }
        }

        /// <summary>
        /// Applies the texture of the map icon.
        /// </summary>
        private void ApplyIconTexture()
        {
            if (mapIcon.iconTexture != null)
            {
                if (meshRenderer.sharedMaterials[0].HasProperty("_MainTex"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_MainTex", mapIcon.iconTexture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_BaseMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_BaseMap", mapIcon.iconTexture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_BaseColorMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_BaseMap", mapIcon.iconTexture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissionMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_EmissionMap", mapIcon.iconTexture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissiveMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_EmissiveMap", mapIcon.iconTexture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissiveColorMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_EmissiveMap", mapIcon.iconTexture);
                }
            }
        }

        /// <summary>
        /// Applies the properties of the map icon.
        /// </summary>
        private void ApplyIconProperites()
        {
            iconTransform.localPosition = mapIcon.iconOffset;

            iconTransform.localScale = mapIcon.iconScale;

            if (mapIcon.minimapCamera != null)
            {
                iconTransform.eulerAngles = new Vector3(0F, mapIcon.minimapCamera.transform.eulerAngles.y + mapIcon.iconRotation, 0F);
            }
            else
            {
                iconTransform.eulerAngles = new Vector3(0F, mapIcon.iconRotation, 0F);
            }
        }

        /// <summary>
        /// Applies the color of the map icon.
        /// </summary>
        private void ApplyIconColor()
        {
            if (meshRenderer.sharedMaterials[0].HasProperty("_Color"))
            {
                meshRenderer.sharedMaterials[0].SetColor("_Color", mapIcon.iconColor);
            }

            if (meshRenderer.sharedMaterials[0].HasProperty("_BaseColor"))
            {
                meshRenderer.sharedMaterials[0].SetColor("_BaseColor", mapIcon.iconColor);
            }

            if (meshRenderer.sharedMaterials[0].HasProperty("_EmissionColor"))
            {
                meshRenderer.sharedMaterials[0].SetColor("_EmissionColor", mapIcon.iconColor);
            }

            if (meshRenderer.sharedMaterials[0].HasProperty("_EmissiveColor"))
            {
                meshRenderer.sharedMaterials[0].SetColor("_EmissiveColor", mapIcon.iconColor);
            }
        }

        /// <summary>
        /// Applies the map icon material.
        /// </summary>
        private void ApplyMaterial()
        {
            meshRenderer.sharedMaterial = mapIcon.iconMaterial;
        }
    }
}
