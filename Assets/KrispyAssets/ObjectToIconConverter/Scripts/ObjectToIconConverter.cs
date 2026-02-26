#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RRS.Converter
{
    [ExecuteInEditMode]
    public class ObjectToIconConverter : MonoBehaviour
    {
        public enum FileTypes
        {
            PNG,
            JPG,
        }

        public enum FileSize
        {
            _1x1 = 1,
            _2x2 = 2,
            _4x4 = 4,
            _8x8 = 8,
            _16x16 = 16,
            _32x32 = 32,
            _64x64 = 64,
            _128x128 = 128,
            _256x256 = 256,
            _512x512 = 512,
            _1024x1024 = 1024,
            _2048x2048 = 2048,
            _4096x4096 = 4096,
            _8192x8192 = 8192,
        }

        public enum AutoCenteringTypes
        {
            Pivot,
            Center,
            FOV_Manipulation
        }

        [Flags]
        public enum GizmoSettings
        {
            Camera_To_Corners = 1,
            Camera_To_Middle_Points = 2,
            Corners_To_Corners = 4,
            Corners_To_End_Corners = 8,
            Middle_Points_To_End_Middle_Points = 16,
            End_Corners_To_End_Corners = 32,
        }

        public enum ClaritySettings
        {
            _1x = 1,
            _2x = 2,
            _4x = 4,
            _8x = 8,
            _16x = 16,
        }

        public enum OrientationDirection
        {
            Forward,
            Back,
            Up,
            Down,
            Left,
            Right,
        }

        [Serializable]
        public class AutoOrientationSettings
        {
            public OrientationDirection Direction = OrientationDirection.Forward;
            public Vector3 DesiredOrientationOffset = new Vector3(-15f, 145f, 0f);
        }

        #region General Settings
        [HideInInspector] public CaptureTarget CaptureTarget = null;
        public string TextureName = "";
        public FileTypes FileType = FileTypes.PNG;
        public TextureImporterType TextureType = TextureImporterType.Default;
        public TextureImporterCompression TextureCompression = TextureImporterCompression.Compressed;
        public FileSize BakingSize = FileSize._512x512;
        public FileSize CompressionSize = FileSize._512x512;
        public FileSize MinPreviewSize = FileSize._32x32;
        public FileSize MaxPreviewSize = FileSize._512x512;
        [Range(0, 1)] public float BackgroundAlpha = 0f;
        public bool SetAlphaIsTransparent = true;
        public bool DisplayAlphaInTexturePreview = true;
        #endregion

        #region Advanced Settings
        public FilterMode Filter_Mode = FilterMode.Bilinear;
        public ClaritySettings AntiAliasing = ClaritySettings._4x;
        public ClaritySettings AnisotropicFiltering = ClaritySettings._1x;
        public TextureFormat Texture_Format = TextureFormat.ARGB32;
        public RenderTextureFormat PreviewRenderTextureFormat = RenderTextureFormat.ARGB32;
        [Range(0, 32)] public int TextureDepthBuffer = 24;
        #endregion

        #region Alignment and View Configuration
        public bool AutoCenterInBounds = false;
        public bool CenterIncludesChildrenBounds = true;
        public AutoCenteringTypes CenteringType = AutoCenteringTypes.Pivot;
        public float CenteringDepthBufferMultiplier = 1.1f;
        public bool AutoOrientate = false;
        public AutoOrientationSettings AutoOrientationSetting = new AutoOrientationSettings();
        [Min(0)] public float CaptureDepth = 3f;
        public float FovManipulationValue = 15f;
        #endregion

        #region Additional Settings
        public bool PingAssetOnSave = true;
        public bool AutoSelectAssetOnSave = false;
        #endregion

        #region Gizmo Settings
        public GizmoSettings Gizmos = GizmoSettings.Camera_To_Corners | GizmoSettings.Camera_To_Middle_Points | GizmoSettings.Corners_To_Corners;
        [Min(0)] public float GizmoLineLengthMultiplier = 1f;
        [Min(0)] public float GizmoFrontToEndDepth = 2f;
        #endregion

        #region For Resetting Purposes
        [HideInInspector] public float DefaultFOV = 60f;
        [HideInInspector] public string SelectedSavePath = "";
        [HideInInspector] public RenderTexture PreviewRenderTexture = null;
        [HideInInspector] public Camera Camera = null;
        [HideInInspector] public Camera PreviewCamera = null;

        [HideInInspector] public Transform CurrentChild = null;
        [HideInInspector] public Transform LastChild = null;
        #endregion

        public bool PathIsSelected => !string.IsNullOrEmpty(SelectedSavePath);
        public bool IsNameValid => !string.IsNullOrEmpty(TextureName);
        public bool IsSaveValid => IsPathValid(SelectedSavePath) && IsNameValid;

        private const string TAB_TITLE = "Tools";
        private const string CONVERTER_TITLE = "Icon Converter";
        private const string COMPLETE_TAB_TITLE = TAB_TITLE + "/" + CONVERTER_TITLE + "/";
        private const string SCENE_NAME = "Object Converter";

        [MenuItem(COMPLETE_TAB_TITLE + "Load Converter Scene", priority = 1)]
        private static void LoadSceneAndSelect()
        {
            string[] guids = AssetDatabase.FindAssets(SCENE_NAME + " t:scene");
            if (guids.Length == 0)
            {
                Debug.LogError("No scene found with name: " + SCENE_NAME);
                return;
            }

            string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);

            if (EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single).IsValid())
            {
                Debug.Log("Scene loaded successfully.");

                ObjectToIconConverter objectToSelect = FindObjectOfType(typeof(ObjectToIconConverter)) as ObjectToIconConverter;

                if (objectToSelect != null)
                {
                    Selection.activeGameObject = objectToSelect.gameObject;
                    EditorGUIUtility.PingObject(objectToSelect);
                }
                else
                {
                    Debug.LogError("Object not found in the scene.");
                }
            }
            else
            {
                Debug.LogError("Failed to load the scene at path: " + scenePath);
            }
        }

        [MenuItem(COMPLETE_TAB_TITLE + "Create Converter", priority = 2)]
        private static void CreateConverterGameObject()
        {
            DestroyConverter();

            var converter = new GameObject("Converter").AddComponent<ObjectToIconConverter>();
            converter.Update();
            Undo.RegisterCreatedObjectUndo(converter.gameObject, "Create " + converter.name);
            Selection.activeGameObject = converter.gameObject;
        }

        [MenuItem(COMPLETE_TAB_TITLE + "Destroy Converter", priority = 3)]
        private static void DestroyConverter()
        {
            var converter = FindObjectOfType<ObjectToIconConverter>();
            DestroyImmediate(converter?.gameObject);
        }

        public void Update()
        {
            if (Camera == null || PreviewCamera == null) { FindOrCreateCameras(); }
            if (CaptureTarget == null) { FindOrCreateCaptureTarget(); }
            if (PreviewRenderTexture == null) { CreatePreviewTexture(); }

            SetCamerasFOV((CenteringType == AutoCenteringTypes.FOV_Manipulation) ? FovManipulationValue : DefaultFOV);
            UpdateCamerasBackgroundAlpha();

            TryGetActiveCaptureTargetChild();
            if (AutoCenterInBounds) { CenterObjectInCaptureBounds(); }
            if (AutoOrientate) { ApplyAutoOrientation(); }
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void Destroy()
        {
            DestroyImmediate(CaptureTarget?.gameObject);
            DestroyImmediate(PreviewCamera?.gameObject);
            DestroyImmediate(Camera?.gameObject);
            ClearPreviewTexture();
        }

        private void FindOrCreateCaptureTarget()
        {
            CaptureTarget = FindObjectOfType<CaptureTarget>();
            if(CaptureTarget != null) { return; }
            CaptureTarget = new GameObject().AddComponent<CaptureTarget>();
            CaptureTarget.name = "Capture Target";

            CaptureTarget.transform.parent = transform;
        }

        private void FindOrCreateCameras()
        {
            var converterCam = FindObjectOfType<ConverterCamera>();

            if (converterCam != null && converterCam.TryGetComponent(out Camera camera))
            {
                Camera = camera;
                SetupCamera(Camera, transform);
            }
            else
            {
                DestroyImmediate(converterCam);
                Camera = new GameObject("Converter Camera").AddComponent<Camera>();
                Camera.gameObject.AddComponent<ConverterCamera>();
                //camera.tag = "MainCamera";
                SetupCamera(Camera, transform);
            }

            for (int i = Camera.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(Camera.transform.GetChild(i).gameObject);
            }

            if(PreviewCamera == null) { PreviewCamera = new GameObject("Preview Camera").AddComponent<Camera>(); }
            SetupCamera(PreviewCamera, Camera.transform);

            CreatePreviewTexture();
        }

        private void SetupCamera(Camera camera, Transform parent)
        {
            camera.transform.parent = parent;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.depth = -1;
            camera.backgroundColor = Color.black;
            camera.nearClipPlane = 0.01f;
        }

        private void SetCamerasFOV(float fov)
        {
            Camera.fieldOfView = fov;
            PreviewCamera.fieldOfView = fov;
        }

        private void UpdateCamerasBackgroundAlpha()
        {
            var color = new Color(Camera.backgroundColor.r, Camera.backgroundColor.g, Camera.backgroundColor.b, BackgroundAlpha);

            Camera.backgroundColor = color;
            PreviewCamera.backgroundColor = color;
        }

        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            if (Camera == null || Camera.orthographic) { return; }

            Transform camTransform = Camera.transform;
            float fov = Camera.fieldOfView;
            float aspect = 1f;
            float fovManipulationMultiplier = (DefaultFOV / Camera.fieldOfView);

            float heightAtDepth = (2f * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) * CaptureDepth) * fovManipulationMultiplier;
            float widthAtDepth = heightAtDepth * aspect;

            UnityEngine.Gizmos.color = Color.cyan;

            Vector3 forward = (camTransform.forward * CaptureDepth * fovManipulationMultiplier) * CenteringDepthBufferMultiplier;
            Vector3 center = camTransform.position + forward;
            Vector3 up = camTransform.up * (heightAtDepth * 0.5f);
            Vector3 right = camTransform.right * (widthAtDepth * 0.5f);

            Vector3 topLeft = (center + up - right) * GizmoLineLengthMultiplier;
            Vector3 topRight = (center + up + right) * GizmoLineLengthMultiplier;
            Vector3 bottomLeft = (center - up - right) * GizmoLineLengthMultiplier;
            Vector3 bottomRight = (center - up + right) * GizmoLineLengthMultiplier;

            Vector3 middleLeft = (topLeft + bottomLeft) / 2f;
            Vector3 middleRight = (topRight + bottomRight) / 2f;
            Vector3 middleTop = (topLeft + topRight) / 2f;
            Vector3 middleBottom = (bottomLeft + bottomRight) / 2f;

            var realLineDepth = ((forward * (GizmoFrontToEndDepth + 1)).z * GizmoLineLengthMultiplier);

            Vector3 topBackLeft = new Vector3(topLeft.x, topLeft.y, realLineDepth);
            Vector3 topBackRight = new Vector3(topRight.x, topRight.y, realLineDepth);
            Vector3 bottomBackLeft = new Vector3(bottomLeft.x, bottomLeft.y, realLineDepth);
            Vector3 bottomBackRight = new Vector3(bottomRight.x, bottomRight.y, realLineDepth);

            Vector3 middleBackLeft = new Vector3(middleLeft.x, middleLeft.y, realLineDepth);
            Vector3 middleBackRight = new Vector3(middleRight.x, middleRight.y, realLineDepth);
            Vector3 middleBackTop = new Vector3(middleTop.x, middleTop.y, realLineDepth);
            Vector3 middleBackBottom = new Vector3(middleBottom.x, middleBottom.y, realLineDepth);

            if (Gizmos.HasFlag(GizmoSettings.Camera_To_Corners))
            {
                //Optional, draw lines connecting the camera position to the corners
                UnityEngine.Gizmos.DrawLine(camTransform.position, topLeft);
                UnityEngine.Gizmos.DrawLine(camTransform.position, topRight);
                UnityEngine.Gizmos.DrawLine(camTransform.position, bottomLeft);
                UnityEngine.Gizmos.DrawLine(camTransform.position, bottomRight);
            }

            if (Gizmos.HasFlag(GizmoSettings.Camera_To_Middle_Points))
            {
                //Optional, draw lines connecting the camera position to middle points between the corners
                UnityEngine.Gizmos.DrawLine(camTransform.position, middleLeft);
                UnityEngine.Gizmos.DrawLine(camTransform.position, middleRight);
                UnityEngine.Gizmos.DrawLine(camTransform.position, middleTop);
                UnityEngine.Gizmos.DrawLine(camTransform.position, middleBottom);
            }

            if (Gizmos.HasFlag(GizmoSettings.Corners_To_Corners))
            {
                //Optional, draw lines connecting the corner points to form a cube
                UnityEngine.Gizmos.DrawLine(topLeft, topRight);
                UnityEngine.Gizmos.DrawLine(topLeft, bottomLeft);
                UnityEngine.Gizmos.DrawLine(topRight, bottomRight);
                UnityEngine.Gizmos.DrawLine(bottomLeft, bottomRight);
            }

            if (Gizmos.HasFlag(GizmoSettings.Corners_To_End_Corners))
            {
                //Optional, draw lines connecting the corner points to the corner points on the other end of the wire cube
                UnityEngine.Gizmos.DrawLine(topLeft, topBackLeft);
                UnityEngine.Gizmos.DrawLine(topRight, topBackRight);
                UnityEngine.Gizmos.DrawLine(bottomLeft, bottomBackLeft);
                UnityEngine.Gizmos.DrawLine(bottomRight, bottomBackRight);
            }

            if (Gizmos.HasFlag(GizmoSettings.Middle_Points_To_End_Middle_Points))
            {
                //Optional, draw lines connecting the middle points to the middle points on the other end of the wire cube
                UnityEngine.Gizmos.DrawLine(middleLeft, middleBackLeft);
                UnityEngine.Gizmos.DrawLine(middleRight, middleBackRight);
                UnityEngine.Gizmos.DrawLine(middleTop, middleBackTop);
                UnityEngine.Gizmos.DrawLine(middleBottom, middleBackBottom);
            }

            if (Gizmos.HasFlag(GizmoSettings.End_Corners_To_End_Corners))
            {
                //Optional, draw lines connecting the end corner points to form a cube
                UnityEngine.Gizmos.DrawLine(topBackLeft, topBackRight);
                UnityEngine.Gizmos.DrawLine(topBackLeft, bottomBackLeft);
                UnityEngine.Gizmos.DrawLine(topBackRight, bottomBackRight);
                UnityEngine.Gizmos.DrawLine(bottomBackLeft, bottomBackRight);
            }

            if (CaptureTarget == null)
            {
                Debug.LogError("Capture Target is null");
                return;
            }

            CaptureTarget.transform.position = center;
        }

        public Texture2D GenerateTexture2DFrom3D()
        {
            RenderTexture renderTexture = CreateRenderTexture();
            Camera.targetTexture = renderTexture;
            Camera.Render();
            var texture2D = RenderTextureToTexture2D(renderTexture);
            Camera.targetTexture = null;
            renderTexture.Release();

            return texture2D;
        }

        private Texture2D RenderTextureToTexture2D(RenderTexture renderTexture)
        {
            Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, Texture_Format, false);
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = null;
            return texture2D;
        }

        public void ClearPreviewTexture()
        {
            if (PreviewCamera != null) { PreviewCamera.targetTexture = null; }
            DestroyImmediate(PreviewRenderTexture);
            PreviewRenderTexture = null;
        }

        public void CreatePreviewTexture()
        {
            ClearPreviewTexture();
            PreviewRenderTexture = CreateRenderTexture(true);
            PreviewCamera.targetTexture = PreviewRenderTexture;
        }

        public RenderTexture CreateRenderTexture(bool createTexture = false)
        {
            var renderTexture = new RenderTexture((int)BakingSize, (int)BakingSize, TextureDepthBuffer);
            renderTexture.filterMode = Filter_Mode;
            renderTexture.antiAliasing = (int)AntiAliasing;
            renderTexture.anisoLevel = (int)AnisotropicFiltering;
            renderTexture.format = PreviewRenderTextureFormat;

            if (createTexture) { renderTexture.Create(); }
            return renderTexture;
        }

        public void SaveTexture()
        {
            var textureToSave = GenerateTexture2DFrom3D();

            var assetName = TextureName + GetFileTypeAsString(FileType);
            var fullPath = SelectedSavePath + "/" + assetName;

            byte[] data = textureToSave.EncodeToPNG();
            System.IO.File.WriteAllBytes(fullPath, data);
            AssetDatabase.Refresh();

            ModifyTextureImportSettings(AbsoluteToRelativePath(fullPath));

            AssetDatabase.SaveAssets();

            TextureName = "";
        }

        public void SetSelectedPath(string path)
        {
            SelectedSavePath = path;
        }

        private string GetFileTypeAsString(FileTypes fileType)
        {
            string fileTypeString = "";

            switch (fileType)
            {
                case FileTypes.PNG:
                    fileTypeString = ".png";
                    break;
                case FileTypes.JPG:
                    fileTypeString = ".jpg";
                    break;
            }

            return fileTypeString;
        }

        public void ModifyTextureImportSettings(string assetPath)
        {
            var textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter == null)
            {
                Debug.LogError($"Failed to find the texture at: {assetPath}");
                return;
            }

            textureImporter.textureType = TextureType;
            textureImporter.alphaIsTransparency = SetAlphaIsTransparent;
            textureImporter.maxTextureSize = (int)CompressionSize;
            textureImporter.textureCompression = TextureCompression;

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

            if (PingAssetOnSave) { EditorGUIUtility.PingObject(textureImporter); }
            if (AutoSelectAssetOnSave) { Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath); }

            AssetDatabase.SaveAssets();
        }

        private bool IsPathValid(string path)
        {
            if (string.IsNullOrEmpty(path)) { return false; }

            string relativePath = AbsoluteToRelativePath(path);

            if (relativePath == null)
            {
                Debug.LogError("The provided path is outside the Assets folder.");
                return false;
            }

            if (string.IsNullOrEmpty(relativePath))
            {
                Debug.LogError("The provided path is null or empty.");
                return false;
            }

            if (!relativePath.StartsWith("Assets"))
            {
                Debug.LogError("The path must start with 'Assets'.");
                return false;
            }

            if (!System.IO.Directory.Exists(relativePath) && !System.IO.File.Exists(relativePath))
            {
                Debug.LogError("The specified path does not exist.");
                return false;
            }

            return true;
        }

        private string AbsoluteToRelativePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath)) { return null; }

            string projectPath = System.IO.Path.GetFullPath(Application.dataPath);

            if (absolutePath.StartsWith(projectPath.Replace('\\', '/')))
            {
                return "Assets" + absolutePath.Substring(projectPath.Length).Replace('\\', '/');
            }

            return null;
        }

        public void CenterObjectInCaptureBounds()
        {
            if (CaptureTarget == null || CaptureTarget.transform.childCount == 0) return;

            SetCamerasFOV((CenteringType == AutoCenteringTypes.FOV_Manipulation) ? FovManipulationValue : DefaultFOV);

            Vector3 forward = Camera.transform.forward * CaptureDepth;
            Vector3 desiredCenter = Camera.transform.position + forward * CenteringDepthBufferMultiplier;
            Transform captureTargetChild = TryGetActiveCaptureTargetChild();

            Bounds combinedBounds = CalculateObjectBounds(captureTargetChild.gameObject, CenterIncludesChildrenBounds);

            switch (CenteringType)
            {
                case AutoCenteringTypes.Pivot:
                    captureTargetChild.transform.position = desiredCenter;
                    break;
                case AutoCenteringTypes.Center:
                    Vector3 centerOffset = desiredCenter - combinedBounds.center;
                    captureTargetChild.transform.position += centerOffset;
                    break;
                case AutoCenteringTypes.FOV_Manipulation:
                    float requiredDistance = CalculateRequiredDistance(Camera, combinedBounds);
                    CenterAndFitObject(Camera, captureTargetChild.transform, combinedBounds, requiredDistance);
                    break;
            }

            var largestBound = Mathf.Max(combinedBounds.size.x, combinedBounds.size.y, combinedBounds.size.z);
            CaptureDepth = largestBound;

            UnityEditor.SceneView.RepaintAll();
        }

        private Transform TryGetActiveCaptureTargetChild()
        {
            if(CaptureTarget.transform.childCount == 0) 
            { 
                CurrentChild = null;
                LastChild = null;
                return null;
            }

            if(CurrentChild != null && CurrentChild.gameObject.activeInHierarchy && CurrentChild.transform.parent == CaptureTarget.transform) { return CurrentChild; }

            CurrentChild = CaptureTarget.transform.GetChild(0);

            for (int i = 0; i < CaptureTarget.transform.childCount; i++)
            {
                if (!CaptureTarget.transform.GetChild(i).gameObject.activeInHierarchy) { continue; }
                CurrentChild = CaptureTarget.transform.GetChild(i);
                break;
            }

            if(CurrentChild != LastChild)
            {
                TextureName = CurrentChild.name;
                LastChild = CurrentChild;
            }

            return CurrentChild;
        }

        private Bounds CalculateObjectBounds(GameObject obj, bool includeChildren)
        {
            Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
            Renderer[] renderers = includeChildren ? obj.GetComponentsInChildren<Renderer>() : obj.GetComponents<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                if (bounds.extents == Vector3.zero)
                {
                    bounds = renderer.bounds;
                }
                else
                {
                    bounds.Encapsulate(renderer.bounds);
                }
            }

            return bounds;
        }

        public void ApplyAutoOrientation()
        {
            if (CaptureTarget.transform.childCount == 0) return;

            var childObject = TryGetActiveCaptureTargetChild();

            Vector3 baseDirection = GetDirectionVector(AutoOrientationSetting.Direction);
            Quaternion baseRotation = Quaternion.LookRotation(baseDirection, Vector3.up);
            Quaternion offsetRotation = Quaternion.Euler(AutoOrientationSetting.DesiredOrientationOffset);

            childObject.transform.rotation = offsetRotation * baseRotation;
        }

        private Vector3 GetDirectionVector(OrientationDirection direction)
        {
            switch (direction)
            {
                case OrientationDirection.Forward:
                    return Vector3.forward;
                case OrientationDirection.Back:
                    return Vector3.back;
                case OrientationDirection.Up:
                    return Vector3.up;
                case OrientationDirection.Down:
                    return Vector3.down;
                case OrientationDirection.Left:
                    return Vector3.left;
                case OrientationDirection.Right:
                    return Vector3.right;
                default:
                    return Vector3.forward;
            }
        }

        private float CalculateRequiredDistance(Camera camera, Bounds bounds)
        {
            float objectSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
            float halfAngle = (camera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
            float distance = objectSize / (2f * Mathf.Tan(halfAngle));

            return distance * CenteringDepthBufferMultiplier;
        }

        private void CenterAndFitObject(Camera camera, Transform objectTransform, Bounds bounds, float distance)
        {
            Vector3 direction = camera.transform.forward;
            Vector3 targetPosition = (camera.transform.position + direction * distance);

            Vector3 offsetToCenter = bounds.center - objectTransform.position;
            objectTransform.position = (targetPosition - offsetToCenter);

            UnityEditor.SceneView.RepaintAll();
        }
    }
}

#endif