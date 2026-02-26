// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace AAMAP
{
    [CustomEditor(typeof(MinimapManager))]
    public class MinimapManagerEditor : Editor
    {
        [Tooltip("Minimap Manager component on the \"Minimap\" GameObject.")] private MinimapManager minimapManager = null;
        [Tooltip("Rect Transform component on the \"Minimap\" GameObject.")] private RectTransform minimapRectTransform = null;

        [Tooltip("Image component on the \"Minimap Mask\" GameObject.")] private Image maskImage = null;
        [Tooltip("Raw Image component on the \"Minimap > Minimap Mask > Minimap Display\" GameObject.")] private RawImage displayImage = null;
        [Tooltip("Image component on the \"Map > Map Mask > Map Background Filler\" GameObject.")] private Image backgroundFillerImage = null;

        [Tooltip("\"Minimap > Minimap Directions\" GameObject.")] private GameObject minimapDirectionsObject = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions\" GameObject.")] private RectTransform minimapDirectionsRect = null;
        [Tooltip("\"Minimap > Minimap Directions > North\" GameObject.")] private GameObject northObject = null;
        [Tooltip("\"Minimap > Minimap Directions > East\" GameObject.")] private GameObject eastObject = null;
        [Tooltip("\"Minimap > Minimap Directions > South\" GameObject.")] private GameObject southObject = null;
        [Tooltip("\"Minimap > Minimap Directions > West\" GameObject.")] private GameObject westObject = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > North\" GameObject.")] private RectTransform northRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > East\" GameObject.")] private RectTransform eastRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > South\" GameObject.")] private RectTransform southRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > West\" GameObject.")] private RectTransform westRect = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > North\" GameObject.")] private Text northText = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > East\" GameObject.")] private Text eastText = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > South\" GameObject.")] private Text southText = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > West\" GameObject.")] private Text westText = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > North\" GameObject.")] private Image northImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > East\" GameObject.")] private Image eastImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > South\" GameObject.")] private Image southImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > West\" GameObject.")] private Image westImage = null;

        [Tooltip("\"Minimap > Minimap Border\" GameObject.")] private GameObject minimapBorderObject = null;
        [Tooltip("Image component on the \"Minimap > Minimap Border\" GameObject.")] private Image borderImage = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Border\" GameObject.")] private RectTransform borderRect = null;

        [Tooltip("Camera component on the \"Minimap Camera\" GameObject.")] private Camera minimapCamera = null;
        [Tooltip("Transform component on the \"Minimap Camera\" GameObject.")] private Transform cameraTransform = null;

        [Tooltip("\"Minimap > Minimap Zoom Buttons\" GameObject.")] private GameObject minimapZoomButtons = null;
        [Tooltip("\"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject.")] private GameObject zoomInButton = null;
        [Tooltip("\"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject.")] private GameObject zoomOutButton = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject.")] private RectTransform zoomInRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject.")] private RectTransform zoomOutRect = null;
        [Tooltip("Image component on the \"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject.")] private Image zoomInImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject.")] private Image zoomOutImage = null;

        [Tooltip("\"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private GameObject gridObject = null;
        [Tooltip("Image component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private Image gridImage = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private RectTransform gridRect = null;

        [Tooltip("This Rect is used to add Tooltips to the inspector fields.")] private Rect typeRect;
        [Tooltip("This GUI Content is used to add Tooltips to the inspector fields.")] private GUIContent guiContent;
        [Tooltip("This is the tab horizontal space distance on the sub-fields in the inspector.")] private readonly float spaceDistance = 20F;

        private readonly string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";
        private readonly string errorMessageSuffix = " Please delete and re-create the minimap.\n";

        /// <summary>
        /// Creates the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            GetGameObjectsAndComponents();

            EditorGUILayout.LabelField("Inner Display", EditorStyles.boldLabel);

            minimapManager.minimapShape = (Sprite)EditorGUILayout.ObjectField("Shape", minimapManager.minimapShape, typeof(Sprite), true, GUILayout.Height(EditorGUIUtility.singleLineHeight), GUILayout.ExpandWidth(true));
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "Shape of the minimap inner display.\n\nMethods:\nGetMinimapShape( );\nSetMinimapShape(...);");
            GUI.Label(typeRect, guiContent);

            minimapManager.minimapOpacity = EditorGUILayout.Slider("Opacity", minimapManager.minimapOpacity, 0F, 1F);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "Opacity of the minimap inner display. Ranged from 0 (inclusive) to 1 (inclusive).\n\nMethods:\nGetMinimapOpacity( );\nSetMinimapOpacity(...);");
            GUI.Label(typeRect, guiContent);

            minimapManager.minimapColor = EditorGUILayout.ColorField("Color", minimapManager.minimapColor);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "Color of the minimap inner display.\n\nMethods:\nGetMinimapColor( );\nSetMinimapColor(...);");
            GUI.Label(typeRect, guiContent);

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Minimap Directions", EditorStyles.boldLabel);

            minimapManager.displayDirections = EditorGUILayout.Toggle("Display Directions", minimapManager.displayDirections);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "If this is true, \"North, East, South and West\" signs will be displayed on the minimap.\n\nMethods:\nAreDirectionsEnabled( );\nEnableDirections( );\nDisableDirections( );");
            GUI.Label(typeRect, guiContent);

            if (minimapManager.displayDirections)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.directionsDistance = EditorGUILayout.FloatField("Distance", minimapManager.directionsDistance);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Distance from the center of the minimap to the positions of the direction signs.\n\nMethods:\nGetDirectionsDistance( );\nSetDirectionsDistance(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.directionsRotation = EditorGUILayout.Slider("Rotation", minimapManager.directionsRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Rotation value of the direction signs.\n\nMethods:\nGetDirectionsRotation( );\nSetDirectionsRotation(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.directionsTextFont = (Font)EditorGUILayout.ObjectField("Font", minimapManager.directionsTextFont, typeof(Font), true);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Text font of the direction signs.\n\nMethods:\nGetDirectionsFont( );\nSetDirectionsFont(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.directionsTextFontSize = EditorGUILayout.IntField("Font Size", minimapManager.directionsTextFontSize);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Text font size of the direction signs.\n\nMethods:\nGetDirectionsFontSize( );\nSetDirectionsFontSize(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.directionsTextColor = EditorGUILayout.ColorField("Font Color", minimapManager.directionsTextColor);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Text color of the direction signs.\n\nMethods:\nGetDirectionsTextColor( );\nSetDirectionsTextColor(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.directionsHaveBackground = EditorGUILayout.Toggle("Background Images", minimapManager.directionsHaveBackground);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the direction signs will have background images.\n\nMethods:\nDoesDirectionsHaveBackgroundImages( );\nEnableDirectionsBackgroundImages( );\nDisableDirectionsBackgroundImages( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                if (minimapManager.directionsHaveBackground)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance * 2);
                    minimapManager.directionsBackgroundImageScale = EditorGUILayout.Vector2Field("Scale", minimapManager.directionsBackgroundImageScale);
                    typeRect = GUILayoutUtility.GetLastRect();
                    guiContent = new GUIContent("", "Background image scales of the direction signs.\n\nMethods:\nGetDirectionsBackgroundScale( );\nSetDirectionsBackgroundScale(...);");
                    GUI.Label(typeRect, guiContent);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance * 2);
                    EditorGUILayout.PrefixLabel("Sprite");
                    typeRect = GUILayoutUtility.GetLastRect();
                    guiContent = new GUIContent("", "Background image sprites of the direction signs.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Direction Backgrounds\".\n\nMethods:\nGetDirectionsBackgroundSprites( );\nSetDirectionsBackgroundSprites(...);");
                    GUI.Label(typeRect, guiContent);
                    minimapManager.directionsBackgroundSprite = (Sprite)EditorGUILayout.ObjectField(minimapManager.directionsBackgroundSprite, typeof(Sprite), true);
                    typeRect = GUILayoutUtility.GetLastRect();
                    GUI.Label(typeRect, guiContent);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance * 2);
                    minimapManager.directionsBackgroundColor = EditorGUILayout.ColorField("Color", minimapManager.directionsBackgroundColor);
                    typeRect = GUILayoutUtility.GetLastRect();
                    guiContent = new GUIContent("", "Background image color of the direction signs.\n\nMethods:\nGetDirectionsBackgroundColor( );\nSetDirectionsBackgroundColor(...);");
                    GUI.Label(typeRect, guiContent);
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.displayNorth = EditorGUILayout.Toggle("Display North", minimapManager.displayNorth);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the North direction sign will be displayed on the minimap.\n\nMethods:\nIsNorthEnabled( );\nEnableNorthSign( );\nDisableNorthSign( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.displayEast = EditorGUILayout.Toggle("Display East", minimapManager.displayEast);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the East direction sign will be displayed on the minimap.\n\nMethods:\nIsEastEnabled( );\nEnableEastSign( );\nDisableEastSign( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.displaySouth = EditorGUILayout.Toggle("Display South", minimapManager.displaySouth);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the South direction sign will be displayed on the minimap.\n\nMethods:\nIsSouthEnabled( );\nEnableSouthSign( );\nDisableSouthSign( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.displayWest = EditorGUILayout.Toggle("Display West", minimapManager.displayWest);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the West direction sign will be displayed on the minimap.\n\nMethods:\nIsWestEnabled( );\nEnableWestSign( );\nDisableWestSign( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Minimap Border", EditorStyles.boldLabel);

            minimapManager.haveBorder = EditorGUILayout.Toggle("Have Border", minimapManager.haveBorder);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "If this is true, the minimap will have a border.\n\nMethods:\nIsBorderEnabled( );\nEnableBorder( );\nDisableBorder( );");
            GUI.Label(typeRect, guiContent);

            if (minimapManager.haveBorder)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Sprite of the minimap border.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Minimap Borders\".\n\nMethods:\nGetBorderSprite( );\nSetBorderSprite(...);");
                GUI.Label(typeRect, guiContent);
                minimapManager.borderSprite = (Sprite)EditorGUILayout.ObjectField(minimapManager.borderSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.borderColor = EditorGUILayout.ColorField("Color", minimapManager.borderColor);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Color of the minimap border.\n\nMethods:\nGetBorderColor( );\nSetBorderColor(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.borderRotation = EditorGUILayout.Slider("Rotation", minimapManager.borderRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Rotation value of the minimap border.\n\nMethods:\nGetBorderRotation( );\nSetBorderRotation(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.rotateWithDisplay = EditorGUILayout.Toggle("Rotate With Inner Display", minimapManager.rotateWithDisplay);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the minimap border is going to rotate with the inner display.\n\nMethods:\nDoesBorderRotate( );\nEnableBorderRotation( );\nDisableBorderRotation( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Zoom In and Zoom Out Buttons", EditorStyles.boldLabel);

            minimapManager.haveZoomButtons = EditorGUILayout.Toggle("Have Zoom Buttons", minimapManager.haveZoomButtons);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "If this is true, the minimap is going to have zoom in and zoom out buttons on it.\n\nMethods:\nAreZoomButtonsEnabled( );\nEnableZoomButtons( );\nDisableZoomButtons( );");
            GUI.Label(typeRect, guiContent);

            if (minimapManager.haveZoomButtons)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomingSensitivity = EditorGUILayout.FloatField("Zooming Sensitivity", minimapManager.zoomingSensitivity);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "The zooming sensitivity is the strength of the zoom in and zoom out actions.\n\nMethods:\nGetZoomingSensitivity( );\nSetZoomingSensitivity(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.minimumRange = EditorGUILayout.FloatField("Minimum Range", minimapManager.minimumRange);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Minimum range the player can get while zooming in.\n\nMethods:\nGetMinimumRange( );\nSetMinimumRange(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.maximumRange = EditorGUILayout.FloatField("Maximum Range", minimapManager.maximumRange);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Maximum range the player can get while zooming out.\n\nMethods:\nGetMaximumRange( );\nSetMaximumRange( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Zoom In Button Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Sprite of the zoom in button.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".\n\nMethods:\nGetZoomInButtonSprite( );\nSetZoomInButtonSprite(...);");
                GUI.Label(typeRect, guiContent);
                minimapManager.zoomInButtonSprite = (Sprite)EditorGUILayout.ObjectField(minimapManager.zoomInButtonSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Zoom Out Button Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Sprite of the zoom out button.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".\n\nMethods:\nGetZoomOutButtonSprite( );\nSetZoomOutButtonSprite(...);");
                GUI.Label(typeRect, guiContent);
                minimapManager.zoomOutButtonSprite = (Sprite)EditorGUILayout.ObjectField(minimapManager.zoomOutButtonSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomInButtonPosition = EditorGUILayout.Vector2Field("Zoom In Button Position", minimapManager.zoomInButtonPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Position of the zoom in button.\n\nMethods:\nGetZoomInButtonPosition( );\nSetZoomInButtonPosition(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomInButtonScale = EditorGUILayout.Vector2Field("Zoom In Button Scale", minimapManager.zoomInButtonScale);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Scale of the zoom in button.\n\nMethods:\nGetZoomInButtonScale( );\nSetZoomInButtonScale(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomOutButtonPosition = EditorGUILayout.Vector2Field("Zoom Out Button Position", minimapManager.zoomOutButtonPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Position of the zoom out button.\n\nMethods:\nGetZoomOutButtonPosition( );\nSetZoomOutButtonPosition(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomOutButtonScale = EditorGUILayout.Vector2Field("Zoom Out Button Scale", minimapManager.zoomOutButtonScale);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Scale of the zoom out button.\n\nMethods:\nGetZoomOutButtonScale( );\nSetZoomOutButtonScale(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomInButtonColor = EditorGUILayout.ColorField("Zoom In Button Color", minimapManager.zoomInButtonColor);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Color of the zoom in button.\n\nMethods:\nGetZoomInButtonColor( );\nSetZoomInButtonColor(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.zoomOutButtonColor = EditorGUILayout.ColorField("Zoom Out Button Color", minimapManager.zoomOutButtonColor);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Color of the zoom out button.\n\nMethods:\nGetZoomOutButtonColor( );\nSetZoomOutButtonColor(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Minimap Grid", EditorStyles.boldLabel);

            minimapManager.displayGrid = EditorGUILayout.Toggle("Display Grid", minimapManager.displayGrid);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "If this is true, a grid will be displayed on the minimap.\n\nMethods:\nIsGridEnabled( );\nEnableGrid( );\nDisableGrid( );");
            GUI.Label(typeRect, guiContent);

            if (minimapManager.displayGrid)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Sprite of the minimap grid.\n\nYou can find these sprites at \"Assets > AA Map and Minimap System > Sprites > Grids\".\n\nMethods:\nGetGridSprite( );\nSetGridSprite(...);");
                GUI.Label(typeRect, guiContent);
                minimapManager.gridSprite = (Sprite)EditorGUILayout.ObjectField(minimapManager.gridSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.gridColor = EditorGUILayout.ColorField("Color", minimapManager.gridColor);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Color of the minimap grid.\n\nMethods:\nGetGridColor( );\nSetGridColor(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.gridOpacity = EditorGUILayout.Slider("Opacity", minimapManager.gridOpacity, 0F, 1F);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Opacity of the minimap grid.\n\nMethods:\nGetGridOpacity( );\nSetGridOpacity(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.gridScale = EditorGUILayout.Vector3Field("Scale", minimapManager.gridScale);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Scale of the minimap grid.\n\nMethods:\nGetGridScale( );\nSetGridScale(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.gridRotation = EditorGUILayout.Slider("Rotation", minimapManager.gridRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Rotation value of the minimap grid.\n\nMethods:\nGetGridRotation( );\nSetGridRotation(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.gridRotateWithCamera = EditorGUILayout.Toggle("Rotate With Camera", minimapManager.gridRotateWithCamera);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the grid will rotate with the camera.\n\nMethods:\nDoesGridRotatesWithCamera( );\nRotateGridWithCamera( );\nDontRotateGridWithCamera( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Target Object", EditorStyles.boldLabel);

            minimapManager.targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", minimapManager.targetObject, typeof(GameObject), true);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "If a GameObject is assigned to this, the minimap camera is going to follow it.\n\nMost of the time, the Target Object is the player GameObject.\n\nHaving a Target Object is optional.\n\nMethods:\nGetTargetObject( );\nSetTargetObject(...);");
            GUI.Label(typeRect, guiContent);

            if (minimapManager.targetObject != null && minimapManager.minimapCamera != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.rotateWithTarget = EditorGUILayout.Toggle("Rotate With Target", minimapManager.rotateWithTarget);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "If this is true, the minimap camera is going to rotate on the Y axis with the target GameObject.\n\nMethods:\nDoesCameraRotateWithTarget( );\nEnableRotationWithTarget( );\nDisableRotationWithTarget( );");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.defaultRotation = EditorGUILayout.Slider("Default Rotation", minimapManager.defaultRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Default rotation value of the camera.\n\nMethods:\nGetCameraDefaultRotation( );\nSetCameraDefaultRotation(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Minimap Camera", EditorStyles.boldLabel);

            minimapManager.minimapCamera = (GameObject)EditorGUILayout.ObjectField("Minimap Camera", minimapManager.minimapCamera, typeof(GameObject), true);
            typeRect = GUILayoutUtility.GetLastRect();
            guiContent = new GUIContent("", "The \"Minimap Camera\" GameObject.\n\nThis GameObject is generated with the Minimap.\n\nMethods:\nGetCamera( );\nSetCamera(...);");
            GUI.Label(typeRect, guiContent);

            if (minimapCamera != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.minimapHeight = EditorGUILayout.FloatField("Camera Height", minimapManager.minimapHeight);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Position of the minimap camera on the Y axis.\n\nIt is recommended to set this value above the shadow distance in your scene.\n\nMethods:\nGetCameraHeight( );\nSetCameraHeight(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.nearClippingPlane = EditorGUILayout.FloatField("Near Clipping Plane", minimapManager.nearClippingPlane);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "The closest point to the Minimap Camera where drawing occurs.\n\nMethods:\nGetCameraNearClippingPlane( );\nSetCameraNearClippingPlane(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.farClippingPlane = EditorGUILayout.FloatField("Far Clipping Plane", minimapManager.farClippingPlane);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "The furthest point from the Minimap Camera that drawing occurs.\n\nMethods:\nGetCameraFarClippingPlane( );\nSetCameraFarClippingPlane(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.minimapRange = EditorGUILayout.FloatField("Camera Range", minimapManager.minimapRange);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "Range of the minimap camera. Increase this value to display larger parts on the map.\n\nMethods:\nGetCameraRange( );\nSetCameraRange(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                minimapManager.clearFlags = (MinimapClearFlags)EditorGUILayout.EnumPopup("Clear Flags", minimapManager.clearFlags);
                typeRect = GUILayoutUtility.GetLastRect();
                guiContent = new GUIContent("", "What to display in empty areas of the minimap camera's view.\n\nChoose Skybox to display a skybox in the empty areas, defaulting to a background color if no skybox is found.\n\nChoose Solid Color to display a solid background color in empty areas.\n\nChoose Depth Only to display nothing in empty areas.\n\nChoose Don't Clear to display whatever was displayed in the previous frame in empty areas.\n\nMethods:\nGetClearFlags( );\nSetClearFlags(...);");
                GUI.Label(typeRect, guiContent);
                GUILayout.EndHorizontal();

                if (minimapManager.clearFlags == MinimapClearFlags.Skybox || minimapManager.clearFlags == MinimapClearFlags.SolidColor)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance);
                    minimapManager.backgroundColor = EditorGUILayout.ColorField("Background Color", minimapManager.backgroundColor);
                    typeRect = GUILayoutUtility.GetLastRect();
                    guiContent = new GUIContent("", "Minimap camera clears the screen to this color before rendering.\n\nMethods:\nGetCameraBackgroundColor( );\nSetCameraBackgroundColor(...);");
                    GUI.Label(typeRect, guiContent);
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space(20F);

            if (GUILayout.Button("Delete Minimap"))
            {
                if (minimapManager.minimapCamera.GetComponent<Camera>().targetTexture != null)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(minimapManager.minimapCamera.GetComponent<Camera>().targetTexture));
                }

                if (minimapManager.minimapCamera != null)
                {
                    GameObject.DestroyImmediate(minimapManager.minimapCamera);
                }
                
                GameObject.DestroyImmediate(minimapManager.gameObject);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Reset Minimap"))
            {
                ResetMinimap();
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (target != null)
                {
                    ApplyMinimapShape();
                    ApplyMinimapOpacity(minimapManager.minimapOpacity);
                    ApplyMinimapColor(minimapManager.minimapColor);

                    if (minimapManager.displayDirections)
                    {
                        minimapDirectionsObject.SetActive(true);

                        ApplyNESWPositions(minimapManager.directionsDistance);
                        ApplyNESWRotations(minimapManager.directionsRotation);
                        ApplyNESWFont(minimapManager.directionsTextFont);
                        ApplyNESWFontSize(minimapManager.directionsTextFontSize);
                        ApplyNESWFontColor(minimapManager.directionsTextColor);
                        EnableOrDisableNESW(minimapManager.displayNorth, minimapManager.displayEast, minimapManager.displaySouth, minimapManager.displayWest);

                        if (minimapManager.directionsHaveBackground)
                        {
                            ApplyNESWBackgroundSize(minimapManager.directionsBackgroundImageScale);
                            ApplyNESWBackgrounds(minimapManager.directionsBackgroundSprite, minimapManager.directionsBackgroundColor);
                        }
                        else
                        {
                            northImage.color = Color.clear;
                            eastImage.color = Color.clear;
                            southImage.color = Color.clear;
                            westImage.color = Color.clear;
                        }
                    }
                    else
                    {
                        minimapDirectionsObject.SetActive(false);
                    }

                    if (minimapManager.haveBorder)
                    {
                        minimapBorderObject.SetActive(true);

                        ApplyBorderSprite(minimapManager.borderSprite);
                        ApplyBorderColor(minimapManager.borderColor);
                        ApplyBorderRotation(minimapManager.borderRotation);
                    }
                    else
                    {
                        minimapBorderObject.SetActive(false);
                    }

                    if (minimapManager.haveZoomButtons)
                    {
                        minimapZoomButtons.SetActive(true);

                        ApplyZoomButtonsSprites(minimapManager.zoomInButtonSprite, minimapManager.zoomOutButtonSprite);
                        ApplyZoomButtonsPositions(minimapManager.zoomInButtonPosition, minimapManager.zoomOutButtonPosition);
                        ApplyZoomInButtonScale(minimapManager.zoomInButtonScale);
                        ApplyZoomOutButtonScale(minimapManager.zoomOutButtonScale);
                        ApplyZoomInButtonColor(minimapManager.zoomInButtonColor);
                        ApplyZoomOutButtonColor(minimapManager.zoomOutButtonColor);
                    }
                    else
                    {
                        if (minimapZoomButtons != null)
                        {
                            minimapZoomButtons.SetActive(false);
                        }
                    }

                    if (minimapManager.displayGrid)
                    {
                        gridObject.SetActive(true);

                        ApplyMinimapGrid(minimapManager.displayGrid, minimapManager.gridSprite, minimapManager.gridColor, minimapManager.gridOpacity, minimapManager.gridScale, minimapManager.gridRotation);
                    }
                    else
                    {
                        gridObject.SetActive(false);
                    }

                    if (minimapManager.targetObject != null && minimapManager.minimapCamera != null)
                    {
                        cameraTransform.position = minimapManager.targetObject.transform.position + new Vector3(0F, minimapManager.minimapHeight, 0F);

                        ApplyMinimapCameraRotation();
                    }

                    ApplyCameraHeightAndRange(minimapManager.minimapHeight, minimapManager.minimapRange);
                    ApplyCameraClippingPlanes(minimapManager.nearClippingPlane, minimapManager.farClippingPlane);

                    ApplyCameraClearFlagsAndColor();

                    Undo.RecordObject(target, "Changed Minimap Manager");
                }
            }
        }

        /// <summary>
        /// This method gets all the necessary GameObjects and components on the minimap.
        /// </summary>
        private void GetGameObjectsAndComponents()
        {
            if (minimapManager == null)
            {
                minimapManager = (MinimapManager)target;
            }

            if (minimapRectTransform == null)
            {
                minimapRectTransform = minimapManager.GetComponent<RectTransform>();

                if (minimapRectTransform == null)
                {
                    Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (maskImage == null)
            {
                for (int i = 0; i < minimapManager.transform.childCount; i++)
                {
                    if (minimapManager.transform.GetChild(i).name.Equals("Minimap Mask"))
                    {
                        maskImage = minimapManager.transform.GetChild(i).GetComponent<Image>();
                        break;
                    }
                }

                if (maskImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Image component on the \"" + minimapManager.gameObject.name + " > Minimap Mask\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (displayImage == null)
            {
                for (int i = 0; i < maskImage.transform.childCount; i++)
                {
                    if (maskImage.transform.GetChild(i).name.Equals("Minimap Display"))
                    {
                        displayImage = maskImage.transform.GetChild(i).GetComponent<RawImage>();
                        break;
                    }
                }

                if (displayImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Raw Image component on the \"" + minimapManager.gameObject.name + " > Minimap Mask > Minimap Display\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (backgroundFillerImage == null)
            {
                for (int i = 0; i < maskImage.transform.childCount; i++)
                {
                    if (maskImage.transform.GetChild(i).name.Equals("Minimap Background Filler"))
                    {
                        backgroundFillerImage = maskImage.transform.GetChild(i).GetComponent<Image>();
                        break;
                    }
                }

                if (backgroundFillerImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Mask > Minimap Background Filler\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (minimapDirectionsObject == null)
            {
                for (int i = 0; i < minimapManager.transform.childCount; i++)
                {
                    if (minimapManager.transform.GetChild(i).name.Equals("Minimap Directions"))
                    {
                        minimapDirectionsObject = minimapManager.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                if (minimapDirectionsObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "\"" + minimapManager.gameObject.name + " > Minimap Directions\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (minimapBorderObject == null)
            {
                for (int i = 0; i < minimapManager.transform.childCount; i++)
                {
                    if (minimapManager.transform.GetChild(i).name.Equals("Minimap Border"))
                    {
                        minimapBorderObject = minimapManager.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                if (minimapBorderObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "\"" + minimapManager.gameObject.name + " > Minimap Border\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (borderImage == null)
            {
                if (minimapBorderObject != null)
                {
                    borderImage = minimapBorderObject.GetComponent<Image>();

                    if (borderImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap Border\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (borderRect == null)
            {
                if (minimapBorderObject != null)
                {
                    borderRect = minimapBorderObject.GetComponent<RectTransform>();

                    if (borderRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"" + minimapManager.gameObject.name + " > Minimap Border\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (minimapDirectionsRect == null)
            {
                if (minimapDirectionsObject != null)
                {
                    minimapDirectionsRect = minimapDirectionsObject.GetComponent<RectTransform>();

                    if (minimapDirectionsRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"" + minimapManager.gameObject.name + " > Minimap Directions\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (northRect == null || eastRect == null || southRect == null || westRect == null)
            {
                if (minimapDirectionsObject != null)
                {
                    for (int i = 0; i < minimapDirectionsObject.transform.childCount; i++)
                    {
                        if (minimapDirectionsObject.transform.GetChild(i).name.Equals("North"))
                        {
                            northRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            northText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            northImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            northObject = minimapDirectionsObject.transform.GetChild(i).gameObject;
                        }
                        else if (minimapDirectionsObject.transform.GetChild(i).name.Equals("East"))
                        {
                            eastRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            eastText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            eastImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            eastObject = minimapDirectionsObject.transform.GetChild(i).gameObject;
                        }
                        else if (minimapDirectionsObject.transform.GetChild(i).name.Equals("South"))
                        {
                            southRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            southText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            southImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            southObject = minimapDirectionsObject.transform.GetChild(i).gameObject;
                        }
                        else if (minimapDirectionsObject.transform.GetChild(i).name.Equals("West"))
                        {
                            westRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            westText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            westImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            westObject = minimapDirectionsObject.transform.GetChild(i).gameObject;
                        }
                    }

                    if (northObject == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastObject == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southObject == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westObject == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (northRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (northText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Text component on the \"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Text component on the \"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Text component on the \"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Text component on the \"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (northImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (minimapCamera == null && minimapManager.minimapCamera != null)
            {
                minimapCamera = minimapManager.minimapCamera.GetComponent<Camera>();

                if (minimapCamera == null)
                {
                    Debug.LogError(errorMessagePrefix + "Camera component on the \"Minimap Camera\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (minimapZoomButtons == null)
            {
                for (int i = 0; i < minimapManager.transform.childCount; i++)
                {
                    if (minimapManager.transform.GetChild(i).name.Equals("Minimap Zoom Buttons"))
                    {
                        minimapZoomButtons = minimapManager.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                if (minimapZoomButtons == null)
                {
                    Debug.LogError(errorMessagePrefix + "\"" + minimapManager.gameObject.name + " > Minimap Zoom Buttons\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (zoomInButton == null)
            {
                if (minimapZoomButtons != null)
                {
                    for (int i = 0; i < minimapZoomButtons.transform.childCount; i++)
                    {
                        if (minimapZoomButtons.transform.GetChild(i).name.Equals("Zoom In Button"))
                        {
                            zoomInButton = minimapZoomButtons.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (zoomInButton == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (zoomOutButton == null)
            {
                if (minimapZoomButtons != null)
                {
                    for (int i = 0; i < minimapZoomButtons.transform.childCount; i++)
                    {
                        if (minimapZoomButtons.transform.GetChild(i).name.Equals("Zoom Out Button"))
                        {
                            zoomOutButton = minimapZoomButtons.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (zoomOutButton == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (zoomInRect == null || zoomOutRect == null || zoomInImage == null || zoomOutImage == null)
            {
                if (zoomInButton != null)
                {
                    zoomInRect = zoomInButton.GetComponent<RectTransform>();
                    zoomInImage = zoomInButton.GetComponent<Image>();

                    if (zoomInRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (zoomInImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }

                if (zoomOutButton != null)
                {
                    zoomOutRect = zoomOutButton.GetComponent<RectTransform>();
                    zoomOutImage = zoomOutButton.GetComponent<Image>();

                    if (zoomOutRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (zoomOutImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (cameraTransform == null)
            {
                if (minimapCamera != null)
                {
                    cameraTransform = minimapCamera.transform;
                }
            }

            if (gridObject == null)
            {
                if (maskImage != null)
                {
                    for (int i = 0; i < maskImage.transform.childCount; i++)
                    {
                        if (maskImage.transform.GetChild(i).name.Equals("Minimap Grid"))
                        {
                            gridObject = maskImage.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (gridObject == null)
                    {
                        Debug.LogError(errorMessagePrefix + "\"Minimap > Minimap Mask > Minimap Grid\" GameObject could not be found." + errorMessageSuffix);
                    }
                    else
                    {
                        gridImage = gridObject.GetComponent<Image>();
                        gridRect = gridObject.GetComponent<RectTransform>();

                        if (gridImage == null)
                        {
                            Debug.LogError(errorMessagePrefix + "Image component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject could not be found." + errorMessageSuffix);
                        }

                        if (gridRect == null)
                        {
                            Debug.LogError(errorMessagePrefix + "Rect Transform component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject could not be found." + errorMessageSuffix);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies the minimap shape according to the inspector value.
        /// </summary>
        private void ApplyMinimapShape()
        {
            maskImage.sprite = minimapManager.minimapShape;
        }

        /// <summary>
        /// Applies the minimap opacity according to the inspector value.
        /// </summary>
        /// <param name="opacity">New opacity of the minimap. Ranged from 0 (inclusive) to 1 (inclusive).</param>
        private void ApplyMinimapOpacity(float opacity)
        {
            displayImage.color = new Color(minimapManager.minimapColor.r, minimapManager.minimapColor.g, minimapManager.minimapColor.b, opacity);
            backgroundFillerImage.color = new Color(0F, 0F, 0F, opacity);
        }

        /// <summary>
        /// Applies the minimap color according to the inspector value.
        /// </summary>
        /// <param name="minimapColor"></param>
        private void ApplyMinimapColor(Color minimapColor)
        {
            displayImage.color = new Color(minimapColor.r, minimapColor.g, minimapColor.b, minimapManager.minimapOpacity);
        }

        /// <summary>
        /// Applies the positions of the North, East, South and West signs according to the inspector values.
        /// </summary>
        private void ApplyNESWPositions(float distance)
        {
            northRect.anchoredPosition = new Vector3(0F, distance, 0F);
            eastRect.anchoredPosition = new Vector3(distance, 0F, 0F);
            southRect.anchoredPosition = new Vector3(0F, distance * -1F, 0F);
            westRect.anchoredPosition = new Vector3(distance * -1F, 0F, 0F);
        }

        /// <summary>
        /// Applies the default rotations of the North, East, South and West signs.
        /// </summary>
        /// <param name="defaultRotation">Default rotation of the direciton signs.</param>
        private void ApplyNESWRotations(float defaultRotation)
        {
            if (minimapManager.minimapCamera != null)
            {
                minimapDirectionsRect.localRotation = Quaternion.Euler(0F, 0F, minimapManager.minimapCamera.transform.eulerAngles.y + defaultRotation);

                northRect.localRotation = Quaternion.Euler(0F, 0F, (minimapManager.minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
                eastRect.localRotation = Quaternion.Euler(0F, 0F, (minimapManager.minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
                southRect.localRotation = Quaternion.Euler(0F, 0F, (minimapManager.minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
                westRect.localRotation = Quaternion.Euler(0F, 0F, (minimapManager.minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
            }
        }

        /// <summary>
        /// Applies the font of the minimap direction signs.
        /// </summary>
        /// <param name="font">New font of the minimap direction signs.</param>
        private void ApplyNESWFont(Font font)
        {
            if (font != null)
            {
                if (minimapManager.directionsTextFont != null)
                {
                    northText.font = font;
                    eastText.font = font;
                    southText.font = font;
                    westText.font = font;
                }
            }
        }

        /// <summary>
        /// Applies the font size of the North, East, South and West signs.
        /// </summary>
        /// <param name="fontSize">Font size of the direction signs.</param>
        private void ApplyNESWFontSize(int fontSize)
        {
            northText.fontSize = fontSize;
            eastText.fontSize = fontSize;
            southText.fontSize = fontSize;
            westText.fontSize = fontSize;
        }

        /// <summary>
        /// Applies the font color of the direction signs.
        /// </summary>
        /// <param name="color">New font color of the directions.</param>
        private void ApplyNESWFontColor(Color color)
        {
            if (color != null)
            {
                northText.color = color;
                eastText.color = color;
                southText.color = color;
                westText.color = color;
            }
        }

        /// <summary>
        /// Applies the background image size of the minimap directions.
        /// </summary>
        /// <param name="size">New size of the directions background images.</param>
        private void ApplyNESWBackgroundSize(Vector2 size)
        {
            northRect.sizeDelta = size;
            eastRect.sizeDelta = size;
            southRect.sizeDelta = size;
            westRect.sizeDelta = size;
        }

        /// <summary>
        /// Applies the background images of the minimap directions.
        /// </summary>
        /// <param name="image">The new background image of the minimap directions.</param>
        /// <param name="color">The new background color of the minimap directions.</param>
        private void ApplyNESWBackgrounds(Sprite image, Color color)
        {
            if (image != null && color != null)
            {
                northImage.sprite = image;
                northImage.color = color;
                eastImage.sprite = image;
                eastImage.color = color;
                southImage.sprite = image;
                southImage.color = color;
                westImage.sprite = image;
                westImage.color = color;
            }
        }

        /// <summary>
        /// Enables or disables the North, East, South and West directions.
        /// </summary>
        /// <param name="displayNorth">If true, the North sign will be displayed.</param>
        /// <param name="displayEast">If true, the East sign will be displayed.</param>
        /// <param name="displaySouth">If true, the South sign will be displayed.</param>
        /// <param name="displayWest">If true, the West sign will be displayed.</param>
        private void EnableOrDisableNESW(bool displayNorth, bool displayEast, bool displaySouth, bool displayWest)
        {
            northObject.SetActive(displayNorth);
            eastObject.SetActive(displayEast);
            southObject.SetActive(displaySouth);
            westObject.SetActive(displayWest);
        }

        /// <summary>
        /// Applies the minimap border sprite.
        /// </summary>
        /// <param name="border">New sprite of the minimap border.</param>
        private void ApplyBorderSprite(Sprite border)
        {
            if (border != null)
            {
                borderImage.sprite = border;
            }
        }

        /// <summary>
        /// Applies the color of the minimap border.
        /// </summary>
        /// <param name="color"></param>
        private void ApplyBorderColor(Color color)
        {
            if (color != null)
            {
                borderImage.color = color;
            }
        }

        /// <summary>
        /// Applies the default rotation of the minimap border.
        /// </summary>
        /// <param name="rotation"></param>
        private void ApplyBorderRotation(float rotation)
        {
            borderRect.rotation = Quaternion.Euler(0F, 0F, rotation);
        }

        /// <summary>
        /// Applies the color of the zoom in button.
        /// </summary>
        /// <param name="color">New color of the zoom in button.</param>
        private void ApplyZoomInButtonColor(Color color)
        {
            if (zoomInImage != null)
            {
                zoomInImage.color = color;
            }
        }

        /// <summary>
        /// Applies the color of the zoom out button.
        /// </summary>
        /// <param name="color">New color of the zoom out button.</param>
        private void ApplyZoomOutButtonColor(Color color)
        {
            if (zoomOutImage != null)
            {
                zoomOutImage.color = color;
            }
        }

        /// <summary>
        /// Applies the scale of the zoom in button.
        /// </summary>
        /// <param name="scale">New scale of the zoom in button.</param>
        private void ApplyZoomInButtonScale(Vector2 scale)
        {
            zoomInRect.sizeDelta = scale;
        }

        /// <summary>
        /// Applies the scale of the zoom out button.
        /// </summary>
        /// <param name="size">New scale of the zoom out button.</param>
        private void ApplyZoomOutButtonScale(Vector2 scale)
        {
            zoomOutRect.sizeDelta = scale;
        }

        /// <summary>
        /// Applies the sprite of the zoom in and zoom out buttons.
        /// </summary>
        /// <param name="zoomInSprite">New sprite of the zoom in button.</param>
        /// <param name="zoomOutSprite">New sprite of the zoom out button.</param>
        private void ApplyZoomButtonsSprites(Sprite zoomInSprite, Sprite zoomOutSprite)
        {
            if (zoomInImage != null)
            {
                zoomInImage.sprite = zoomInSprite;
            }

            if (zoomOutImage != null)
            {
                zoomOutImage.sprite = zoomOutSprite;
            }
        }

        /// <summary>
        /// Applies the positions of the zoom buttons.
        /// </summary>
        /// <param name="zoomInPosition">New position of the zoom in button.</param>
        /// <param name="zoomOutPosition">New position of the zoom out button.</param>
        private void ApplyZoomButtonsPositions(Vector2 zoomInPosition, Vector2 zoomOutPosition)
        {
            zoomInRect.anchoredPosition = zoomInPosition;
            zoomOutRect.anchoredPosition = zoomOutPosition;
        }

        /// <summary>
        /// Applies all the values for the minimap grid.
        /// </summary>
        /// <param name="gridEnabled">If this is true, the minimap is going to have a grid. If this is false, the minimap is not going to have a grid.</param>
        /// <param name="gridSprite">The sprite of the Minimap Grid.</param>
        /// <param name="gridColor">The color of the Minimap Grid.</param>
        /// <param name="gridOpacity">The opacity of the Minimap Grid.</param>
        /// <param name="gridScale">The scale of the Minimap Grid.</param>
        /// <param name="gridRotation">The default rotation of the Minimap Grid.</param>
        private void ApplyMinimapGrid(bool gridEnabled, Sprite gridSprite, Color gridColor, float gridOpacity, Vector3 gridScale, float gridRotation)
        {
            gridObject.SetActive(gridEnabled);
            gridImage.sprite = gridSprite;
            gridImage.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            gridRect.localScale = gridScale;
            gridRect.localRotation = Quaternion.Euler(0F, 0F, gridRotation);
        }

        /// <summary>
        /// Applies the rotation of the minimap camera.
        /// </summary>
        private void ApplyMinimapCameraRotation()
        {
            if (minimapManager.rotateWithTarget)
            {
                cameraTransform.rotation = Quaternion.Euler(90F, minimapManager.targetObject.transform.eulerAngles.y + minimapManager.defaultRotation, 0F);
            }
            else
            {
                cameraTransform.rotation = Quaternion.Euler(90F, minimapManager.defaultRotation, 0F);
            }

            if (minimapManager.rotateWithDisplay)
            {
                borderRect.localRotation = Quaternion.Euler(0F, 0F, minimapManager.minimapCamera.transform.eulerAngles.y + minimapManager.borderRotation);
            }
        }

        /// <summary>
        /// Applies the height and the range of the minimap camera.
        /// </summary>
        /// <param name="height">New height of the minimap camera.</param>
        /// <param name="range">New range of the minimap camera.</param>
        private void ApplyCameraHeightAndRange(float height, float range)
        {
            if (minimapCamera != null)
            {
                if (minimapManager.targetObject != null)
                {
                    cameraTransform.position = new Vector3(minimapManager.targetObject.transform.position.x, height, minimapManager.targetObject.transform.position.z);
                }
                else
                {
                    cameraTransform.position = new Vector3(minimapManager.minimapCamera.transform.position.x, height, minimapManager.minimapCamera.transform.position.z);
                }

                minimapCamera.orthographicSize = range;
            }
        }

        /// <summary>
        /// Applies the near and far clipping planes of the minimap camera.
        /// </summary>
        /// <param name="near">New near clipping plane of the minimap camera.</param>
        /// <param name="far">New far clipping plane of the minimap camera.</param>
        private void ApplyCameraClippingPlanes(float near, float far)
        {
            if (minimapCamera != null)
            {
                minimapCamera.nearClipPlane = near;
                minimapCamera.farClipPlane = far;
            }
        }

        /// <summary>
        /// Applies the clear flags and the background color the minimap camera.
        /// </summary>
        private void ApplyCameraClearFlagsAndColor()
        {
            if (minimapCamera != null)
            {
                minimapCamera.backgroundColor = minimapManager.backgroundColor;

                if (minimapManager.clearFlags == MinimapClearFlags.Skybox)
                {
                    minimapCamera.clearFlags = CameraClearFlags.Skybox;
                }
                else if (minimapManager.clearFlags == MinimapClearFlags.SolidColor)
                {
                    minimapCamera.clearFlags = CameraClearFlags.SolidColor;
                }
                else if (minimapManager.clearFlags == MinimapClearFlags.DepthOnly)
                {
                    minimapCamera.clearFlags = CameraClearFlags.Depth;
                }
                else if (minimapManager.clearFlags == MinimapClearFlags.DontClear)
                {
                    minimapCamera.clearFlags = CameraClearFlags.Nothing;
                }
            }
        }

        /// <summary>
        /// Resets all the values of the Minimap.
        /// </summary>
        private void ResetMinimap()
        {
            Sprite tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Minimap Shapes/Minimap Shape Circle.png");

            if (tempSprite != null)
            {
                minimapManager.minimapShape = tempSprite;
            }

            minimapManager.minimapOpacity = 1F;
            minimapManager.minimapColor = Color.white;

            minimapManager.displayDirections = false;
            minimapManager.directionsDistance = 220F;
            minimapManager.directionsRotation = 0F;
            minimapManager.directionsTextFont = null;
            minimapManager.directionsTextFontSize = 40;
            minimapManager.directionsTextColor = Color.white;

            minimapManager.directionsHaveBackground = false;
            minimapManager.directionsBackgroundImageScale = new Vector2(40F, 40F);
            minimapManager.directionsBackgroundColor = Color.black;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Direction Backgrounds/Directions_Background_1.png");

            if (tempSprite != null)
            {
                minimapManager.directionsBackgroundSprite = tempSprite;
            }

            minimapManager.displayNorth = true;
            minimapManager.displayEast = true;
            minimapManager.displaySouth = true;
            minimapManager.displayWest = true;

            minimapManager.haveBorder = false;
            minimapManager.borderColor = Color.white;
            minimapManager.borderRotation = 0F;
            minimapManager.rotateWithDisplay = false;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Minimap Borders/Minimap Circle Border 1.png");

            if (tempSprite != null)
            {
                minimapManager.borderSprite = tempSprite;
            }

            minimapManager.haveZoomButtons = false;
            minimapManager.zoomingSensitivity = 50F;
            minimapManager.minimumRange = 0F;
            minimapManager.maximumRange = 1000F;
            minimapManager.zoomInButtonPosition = new Vector2(-65F, 0F);
            minimapManager.zoomInButtonScale = new Vector2(50F, 50F);
            minimapManager.zoomOutButtonPosition = Vector2.zero;
            minimapManager.zoomOutButtonScale = new Vector2(50F, 50F);
            minimapManager.zoomInButtonColor = Color.white;
            minimapManager.zoomOutButtonColor = Color.white;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Zooming Icons/Zoom In Icon 1.png");

            if (tempSprite != null)
            {
                minimapManager.zoomInButtonSprite = tempSprite;
            }

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Zooming Icons/Zoom Out Icon 1.png");

            if (tempSprite != null)
            {
                minimapManager.zoomOutButtonSprite = tempSprite;
            }

            minimapManager.displayGrid = false;
            minimapManager.gridColor = Color.white;
            minimapManager.gridOpacity = 1F;
            minimapManager.gridScale = Vector3.one;
            minimapManager.gridRotation = 0F;
            minimapManager.gridRotateWithCamera = false;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Grids/Grid_1.png");

            if (tempSprite != null)
            {
                minimapManager.gridSprite = tempSprite;
            }

            minimapManager.targetObject = null;
            minimapManager.rotateWithTarget = false;
            minimapManager.defaultRotation = 0F;
            
            minimapManager.minimapHeight = 180F;
            minimapManager.nearClippingPlane = 0.3F;
            minimapManager.farClippingPlane = 1000F;
            minimapManager.minimapRange = 120F;
            minimapManager.clearFlags = MinimapClearFlags.Skybox;
            minimapManager.backgroundColor = Color.white;
        }
    }
}
