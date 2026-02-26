#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace RRS.Converter
{
    [CustomEditor(typeof(ObjectToIconConverter))]
    public class ObjectToIconConverterEditor : Editor
    {
        #region Serialized Properties Initialization
        // General Settings
        private SerializedProperty textureNameProp;
        private SerializedProperty fileTypeProp;
        private SerializedProperty textureTypeProp;
        private SerializedProperty bakingSizeProp;
        private SerializedProperty compressionSizeProp;
        private SerializedProperty backgroundAlphaProp;
        private SerializedProperty setAlphaIsTransparentProp;

        // Advanced Settings
        private SerializedProperty textureDepthBufferProp;
        private SerializedProperty textureCompressionProp;
        private SerializedProperty filterModeProp;
        private SerializedProperty antiAliasingProp;
        private SerializedProperty anisotropicFilteringProp;
        private SerializedProperty textureFormatProp;

        // Preview Settings
        private SerializedProperty minPreviewSizeProp;
        private SerializedProperty maxPreviewSizeProp;
        private SerializedProperty previewRenderTextureFormatProp;
        private SerializedProperty displayAlphaInTexturePreviewProp;

        // Alignment and View Configuration
        private SerializedProperty autoCenterInBoundsProp;
        private SerializedProperty centerIncludesChildrenBoundsProp;
        private SerializedProperty centeringTypeProp;
        private SerializedProperty centeringDepthBufferMultiplierProp;
        private SerializedProperty autoOrientateProp;
        private SerializedProperty autoOrientationSettingProp;
        private SerializedProperty fovManipulationValueProp;

        // Gizmo Settings
        private SerializedProperty gizmosProp;
        private SerializedProperty gizmoLineLengthMultiplierProp;
        private SerializedProperty gizmoFrontToEndDepthProp;

        // Additional Settings
        private SerializedProperty pingAssetOnSaveProp;
        private SerializedProperty autoSelectAssetOnSaveProp;
        #endregion

        private bool _showGeneralSettings = true;
        private bool _showAdvancedSettings = true;
        private bool _showAlignmentAndViewConfiguration = true;
        private bool _showPreviewSettings = true;
        private bool _showGizmoSettings = false;
        private bool _showAdditionalSettings = false;
        private GUIStyle _indentedBackgroundStyle;

        private ObjectToIconConverter _converter = null;
        private Texture2D _checkerboardTexture = null;
        private Vector2 _lastPreviewSize = Vector2.zero;

        private GUIStyle IndentedBackgroundStyle
        {
            get 
            {
                return (_indentedBackgroundStyle == null) ? _indentedBackgroundStyle = CreateIndentedBackgroundStyle() : _indentedBackgroundStyle;
            }
            set
            {
                _indentedBackgroundStyle = value;
            }
        }

        private void OnEnable()
        {
            Undo.undoRedoPerformed += UndoRedoCallback;
            Initialize();
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= UndoRedoCallback;

            _converter.ClearPreviewTexture();
        }

        public void Initialize()
        {
            _converter = (ObjectToIconConverter)target;
            _converter.CreatePreviewTexture();
            _checkerboardTexture = null;
            _lastPreviewSize = Vector2.zero;

            InitializeSerializedProperties();
            CreateIndentedBackgroundStyle();
        }

        private void UndoRedoCallback()
        {
            _converter.CreatePreviewTexture();
        }

        public override void OnInspectorGUI()
        {
            if (_converter.CaptureTarget == null)
            {
                EditorGUILayout.LabelField("MISSING CAPTURE TARGET!", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("CREATE AN EMPTY OBJECT WITH CaptureTarget.cs ATTACHED!", EditorStyles.boldLabel);
                return;
            }

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            DrawGeneralSettings();
            DrawAdvancedSettings();
            DrawAlignmentandViewConfiguration();
            DrawPreviewSettings();
            DrawGizmoSettings();
            DrawAdditionalSettings();
            DrawPathSelectionButton();
            DrawHelperButtons();

            GUILayout.BeginHorizontal();
            DrawRefreshTextureButton();
            DrawSaveTextureButton();
            GUILayout.EndHorizontal();

            DrawLivePreview();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                _converter.CreatePreviewTexture();

                UnityEditor.SceneView.RepaintAll();
            }
        }

        private void OnSceneGUI()
        {
            //DrawLiveScenePreview();
            this.Repaint();
        }

        private void InitializeSerializedProperties()
        {
            // General Settings Properties
            textureNameProp = serializedObject.FindProperty("TextureName");
            fileTypeProp = serializedObject.FindProperty("FileType");
            textureTypeProp = serializedObject.FindProperty("TextureType");
            bakingSizeProp = serializedObject.FindProperty("BakingSize");
            compressionSizeProp = serializedObject.FindProperty("CompressionSize");
            backgroundAlphaProp = serializedObject.FindProperty("BackgroundAlpha");
            setAlphaIsTransparentProp = serializedObject.FindProperty("SetAlphaIsTransparent");

            // Advanced Settings Properties
            textureDepthBufferProp = serializedObject.FindProperty("TextureDepthBuffer");
            textureCompressionProp = serializedObject.FindProperty("TextureCompression");
            filterModeProp = serializedObject.FindProperty("Filter_Mode");
            antiAliasingProp = serializedObject.FindProperty("AntiAliasing");
            anisotropicFilteringProp = serializedObject.FindProperty("AnisotropicFiltering");
            textureFormatProp = serializedObject.FindProperty("Texture_Format");

            // Preview Settings Properties
            minPreviewSizeProp = serializedObject.FindProperty("MinPreviewSize");
            maxPreviewSizeProp = serializedObject.FindProperty("MaxPreviewSize");
            previewRenderTextureFormatProp = serializedObject.FindProperty("PreviewRenderTextureFormat");
            displayAlphaInTexturePreviewProp = serializedObject.FindProperty("DisplayAlphaInTexturePreview");

            // Centering and FOV Settings Properties
            autoCenterInBoundsProp = serializedObject.FindProperty("AutoCenterInBounds");
            centerIncludesChildrenBoundsProp = serializedObject.FindProperty("CenterIncludesChildrenBounds");
            centeringTypeProp = serializedObject.FindProperty("CenteringType");
            centeringDepthBufferMultiplierProp = serializedObject.FindProperty("CenteringDepthBufferMultiplier");
            autoOrientateProp = serializedObject.FindProperty("AutoOrientate");
            autoOrientationSettingProp = serializedObject.FindProperty("AutoOrientationSetting");
            fovManipulationValueProp = serializedObject.FindProperty("FovManipulationValue");

            // Gizmo Settings Properties
            gizmosProp = serializedObject.FindProperty("Gizmos");
            gizmoLineLengthMultiplierProp = serializedObject.FindProperty("GizmoLineLengthMultiplier");
            gizmoFrontToEndDepthProp = serializedObject.FindProperty("GizmoFrontToEndDepth");

            // Additional Settings Properties
            pingAssetOnSaveProp = serializedObject.FindProperty("PingAssetOnSave");
            autoSelectAssetOnSaveProp = serializedObject.FindProperty("AutoSelectAssetOnSave");
        }

        private GUIStyle CreateIndentedBackgroundStyle()
        {
            Texture2D backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, new Color(0.35f, 0.35f, 0.35f, 0.35f));
            backgroundTexture.Apply();

            var guiStyle = new GUIStyle();
            guiStyle.normal.background = backgroundTexture;
            guiStyle.padding = new RectOffset(10, 10, 5, 5);

            return guiStyle;
        }

        private void DrawGeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.Foldout(_showGeneralSettings, "General Settings", true);
            if (_showGeneralSettings)
            {
                EditorGUILayout.BeginVertical(IndentedBackgroundStyle);
                EditorGUILayout.PropertyField(textureNameProp, new GUIContent("Texture Name"));
                EditorGUILayout.PropertyField(fileTypeProp, new GUIContent("File Type"));
                EditorGUILayout.PropertyField(textureTypeProp, new GUIContent("Texture Type"));
                EditorGUILayout.PropertyField(bakingSizeProp, new GUIContent("Baking Size"));
                EditorGUILayout.PropertyField(compressionSizeProp, new GUIContent("Compression Size"));
                EditorGUILayout.PropertyField(backgroundAlphaProp, new GUIContent("Background Alpha"));
                EditorGUILayout.PropertyField(setAlphaIsTransparentProp, new GUIContent("Set Alpha Is Transparent"));
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawAdvancedSettings()
        {
            _showAdvancedSettings = EditorGUILayout.Foldout(_showAdvancedSettings, "Advanced Settings", true);
            if (_showAdvancedSettings)
            {
                EditorGUILayout.BeginVertical(IndentedBackgroundStyle);
                EditorGUILayout.PropertyField(textureDepthBufferProp, new GUIContent("Texture Depth"));
                EditorGUILayout.PropertyField(textureCompressionProp, new GUIContent("Texture Compression"));
                EditorGUILayout.PropertyField(filterModeProp, new GUIContent("Filter Mode"));
                EditorGUILayout.PropertyField(antiAliasingProp, new GUIContent("Anti Aliasing"));
                EditorGUILayout.PropertyField(anisotropicFilteringProp, new GUIContent("Anisotropic Filtering"));
                EditorGUILayout.PropertyField(textureFormatProp, new GUIContent("Texture Format"));
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawAlignmentandViewConfiguration()
        {
            _showAlignmentAndViewConfiguration = EditorGUILayout.Foldout(_showAlignmentAndViewConfiguration, "Alignment and View Configuration", true);
            if (_showAlignmentAndViewConfiguration)
            {
                EditorGUILayout.BeginVertical(IndentedBackgroundStyle);
                EditorGUILayout.PropertyField(centeringDepthBufferMultiplierProp, new GUIContent("Centering Depth Buffer Multiplier"));
                EditorGUILayout.PropertyField(centeringTypeProp, new GUIContent("Centering Type"));
                if (centeringTypeProp.enumValueIndex == (int)ObjectToIconConverter.AutoCenteringTypes.FOV_Manipulation)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(fovManipulationValueProp, new GUIContent("FOV Manipulation Value"));
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(autoCenterInBoundsProp, new GUIContent("Auto Center In Bounds"));
                if (autoCenterInBoundsProp.boolValue == true)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(centerIncludesChildrenBoundsProp, new GUIContent("Include Children Bounds"));
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(autoOrientateProp, new GUIContent("Auto Orientate"));
                if (autoOrientateProp.boolValue == true)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(autoOrientationSettingProp, new GUIContent("Auto Orientation Settings"));
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.EndVertical();
            }
        }

        private void DrawPreviewSettings()
        {
            _showPreviewSettings = EditorGUILayout.Foldout(_showPreviewSettings, "Preview Settings", true);
            if (_showPreviewSettings)
            {
                EditorGUILayout.BeginVertical(IndentedBackgroundStyle);
                EditorGUILayout.PropertyField(minPreviewSizeProp, new GUIContent("Min Preview Size"));
                EditorGUILayout.PropertyField(maxPreviewSizeProp, new GUIContent("Max Preview Size"));
                EditorGUILayout.PropertyField(previewRenderTextureFormatProp, new GUIContent("Preview Render Texture Format"));
                EditorGUILayout.PropertyField(displayAlphaInTexturePreviewProp, new GUIContent("Display Alpha In Texture Preview"));
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawGizmoSettings()
        {
            _showGizmoSettings = EditorGUILayout.Foldout(_showGizmoSettings, "Gizmo Visualization Settings", true);
            if (_showGizmoSettings)
            {
                EditorGUILayout.BeginVertical(IndentedBackgroundStyle);
                EditorGUILayout.PropertyField(gizmosProp, new GUIContent("Gizmos"));
                if (gizmosProp.intValue != 0) // Check if any flags are set
                {
                    EditorGUILayout.PropertyField(gizmoLineLengthMultiplierProp, new GUIContent("Gizmo Line Length Multiplier"));
                    EditorGUILayout.PropertyField(gizmoFrontToEndDepthProp, new GUIContent("Gizmo Front To End Depth"));
                }
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawAdditionalSettings()
        {
            _showAdditionalSettings = EditorGUILayout.Foldout(_showAdditionalSettings, "Additional Settings", true);
            if (_showAdditionalSettings)
            {
                EditorGUILayout.BeginVertical(IndentedBackgroundStyle);
                EditorGUILayout.PropertyField(pingAssetOnSaveProp, new GUIContent("Ping Asset On Save"));
                EditorGUILayout.PropertyField(autoSelectAssetOnSaveProp, new GUIContent("Auto Select Asset On Save"));
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawHelperButtons()
        {
            GUILayout.BeginHorizontal();

            DrawCenterBoundsAroundObject();

            GUILayout.EndHorizontal();
        }

        private void DrawCenterBoundsAroundObject()
        {
            EditorGUI.BeginDisabledGroup(_converter.CaptureTarget == null || _converter.CaptureTarget.transform.childCount == 0 || _converter.AutoCenterInBounds);

            if (GUILayout.Button("Center Target in Capture Bounds"))
            {
                _converter.CenterObjectInCaptureBounds();
            }

            EditorGUI.EndDisabledGroup();
        }

        private void DrawPathSelectionButton()
        {
            var buttonTitle = (_converter.PathIsSelected) ? "Change Folder" : "Select Folder";

            if (_converter.PathIsSelected)
            {
                EditorGUILayout.LabelField("Selected Path", _converter.SelectedSavePath);
            }

            if (GUILayout.Button(buttonTitle))
            {
                string path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
                if (!string.IsNullOrEmpty(path))
                {
                    _converter.SetSelectedPath(path);
                    EditorUtility.SetDirty(target);
                }
            }
        }

        private void DrawRefreshTextureButton()
        {
            if (GUILayout.Button("Refresh Live Texture"))
            {
                _converter.CreatePreviewTexture();
            }
        }

        private void DrawSaveTextureButton()
        {
            EditorGUI.BeginDisabledGroup(!_converter.IsSaveValid);

            if (GUILayout.Button("Save Texture"))
            {
                _converter.SaveTexture();
            }

            EditorGUI.EndDisabledGroup();
        }

        private void DrawLiveScenePreview()
        {
            EditorGUILayout.LabelField("Live Preview:", EditorStyles.boldLabel);

            var minPreview = new Vector2(MathF.Max((int)_converter.BakingSize, (int)_converter.MinPreviewSize), MathF.Max((int)_converter.BakingSize, (int)_converter.MinPreviewSize));
            var previewSize = new Vector2(MathF.Min((int)minPreview.x, (int)_converter.MaxPreviewSize), MathF.Min((int)minPreview.x, (int)_converter.MaxPreviewSize));

            Rect previewRect = new Rect(new Vector2(SceneView.lastActiveSceneView.position.width - previewSize.x,
                                    SceneView.lastActiveSceneView.position.height - previewSize.y - 25f),
                                    previewSize); // GUILayoutUtility.GetRect(previewSize.x, previewSize.y, GUI.skin.box);

            if (_converter.DisplayAlphaInTexturePreview)
            {
                if (_checkerboardTexture == null || _lastPreviewSize.x != previewSize.x || _lastPreviewSize.y != previewSize.y)
                {
                    DestroyImmediate(_checkerboardTexture);
                    _checkerboardTexture = GenerateCheckerboardTexture(128, 128, 16);

                    _lastPreviewSize = previewSize;
                }

                FillRectWithTexture(previewRect, _checkerboardTexture, previewSize.x, previewSize.y);
            }

            if (_converter.PreviewRenderTexture)
            {
                if (Event.current.type == EventType.Repaint)
                {
                    GUI.DrawTexture(previewRect, _converter.PreviewRenderTexture);
                }
            }

            UnityEditor.SceneView.RepaintAll();
        }

        private void DrawLivePreview()
        {
            EditorGUILayout.LabelField("Live Preview:", EditorStyles.boldLabel);

            var minPreview = new Vector2(MathF.Max((int)_converter.BakingSize, (int)_converter.MinPreviewSize), MathF.Max((int)_converter.BakingSize, (int)_converter.MinPreviewSize));
            var previewSize = new Vector2(MathF.Min((int)minPreview.x, (int)_converter.MaxPreviewSize), MathF.Min((int)minPreview.x, (int)_converter.MaxPreviewSize));

            var previewRect = GUILayoutUtility.GetRect(previewSize.x, previewSize.y, GUI.skin.box);

            if (_converter.DisplayAlphaInTexturePreview)
            {
                if (_checkerboardTexture == null || _lastPreviewSize.x != previewSize.x || _lastPreviewSize.y != previewSize.y)
                {
                    DestroyImmediate(_checkerboardTexture);
                    _checkerboardTexture = GenerateCheckerboardTexture(128, 128, 16);

                    _lastPreviewSize = previewSize;
                }

                FillRectWithTexture(previewRect, _checkerboardTexture, previewSize.x, previewSize.y);
            }

            if (_converter.PreviewRenderTexture)
            {
                if (Event.current.type == EventType.Repaint)
                {
                    GUI.DrawTexture(previewRect, _converter.PreviewRenderTexture);
                }
            }
        }

        private Texture2D GenerateCheckerboardTexture(int width, int height, int cellSize)
        {
            Texture2D texture = new Texture2D(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool isCellWhite = (x / cellSize + y / cellSize) % 2 == 0;
                    texture.SetPixel(x, y, isCellWhite ? Color.white : Color.gray);
                }
            }

            texture.Apply();
            texture.filterMode = FilterMode.Point;
            return texture;
        }

        private void FillRectWithTexture(Rect rect, Texture2D texture, float previewWidth, float previewHeight)
        {
            float offsetX = (rect.width - previewWidth) * 0.5f;
            float offsetY = (rect.height - previewHeight) * 0.5f;

            GUI.BeginGroup(new Rect(rect.x + offsetX, rect.y + offsetY, previewWidth, previewHeight));
            float xTiles = previewWidth / texture.width;
            float yTiles = previewHeight / texture.height;

            for (float y = 0; y < yTiles; y++)
            {
                for (float x = 0; x < xTiles; x++)
                {
                    GUI.DrawTexture(new Rect(x * texture.width, y * texture.height, texture.width, texture.height), texture);
                }
            }

            GUI.EndGroup();
        }
    }
}

#endif