// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;

namespace AAMAP
{
    public class MapIcon : MonoBehaviour
    {
        [Tooltip("Texture of the map icon.\n\nYou can locate these textures in \"Assets > AA Map and Minimap System > Sprites > Map Icons\".")] public Texture iconTexture = null;
        [Tooltip("Color of the map icon texture.")] public Color iconColor = Color.white;

        [Tooltip("Local offset position of the map icon, based on its parent GameObject.")] public Vector3 iconOffset = new Vector3(0F, 100F, 0F);
        [Tooltip("Local scale of the map icon.")] public Vector3 iconScale = new Vector3(3F, 1F, 3F);
        [Tooltip("Default local Y rotation value of the map icon.")] public float iconRotation = 0F;

        [Tooltip("\"Minimap Camera\" GameObject.")] public GameObject minimapCamera = null;
        [Tooltip("If this is true, the map icon is going to rotate with the minimap camera.")] public bool rotateWithCamera = true;

        [Tooltip("\"Map Camera\" GameObject.")] public GameObject mapCamera = null;
        [Tooltip("If this is true, the icon rotation will change when the map is enabled to display the icon with a much better rotation value.  This is recommended if you have set this map icon to rotate with the Minimap Camera. If this is false, the icon is not going to change rotation when the map is enabled.")] public bool haveCustomRotation = true;
        [Tooltip("Custom global Y axis rotation of the map icon when the map is enabled. This value is added to the global Y rotation value of the map camera.")] public float customRotation = 0F;

        [Tooltip("Material on this Map Icon.")] public Material iconMaterial = null;

        [Tooltip("\"Visuals\" GameObject.")] private GameObject visualsObject = null;
        [Tooltip("Mesh Renderer component on the \"Map Icon > Visuals\" GameObject.")] private MeshRenderer meshRenderer = null;

        private readonly string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";
        private readonly string errorMessageSuffix = " Please delete and re-create the icon.\n";

        private void Start()
        {
            FindVisualsObject();
            FindMeshRenderer();

            InitializeIconMesh();
            InitializeIconTransform();
        }

        private void LateUpdate()
        {
            if (rotateWithCamera)
            {
                if (minimapCamera != null)
                {
                    if (!haveCustomRotation)
                    {
                        transform.eulerAngles = new Vector3(0F, minimapCamera.transform.eulerAngles.y + iconRotation, 0F);
                    }
                    else
                    {
                        if (mapCamera != null)
                        {
                            if (!mapCamera.activeSelf)
                            {
                                transform.eulerAngles = new Vector3(0F, minimapCamera.transform.eulerAngles.y + iconRotation, 0F);
                            }
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0F, minimapCamera.transform.eulerAngles.y + iconRotation, 0F);
                        }
                    }
                }
            }

            if (haveCustomRotation)
            {
                if (mapCamera != null)
                {
                    if (mapCamera.activeSelf)
                    {
                        transform.eulerAngles = new Vector3(0F, mapCamera.transform.eulerAngles.y + customRotation, 0F);
                    }
                }
            }
        }

