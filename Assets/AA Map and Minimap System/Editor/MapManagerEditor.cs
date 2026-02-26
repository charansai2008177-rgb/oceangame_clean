// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace AAMAP
{
    [CustomEditor(typeof(MapManager))]
    public class MapManagerEditor : Editor
    {
        [Tooltip("Map Manager component on the \"Map\" GameObject.")] private MapManager mapManager = null;
        [Tooltip("Rect Transform component on the \"Map\" GameObject.")] private RectTransform mapRectTransform = null;

        [Tooltip("Image component on the \"Map > Map Mask\" GameObject.")] private Image maskImage = null;
        [Tooltip("Raw Image component on the \"Map > Mask Mask > Map Display\" GameObject.")] private RawImage displayImage;
        [Tooltip("Image component on the \"Map > Map Mask > Map Background Filler\" GameObject.")] private Image backgroundFillerImage = null;

        [Tooltip("\"Map > Map Mask > Map Background\" GameObject.")] private GameObject backgroundObject = null;
        [Tooltip("Image component on the \"Map > Map Mask > Map Background\" GameObject.")] private Image backgroundImage = null;

        [Tooltip("Raw Image component on the \"Map > Map Mask > Map Display\" GameObject.")] private RawImage mapDisplayImage = null;

        [Tooltip("\"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private GameObject gridObject = null;
        [Tooltip("Image component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private Image gridImage = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private RectTransform gridRect = null;

        [Tooltip("\"Map > Map Border\" GameObject.")] private GameObject mapBorderObject = null;
        [Tooltip("Image component on the \"Map > Map Border\" GameObject.")] private Image borderImage = null;
        [Tooltip("Rect Transform component on the \"Map > Map Border\" GameObject.")] private RectTransform borderRect = null;

        [Tooltip("\"Map > Map Directions\" GameObject.")] private GameObject mapDirectionsObject = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions\" GameObject.")] private RectTransform mapDirectionsRect = null;
        [Tooltip("\"Map > Map Directions > North\" GameObject.")] private GameObject northObject = null;
        [Tooltip("\"Map > Map Directions > East\" GameObject.")] private GameObject eastObject = null;
        [Tooltip("\"Map > Map Directions > South\" GameObject.")] private GameObject southObject = null;
        [Tooltip("\"Map > Map Directions > West\" GameObject.")] private GameObject westObject = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > North\" GameObject.")] private RectTransform northRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > East\" GameObject.")] private RectTransform eastRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > South\" GameObject.")] private RectTransform southRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > West\" GameObject.")] private RectTransform westRect = null;
        [Tooltip("Image component on the \"Map > Map Directions > North\" GameObject.")] private Image northImage = null;
        [Tooltip("Image component on the \"Map > Map Directions > East\" GameObject.")] private Image eastImage = null;
        [Tooltip("Image component on the \"Map > Map Directions > South\" GameObject.")] private Image southImage = null;
        [Tooltip("Image component on the \"Map > Map Directions > West\" GameObject.")] private Image westImage = null;
        [Tooltip("Text component on the \"Map > Map Directions > North\" GameObject.")] private Text northText = null;
        [Tooltip("Text component on the \"Map > Map Directions > East\" GameObject.")] private Text eastText = null;
        [Tooltip("Text component on the \"Map > Map Directions > South\" GameObject.")] private Text southText = null;
        [Tooltip("Text component on the \"Map > Map Directions > West\" GameObject.")] private Text westText = null;

        [Tooltip("\"Map > Map Zoom Buttons\" GameObject.")] private GameObject mapZoomButtons = null;
        [Tooltip("\"Map > Map Zoom Buttons > Zoom In Button\" GameObject.")] private GameObject zoomInButton = null;
        [Tooltip("\"Map > Map Zoom Buttons > Zoom Out Button\" GameObject.")] private GameObject zoomOutButton = null;
        [Tooltip("Image component on the \"Map > Map Zoom Buttons > Zoom In Button\" GameObject.")] private Image zoomInImage = null;
        [Tooltip("Image component on the \"Map > Map Zoom Buttons > Zoom Out Button\" GameObject.")] private Image zoomOutImage = null;
        [Tooltip("Rect Transform component on the \"Map > Map Zoom Buttons > Zoom In Button\" GameObject.")] private RectTransform zoomInRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Zoom Buttons > Zoom Out Button\" GameObject.")] private RectTransform zoomOutRect = null;

        [Tooltip("\"Map > Map Exit Button\" GameObject.")] private GameObject mapExitButtonObject = null;
        [Tooltip("Image component on the \"Map Exit Button\" GameObject.")] private Image exitButtonImage = null;
        [Tooltip("Rect Transform component on the \"Map Exit Button\" GameObject.")] private RectTransform exitButtonRect = null;

        [Tooltip("Camera component on the \"Map Camera\" GameObject.")] private Camera mapCameraComp = null;
        [Tooltip("Transform component on the \"Map Camera\" GameObject.")] private Transform cameraTransform = null;

        [Tooltip("This Rect is used to add Tooltips to the inspector fields.")] private Rect typeRect;
        [Tooltip("This GUI Content is used to add Tooltips to the inspector fields.")] private GUIContent gUIContent;
        [Tooltip("This is the tab horizontal space distance on the sub-fields in the inspector.")] private readonly float spaceDistance = 20F;

        private readonly string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";
        private readonly string errorMessageSuffix = " Please delete and re-create the map.\n";

        /// <summary>
        /// Creates the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            GetGameObjectsAndComponents();

            EditorGUILayout.LabelField("Enable or Disable the Map", EditorStyles.boldLabel);

            mapManager.mapEnabled = EditorGUILayout.Toggle("Enabled", mapManager.mapEnabled);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, the map will be active.\n\nMethods:\nIsMapEnabled( );\nEnableMap( );\nDisableMap( );");
            GUI.Label(typeRect, gUIContent);

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Shortcuts", EditorStyles.boldLabel);

            mapManager.enablingShortcut = (KeyCode)EditorGUILayout.EnumPopup("Enabling Shortcut", mapManager.enablingShortcut);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Shortcut button to enable the map during runtime.\n\nMethods:\nGetEnablingShortcut( );\nSetEnablingShortcut(...);");
            GUI.Label(typeRect, gUIContent);

            mapManager.disablingShortcut = (KeyCode)EditorGUILayout.EnumPopup("Disabling Shortcut", mapManager.disablingShortcut);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Shortcut button to disable the map during runtime.\n\nMethods:\nGetDisablingShortcut( );\nSetDisablingShortcut(...);");
            GUI.Label(typeRect, gUIContent);

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Inner Display", EditorStyles.boldLabel);

            mapManager.mapShape = (Sprite)EditorGUILayout.ObjectField("Shape", mapManager.mapShape, typeof(Sprite), true, GUILayout.Height(EditorGUIUtility.singleLineHeight), GUILayout.ExpandWidth(true));
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Shape of the map inner display.\n\nMethods:\nGetMapShape( );\nSetMapShape(...);");
            GUI.Label(typeRect, gUIContent);
            
            mapManager.mapOpacity = EditorGUILayout.Slider("Opacity", mapManager.mapOpacity, 0F, 1F);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Opacity of the map inner display. Ranged from 0 (inclusive) to 1 (inclusive).\n\nMethods:\nGetMapOpacity( );\nSetMapOpacity(...);");
            GUI.Label(typeRect, gUIContent);

            mapManager.mapColor = EditorGUILayout.ColorField("Color", mapManager.mapColor);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "Color of the map inner display.\n\nMethods:\nGetMapColor( );\nSetMapColor(...);");
            GUI.Label(typeRect, gUIContent);

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Directions", EditorStyles.boldLabel);

            mapManager.displayDirections = EditorGUILayout.Toggle("Display Directions", mapManager.displayDirections);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, \"North, East, South and West\" directions will be displayed on the map.\n\nMethods:\nAreDirectionsEnabled( );\nEnableDirections( );\nDisableDirections( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.displayDirections)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsPosition = EditorGUILayout.Vector2Field("Position", mapManager.directionsPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Position of the map directions.\n\nMethods:\nGetDirectionsPosition( );\nSetDirectionsPosition(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsDistance = EditorGUILayout.FloatField("Distance", mapManager.directionsDistance);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Distance from the center of the directions to the positions of the direction signs.\n\nMethods:\nGetDirectionsDistance( );\nSetDirectionsDistance(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsRotation = EditorGUILayout.Slider("Rotation", mapManager.directionsRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Rotation value of the direction signs.\n\nMethods:\nGetDirectionsRotation( );\nSetDirectionsRotation(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsTextFont = (Font)EditorGUILayout.ObjectField("Font", mapManager.directionsTextFont, typeof(Font), true);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Text font of the direction signs.\n\nGetDirectionsFont( );\nSetDirectionsFont(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsTextFontSize = EditorGUILayout.IntField("Font Size", mapManager.directionsTextFontSize);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Text font size of the direction signs.\n\nMethods:\nGetDirectionsFontSize( );\nSetDirectionsFontSize(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsTextColor = EditorGUILayout.ColorField("Font Color", mapManager.directionsTextColor);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Text color of the direction signs.\n\nMethods:\nGetDirectionsTextColor( );\nSetDirectionsTextColor(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.directionsHaveBackground = EditorGUILayout.Toggle("Background Images", mapManager.directionsHaveBackground);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the directions are going to have background images.\n\nDoesDirectionsHaveBackgroundImages( );\nEnableDirectionsBackgroundImages( );\nDisableDirectionsBackgroundImages( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                if (mapManager.directionsHaveBackground)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance * 2);
                    mapManager.directionsBackgroundImageScale = EditorGUILayout.Vector2Field("Scale", mapManager.directionsBackgroundImageScale);
                    typeRect = GUILayoutUtility.GetLastRect();
                    gUIContent = new GUIContent("", "Background image scales of the direction signs.\n\nMethods:\nGetDirectionsBackgroundScale( );\nSetDirectionsBackgroundScale(...);");
                    GUI.Label(typeRect, gUIContent);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance * 2);
                    EditorGUILayout.PrefixLabel("Sprite");
                    typeRect = GUILayoutUtility.GetLastRect();
                    gUIContent = new GUIContent("", "Background image sprites of the direction signs.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Direction Backgrounds\".\n\nMethods:\nGetDirectionsBackgroundSprites( );\nSetDirectionsBackgroundSprites(...);");
                    GUI.Label(typeRect, gUIContent);
                    mapManager.directionsBackgroundSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.directionsBackgroundSprite, typeof(Sprite), true);
                    typeRect = GUILayoutUtility.GetLastRect();
                    GUI.Label(typeRect, gUIContent);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance * 2);
                    mapManager.directionsBackgroundColor = EditorGUILayout.ColorField("Color", mapManager.directionsBackgroundColor);
                    typeRect = GUILayoutUtility.GetLastRect();
                    gUIContent = new GUIContent("", "Background image color of the direction signs.\n\nMethods:\nGetDirectionsBackgroundColor( );\nSetDirectionsBackgroundColor(...);");
                    GUI.Label(typeRect, gUIContent);
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.displayNorth = EditorGUILayout.Toggle("Display North", mapManager.displayNorth);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the North direction sign will be displayed on the map.\n\nMethods:\nIsNorthEnabled( );\nEnableNorthSign( );\nDisableNorthSign( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.displayEast = EditorGUILayout.Toggle("Display East", mapManager.displayEast);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the East direction sign will be displayed on the minimap.\n\nMethods:\nIsEastEnabled( );\nEnableEastSign( );\nDisableEastSign( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.displaySouth = EditorGUILayout.Toggle("Display South", mapManager.displaySouth);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the South direction sign will be displayed on the minimap.\n\nMethods:\nIsSouthEnabled( );\nEnableSouthSign( );\nDisableSouthSign( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.displayWest = EditorGUILayout.Toggle("Display West", mapManager.displayWest);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "If this is true, the West direction sign will be displayed on the minimap.\n\nMethods:\nIsWestEnabled( );\nEnableWestSign( );\nDisableWestSign( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Border", EditorStyles.boldLabel);

            mapManager.haveBorder = EditorGUILayout.Toggle("Have Border", mapManager.haveBorder);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, the map is going to have a border.\n\nMethods:\nIsBorderEnabled( );\nEnableBorder( );\nDisableBorder( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.haveBorder)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Sprite of the map border.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Map Borders\".\n\nMethods:\nGetBorderSprite( );\nSetBorderSprite(...);");
                GUI.Label(typeRect, gUIContent);
                mapManager.borderSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.borderSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.borderColor = EditorGUILayout.ColorField("Color", mapManager.borderColor);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Color of the map border.\n\nMethods:\nGetBorderColor( );\nSetBorderColor(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.borderRotation = EditorGUILayout.Slider("Rotation", mapManager.borderRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Rotation value of the map border.\n\nMethods:\nGetBorderRotation( );\nSetBorderRotation(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Zoom In and Zoom Out Buttons", EditorStyles.boldLabel);

            mapManager.haveZoomButtons = EditorGUILayout.Toggle("Have Zoom Buttons", mapManager.haveZoomButtons);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, the map is going to have zoom in and zoom out buttons on it.\n\nMethods:\nAreZoomButtonsEnabled( );\nEnableZoomButtons( );\nDisableZoomButtons( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.haveZoomButtons)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomingSensitivity = EditorGUILayout.FloatField("Zooming Sensitivity", mapManager.zoomingSensitivity);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "The zooming sensitivity is the strength of the zoom in and zoom out actions.\n\nMethods:\nGetZoomingSensitivity( );\nSetZoomingSensitivity(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.minimumRange = EditorGUILayout.FloatField("Minimum Range", mapManager.minimumRange);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Minimum range the player can get while zooming in.\n\nMethods:\nGetMinimumRange( );\nSetMinimumRange(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.maximumRange = EditorGUILayout.FloatField("Maximum Range", mapManager.maximumRange);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Maximum range the player can get while zooming out.\n\nMethods:\nGetMaximumRange( );\nSetMaximumRange( );");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Zoom In Button Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Sprite of the zoom in button.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".\n\nMethods:\nGetZoomInButtonSprite( );\nSetZoomInButtonSprite(...);");
                GUI.Label(typeRect, gUIContent);
                mapManager.zoomInButtonSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.zoomInButtonSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Zoom Out Button Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Sprite of the zoom out button.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".\n\nMethods:\nGetZoomOutButtonSprite( );\nSetZoomOutButtonSprite(...);");
                GUI.Label(typeRect, gUIContent);
                mapManager.zoomOutButtonSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.zoomOutButtonSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomInButtonPosition = EditorGUILayout.Vector2Field("Zoom In Button Position", mapManager.zoomInButtonPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Position of the zoom in button.\n\nMethods:\nGetZoomInButtonPosition( );\nSetZoomInButtonPosition(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomInButtonScale = EditorGUILayout.Vector2Field("Zoom In Button Scale", mapManager.zoomInButtonScale);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Scale of the zoom in button.\n\nMethods:\nGetZoomInButtonScale( );\nSetZoomInButtonScale(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomOutButtonPosition = EditorGUILayout.Vector2Field("Zoom Out Button Position", mapManager.zoomOutButtonPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Position of the zoom out button.\n\nMethods:\nGetZoomOutButtonPosition( );\nSetZoomOutButtonPosition(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomOutButtonScale = EditorGUILayout.Vector2Field("Zoom Out Button Scale", mapManager.zoomOutButtonScale);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Scale of the zoom out button.\n\nMethods:\nGetZoomOutButtonScale( );\nSetZoomOutButtonScale(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomInButtonColor = EditorGUILayout.ColorField("Zoom In Button Color", mapManager.zoomInButtonColor);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Color of the zoom in button.\n\nMethods:\nGetZoomInButtonColor( );\nSetZoomInButtonColor(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.zoomOutButtonColor = EditorGUILayout.ColorField("Zoom Out Button Color", mapManager.zoomOutButtonColor);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Color of the zoom out button.\n\nMethods:\nGetZoomOutButtonColor( );\nSetZoomOutButtonColor(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Grid", EditorStyles.boldLabel);

            mapManager.displayGrid = EditorGUILayout.Toggle("Display Grid", mapManager.displayGrid);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, a grid will be displayed on the Map.\n\nMethods:\nIsGridEnabled( );\nEnableGrid( );\nDisableGrid( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.displayGrid)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Sprite of the map grid.\n\nYou can find these sprites at \"Assets > AA Map and Minimap System > Sprites > Grids\".\n\nMethods:\nGetGridSprite( );\nSetGridSprite(...);");
                GUI.Label(typeRect, gUIContent);
                mapManager.gridSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.gridSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.gridColor = EditorGUILayout.ColorField("Color", mapManager.gridColor);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Color of the map grid.\n\nMethods:\nGetGridColor( );\nSetGridColor(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.gridOpacity = EditorGUILayout.Slider("Opacity", mapManager.gridOpacity, 0F, 1F);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Opacity of the map grid.\n\nMethods:\nGetGridOpacity( );\nSetGridOpacity(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.gridScale = EditorGUILayout.Vector3Field("Scale", mapManager.gridScale);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Scale of the map grid.\n\nMethods:\nGetGridScale( );\nSetGridScale(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.gridRotation = EditorGUILayout.Slider("Rotation", mapManager.gridRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Rotation value of the map grid.\n\nMethods:\nGetGridRotation( );\nSetGridRotation(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Camera", EditorStyles.boldLabel);

            mapManager.mapCamera = (GameObject)EditorGUILayout.ObjectField("Map Camera", mapManager.mapCamera, typeof(GameObject), true);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "\"Map Camera\" GameObject.\n\nMethods:\nGetMapCamera( );\nSetMapCamera(...);");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.mapCamera != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.cameraPosition = EditorGUILayout.Vector3Field("Position", mapManager.cameraPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "World position of the map camera.\n\nMethods:\nGetCameraPosition( );\nSetCameraPosition(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.cameraRotation = EditorGUILayout.Slider("Rotation", mapManager.cameraRotation, -360F, 360F);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Rotation value of the map camera on the Y axis.\n\nMethods:\nGetMapCameraRotation( );\nSetMapCameraRotation(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.cameraOrthographicSize = EditorGUILayout.FloatField("Orthographic Size", mapManager.cameraOrthographicSize);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Orthographic size of the map camera. Increase this value to display larger parts on the map.\n\nMethods:\nGetCameraOrthograpicSize( );\nSetCameraOrthograpicSize(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.nearClippingPlane = EditorGUILayout.FloatField("Near Clipping Plane", mapManager.nearClippingPlane);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "The closest point to the Map Camera where drawing occurs.\n\nMethods:\nGetCameraNearClippingPlane( );\nSetCameraNearClippingPlane(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.farClippingPlane = EditorGUILayout.FloatField("Far Clipping Plane", mapManager.farClippingPlane);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "The furthest point from the Map Camera that drawing occurs.\n\nMethods:\nGetCameraFarClippingPlane( );\nSetCameraFarClippingPlane(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.clearFlags = (MapClearFlags)EditorGUILayout.EnumPopup("Clear Flags", mapManager.clearFlags);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "What to display in empty areas of the map camera's view.\n\nChoose Skybox to display a skybox in the empty areas, defaulting to a background color if no skybox is found.\n\nChoose Solid Color to display a solid background color in empty areas.\n\nChoose Depth Only to display nothing in empty areas.\n\nChoose Don't Clear to display whatever was displayed in the previous frame in empty areas.\n\nMethods:\nGetClearFlags( );\nSetClearFlags(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                if (mapManager.clearFlags != MapClearFlags.DontClear)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(spaceDistance);
                    mapManager.cameraBGColor = EditorGUILayout.ColorField("Background Color", mapManager.cameraBGColor);
                    typeRect = GUILayoutUtility.GetLastRect();
                    gUIContent = new GUIContent("", "The map camera clears the screen to this color before rendering.\n\nMethods:\nGetCameraBackgroundColor( );\nSetCameraBackgroundColor(...);");
                    GUI.Label(typeRect, gUIContent);
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Map Background Image", EditorStyles.boldLabel);

            mapManager.haveBackgroundImage = EditorGUILayout.Toggle("Have Background Image", mapManager.haveBackgroundImage);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, the map is going to have a background image.\n\nMethods:\nIsBackgroundImageEnabled( );\nEnableBackgroundImage( );\nDisableBackgroundImage( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.haveBackgroundImage)
            {
                backgroundObject.SetActive(true);

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                gUIContent = new GUIContent("", "Sprite of the map background image.\n\nYou can locate these images at \"Assets > AA Map and Minimap System > Sprites > Map Backgrounds\".\n\nMethods:\nGetBackgroundImageSprite( );\nSetBackgroundImageSprite( );");
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                mapManager.backgroundImageSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.backgroundImageSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.backgroundImageColor = EditorGUILayout.ColorField("Color", mapManager.backgroundImageColor);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "Color of the map background image.\n\nMethods:\nGetBackgroundImageColor( );\nSetBackgroundImageColor(...);");
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Exit Button", EditorStyles.boldLabel);

            mapManager.haveExitButton = EditorGUILayout.Toggle("Have Exit Button", mapManager.haveExitButton);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, the map is going to have an exit button.\n\nMethods:\nIsExitButtonEnabled( );\nEnableExitButton( );\nDisableExitButton( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.haveExitButton)
            {
                gUIContent = new GUIContent("", "Sprite for the map exit button.\n\nYou can locate these sprites at \"Asset > AA Map and Minimap System > Sprites > Map Exit Buttons\".\n\nMethods:\nGetExitButtonSprite( );\nSetExitButtonSprite(...);");
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                EditorGUILayout.PrefixLabel("Sprite");
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                mapManager.exitButtonSprite = (Sprite)EditorGUILayout.ObjectField(mapManager.exitButtonSprite, typeof(Sprite), true);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                gUIContent = new GUIContent("", "Color of the map exit button.\n\nMethods:\nGetExitButtonColor( );\nSetExitButtonColor(...);");
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.exitButtonColor = EditorGUILayout.ColorField("Color", mapManager.exitButtonColor);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                gUIContent = new GUIContent("", "Position of the map exit button.\n\nMethods:\nGetExitButtonPosition( );\nSetExitButtonPosition(...);");
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.exitButtonPosition = EditorGUILayout.Vector2Field("Position", mapManager.exitButtonPosition);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();

                gUIContent = new GUIContent("", "Scale of the map exit button.\n\nMethods:\nGetExitButtonScale( );\nSetExitButtonScale(...);");
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);
                mapManager.exitButtonScale = EditorGUILayout.Vector2Field("Scale", mapManager.exitButtonScale);
                typeRect = GUILayoutUtility.GetLastRect();
                GUI.Label(typeRect, gUIContent);
                GUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(20F);

            EditorGUILayout.LabelField("Disable The Minimap When The Map Is Enabled", EditorStyles.boldLabel);

            mapManager.disableMinimap = EditorGUILayout.Toggle("Disable Minimap", mapManager.disableMinimap);
            typeRect = GUILayoutUtility.GetLastRect();
            gUIContent = new GUIContent("", "If this is true, the minimap will be disabled when the map is enabled.\n\nIf this is false, the minimap will NOT be disabled when the map is enabled.\n\nMethods:\nDoesMapDisablesMinimap( );\nDisableMinimap( );\nDontDisableMinimap( );");
            GUI.Label(typeRect, gUIContent);

            if (mapManager.disableMinimap)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(spaceDistance);

                mapManager.minimapGameObject = (GameObject)EditorGUILayout.ObjectField("Minimap", mapManager.minimapGameObject, typeof(GameObject), true);
                typeRect = GUILayoutUtility.GetLastRect();
                gUIContent = new GUIContent("", "\"Minimap\" GameObject on the Canvas.\n\nMethods:\nGetMinimap( );\nSetMinimap(...);");
                GUI.Label(typeRect, gUIContent);

                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20F);

            if (GUILayout.Button("Delete Map"))
            {
                if (mapManager.mapCamera.GetComponent<Camera>().targetTexture != null)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(mapManager.mapCamera.GetComponent<Camera>().targetTexture));
                }

                GameObject.DestroyImmediate(mapManager.mapCamera);
                GameObject.DestroyImmediate(mapManager.gameObject);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Reset Map"))
            {
                ResetMap();
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (mapManager == null)
                    return;

                EnableOrDisableMap();
                ApplyMapShape();
                ApplyOpacity();
                ApplyMapColor();

                if (mapManager == null)
                    return;

                if (mapManager.displayDirections)
                {
                    if (mapManager == null || mapDirectionsObject == null)
                        return;

                    ApplyDirections(mapManager.displayDirections, mapManager.directionsPosition, mapManager.directionsDistance, mapManager.directionsRotation, mapManager.directionsTextFont, mapManager.directionsTextFontSize, mapManager.directionsTextColor, mapManager.displayNorth, mapManager.displayEast, mapManager.displaySouth, mapManager.displayWest);
                    ApplyDirectionsBackgroundSize(mapManager.directionsBackgroundImageScale);
                    ApplyDirectionsBackground(mapManager.directionsBackgroundSprite, mapManager.directionsBackgroundColor);
                }
                else
                {
                    if (mapDirectionsObject == null)
                        return;

                    if (mapDirectionsObject != null)
                    {
                        mapDirectionsObject.SetActive(false);
                    }
                }

                if (mapManager == null)
                    return;

                if (mapManager.haveBorder)
                {
                    if (mapBorderObject == null)
                        return;

                    mapBorderObject.SetActive(true);

                    ApplyBorder();
                }
                else
                {
                    if (mapBorderObject == null)
                        return;

                    mapBorderObject.SetActive(false);
                }

                if (mapManager == null)
                    return;

                if (mapManager.haveZoomButtons)
                {
                    ApplyZoomButtons(mapManager.zoomInButtonColor, mapManager.zoomOutButtonColor, mapManager.zoomInButtonScale, mapManager.zoomOutButtonScale, mapManager.zoomInButtonSprite, mapManager.zoomOutButtonSprite, mapManager.zoomInButtonPosition, mapManager.zoomOutButtonPosition);
                }
                else
                {
                    if (mapZoomButtons == null)
                        return;

                    mapZoomButtons.SetActive(false);
                }

                if (mapManager == null)
                    return;

                if (mapManager.displayGrid)
                {
                    if (gridObject == null)
                        return;

                    gridObject.SetActive(true);
                    ApplyMapGrid(mapManager.displayGrid, mapManager.gridSprite, mapManager.gridColor, mapManager.gridOpacity, mapManager.gridScale, mapManager.gridRotation);
                }
                else
                {
                    if (gridObject == null)
                        return;

                    gridObject.SetActive(false);
                }

                if (mapManager == null)
                    return;

                if (mapManager.mapCamera == null)
                    return;

                if (mapManager.mapCamera != null)
                {
                    if (cameraTransform == null)
                        return;

                    cameraTransform.rotation = Quaternion.Euler(90F, mapManager.cameraRotation, 0F);
                    ApplyClearFlags(mapManager.clearFlags);
                    ApplyCameraBackgroundColor(mapManager.cameraBGColor);
                }

                if (mapManager == null)
                    return;

                if (mapManager.mapCamera == null)
                    return;

                if (mapManager.mapCamera != null)
                {
                    ApplyCameraValues();
                }

                if (mapManager.haveBackgroundImage)
                {
                    if (backgroundObject == null)
                        return;

                    backgroundObject.SetActive(true);
                    ApplyBackgroundValues();
                }
                else
                {
                    if (backgroundObject == null)
                        return;

                    backgroundObject.SetActive(false);
                }

                if (mapManager == null)
                    return;

                if (mapManager.haveExitButton)
                {
                    if (mapExitButtonObject == null)
                        return;

                    mapExitButtonObject.SetActive(true);
                    ApplyExitButtonValues();
                }
                else
                {
                    if (mapExitButtonObject == null)
                        return;

                    mapExitButtonObject.SetActive(false);
                }

                if (mapManager == null)
                    return;

                if (target != null)
                {
                    Undo.RecordObject(target, "Changed Map Manager");
                }
            }
        }

        /// <summary>
        /// This method gets all the necessary GameObjects and components on the map.
        /// </summary>
        private void GetGameObjectsAndComponents()
        {
            if (mapManager == null)
            {
                mapManager = (MapManager)target;
            }

            if (mapRectTransform == null)
            {
                mapRectTransform = mapManager.transform.GetComponent<RectTransform>();

                if (mapRectTransform == null)
                {
                    Debug.LogError("Rect Transform component on the \"Map\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (maskImage == null)
            {
                for (int i = 0; i < mapManager.transform.childCount; i++)
                {
                    if (mapManager.transform.GetChild(i).name.Equals("Map Mask"))
                    {
                        maskImage = mapManager.transform.GetChild(i).GetComponent<Image>();
                        break;
                    }
                }

                if (maskImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Image component on the \"Map > Map Mask\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (displayImage == null)
            {
                for (int i = 0; i < maskImage.transform.childCount; i++)
                {
                    if (maskImage.transform.GetChild(i).name.Equals("Map Display"))
                    {
                        displayImage = maskImage.transform.GetChild(i).GetComponent<RawImage>();
                        break;
                    }
                }

                if (displayImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Raw Image component on the \"Map > Map Mask > Map Display\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (backgroundFillerImage == null)
            {
                for (int i = 0; i < maskImage.transform.childCount; i++)
                {
                    if (maskImage.transform.GetChild(i).name.Equals("Map Background Filler"))
                    {
                        backgroundFillerImage = maskImage.transform.GetChild(i).GetComponent<Image>();
                        break;
                    }
                }

                if (backgroundFillerImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Image component on the \"Map > Map Mask > Map Background Filler\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (mapDirectionsObject == null)
            {
                for (int i = 0; i < mapManager.transform.childCount; i++)
                {
                    if (mapManager.transform.GetChild(i).name.Equals("Map Directions"))
                    {
                        mapDirectionsObject = mapManager.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                if (mapDirectionsObject == null)
                {
                    Debug.LogError("\"Map > Map Directions\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (mapBorderObject == null)
            {
                for (int i = 0; i < mapManager.transform.childCount; i++)
                {
                    if (mapManager.transform.GetChild(i).name.Equals("Map Border"))
                    {
                        mapBorderObject = mapManager.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                if (mapBorderObject == null)
                {
                    Debug.LogWarning("\"Map Border\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (borderImage == null)
            {
                if (mapBorderObject != null)
                {
                    borderImage = mapBorderObject.GetComponent<Image>();

                    if (borderImage == null)
                    {
                        Debug.LogError("Image component on the \"Map > Map Border\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (borderRect == null)
            {
                if (mapBorderObject != null)
                {
                    borderRect = mapBorderObject.GetComponent<RectTransform>();

                    if (borderRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Map > Map Border\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (mapDirectionsRect == null)
            {
                if (mapDirectionsObject != null)
                {
                    mapDirectionsRect = mapDirectionsObject.GetComponent<RectTransform>();

                    if (mapDirectionsRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Map > Map Directions\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (northRect == null || eastRect == null || southRect == null || westRect == null)
            {
                if (mapDirectionsObject != null)
                {
                    for (int i = 0; i < mapDirectionsObject.transform.childCount; i++)
                    {
                        if (mapDirectionsObject.transform.GetChild(i).name.Equals("North"))
                        {
                            northRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            northImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            northText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            northObject = mapDirectionsObject.transform.GetChild(i).gameObject;
                        }

                        if (mapDirectionsObject.transform.GetChild(i).name.Equals("East"))
                        {
                            eastRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            eastImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            eastText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            eastObject = mapDirectionsObject.transform.GetChild(i).gameObject;
                        }

                        if (mapDirectionsObject.transform.GetChild(i).name.Equals("South"))
                        {
                            southRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            southImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            southText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            southObject = mapDirectionsObject.transform.GetChild(i).gameObject;
                        }

                        if (mapDirectionsObject.transform.GetChild(i).name.Equals("West"))
                        {
                            westRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                            westImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                            westText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                            westObject = mapDirectionsObject.transform.GetChild(i).gameObject;
                        }
                    }

                    if (northObject == null)
                    {
                        Debug.LogError("\"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastObject == null)
                    {
                        Debug.LogError("\"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southObject == null)
                    {
                        Debug.LogError("\"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westObject == null)
                    {
                        Debug.LogError("\"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (northRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (northText == null)
                    {
                        Debug.LogError("Text component on the \"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastText == null)
                    {
                        Debug.LogError("Text component on the \"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southText == null)
                    {
                        Debug.LogError("Text component on the \"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westText == null)
                    {
                        Debug.LogError("Text component on the \"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (northImage == null)
                    {
                        Debug.LogError("Image component on the \"Minimap > Minimap Directions > North\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (eastImage == null)
                    {
                        Debug.LogError("Image component on the \"Minimap > Minimap Directions > East\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (southImage == null)
                    {
                        Debug.LogError("Image component on the \"Minimap > Minimap Directions > South\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (westImage == null)
                    {
                        Debug.LogError("Image component on the \"Minimap > Minimap Directions > West\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (mapCameraComp == null && mapManager.mapCamera != null)
            {
                if (mapManager.mapCamera != null)
                {
                    mapCameraComp = mapManager.mapCamera.GetComponent<Camera>();

                    if (mapCameraComp == null)
                    {
                        Debug.LogError("Camera component on the \"Map Camera\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (mapZoomButtons == null)
            {
                for (int i = 0; i < mapManager.transform.childCount; i++)
                {
                    if (mapManager.transform.GetChild(i).name.Equals("Map Zoom Buttons"))
                    {
                        mapZoomButtons = mapManager.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                if (mapBorderObject == null)
                {
                    Debug.LogWarning("\"Map > Map Zoom Buttons\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (zoomInButton == null)
            {
                if (mapZoomButtons != null)
                {
                    for (int i = 0; i < mapZoomButtons.transform.childCount; i++)
                    {
                        if (mapZoomButtons.transform.GetChild(i).name.Equals("Zoom In Button"))
                        {
                            zoomInButton = mapZoomButtons.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (zoomInButton == null)
                    {
                        Debug.LogWarning("\"Map > Map Zoom Buttons > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (zoomOutButton == null)
            {
                if (mapZoomButtons != null)
                {
                    for (int i = 0; i < mapZoomButtons.transform.childCount; i++)
                    {
                        if (mapZoomButtons.transform.GetChild(i).name.Equals("Zoom Out Button"))
                        {
                            zoomOutButton = mapZoomButtons.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (zoomOutButton == null)
                    {
                        Debug.LogWarning("\"Map > Map Zoom Buttons > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
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
                        Debug.LogError("Rect Transform component on the \"Map > Map Zoom Buttons > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (zoomInImage == null)
                    {
                        Debug.LogError("Image component on the \"Map > Map Zoom Buttons > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }

                if (zoomOutButton != null)
                {
                    zoomOutRect = zoomOutButton.GetComponent<RectTransform>();
                    zoomOutImage = zoomOutButton.GetComponent<Image>();

                    if (zoomOutRect == null)
                    {
                        Debug.LogError("Rect Transform component on the \"Map > Map Zoom Buttons > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    }

                    if (zoomOutImage == null)
                    {
                        Debug.LogError("Image component on the \"Map > Map Zoom Buttons > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (cameraTransform == null)
            {
                if (mapCameraComp != null)
                {
                    cameraTransform = mapCameraComp.transform;
                }
            }

            if (backgroundObject == null)
            {
                for (int i = 0; i < mapManager.transform.childCount; i++)
                {
                    if (mapManager.transform.GetChild(i).name.Equals("Map Mask"))
                    {
                        for (int j = 0; j < mapManager.transform.GetChild(i).transform.childCount; j++)
                        {
                            if (mapManager.transform.GetChild(i).transform.GetChild(j).name.Equals("Map Background"))
                            {
                                backgroundObject = mapManager.transform.GetChild(i).transform.GetChild(j).gameObject;
                                break;
                            }
                        }

                        break;
                    }
                }

                if (backgroundObject == null)
                {
                    Debug.LogWarning("\"Map > Map Mask > Map Background\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (backgroundImage == null)
            {
                if (backgroundObject != null)
                {
                    backgroundImage = backgroundObject.GetComponent<Image>();

                    if (backgroundImage == null)
                    {
                        Debug.LogError("Image component on the \"Map > Map Mask > Map Background\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (mapExitButtonObject == null)
            {
                for (int i = 0; i < mapManager.transform.childCount; i++)
                {
                    if (mapManager.transform.GetChild(i).name.Equals("Map Exit Button"))
                    {
                        mapExitButtonObject = mapManager.transform.GetChild(i).gameObject;
                        exitButtonImage = mapExitButtonObject.GetComponent<Image>();
                        exitButtonRect = mapExitButtonObject.GetComponent<RectTransform>();
                        break;
                    }
                }

                if (mapExitButtonObject == null)
                {
                    Debug.LogWarning("\"Map > Map Exit Button\" GameObject could not be found." + errorMessageSuffix);
                }

                if (exitButtonImage == null)
                {
                    Debug.LogWarning("Image component on the\"Map > Map Exit Button\" GameObject could not be found." + errorMessageSuffix);
                }

                if (exitButtonRect == null)
                {
                    Debug.LogWarning("Rect Transform component on the\"Map > Map Exit Button\" GameObject could not be found." + errorMessageSuffix);
                }
            }

            if (mapDisplayImage == null)
            {
                if (maskImage != null)
                {
                    for (int i = 0; i < maskImage.transform.childCount; i++)
                    {
                        if (maskImage.transform.GetChild(i).name.Equals("Map Display"))
                        {
                            mapDisplayImage = maskImage.transform.GetChild(i).GetComponent<RawImage>();
                        }
                    }

                    if (mapDisplayImage == null)
                    {
                        Debug.LogError("Raw Image component on the \"Map > Map Mask > Map Display\" GameObject could not be found." + errorMessageSuffix);
                    }
                }
            }

            if (gridObject == null)
            {
                if (maskImage != null)
                {
                    for (int i = 0; i < maskImage.transform.childCount; i++)
                    {
                        if (maskImage.transform.GetChild(i).name.Equals("Map Grid"))
                        {
                            gridObject = maskImage.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (gridObject == null)
                    {
                        Debug.LogError("\"Map > Map Mask > Map Grid\" GameObject could not be found." + errorMessageSuffix);
                    }
                    else
                    {
                        gridImage = gridObject.GetComponent<Image>();
                        gridRect = gridObject.GetComponent<RectTransform>();

                        if (gridImage == null)
                        {
                            Debug.LogError("Failed to generate the Map because the Image component on the \"Map > Map Mask > Map Grid\" GameObject could not be found." + errorMessageSuffix);
                        }

                        if (gridRect == null)
                        {
                            Debug.LogError("Failed to generate the Map because the Rect Transform component on the \"Map > Map Mask > Map Grid\" GameObject could not be found." + errorMessageSuffix);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enables or disables the map.
        /// </summary>
        private void EnableOrDisableMap()
        {
            if (mapManager.mapEnabled)
            {
                //Enables the map
                if (maskImage != null)
                {
                    maskImage.gameObject.SetActive(true);
                }

                if (mapManager.haveBorder)
                    mapBorderObject.SetActive(true);

                if (mapManager.displayDirections)
                    mapDirectionsObject.SetActive(true);

                if (mapManager.haveZoomButtons)
                    mapZoomButtons.SetActive(true);

                if (mapManager.haveExitButton)
                    mapExitButtonObject.SetActive(true);

                if (mapManager != null && mapCameraComp != null)
                {
                    mapManager.mapCamera.SetActive(true);
                }
            }
            else
            {
                // Disables the map
                maskImage.gameObject.SetActive(false);
                mapBorderObject.SetActive(false);
                mapDirectionsObject.SetActive(false);
                mapZoomButtons.SetActive(false);
                mapExitButtonObject.SetActive(false);
                mapManager.mapCamera.SetActive(false);
            }
        }

        /// <summary>
        /// Applies the shape of the map.
        /// </summary>
        private void ApplyMapShape()
        {
            maskImage.sprite = mapManager.mapShape;
        }

        /// <summary>
        /// Applies the opacity of the map.
        /// </summary>
        private void ApplyOpacity()
        {
            displayImage.color = new Color(mapManager.mapColor.r, mapManager.mapColor.g, mapManager.mapColor.b, mapManager.mapOpacity);
            backgroundFillerImage.color = new Color(0F, 0F, 0F, mapManager.mapOpacity);
        }

        /// <summary>
        /// Applies the color of the map.
        /// </summary>
        private void ApplyMapColor()
        {
            displayImage.color = new Color(mapManager.mapColor.r, mapManager.mapColor.g, mapManager.mapColor.b, mapManager.mapOpacity);
        }

        /// <summary>
        /// Applies the map directions.
        /// </summary>
        /// <param name="displayDirections">If this is true, the map directions will be displayed.</param>
        /// <param name="position">Position of the map directions.</param>
        /// <param name="distance">Distance of the map directions from their centre point.</param>
        /// <param name="rotation">Default rotation value of the map directions.</param>
        /// <param name="font">Font of the map directions.</param>
        /// <param name="fontSize">Font size of the map directions.</param>
        /// <param name="color">Color of the map directions.</param>
        /// <param name="north">Display north.</param>
        /// <param name="east">Display east.</param>
        /// <param name="south">Display south.</param>
        /// <param name="west">Display west.</param>
        private void ApplyDirections(bool displayDirections, Vector2 position, float distance, float rotation, Font font, int fontSize, Color color, bool north, bool east, bool south, bool west)
        {
            if (mapManager.mapEnabled)
            {
                mapDirectionsObject.SetActive(displayDirections);

                if (!displayDirections)
                {
                    return;
                }

                mapDirectionsRect.anchoredPosition = position;

                northRect.anchoredPosition = new Vector3(0F, distance, 0F);
                eastRect.anchoredPosition = new Vector3(distance, 0F, 0F);
                southRect.anchoredPosition = new Vector3(0F, distance * -1F, 0F);
                westRect.anchoredPosition = new Vector3(distance * -1F, 0F, 0F);

                if (cameraTransform != null)
                {
                    mapDirectionsRect.localRotation = Quaternion.Euler(0F, 0F, cameraTransform.eulerAngles.y + rotation);

                    northRect.localRotation = Quaternion.Euler(0F, 0F, (cameraTransform.eulerAngles.y + rotation) * -1F);
                    eastRect.localRotation = Quaternion.Euler(0F, 0F, (cameraTransform.eulerAngles.y + rotation) * -1F);
                    southRect.localRotation = Quaternion.Euler(0F, 0F, (cameraTransform.eulerAngles.y + rotation) * -1F);
                    westRect.localRotation = Quaternion.Euler(0F, 0F, (cameraTransform.eulerAngles.y + rotation) * -1F);
                }

                if (font != null)
                {
                    northText.font = font;
                    eastText.font = font;
                    southText.font = font;
                    westText.font = font;
                }

                northText.fontSize = fontSize;
                eastText.fontSize = fontSize;
                southText.fontSize = fontSize;
                westText.fontSize = fontSize;

                if (color != null)
                {
                    northText.color = color;
                    eastText.color = color;
                    southText.color = color;
                    westText.color = color;
                }

                northObject.SetActive(north);
                eastObject.SetActive(east);
                southObject.SetActive(south);
                westObject.SetActive(west);
            }
            else
            {
                mapDirectionsObject.SetActive(false);
            }
        }

        /// <summary>
        /// Applies the background size of the map directions.
        /// </summary>
        /// <param name="size">New size of the directions backgrounds.</param>
        private void ApplyDirectionsBackgroundSize(Vector2 size)
        {
            northRect.sizeDelta = size;
            eastRect.sizeDelta = size;
            southRect.sizeDelta = size;
            westRect.sizeDelta = size;
        }

        /// <summary>
        /// Applies the sprite and the color of the map directions backgrounds.
        /// </summary>
        /// <param name="backgroundSprite">New background image of the map directions.</param>
        /// <param name="color">New background color of the map directions.</param>
        private void ApplyDirectionsBackground(Sprite backgroundSprite, Color color)
        {
            if (!mapManager.directionsHaveBackground)
            {
                northImage.color = Color.clear;
                eastImage.color = Color.clear;
                southImage.color = Color.clear;
                westImage.color = Color.clear;

                return;
            }

            if (mapManager.directionsBackgroundSprite != null)
            {
                northImage.sprite = backgroundSprite;
                northImage.color = color;
                eastImage.sprite = backgroundSprite;
                eastImage.color = color;
                southImage.sprite = backgroundSprite;
                southImage.color = color;
                westImage.sprite = backgroundSprite;
                westImage.color = color;
            }
        }

        /// <summary>
        /// Applies the values of the map border.
        /// </summary>
        private void ApplyBorder()
        {
            if (mapManager.mapEnabled)
            {
                mapBorderObject.SetActive(mapManager.haveBorder);

                if (mapManager.borderSprite != null)
                {
                    borderImage.sprite = mapManager.borderSprite;
                }

                borderImage.color = mapManager.borderColor;

                borderRect.rotation = Quaternion.Euler(0F, 0F, mapManager.borderRotation);
            }
            else
            {
                mapBorderObject.SetActive(false);
            }
        }

        /// <summary>
        /// Applies the values of the zoom button.
        /// </summary>
        /// <param name="zoomInButtonColor">Color of the zoom in button.</param>
        /// <param name="zoomOutButtonColor">Color of the zoom out button.</param>
        /// <param name="zoomInButtonSize">Size of the zoom in button.</param>
        /// <param name="zoomOutButtonSize">Size of the zoom out button.</param>
        /// <param name="zoomInTexture">Texture of the zoom in button.</param>
        /// <param name="zoomOutTexture">Texture of the zoom out button.</param>
        /// <param name="zoomInPosition">Position of the zoom in button.</param>
        /// <param name="zoomOutPosition">Position of the zoom out button.</param>
        private void ApplyZoomButtons(Color zoomInButtonColor, Color zoomOutButtonColor, Vector2 zoomInButtonSize, Vector2 zoomOutButtonSize, Sprite zoomInTexture, Sprite zoomOutTexture, Vector2 zoomInPosition, Vector2 zoomOutPosition)
        {
            if (mapManager.mapEnabled)
            {
                mapZoomButtons.SetActive(mapManager.haveZoomButtons);

                zoomInImage.color = zoomInButtonColor;
                zoomOutImage.color = zoomOutButtonColor;

                zoomInRect.sizeDelta = zoomInButtonSize;
                zoomOutRect.sizeDelta = zoomOutButtonSize;

                if (zoomInTexture != null)
                {
                    zoomInImage.sprite = zoomInTexture;
                }

                if (zoomOutTexture != null)
                {
                    zoomOutImage.sprite = zoomOutTexture;
                }

                zoomInButton.transform.localPosition = zoomInPosition;

                zoomOutButton.transform.localPosition = zoomOutPosition;
            }
            else
            {
                mapZoomButtons.SetActive(false);
            }
        }

        /// <summary>
        /// Applies all the values for the Map Grid.
        /// </summary>
        /// <param name="gridEnabled">If this is true, the map is going to have a grid. If this is false, the map is not going to have a grid.</param>
        /// <param name="gridSprite">Sprite of the Map Grid.</param>
        /// <param name="gridColor">Color of the Map Grid.</param>
        /// <param name="gridOpacity">Opacity of the Map Grid.</param>
        /// <param name="gridScale">Scale of the Map Grid.</param>
        /// <param name="gridRotation">Default rotation of the Map Grid.</param>
        private void ApplyMapGrid(bool gridEnabled, Sprite gridSprite, Color gridColor, float gridOpacity, Vector3 gridScale, float gridRotation)
        {
            gridObject.SetActive(gridEnabled);
            gridImage.sprite = gridSprite;
            gridImage.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            gridRect.localScale = gridScale;
            gridRect.localRotation = Quaternion.Euler(0F, 0F, gridRotation);
        }

        /// <summary>
        /// Applies the clear flags of the map camera.
        /// </summary>
        private void ApplyClearFlags(MapClearFlags clearFlags)
        {
            if (clearFlags == MapClearFlags.Skybox)
            {
                mapCameraComp.clearFlags = CameraClearFlags.Skybox;
            }
            else if (clearFlags == MapClearFlags.SolidColor)
            {
                mapCameraComp.clearFlags = CameraClearFlags.SolidColor;
            }
            else if (clearFlags == MapClearFlags.DepthOnly)
            {
                mapCameraComp.clearFlags = CameraClearFlags.Depth;
            }
            else if (clearFlags == MapClearFlags.DontClear)
            {
                mapCameraComp.clearFlags = CameraClearFlags.Nothing;
            }
        }

        /// <summary>
        /// Sets the background color of the map camera.
        /// </summary>
        /// <param name="color">New background color of the map camera.</param>
        private void ApplyCameraBackgroundColor(Color color)
        {
            if (color == null)
            {
                mapManager.cameraBGColor = Color.clear;

                if (mapCameraComp != null)
                {
                    mapCameraComp.backgroundColor = Color.clear;
                }
            }
            else
            {
                mapManager.cameraBGColor = color;

                if (mapCameraComp != null)
                {
                    mapCameraComp.backgroundColor = color;
                }

            }
        }

        /// <summary>
        /// Applies the values of the map camera.
        /// </summary>
        private void ApplyCameraValues()
        {
            if (mapManager.mapCamera != null)
            {
                cameraTransform.position = mapManager.cameraPosition;
                mapCameraComp.orthographicSize = mapManager.cameraOrthographicSize;
                mapCameraComp.nearClipPlane = mapManager.nearClippingPlane;
                mapCameraComp.farClipPlane = mapManager.farClippingPlane;
            }
        }

        /// <summary>
        /// Applies the values of the map background.
        /// </summary>
        private void ApplyBackgroundValues()
        {
            if (mapManager.backgroundImageSprite != null)
            {
                backgroundImage.sprite = mapManager.backgroundImageSprite;
            }

            backgroundImage.color = mapManager.backgroundImageColor;

            if (mapManager.haveBackgroundImage && mapCameraComp.backgroundColor.a != 0F)
            {
                mapCameraComp.backgroundColor = new Color(mapCameraComp.backgroundColor.r, mapCameraComp.backgroundColor.g, mapCameraComp.backgroundColor.b, 0F);
            }
        }

        /// <summary>
        /// Applies the values of the map exit button.
        /// </summary>
        private void ApplyExitButtonValues()
        {
            if (mapManager.mapEnabled)
            {
                mapExitButtonObject.SetActive(mapManager.haveExitButton);

                exitButtonImage.sprite = mapManager.exitButtonSprite;
                exitButtonImage.color = mapManager.exitButtonColor;
                exitButtonRect.anchoredPosition = mapManager.exitButtonPosition;
                exitButtonRect.sizeDelta = mapManager.exitButtonScale;
            }
            else
            {
                mapExitButtonObject.SetActive(false);
            }
        }

        /// <summary>
        /// Resets the map.
        /// </summary>
        private void ResetMap()
        {
            mapManager.mapEnabled = true;

            mapManager.enablingShortcut = KeyCode.M;
            mapManager.disablingShortcut = KeyCode.Escape;

            Sprite tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Map Shapes/Map Shape Sharp Rectangle.png");

            if (tempSprite != null)
            {
                mapManager.mapShape = tempSprite;
            }

            mapManager.mapOpacity = 1F;
            mapManager.mapColor = Color.white;

            mapManager.displayDirections = false;
            mapManager.directionsPosition = Vector2.zero;
            mapManager.directionsDistance = 80F;
            mapManager.directionsRotation = 0F;
            mapManager.directionsTextFont = null;
            mapManager.directionsTextFontSize = 40;
            mapManager.directionsTextColor = Color.white;
            mapManager.directionsHaveBackground = false;
            mapManager.directionsBackgroundImageScale = new Vector2(70F, 70F);
            mapManager.directionsBackgroundColor = Color.black;
            mapManager.displayNorth = true;
            mapManager.displayEast = true;
            mapManager.displaySouth = true;
            mapManager.displayWest = true;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Direction Backgrounds/Directions_Background_7.png");

            if (tempSprite != null)
            {
                mapManager.directionsBackgroundSprite = tempSprite;
            }

            mapManager.haveBorder = false;
            mapManager.borderColor = Color.white;
            mapManager.borderRotation = 0F;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Map Borders/Map Sharp Rectangle Border 1.png");

            if (tempSprite != null)
            {
                mapManager.borderSprite = tempSprite;
            }

            mapManager.haveZoomButtons = false;
            mapManager.zoomingSensitivity = 50F;
            mapManager.minimumRange = 150F;
            mapManager.maximumRange = 1000F;
            mapManager.zoomInButtonPosition = new Vector2(-100F, 0F);
            mapManager.zoomInButtonScale = new Vector2(80F, 80F);
            mapManager.zoomOutButtonPosition = Vector2.zero;
            mapManager.zoomOutButtonScale = new Vector2(80F, 80F);
            mapManager.zoomInButtonColor = Color.white;
            mapManager.zoomOutButtonColor = Color.white;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Zooming Icons/Zoom In Icon 1.png");

            if (tempSprite != null)
            {
                mapManager.zoomInButtonSprite = tempSprite;
            }

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Zooming Icons/Zoom Out Icon 1.png");

            if (tempSprite != null)
            {
                mapManager.zoomOutButtonSprite = tempSprite;
            }

            mapManager.displayGrid = false;
            mapManager.gridColor = Color.white;
            mapManager.gridOpacity = 1F;
            mapManager.gridScale = Vector3.one;
            mapManager.gridRotation = 0F;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Grids/Grid_2.png");

            if (tempSprite != null)
            {
                mapManager.gridSprite = tempSprite;
            }

            mapManager.cameraPosition = new Vector3(0F, 160F, 0F);
            mapManager.cameraRotation = 0F;
            mapManager.cameraOrthographicSize = 500F;
            mapManager.nearClippingPlane = 0.3F;
            mapManager.farClippingPlane = 1000F;
            mapManager.clearFlags = MapClearFlags.SolidColor;
            mapManager.cameraBGColor = Color.white;

            mapManager.haveBackgroundImage = false;
            mapManager.backgroundImageColor = Color.white;

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Map Backgrounds/Map Background 1.png");

            if (tempSprite != null)
            {
                mapManager.backgroundImageSprite = tempSprite;
            }

            mapManager.haveExitButton = false;
            mapManager.exitButtonColor = Color.white;
            mapManager.exitButtonPosition = Vector2.zero;
            mapManager.exitButtonScale = new Vector2(100F, 100F);

            tempSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AA Map and Minimap System/Sprites/Map Exit Buttons/Map Exit Button 2.png");

            if (tempSprite != null)
            {
                mapManager.exitButtonSprite = tempSprite;
            }

            mapManager.disableMinimap = false;
        }
    }
}