        /// <summary>
        /// Disables the map icon.
        /// </summary>
        private void DisableIcon()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Finds the Visuals GameObject under the Map Icon GameObject.
        /// </summary>
        private void FindVisualsObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name.Equals("Visuals"))
                {
                    visualsObject = transform.GetChild(i).gameObject;
                    break;
                }
            }

            if (visualsObject == null)
            {
                Debug.LogWarning(errorMessagePrefix + "\"" + gameObject.name + " > Visuals \" GameObject could not be found." + errorMessageSuffix);
                DisableIcon();
            }
        }

        /// <summary>
        /// Finds the Mesh Renderer component on the Map Icon > Visuals GameObject.
        /// </summary>
        private void FindMeshRenderer()
        {
            meshRenderer = visualsObject.GetComponent<MeshRenderer>();

            if (meshRenderer == null)
            {
                Debug.LogWarning(errorMessagePrefix + "Failed to generate the map icon because the Mesh Renderer component on the \"" + gameObject.name + " > Visuals\" GameObject could not be found." + errorMessageSuffix);
                DisableIcon();
            }
        }

        /// <summary>
        /// Returns the texture of the map icon.
        /// </summary>
        public Texture GetIconTexture()
        {
            return iconTexture;
        }

        /// <summary>
        /// Sets the texture of the map icon.
        /// </summary>
        /// <param name="texture">New texture of the map icon.</param>
        public void SetIconTexture(Texture texture)
        {
            if (texture == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new texture of the map icon.\n");
            }
            else
            {
                iconTexture = texture;

                if (meshRenderer.sharedMaterials[0].HasProperty("_MainTex"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_MainTex", texture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_BaseMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_BaseMap", texture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_BaseColorMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_BaseColorMap", texture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissionMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_EmissionMap", texture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissiveMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_EmissiveMap", texture);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissiveColorMap"))
                {
                    meshRenderer.sharedMaterials[0].SetTexture("_EmissiveColorMap", texture);
                }
            }
        }

        /// <summary>
        /// Returns the color of the map icon.
        /// </summary>
        public Color GetIconColor()
        {
            return iconColor;
        }

        /// <summary>
        /// Sets the color of the map icon.
        /// </summary>
        /// <param name="color">New color of the map icon.</param>
        public void SetIconColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the map icon.\n");
            }
            else
            {
                iconColor = color;

                if (meshRenderer.sharedMaterials[0].HasProperty("_Color"))
                {
                    meshRenderer.sharedMaterials[0].SetColor("_Color", color);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_BaseColor"))
                {
                    meshRenderer.sharedMaterials[0].SetColor("_BaseColor", color);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissionColor"))
                {
                    meshRenderer.sharedMaterials[0].SetColor("_EmissionColor", color);
                }

                if (meshRenderer.sharedMaterials[0].HasProperty("_EmissiveColor"))
                {
                    meshRenderer.sharedMaterials[0].SetColor("_EmissiveColor", color);
                }
            }
        }

        /// <summary>
        /// Returns the local offset position of the map icon.
        /// </summary>
        public Vector3 GetIconOffset()
        {
            return iconOffset;
        }

        /// <summary>
        /// Sets the local offset position of the map icon.
        /// </summary>
        /// <param name="offset">New local offset position of the map icon.</param>
        public void SetIconOffset(Vector3 offset)
        {
            if (offset == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new local offset position of the map icon.\n");
            }
            else
            {
                iconOffset = offset;
                transform.localPosition = offset;
            }
        }

        /// <summary>
        /// Returns the scale of the map icon.
        /// </summary>
        public Vector3 GetIconScale()
        {
            return iconScale;
        }

        /// <summary>
        /// Sets the scale of the map icon.
        /// </summary>
        /// <param name="newScale">New scale of the map icon.</param>
        public void SetIconScale(Vector3 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the Map Icon.\n");
            }
            else
            {
                iconScale = scale;
                transform.localScale = scale;
            }
        }

        /// <summary>
        /// Returns the default local Y rotation value of the map icon.
        /// </summary>
        public float GetIconRotation()
        {
            return iconRotation;
        }

        /// <summary>
        /// Sets the default local Y rotation value of the map icon.
        /// </summary>
        /// <param name="rotation">New default local Y rotation value of the map icon.</param>
        public void SetIconRotation(float rotation)
        {
            if (rotation < -360F)
            {
                rotation %= -360F;
            }

            if (rotation < 360F)
            {
                rotation %= 360F;
            }

            iconRotation = rotation;

            if (minimapCamera == null || !rotateWithCamera)
            {
                transform.rotation = Quaternion.Euler(0F, rotation, 0F);
            }
        }

        /// <summary>
        /// Returns the minimap camera of the map icon.
        /// </summary>
        public GameObject GetMinimapCamera()
        {
            return minimapCamera;
        }

        /// <summary>
        /// Sets the minimap camera of the map icon.
        /// </summary>
        /// <param name="minimapCamera">The minimap camera.</param>
        public void SetMinimapCamera(GameObject minimapCamera)
        {
            if (minimapCamera == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null GameObject as the Minimap Camera on the Map Icon.\n");
                return;
            }

            if (minimapCamera.GetComponent<Camera>() == null)
            {
                Debug.LogWarning(errorMessagePrefix + "The GameObject you have assigned as the Minimap Camera on the Map Icon doesn't have a Camera component on it.\n");
            }
            else
            {
                this.minimapCamera = minimapCamera;
            }
        }

        /// <summary>
        /// Returns true if the map icon is rotating with the minimap camera and returns false if the map icon is not rotating with the minimap camera.
        /// </summary>
        public bool DoesIconRotateWithCamera()
        {
            return rotateWithCamera;
        }

        /// <summary>
        /// Enables the map icon to rotate with the minimap camera.
        /// </summary>
        public void RotateIconWithCamera()
        {
            rotateWithCamera = true;
        }

        /// <summary>
        /// Disables the map icon from rotating with the minimap camera.
        /// </summary>
        public void DontRotateIconWithCamera()
        {
            rotateWithCamera = false;
        }

        /// <summary>
        /// Initializes the icon mesh.
        /// </summary>
        private void InitializeIconMesh()
        {
            if (iconTexture != null && meshRenderer != null)
            {
                meshRenderer.sharedMaterials[0].SetTexture("_MainTex", iconTexture);
            }
        }

        /// <summary>
        /// Initializes the Transform component on the Map Icon.
        /// </summary>
        private void InitializeIconTransform()
        {
            transform.localPosition = iconOffset;

            transform.localScale = iconScale;

            transform.rotation = Quaternion.Euler(-0F, iconRotation, 0F);
        }

        /// <summary>
        /// Returns the Map Camera GameObject.
        /// </summary>
        public GameObject GetMapCamera()
        {
            return mapCamera;
        }

        /// <summary>
        /// Sets the Map Camera of the map icon.
        /// </summary>
        /// <param name="mapCamera">New Map Camera of the map icon.</param>
        public void SetMapCamera(GameObject mapCamera)
        {
            if (mapCamera == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null GameObject as the Map Camera on the Map Icon.\n");
                return;
            }

            if (mapCamera.GetComponent<Camera>() == null)
            {
                Debug.LogWarning(errorMessagePrefix + "The GameObject you have assigned as the Map Camera on the Map Icon doesn't have a Camera component on it.\n");
            }
            else
            {
                this.mapCamera = mapCamera;
            }
        }

        /// <summary>
        /// Returns true if the map icon snaps to a custom rotation when the map is enabled. Returns false if the map icon doesn't react when the map is enabled.
        /// </summary>
        public bool DoesHaveCustomMapRotation()
        {
            return haveCustomRotation;
        }

        /// <summary>
        /// Makes the map icon snaps to a custom rotation when the map is enabled. This is recommended if the map icon is set to rotate with the minimap camera.
        /// </summary>
        public void EnableCustomMapRotation()
        {
            haveCustomRotation = true;
        }

        /// <summary>
        /// Stops the map icon from snaping to a custom rotation when the map is enabled.
        /// </summary>
        public void DisableCustomMapRotation()
        {
            haveCustomRotation = false;
        }

        /// <summary>
        /// Returns the custom global Y rotation of the map icon when the map is enabled.
        /// </summary>
        public float GetCustomMapRotation()
        {
            return customRotation;
        }

        /// <summary>
        /// Sets the custom global Y rotation of the map icon when the map is enabled.
        /// </summary>
        /// <param name="rotation">New global Y rotation of the map icon when the map is enabled.</param>
        public void SetCustomMapRotation(float rotation)
        {
            if (rotation < -360F)
            {
                rotation %= -360F;
            }

            if (rotation < 360F)
            {
                rotation %= 360F;
            }

            customRotation = rotation;
        }

        /// <summary>
        /// Returns the material on this Map Icon.
        /// </summary>
        public Material GetIconMaterial()
        {
            return iconMaterial;
        }

        /// <summary>
        /// Sets the material on this Map Icon.
        /// </summary>
        /// <param name="material">New material of the map icon.</param>
        public void SetIconMaterial(Material material)
        {
            if (material != null)
            {
                iconMaterial = material;
                meshRenderer.sharedMaterials[0] = material;
                iconColor = material.color;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new material of the map icon.\n");
            }
        }

    }
}
