// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;
using UnityEngine.UI;

namespace AAMAP
{
    public class MapManager : MonoBehaviour
    {
        [Tooltip("If this is true, the map will be active.")] public bool mapEnabled = true;

        [Tooltip("Shortcut button to enable the map during runtime.")] public KeyCode enablingShortcut = KeyCode.M;
        [Tooltip("Shortcut button to disable the map during runtime.")] public KeyCode disablingShortcut = KeyCode.Escape;

        [Tooltip("Shape of the map inner display.")] public Sprite mapShape;
        [Tooltip("Opacity of the map inner display. Ranged from 0 (inclusive) to 1 (inclusive).")] public float mapOpacity = 1F;
        [Tooltip("Color of the map inner display.")] public Color mapColor = Color.white;

        [Tooltip("If this is true, \"North, East, South and West\" directions will be displayed on the map.")] public bool displayDirections = false;
        [Tooltip("Position of the map directions.")] public Vector2 directionsPosition = Vector2.zero;
        [Tooltip("Distance from the center of the directions to the positions of the direction signs.")] public float directionsDistance = 80F;
        [Tooltip("Rotation value of the direction signs.")] public float directionsRotation = 0F;
        [Tooltip("Text font of the direction signs.")] public Font directionsTextFont = null;
        [Tooltip("Text font size of the direction signs.")] public int directionsTextFontSize = 40;
        [Tooltip("Text color of the direction signs.")] public Color directionsTextColor = Color.white;
        [Tooltip("If this is true, the direction signs will have background images.")] public bool directionsHaveBackground = false;
        [Tooltip("Background image scales of the direction signs.")] public Vector2 directionsBackgroundImageScale = new Vector2(70F, 70F);
        [Tooltip("Background image sprites of the direction signs. You can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Direction Backgrounds\".")] public Sprite directionsBackgroundSprite = null;
        [Tooltip("Background image color of the direction signs.")] public Color directionsBackgroundColor = Color.white;
        [Tooltip("If this is true, the North direction sign will be displayed on the map.")] public bool displayNorth = true;
        [Tooltip("If this is true, the East direction sign will be displayed on the map.")] public bool displayEast = true;
        [Tooltip("If this is true, the South direction sign will be displayed on the map.")] public bool displaySouth = true;
        [Tooltip("If this is true, the West direction sign will be displayed on the map.")] public bool displayWest = true;

        [Tooltip("If this is true, the map is going to have a border.")] public bool haveBorder = false;
        [Tooltip("Sprite of the map border. You can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Map Borders\".")] public Sprite borderSprite = null;
        [Tooltip("Color of the map border.")] public Color borderColor = Color.white;
        [Tooltip("Rotation value of the map border.")] public float borderRotation = 0F;

        [Tooltip("If this is true, the map is going to have zoom in and zoom out buttons on it.")] public bool haveZoomButtons = false;
        [Tooltip("The zooming sensitivity is the strength of the zoom in and zoom out actions.")] public float zoomingSensitivity = 5F;
        [Tooltip("Minimum range the player can get while zooming in.")] public float minimumRange = 0F;
        [Tooltip("Maximum range the player can get while zooming out.")] public float maximumRange = 1000F;
        [Tooltip("Sprite of the zoom in button.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".")] public Sprite zoomInButtonSprite = null;
        [Tooltip("Sprite of the zoom out button.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".")] public Sprite zoomOutButtonSprite = null;
        [Tooltip("Position of the zoom in button.")] public Vector2 zoomInButtonPosition = new Vector2(0F, 0F);
        [Tooltip("Scale of the zoom in button.")] public Vector2 zoomInButtonScale = new Vector2(80F, 80F);
        [Tooltip("Position of the zoom out button.")] public Vector2 zoomOutButtonPosition = new Vector2(0F, 0F);
        [Tooltip("Scale of the zoom out button.")] public Vector2 zoomOutButtonScale = new Vector2(80F, 80F);
        [Tooltip("Color of the zoom in button.")] public Color zoomInButtonColor = Color.white;
        [Tooltip("Color of the zoom out button.")] public Color zoomOutButtonColor = Color.white;
        
        [Tooltip("If this is true, a grid will be displayed on the Map.")] public bool displayGrid = false;
        [Tooltip("Sprite of the map grid. You can find these sprites at \"Assets > AA Map and Minimap System > Sprites > Grids\".")] public Sprite gridSprite = null;
        [Tooltip("Color of the map grid.")] public Color gridColor = Color.white;
        [Tooltip("Opacity of the map grid.")] public float gridOpacity = 1F;
        [Tooltip("Scale of the map grid.")] public Vector3 gridScale = Vector3.one;
        [Tooltip("Rotation value of the map grid.")] public float gridRotation = 0F;

        [Tooltip("\"Map Camera\" GameObject.")] public GameObject mapCamera;
        [Tooltip("World position of the map camera.")] public Vector3 cameraPosition = new Vector3(0F, 160F, 0F);
        [Tooltip("Rotation value of the map camera.")] public float cameraRotation = 0F;
        [Tooltip("Orthographic size of the map camera. Increase this value to display larger parts on the map.")] public float cameraOrthographicSize = 500F;
        [Tooltip("The closest point to the Map Camera where drawing occurs.")] public float nearClippingPlane = 0.3F;
        [Tooltip("The furthest point from the Map Camera that drawing occurs.")] public float farClippingPlane = 1000;
        [Tooltip("What to display in empty areas of the map camera's view.\n\nChoose Skybox to display a skybox in the empty areas, defaulting to a background color if no skybox is found.\n\nChoose Solid Color to display a solid background color in empty areas.\n\nChoose Depth Only to display nothing in empty areas.\n\nChoose Don't Clear to display whatever was displayed in the previous frame in empty areas.")] public MapClearFlags clearFlags = MapClearFlags.SolidColor;
        [Tooltip("The map camera clears the screen to this color before rendering.")] public Color cameraBGColor = Color.white;

        [Tooltip("If this is true, the map is going to have a background image.")] public bool haveBackgroundImage = false;
        [Tooltip("Sprites of the map background image.\n\nYou can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Map Backgrounds\".")] public Sprite backgroundImageSprite = null;
        [Tooltip("Color of the map background image.")] public Color backgroundImageColor = Color.white;

        [Tooltip("If this is true, the map is going to have an exit button.")] public bool haveExitButton = false;
        [Tooltip("Sprite for the map exit button.\n\nYou can locate these sprites at \"Asset > AA Map and Minimap System > Sprites > Map Exit Buttons\".")] public Sprite exitButtonSprite = null;
        [Tooltip("Color of the map exit button.")] public Color exitButtonColor = Color.white;
        [Tooltip("Position of the map exit button.")] public Vector2 exitButtonPosition = Vector2.zero;
        [Tooltip("Scale of the map exit button.")] public Vector2 exitButtonScale = new Vector2(100F, 100F);

        [Tooltip("If this is true, the minimap will be disabled when the map is enabled. If this is false, the minimap will NOT be disabled when the map is enabled.")] public bool disableMinimap = false;
        [Tooltip("\"Minimap\" GameObject on the Canvas.")] public GameObject minimapGameObject = null;
        [Tooltip("Minimap Manager component on the \"Minimap\" GameObject.")] public MinimapManager minimapManager = null;

        [Tooltip("Raw Image component on the \"Map > Map Mask > Map Display\" GameObject.")] private RawImage displayImage = null;
        [Tooltip("Image component on the \"Map > Map Mask\" GameObject.")] private Image maskImage = null;
        [Tooltip("\"Map > Map Directions\" GameObject.")] private GameObject mapDirectionsObject = null;
        [Tooltip("\"Map > Map Directions > North\" GameObject.")] private GameObject northObject = null;
        [Tooltip("\"Map > Map Directions > East\" GameObject.")] private GameObject eastObject = null;
        [Tooltip("\"Map > Map Directions > South\" GameObject.")] private GameObject southObject = null;
        [Tooltip("\"Map > Map Directions > West\" GameObject.")] private GameObject westObject = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions\" GameObject.")] private RectTransform mapDirectionsRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > North\" GameObject.")] private RectTransform northRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > East\" GameObject.")] private RectTransform eastRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > South\" GameObject.")] private RectTransform southRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Directions > West\" GameObject.")] private RectTransform westRect = null;
        [Tooltip("Text component on the \"Map > Map Directions > North\" GameObject.")] private Text northText = null;
        [Tooltip("Text component on the \"Map > Map Directions > East\" GameObject.")] private Text eastText = null;
        [Tooltip("Text component on the \"Map > Map Directions > South\" GameObject.")] private Text southText = null;
        [Tooltip("Text component on the \"Map > Map Directions > West\" GameObject.")] private Text westText = null;
        [Tooltip("Image component on the \"Map > Map Directions > North\" GameObject.")] private Image northImage = null;
        [Tooltip("Image component on the \"Map > Map Directions > East\" GameObject.")] private Image eastImage = null;
        [Tooltip("Image component on the \"Map > Map Directions > South\" GameObject.")] private Image southImage = null;
        [Tooltip("Image component on the \"Map > Map Directions > West\" GameObject.")] private Image westImage = null;
        [Tooltip("\"Map > Map Border\" GameObject.")] private GameObject mapBorderObject = null;
        [Tooltip("Rect Transform component on the \"Map > Map Border\" GameObject.")] private RectTransform mapBorderRect = null;
        [Tooltip("\"Map > Map Zoom Buttons\" GameObject.")] private GameObject mapZoomButtonsObject = null;
        [Tooltip("\"Map > Map Zoom Buttons > Zoom In Button\" GameObject.")] private GameObject zoomInObject = null;
        [Tooltip("\"Map > Map Zoom Buttons > Zoom Out Button\" GameObject.")] private GameObject zoomOutObject = null;
        [Tooltip("Rect Transform component on the \"Map > Map Zoom Buttons > Zoom In Button\" GameObject.")] private RectTransform zoomInRect = null;
        [Tooltip("Rect Transform component on the \"Map > Map Zoom Buttons > Zoom Out Button\" GameObject.")] private RectTransform zoomOutRect = null;
        [Tooltip("Image component on the \"Map > Map Zoom Buttons > Zoom In Button\" GameObject.")] private Image zoomInImage = null;
        [Tooltip("Image component on the \"Map > Map Zoom Buttons > Zoom Out Button\" GameObject.")] private Image zoomOutImage = null;
        [Tooltip("Image component on the \"Map > Map Border\" GameObject.")] private Image mapBorderImage = null;
        [Tooltip("\"Map > Map Exit Button\" GameObject.")] private GameObject exitButtonObject = null;
        [Tooltip("\"Map > Map Mask > Map Background\" GameObject.")] private GameObject backgroundObject = null;
        [Tooltip("Image component on the \"Map > Map Mask > Map Background\" GameObject.")] private Image backgroundImageComp = null;
        [Tooltip("Image component on the \"Map > Map Exit Button\" GameObject.")] private Image exitButtonImage = null;
        [Tooltip("Rect Transform component on the \"Map > Map Exit Button\" GameObject.")] private RectTransform exitButtonRect = null;
        [Tooltip("Camera component on the \"Map Camera\" GameObject.")] private Camera mapCameraComponent = null;
        [Tooltip("Transform component on the \"Map Camera\" GameObject.")] private Transform mapCameraTransform = null;
        [Tooltip("\"Map > Map Mask > Map Grid\" GameObject.")] private GameObject gridObject = null;
        [Tooltip("Image component on the \"Map > Map Mask > Map Grid\" GameObject.")] private Image gridImage = null;
        [Tooltip("Rect Transform component on the \"Map > Map Mask > Map Grid\" GameObject.")] private RectTransform gridRect = null;
        [Tooltip("Image component on the \"Map > Map Mask > Map Background Filler\" GameObject.")] private Image backgroundFillerImage = null;

        [Tooltip("Render Texture of the map.")] public RenderTexture renderTexture;

        private readonly string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";
        private readonly string errorMessageSuffix = "Please delete and re-create the map.\n";

        private void Start()
        {
            FindMaskImage();
            FindDisplayRawImage();
            FindBackgroundFillerImage();
            FindDirectionsObject();
            FindNESWComponents();
            FindBorderObject();
            FindMapBorderImage();
            FindZoomButtonsObject();
            FindZoomButtonComponents();
            FindGridComponents();
            FindCameraComponents();
            FindBackgroundObject();
            FindBackgroundImage();
            FindExitButtonObject();
            FindExitButtonComponents();

            InitializeMap();
            SetEnablingShortcut(enablingShortcut);
            SetMapShape(mapShape);
            SetMapOpacity(mapOpacity);
            SetMapColor(mapColor);
            InitializeDirections();
            InitializeBorder();
            InitializeZoomButtons();
            InitializeGrid();
            InitializeCamera();
            SetClearFlags(clearFlags);
            InitializeBackgroundImage();
            InitializeExitButton();
        }

        private void Update()
        {
            if (Input.GetKeyDown(enablingShortcut))
            {
                if (!mapEnabled)
                {
                    EnableMap();
                    return;
                }
            }

            if (Input.GetKeyDown(disablingShortcut))
            {
                if (mapEnabled)
                {
                    DisableMap();
                    return;
                }
            }
        }

        /// <summary>
        /// Disables the map if the initialization process fails.
        /// </summary>
        private void DisableMapOnStart()
        {
            if (mapCamera != null)
            {
                mapCamera.SetActive(false);
            }

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Returns true if the map is enabled and returns false if the map is disabled.
        /// </summary>
        public bool IsMapEnabled()
        {
            return mapEnabled;
        }

        /// <summary>
        /// Enables the map.
        /// </summary>
        public void EnableMap()
        {
            mapEnabled = true;

            maskImage.gameObject.SetActive(true);
            mapBorderObject.SetActive(haveBorder);
            mapDirectionsObject.SetActive(displayDirections);
            mapZoomButtonsObject.SetActive(haveZoomButtons);
            exitButtonObject.SetActive(haveExitButton);
            mapCamera.SetActive(true);

            if (mapCameraComponent != null && renderTexture != null)
            {
                mapCameraComponent.targetTexture = renderTexture;
            }

            if (disableMinimap && minimapGameObject != null)
            {
                if (minimapManager == null)
                {
                    minimapManager = minimapGameObject.GetComponent<MinimapManager>();
                }

                if (minimapManager != null)
                {
                    minimapManager.DisableMinimapForMapEnabling();
                }
            }
        }

        /// <summary>
        /// Disables the map.
        /// </summary>
        public void DisableMap()
        {
            mapEnabled = false;

            if (disableMinimap && minimapGameObject != null)
            {
                if (minimapManager == null)
                {
                    minimapManager = minimapGameObject.GetComponent<MinimapManager>();
                }

                if (minimapManager != null)
                {
                    minimapManager.EnableMinimapForMapDisabling();
                }
            }

            maskImage.gameObject.SetActive(false);
            mapBorderObject.SetActive(false);
            mapDirectionsObject.SetActive(false);
            mapZoomButtonsObject.SetActive(false);
            exitButtonObject.SetActive(false);
            mapCamera.SetActive(false);
        }

        /// <summary>
        /// Initializes the map.
        /// </summary>
        private void InitializeMap()
        {
            if (mapEnabled)
            {
                EnableMap();
            }
            else
            {
                DisableMap();
            }
        }

        /// <summary>
        /// Returns the shortcut key that enables the map.
        /// </summary>
        public KeyCode GetEnablingShortcut()
        {
            return enablingShortcut;
        }

        /// <summary>
        /// Sets the shortcut key that enables the map.
        /// </summary>
        /// <param name="shortcut">New shortcut key to enable the map.</param>
        public void SetEnablingShortcut(KeyCode shortcut)
        {
            enablingShortcut = shortcut;
        }

        /// <summary>
        /// Returns the shortcut key that disables the map.
        /// </summary>
        public KeyCode GetDisablingShortcut()
        {
            return disablingShortcut;
        }

        /// <summary>
        /// Sets the shortcut key that disables the map.
        /// </summary>
        /// <param name="shortcut">New shortcut key to disable the map.</param>
        public void SetDisablingShortcut(KeyCode shortcut)
        {
            disablingShortcut = shortcut;
        }

        /// <summary>
        /// Finds the Image component on the Map Mask GameObject.
        /// </summary>
        private void FindMaskImage()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Map Mask"))
                {
                    maskImage = transform.GetChild(i).GetComponent<Image>();
                    break;
                }
            }

            if (maskImage == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > Map Mask\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Returns the shape of the map inner display.
        /// </summary>
        public Sprite GetMapShape()
        {
            return mapShape;
        }

        /// <summary>
        /// Sets the shape of the map inner display.
        /// </summary>
        /// <param name="shape">New shape of the map.</param>
        public void SetMapShape(Sprite shape)
        {
            if (shape != null)
            {
                mapShape = shape;
                maskImage.sprite = shape;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new shape of the map inner display.\n");
            }
        }

        /// <summary>
        /// Finds the Raw Image component on the Map Display GameObject.
        /// </summary>
        private void FindDisplayRawImage()
        {
            if (maskImage == null)
            {
                FindMaskImage();
            }

            if (maskImage != null)
            {
                for (int i = 0; i < maskImage.transform.childCount; i++)
                {
                    if (maskImage.transform.GetChild(i).name.Equals("Map Display"))
                    {
                        displayImage = maskImage.transform.GetChild(i).GetComponent<RawImage>();
                        break;
                    }
                }
            }

            if (displayImage == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Raw Image component on the \"" + gameObject.name + " > Map Mask > Map Display\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Finds the Image component on the Map Background Filler GameObject.
        /// </summary>
        private void FindBackgroundFillerImage()
        {
            if (maskImage == null)
            {
                FindMaskImage();
            }

            if (maskImage != null)
            {
                for (int i = 0; i < maskImage.transform.childCount; i++)
                {
                    if (maskImage.transform.GetChild(i).name.Equals("Map Background Filler"))
                    {
                        backgroundFillerImage = maskImage.transform.GetChild(i).GetComponent<Image>();
                        break;
                    }
                }
            }

            if (backgroundFillerImage == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > Map Mask > Map Background Filler\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Returns the opacity of the map inner display. Ranged from 0 (inclusive) to 1 (inclusive).
        /// </summary>
        public float GetMapOpacity()
        {
            return mapOpacity;
        }

        /// <summary>
        /// Sets the opacity of the map inner display.
        /// </summary>
        /// <param name="opacity">New opacity of the map inner display. Ranged from 0 (inclusive) and 1 (inclusive).</param>
        public void SetMapOpacity(float opacity)
        {
            if (opacity < 0F)
            {
                opacity = 0F;
            }

            if (opacity > 1F)
            {
                opacity = 1F;
            }

            mapOpacity = opacity;

            if (mapEnabled)
            {
                mapOpacity = opacity;
                displayImage.color = new Color(mapColor.r, mapColor.g, mapColor.b, opacity);
                backgroundFillerImage.color = new Color(backgroundFillerImage.color.r, backgroundFillerImage.color.g, backgroundFillerImage.color.b, opacity);
            }
        }

        /// <summary>
        /// Returns the color of the map inner display.
        /// </summary>
        public Color GetMapColor()
        {
            return mapColor;
        }

        /// <summary>
        /// Sets the color of the map inner display.
        /// </summary>
        /// <param name="color">New color of the map inner display.</param>
        public void SetMapColor(Color color)
        {
            if (color != null)
            {
                mapColor = color;
                displayImage.color = new Color(color.r, color.g, color.b, mapOpacity);
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new inner display color of the map.\n");
            }
        }

        /// <summary>
        /// Finds the Map Directions GameObject.
        /// </summary>
        private void FindDirectionsObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Map Directions"))
                {
                    mapDirectionsObject = transform.GetChild(i).gameObject;
                    mapDirectionsRect = transform.GetChild(i).GetComponent<RectTransform>();
                    break;
                }
            }

            if (mapDirectionsObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > Map Directions\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
            else
            {
                if (mapDirectionsRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > Map Directions\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
            }
        }

        /// <summary>
        /// Finds the Rect Transform, Text and Image components on the North, East, South and West GameObjects.
        /// </summary>
        private void FindNESWComponents()
        {
            if (mapDirectionsObject != null)
            {
                for (int i = 0; i < mapDirectionsObject.transform.childCount; i++)
                {
                    if (mapDirectionsObject.transform.GetChild(i).name.Equals("North"))
                    {
                        northObject = mapDirectionsObject.transform.GetChild(i).gameObject;
                        northRect = mapDirectionsObject.transform.GetChild(i).transform.GetComponent<RectTransform>();
                        northText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                        northImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                    }
                    else if (mapDirectionsObject.transform.GetChild(i).name.Equals("East"))
                    {
                        eastObject = mapDirectionsObject.transform.GetChild(i).transform.gameObject;
                        eastRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                        eastText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                        eastImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                    }
                    else if (mapDirectionsObject.transform.GetChild(i).name.Equals("South"))
                    {
                        southObject = mapDirectionsObject.transform.GetChild(i).transform.gameObject;
                        southRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                        southText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                        southImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                    }
                    else if (mapDirectionsObject.transform.GetChild(i).name.Equals("West"))
                    {
                        westObject = mapDirectionsObject.transform.GetChild(i).transform.gameObject;
                        westRect = mapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                        westText = mapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                        westImage = mapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                    }
                }

                if (northObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > North\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
                else
                {
                    if (northRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > North\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (northText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Text component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > North > Text\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (northImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > North\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }
                }

                if (eastObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > East\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
                else
                {
                    if (eastRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > East\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (eastText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Text component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > East > Text\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (eastImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > East\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }
                }

                if (southObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > South\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
                else
                {
                    if (southRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > South\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (southText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Text component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > South > Text\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (southImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > South\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }
                }

                if (westObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > West\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
                else
                {
                    if (westRect == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > West\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (westText == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Text component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > West > Text\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (westImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > " + mapDirectionsObject.name + " > West\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the directions are enabled and returns false if the directions are disabled.
        /// </summary>
        public bool AreDirectionsEnabled()
        {
            return displayDirections;
        }

        /// <summary>
        /// Enables the directions on the map.
        /// </summary>
        public void EnableDirections()
        {
            displayDirections = true;
            InitializeDirections();
        }

        /// <summary>
        /// Disables the directions on the map.
        /// </summary>
        public void DisableDirections()
        {
            displayDirections = false;
            mapDirectionsObject.SetActive(false);
        }

        /// <summary>
        /// Returns the position of the map directions.
        /// </summary>
        public Vector2 GetDirectionsPosition()
        {
            return directionsPosition;
        }

        /// <summary>
        /// Sets the position of the map directions.
        /// </summary>
        /// <param name="position">New position of the map directions.</param>
        public void SetDirectionsPosition(Vector2 position)
        {
            if (position == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the map directions.\n");
            }
            else
            {
                directionsPosition = position;
                mapDirectionsRect.anchoredPosition = (Vector3)position;
            }
        }

        /// <summary>
        /// Returns the distance to the direction icons from their pivot points.
        /// </summary>
        public float GetDirectionsDistance()
        {
            return directionsDistance;
        }

        /// <summary>
        /// Sets the distance of the direction icons from their pivot point.
        /// </summary>
        /// <param name="distance">New distance of the map directions.</param>
        public void SetDirectionsDistance(float distance)
        {
            directionsDistance = distance;

            northRect.anchoredPosition = new Vector3(0F, distance, 0F);
            eastRect.anchoredPosition = new Vector3(distance, 0F, 0F);
            southRect.anchoredPosition = new Vector3(0F, distance * -1F, 0F);
            westRect.anchoredPosition = new Vector3(distance * -1F, 0F, 0F);
        }

        /// <summary>
        /// Returns the rotation value of the map directions.
        /// </summary>
        public float GetDirectionsRotation()
        {
            return directionsRotation;
        }

        /// <summary>
        /// Sets the rotation value of the map directions.
        /// </summary>
        /// <param name="rotation">New rotation value of the map directions. From -360 (inclusive) to 360 (inclusive).</param>
        public void SetDirectionsRotation(float rotation)
        {
            if (rotation < -360F)
            {
                rotation %= -360F;
            }

            if (rotation > 360F)
            {
                rotation %= 360F;
            }

            directionsRotation = rotation;

            mapDirectionsRect.localRotation = Quaternion.Euler(0F, 0F, mapCamera.transform.eulerAngles.y + rotation);

            northRect.localRotation = Quaternion.Euler(0F, 0F, (mapCamera.transform.eulerAngles.y + rotation) * -1F);
            eastRect.localRotation = Quaternion.Euler(0F, 0F, (mapCamera.transform.eulerAngles.y + rotation) * -1F);
            southRect.localRotation = Quaternion.Euler(0F, 0F, (mapCamera.transform.eulerAngles.y + rotation) * -1F);
            westRect.localRotation = Quaternion.Euler(0F, 0F, (mapCamera.transform.eulerAngles.y + rotation) * -1F);
        }

        /// <summary>
        /// Returns the text font of the direction signs.
        /// </summary>
        public Font GetDirectionsFont()
        {
            return directionsTextFont;
        }

        /// <summary>
        /// Sets the text font of the direction signs.
        /// </summary>
        /// <param name="font">New text font of the direction signs.</param>
        public void SetDirectionsFont(Font font)
        {
            // Do not check for null in this method.

            if (font != null)
            {
                directionsTextFont = font;
                northText.font = directionsTextFont;
                eastText.font = directionsTextFont;
                southText.font = directionsTextFont;
                westText.font = directionsTextFont;
            }
        }

        /// <summary>
        /// Returns the font size of the map directions.
        /// </summary>
        public int GetDirectionsFontSize()
        {
            return directionsTextFontSize;
        }

        /// <summary>
        /// Sets the font size of the map directions.
        /// </summary>
        /// <param name="fontSize">New font size of the map directions.</param>
        public void SetDirectionsFontSize(int fontSize)
        {
            directionsTextFontSize = fontSize;
            northText.fontSize = fontSize;
            eastText.fontSize = fontSize;
            southText.fontSize = fontSize;
            westText.fontSize = fontSize;
        }

        /// <summary>
        /// Returns the font color of the map directions.
        /// </summary>
        public Color GetDirectionsTextColor()
        {
            return directionsTextColor;
        }

        /// <summary>
        /// Sets the font color of the map directions.
        /// </summary>
        /// <param name="color">New font color of the map directions.</param>
        public void SetDirectionsTextColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the font color of the map directions.\n");
            }
            else
            {
                directionsTextColor = color;
                northText.color = color;
                eastText.color = color;
                southText.color = color;
                westText.color = color;
            }
        }

        /// <summary>
        /// Returns true if the direction backgrounds are enabled and returns false if the direction backgrounds are disabled.
        /// </summary>
        public bool DoesDirectionsHaveBackgroundImages()
        {
            return directionsHaveBackground;
        }

        /// <summary>
        /// Enables the background images of the map directions.
        /// </summary>
        public void EnableDirectionsBackgroundImages()
        {
            directionsHaveBackground = true;

            northImage.color = directionsTextColor;
            eastImage.color = directionsTextColor;
            southImage.color = directionsTextColor;
            westImage.color = directionsTextColor;

            if (directionsBackgroundSprite != null)
            {
                northImage.sprite = directionsBackgroundSprite;
                eastImage.sprite = directionsBackgroundSprite;
                southImage.sprite = directionsBackgroundSprite;
                westImage.sprite = directionsBackgroundSprite;
            }
        }

        /// <summary>
        /// Disables the background images of the map directions.
        /// </summary>
        public void DisableDirectionsBackgroundImages()
        {
            directionsHaveBackground = false;

            northImage.color = Color.clear;
            eastImage.color = Color.clear;
            southImage.color = Color.clear;
            westImage.color = Color.clear;
        }

        /// <summary>
        /// Returns the scale of the directions background images.
        /// </summary>
        public Vector2 GetDirectionsBackgroundScale()
        {
            return directionsBackgroundImageScale;
        }

        /// <summary>
        /// Sets the scale of the directions background images.
        /// </summary>
        /// <param name="scale">New scale of the directions background images.</param>
        public void SetDirectionsBackgroundScale(Vector2 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the map directions background images.\n");
            }
            else
            {
                directionsBackgroundImageScale = scale;
                northRect.sizeDelta = scale;
                eastRect.sizeDelta = scale;
                southRect.sizeDelta = scale;
                westRect.sizeDelta = scale;
            }
        }

        /// <summary>
        /// Returns the sprite of the directions background images.
        /// </summary>
        public Sprite GetDirectionsBackgroundSprites()
        {
            return directionsBackgroundSprite;
        }

        /// <summary>
        /// Sets the sprite of the directions background images.
        /// </summary>
        /// <param name="sprite">New sprite of the directions background images.</param>
        public void SetDirectionsBackgroundSprites(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new background image of the map directions.\n");
            }
            else
            {
                directionsBackgroundSprite = sprite;
                northImage.sprite = sprite;
                eastImage.sprite = sprite;
                southImage.sprite = sprite;
                westImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the color of the directions background images.
        /// </summary>
        public Color GetDirectionsBackgroundColor()
        {
            return directionsBackgroundColor;
        }

        /// <summary>
        /// Sets the color of the directions background images.
        /// </summary>
        /// <param name="color">New color of the directions background images.</param>
        public void SetDirectionsBackgroundColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new background color of the directions.\n");
            }
            else
            {
                directionsBackgroundColor = color;
                if (directionsHaveBackground && directionsBackgroundSprite != null)
                {
                    northImage.color = color;
                    eastImage.color = color;
                    southImage.color = color;
                    westImage.color = color;
                }
                else
                {
                    northImage.color = Color.clear;
                    eastImage.color = Color.clear;
                    southImage.color = Color.clear;
                    westImage.color = Color.clear;
                }
            }
        }

        /// <summary>
        /// Returns true if the North icon is enabled and returns false if the North icon is disabled.
        /// </summary>
        public bool IsNorthEnabled()
        {
            return displayNorth;
        }

        /// <summary>
        /// Enables the North icon on the map directions.
        /// </summary>
        public void EnableNorthSign()
        {
            displayNorth = true;
            northObject.SetActive(true);
        }

        /// <summary>
        /// Disables the North icon on the map directions.
        /// </summary>
        public void DisableNorthSign()
        {
            displayNorth = false;
            northObject.SetActive(false);
        }

        /// <summary>
        /// Returns true if the East icon is enabled and returns false if the East icon is disabled.
        /// </summary>
        public bool IsEastEnabled()
        {
            return displayEast;
        }

        /// <summary>
        /// Enables the East icon on the map directions.
        /// </summary>
        public void EnableEastSign()
        {
            displayEast = true;
            eastObject.SetActive(true);
        }

        /// <summary>
        /// Disables the East icon on the map directions.
        /// </summary>
        public void DisableEastSign()
        {
            displayEast = false;
            eastObject.SetActive(false);
        }

        /// <summary>
        /// Returns true if the South icon is enabled and returns false if the South icon is disabled.
        /// </summary>
        public bool IsSouthEnabled()
        {
            return displaySouth;
        }

        /// <summary>
        /// Enables the South icon on the map directions.
        /// </summary>
        public void EnableSouthSign()
        {
            displaySouth = true;
            southObject.SetActive(true);
        }

        /// <summary>
        /// Disables the South icon on the map directions.
        /// </summary>
        public void DisableSouthSign()
        {
            displaySouth = false;
            southObject.SetActive(false);
        }

        /// <summary>
        /// Returns true if the West icon is enabled and returns false if the West icon is disabled.
        /// </summary>
        public bool IsWestEnabled()
        {
            return displayWest;
        }

        /// <summary>
        /// Enables the West icon on the map directions.
        /// </summary>
        public void EnableWest()
        {
            displayWest = true;
            westObject.SetActive(true);
        }

        /// <summary>
        /// Disables the West icon on the map directions.
        /// </summary>
        public void DisableWest()
        {
            displayWest = false;
            westObject.SetActive(false);
        }

        /// <summary>
        /// Initializes the map directions.
        /// </summary>
        private void InitializeDirections()
        {
            SetDirectionsFont(directionsTextFont);
            SetDirectionsFontSize(directionsTextFontSize);
            SetDirectionsTextColor(directionsTextColor);
            SetDirectionsPosition(directionsPosition);
            SetDirectionsDistance(directionsDistance);
            SetDirectionsRotation(directionsRotation);
            SetDirectionsBackgroundScale(directionsBackgroundImageScale);

            if (directionsHaveBackground && directionsBackgroundSprite != null)
            {
                EnableDirectionsBackgroundImages();
                SetDirectionsBackgroundSprites(directionsBackgroundSprite);
                SetDirectionsBackgroundColor(directionsBackgroundColor);
                SetDirectionsBackgroundScale(directionsBackgroundImageScale);
            }
            else
            {
                SetDirectionsBackgroundColor(Color.clear);
            }

            if (displayDirections && mapEnabled)
            {
                mapDirectionsObject.SetActive(true);

                northObject.SetActive(displayNorth);
                eastObject.SetActive(displayEast);
                southObject.SetActive(displaySouth);
                westObject.SetActive(displayWest);
            }
            else
            {
                mapDirectionsObject.SetActive(false);
            }
        }

        /// <summary>
        /// Finds the Map Border GameObject.
        /// </summary>
        private void FindBorderObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Map Border"))
                {
                    mapBorderObject = transform.GetChild(i).gameObject;
                    mapBorderRect = transform.GetChild(i).GetComponent<RectTransform>();
                    break;
                }
            }

            if (mapBorderObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > Map Border\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
            else
            {
                if (mapBorderRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > Map Border\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
            }
        }

        /// <summary>
        /// Finds the Image component on the Map Border GameObject.
        /// </summary>
        private void FindMapBorderImage()
        {
            if (mapBorderObject != null)
            {
                mapBorderImage = mapBorderObject.GetComponent<Image>();

                if (mapBorderImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > Map Border\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
            }
        }

        /// <summary>
        /// Returns true if the map border is enabled and returns false if the map border is disabled.
        /// </summary>
        public bool IsBorderEnabled()
        {
            return haveBorder;
        }

        /// <summary>
        /// Enables the map border.
        /// </summary>
        public void EnableBorder()
        {
            haveBorder = true;
            mapBorderObject.SetActive(true);
            InitializeBorder();
        }

        /// <summary>
        /// Disables the map border.
        /// </summary>
        public void DisableBorder()
        {
            haveBorder = false;
            mapBorderObject.SetActive(false);
        }

        /// <summary>
        /// Returns the map border sprite.
        /// </summary>
        public Sprite GetBorderSprite()
        {
            return borderSprite;
        }

        /// <summary>
        /// Sets the sprite of the map border.
        /// </summary>
        /// <param name="sprite">New sprite of the map border.</param>
        public void SetBorderSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new map border sprite.\n");
            }
            else
            {
                borderSprite = sprite;
                mapBorderImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the map border color.
        /// </summary>
        public Color GetBorderColor()
        {
            return borderColor;
        }

        /// <summary>
        /// Sets the color of the map border.
        /// </summary>
        /// <param name="color">New color of the map border.</param>
        public void SetBorderColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the map border.\n");
            }
            else
            {
                borderColor = color;
                mapBorderImage.color = color;
            }
        }

        /// <summary>
        /// Returns the rotation of the border.
        /// </summary>
        public float GetBorderRotation()
        {
            return borderRotation;
        }

        /// <summary>
        /// Sets the rotation of the map border.
        /// </summary>
        /// <param name="rotation">New rotation of the map border. From -360 (inclusive) to 360 (inclusive).</param>
        public void SetBorderRotation(float rotation)
        {
            if (rotation < -360F)
            {
                rotation %= -360F;
            }

            if (rotation > 360F)
            {
                rotation %= 360F;
            }

            borderRotation = rotation;
            mapBorderRect.localRotation = Quaternion.Euler(0F, 0F, rotation);
        }

        /// <summary>
        /// Initializes the map border.
        /// </summary>
        private void InitializeBorder()
        {
            if (borderSprite != null)
            {
                SetBorderSprite(borderSprite);
                SetBorderColor(borderColor);
                SetBorderRotation(borderRotation);
            }

            if (haveBorder && mapEnabled)
            {
                mapBorderObject.SetActive(true);
            }
            else
            {
                mapBorderObject.SetActive(false);
            }
        }

        /// <summary>
        /// Finds the Map Zoom Buttons GameObject.
        /// </summary>
        private void FindZoomButtonsObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Map Zoom Buttons"))
                {
                    mapZoomButtonsObject = transform.GetChild(i).gameObject;
                    break;
                }
            }

            if (mapZoomButtonsObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > Map Zoom Buttons\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Finds the Image and Rect Transform components on the Zoom In Button and Zoom Out Button GameObjects.
        /// </summary>
        private void FindZoomButtonComponents()
        {
            if (mapZoomButtonsObject != null)
            {
                for (int i = 0; i < mapZoomButtonsObject.transform.childCount; i++)
                {
                    if (mapZoomButtonsObject.transform.GetChild(i).name.Equals("Zoom In Button"))
                    {
                        zoomInObject = mapZoomButtonsObject.transform.GetChild(i).gameObject;
                        zoomInImage = mapZoomButtonsObject.transform.GetChild(i).GetComponent<Image>();
                        zoomInRect = mapZoomButtonsObject.transform.GetChild(i).GetComponent<RectTransform>();
                    }
                    else if (mapZoomButtonsObject.transform.GetChild(i).name.Equals("Zoom Out Button"))
                    {
                        zoomOutObject = mapZoomButtonsObject.transform.GetChild(i).gameObject;
                        zoomOutImage = mapZoomButtonsObject.transform.GetChild(i).GetComponent<Image>();
                        zoomOutRect = mapZoomButtonsObject.transform.GetChild(i).GetComponent<RectTransform>();
                    }
                }

                if (zoomInObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > " + mapZoomButtonsObject.name + " > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }

                if (zoomInImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > " + mapZoomButtonsObject.name + " > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }

                if (zoomInRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > " + mapZoomButtonsObject.name + " > Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }

                if (zoomOutObject == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > " + mapZoomButtonsObject.name + " > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }

                if (zoomOutImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"" + gameObject.name + " > " + mapZoomButtonsObject.name + " > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }

                if (zoomOutRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"" + gameObject.name + " > " + mapZoomButtonsObject.name + " > Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
            }
        }

        /// <summary>
        /// Returns true if the map zoom buttons are enabled and returns false if the map zoom buttons are disabled.
        /// </summary>
        public bool AreZoomButtonsEnabled()
        {
            return haveZoomButtons;
        }

        /// <summary>
        /// Enables the zoom buttons on the map.
        /// </summary>
        public void EnableZoomButtons()
        {
            haveZoomButtons = true;
            mapZoomButtonsObject.SetActive(true);
            zoomInObject.SetActive(true);
            zoomOutObject.SetActive(true);
            InitializeZoomButtons();
        }

        /// <summary>
        /// Disables the zoom buttons on the map.
        /// </summary>
        public void DisableZoomButtons()
        {
            haveZoomButtons = false;
            mapZoomButtonsObject.SetActive(false);
            zoomInObject.SetActive(false);
            zoomOutObject.SetActive(false);
        }

        /// <summary>
        /// Returns the map zooming sensitivity.
        /// </summary>
        public float GetZoomingSensitivity()
        {
            return zoomingSensitivity;
        }

        /// <summary>
        /// Sets the zooming sensitivity of the zoom buttons.
        /// </summary>
        /// <param name="sensitivity">New zooming sensitivity of the zoom buttons.</param>
        public void SetZoomingSensitivity(float sensitivity)
        {
            zoomingSensitivity = sensitivity;
        }

        /// <summary>
        /// Returns the minimum range of the map while zooming in.
        /// </summary>
        public float GetMinimumRange()
        {
            return minimumRange;
        }

        /// <summary>
        /// Sets the minimum range while zooming in on the map.
        /// </summary>
        /// <param name="minimumRange">New minimum range of the map.</param>
        public void SetMinimumRange(float minimumRange)
        {
            this.minimumRange = minimumRange;
        }

        /// <summary>
        /// Returns the maximum range of the map while zooming out.
        /// </summary>
        public float GetMaximumRange()
        {
            return maximumRange;
        }

        /// <summary>
        /// Sets the maximum range while zooming out of the map.
        /// </summary>
        /// <param name="maximumRange">New maximum range of the map.</param>
        public void SetMaximumRange(float maximumRange)
        {
            this.maximumRange = maximumRange;
        }

        /// <summary>
        /// Returns the sprite of the zoom in button.
        /// </summary>
        public Sprite GetZoomInButtonSprite()
        {
            return zoomInButtonSprite;
        }

        /// <summary>
        /// Sets the sprite of the zoom in button.
        /// </summary>
        /// <param name="sprite">New sprite of the zoom in button.</param>
        public void SetZoomInButtonSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the zoom in button.\n");
            }
            else
            {
                zoomInButtonSprite = sprite;
                zoomInImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the sprite of the zoom out button.
        /// </summary>
        public Sprite GetZoomOutButtonSprite()
        {
            return zoomOutButtonSprite;
        }

        /// <summary>
        /// Sets the sprite of the zoom out button.
        /// </summary>
        /// <param name="sprite">New sprite of the zoom out button.</param>
        public void SetZoomOutButtonSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the zoom out button.\n");
            }
            else
            {
                zoomOutButtonSprite = sprite;
                zoomOutImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the position of the zoom in button.
        /// </summary>
        public Vector2 GetZoomInButtonPosition()
        {
            return zoomInButtonPosition;
        }

        /// <summary>
        /// Sets the position of the zoom in button.
        /// </summary>
        /// <param name="position">New position of the zoom in button.</param>
        public void SetZoomInButtonPosition(Vector2 position)
        {
            if (position == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the zoom in button.\n");
            }
            else
            {
                zoomInButtonPosition = position;
                zoomInRect.anchoredPosition = position;
            }
        }

        /// <summary>
        /// Returns the scale of the zoom in button.
        /// </summary>
        public Vector2 GetZoomInButtonScale()
        {
            return zoomInButtonScale;
        }

        /// <summary>
        /// Sets the scale of the zoom in button.
        /// </summary>
        /// <param name="scale">New scale of the zoom in button.</param>
        public void SetZoomInButtonScale(Vector2 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the zoom in button.\n");
            }
            else
            {
                zoomInButtonScale = scale;
                zoomInRect.sizeDelta = scale;
            }
        }

        /// <summary>
        /// Returns the position of the zoom out button.
        /// </summary>
        public Vector2 GetZoomOutButtonPosition()
        {
            return zoomOutButtonPosition;
        }

        /// <summary>
        /// Sets the position of the zoom out button.
        /// </summary>
        /// <param name="position">New position of the zoom out button.</param>
        public void SetZoomOutButtonPosition(Vector2 position)
        {
            if (position == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the zoom out button.\n");
            }
            else
            {
                zoomOutButtonPosition = position;
                zoomOutRect.anchoredPosition = position;
            }
        }

        /// <summary>
        /// Returns the scale of the zoom out button.
        /// </summary>
        public Vector2 GetZoomOutButtonScale()
        {
            return zoomOutButtonScale;
        }

        /// <summary>
        /// Sets the scale of the zoom out button.
        /// </summary>
        /// <param name="scale">New scale of the zoom out button.</param>
        public void SetZoomOutButtonScale(Vector2 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the zoom out button.\n");
            }
            else
            {
                zoomOutButtonScale = scale;
                zoomOutRect.sizeDelta = scale;
            }
        }

        /// <summary>
        /// Returns the color of the zoom in button.
        /// </summary>
        public Color GetZoomInButtonColor()
        {
            return zoomInButtonColor;
        }

        /// <summary>
        /// Sets the color of the zoom in button.
        /// </summary>
        /// <param name="color">New color of the zoom in button.</param>
        public void SetZoomInButtonColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the zoom in button.\n");
            }
            else
            {
                zoomInButtonColor = color;
                zoomInImage.color = color;
            }
        }

        /// <summary>
        /// Returns the color of the zoom out button.
        /// </summary>
        public Color GetZoomOutButtonColor()
        {
            return zoomOutButtonColor;
        }

        /// <summary>
        /// Sets the color of the zoom out button.
        /// </summary>
        /// <param name="color">New color of the zoom out button.</param>
        public void SetZoomOutButtonColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the zoom out button.\n");
            }
            else
            {
                zoomOutButtonColor = color;
                zoomOutImage.color = color;
            }
        }

        /// <summary>
        /// Initializes the map zoom buttons.
        /// </summary>
        private void InitializeZoomButtons()
        {
            SetZoomInButtonScale(zoomInButtonScale);
            SetZoomOutButtonScale(zoomOutButtonScale);

            if (zoomInButtonSprite != null)
                SetZoomInButtonSprite(zoomInButtonSprite);

            if (zoomOutButtonSprite != null)
                SetZoomOutButtonSprite(zoomOutButtonSprite);

            SetZoomInButtonPosition(zoomInButtonPosition);
            SetZoomOutButtonPosition(zoomOutButtonPosition);
            SetZoomInButtonColor(zoomInButtonColor);
            SetZoomOutButtonColor(zoomOutButtonColor);
            SetZoomingSensitivity(zoomingSensitivity);
            SetMinimumRange(minimumRange);
            SetMaximumRange(maximumRange);

            if (haveZoomButtons && mapEnabled)
            {
                mapZoomButtonsObject.SetActive(true);
            }
            else
            {
                mapZoomButtonsObject.SetActive(false);
            }
        }

        /// <summary>
        /// Zooms in on the map. This method is called when the user press the zoom in button.
        /// </summary>
        public void ZoomIn()
        {
            if (cameraOrthographicSize - zoomingSensitivity >= minimumRange)
            {
                cameraOrthographicSize -= zoomingSensitivity;
            }
            else
            {
                cameraOrthographicSize = minimumRange;
            }

            mapCameraComponent.orthographicSize = cameraOrthographicSize;
        }

        /// <summary>
        /// Zooms out on the map. This method is called when the user press the zoom out button.
        /// </summary>
        public void ZoomOut()
        {
            if (cameraOrthographicSize + zoomingSensitivity <= maximumRange)
            {
                cameraOrthographicSize += zoomingSensitivity;
            }
            else
            {
                cameraOrthographicSize = maximumRange;
            }

            mapCameraComponent.orthographicSize = cameraOrthographicSize;
        }

        /// <summary>
        /// Zooms in on the map.
        /// </summary>
        /// <param name="zoomingValue">The strength of the zooming action. Keep in mind that you cannot go lower than the minimum range.</param>
        public void ZoomIn(int zoomingValue)
        {
            if (cameraOrthographicSize - zoomingValue >= minimumRange)
            {
                cameraOrthographicSize -= zoomingValue;
            }
            else
            {
                cameraOrthographicSize = minimumRange;
            }

            mapCameraComponent.orthographicSize = cameraOrthographicSize;
        }

        /// <summary>
        /// Zooms out on the map.
        /// </summary>
        /// <param name="zoomingValue">The strength of the zooming action. Keep in mind that you cannot go higher than the maximum range.</param>
        public void ZoomOut(int zoomingValue)
        {
            if (cameraOrthographicSize + zoomingValue <= maximumRange)
            {
                cameraOrthographicSize += zoomingValue;
            }
            else
            {
                cameraOrthographicSize = maximumRange;
            }

            mapCameraComponent.orthographicSize = cameraOrthographicSize;
        }

        /// <summary>
        /// Finds all the necessary GameObjects and components about the map grid.
        /// </summary>
        private void FindGridComponents()
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
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"Map > Map Mask > Map Grid\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
                else
                {
                    gridImage = gridObject.GetComponent<Image>();
                    gridRect = gridObject.GetComponent<RectTransform>();

                    if (gridImage == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"Map > Map Mask > Map Grid\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }

                    if (gridRect == null)
                    {
                        Debug.LogError("Failed to generate the map because the Rect Transform component on the \"Map > Map Mask > Map Grid\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the map grid is enabled and returns false if the map grid is disabled.
        /// </summary>
        public bool IsGridEnabled()
        {
            return displayGrid;
        }

        /// <summary>
        /// Enables the map grid.
        /// </summary>
        public void EnableGrid()
        {
            displayGrid = true;
            InitializeGrid();
        }

        /// <summary>
        /// Disables the map grid.
        /// </summary>
        public void DisableGrid()
        {
            displayGrid = false;
            gridObject.SetActive(false);
        }

        /// <summary>
        /// Returns the sprite of the map grid.
        /// </summary>
        public Sprite GetGridSprite()
        {
            return gridImage.sprite;
        }

        /// <summary>
        /// Sets the sprite of the map grid.
        /// </summary>
        /// <param name="sprite">New sprite of the map grid.</param>
        public void SetGridSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the map grid.\n");
            }
            else
            {
                gridSprite = sprite;
                gridImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the color of the map grid.
        /// </summary>
        public Color GetGridColor()
        {
            return gridImage.color;
        }

        /// <summary>
        /// Sets the color of the map grid.
        /// </summary>
        /// <param name="color">New color of the map grid.</param>
        public void SetGridColor(Color color)
        {
            if (color != null)
            {
                gridColor = new Color(color.r, color.g, color.b, gridOpacity);
                gridImage.color = new Color(color.r, color.g, color.b, gridOpacity);
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the map grid.\n");
            }
        }

        /// <summary>
        /// Returns the opacity of the map grid.
        /// </summary>
        public float GetGridOpacity()
        {
            return gridImage.color.a;
        }

        /// <summary>
        /// Sets the opacity of the map grid.
        /// </summary>
        /// <param name="opacity">New opacity of the map grid. Between 0 (inclusive) and 1 (inclusive)./param>
        public void SetGridOpacity(float opacity)
        {
            if (opacity < 0F)
            {
                opacity = 0F;
            }
            else if (opacity > 1F)
            {
                opacity = 1F;
            }

            gridOpacity = opacity;
            gridImage.color = new Color(gridColor.r, gridColor.g, gridColor.b, opacity);
        }

        /// <summary>
        /// Returns the scale of the map grid.
        /// </summary>
        public Vector3 GetGridScale()
        {
            return gridScale;
        }

        /// <summary>
        /// Sets the scale of the map grid.
        /// </summary>
        /// <param name="scale">New scale of the map grid.</param>
        public void SetGridScale(Vector3 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the map grid.\n");
            }
            else
            {
                gridScale = scale;
                gridRect.sizeDelta = scale;
            }
        }

        /// <summary>
        /// Returns the rotation value of the map grid.
        /// </summary>
        public float GetGridRotation()
        {
            return gridRotation;
        }

        /// <summary>
        /// Sets the rotation value of the map grid.
        /// </summary>
        /// <param name="rotation">New rotation value of the map grid.</param>
        public void SetGridRotation(float rotation)
        {
            if (rotation < -360F)
            {
                rotation %= -360F;
            }

            if (rotation > 360F)
            {
                rotation %= 360F;
            }

            gridRotation = rotation;
            gridRect.localRotation = Quaternion.Euler(0F, 0F, rotation);
        }

        /// <summary>
        /// Initializes the map grid.
        /// </summary>
        private void InitializeGrid()
        {
            gridImage.sprite = gridSprite;
            gridImage.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            gridRect.localScale = gridScale;

            if (mapCamera != null)
            {
                gridRect.localRotation = Quaternion.Euler(0F, 0F, mapCameraTransform.eulerAngles.y + gridRotation);
            }
            else
            {
                gridRect.localRotation = Quaternion.Euler(0F, 0F, gridRotation);
            }

            if (mapEnabled && displayGrid)
            {
                gridObject.SetActive(true);
            }
            else
            {
                gridObject.SetActive(false);
            }
        }

        /// <summary>
        /// Finds the necessary components on the Map Camera GameObject.
        /// </summary>
        private void FindCameraComponents()
        {
            if (mapCamera != null)
            {
                mapCameraTransform = mapCamera.GetComponent<Transform>();
                mapCameraComponent = mapCamera.GetComponent<Camera>();

                if (mapCameraComponent == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Camera component on the \"Map Camera\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
            }
            else
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"Map Camera\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Returns the map camera.
        /// </summary>
        public GameObject GetMapCamera()
        {
            return mapCamera;
        }

        /// <summary>
        /// Sets the map camera.
        /// </summary>
        /// <param name="camera">New map camera.</param>
        public void SetMapCamera(GameObject camera)
        {
            if (camera == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new map camera.\n");
            }
            else
            {
                if (camera.GetComponent<Camera>() == null)
                {
                    Debug.LogWarning(errorMessagePrefix + "The GameObject you have assigned as the new map camera doesn't have a Camera component on it.\n");
                }
                else
                {
                    mapCamera = camera;
                    mapCameraComponent = camera.GetComponent<Camera>();
                    mapCameraTransform = camera.transform;

                    if (mapCameraComponent != null && renderTexture != null)
                    {
                        mapCameraComponent.targetTexture = renderTexture;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the world position of the map camera.
        /// </summary>
        public Vector3 GetCameraPosition()
        {
            return cameraPosition;
        }

        /// <summary>
        /// Sets the world position of the map camera.
        /// </summary>
        /// <param name="position">New world position of the map camera.</param>
        public void SetCameraPosition(Vector3 position)
        {
            if (position == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the map camera.\n");
            }
            else
            {
                cameraPosition = position;
                mapCameraTransform.position = position;
            }
        }

        /// <summary>
        /// Returns the rotation value of the map camera on the Y axis.
        /// </summary>
        public float GetMapCameraRotation()
        {
            return cameraRotation;
        }

        /// <summary>
        /// Sets the rotation value of the map camera on the Y axis.
        /// </summary>
        /// <param name="rotation">New rotation value of the map camera on the Y axis.</param>
        public void SetMapCameraRotation(float rotation)
        {
            if (rotation < -360F)
            {
                rotation %= -360F;
            }

            if (rotation > 360F)
            {
                rotation %= 360F;
            }

            cameraRotation = rotation;

            if (mapCamera != null)
            {
                mapCameraTransform.rotation = Quaternion.Euler(90F, rotation, 0F);
            }
        }

        /// <summary>
        /// Returns the orthographic size of the map camera.
        /// </summary>
        public float GetCameraOrthograpicSize()
        {
            return cameraOrthographicSize;
        }

        /// <summary>
        /// Sets the orthographic size of the map camera.
        /// </summary>
        /// <param name="size">New orthographic size of the map camera.</param>
        public void SetCameraOrthograpicSize(float size)
        {
            cameraOrthographicSize = size;

            if (mapCameraComponent != null)
            {
                mapCameraComponent.orthographicSize = size;
            }
        }

        /// <summary>
        /// Returns the near clipping plane of the map camera.
        /// </summary>
        public float GetCameraNearClippingPlane()
        {
            return nearClippingPlane;
        }

        /// <summary>
        /// Sets the near clipping plane of the map camera.
        /// </summary>
        /// <param name="nearClippingPlane">New near clipping plane of the map camera.</param>
        public void SetCameraNearClippingPlane(float nearClippingPlane)
        {
            this.nearClippingPlane = nearClippingPlane;

            if (mapCameraComponent != null)
            {
                mapCameraComponent.nearClipPlane = nearClippingPlane;
            }
        }

        /// <summary>
        /// Returns the far clipping plane of the map camera.
        /// </summary>
        public float GetCameraFarClippingPlane()
        {
            return farClippingPlane;
        }

        /// <summary>
        /// Sets the far clipping plane of the map camera.
        /// </summary>
        /// <param name="farClippingPlane">New far clipping plane of the map camera.</param>
        public void SetCameraFarClippingPlane(float farClippingPlane)
        {
            this.farClippingPlane = farClippingPlane;

            if (mapCameraComponent != null)
            {
                mapCameraComponent.farClipPlane = farClippingPlane;
            }
        }

        /// <summary>
        /// Returns the Clear Flags of the Map Camera.
        /// </summary>
        public MapClearFlags GetClearFlags()
        {
            return clearFlags;
        }

        /// <summary>
        /// Sets the Clear Flags of the map camera.
        /// </summary>
        /// <param name="clearFlags">New Clear Flags of the camera.</param>
        public void SetClearFlags(MapClearFlags clearFlags)
        {
            this.clearFlags = clearFlags;

            if (mapCameraComponent != null)
            {
                if (this.clearFlags == MapClearFlags.Skybox)
                {
                    mapCameraComponent.clearFlags = CameraClearFlags.Skybox;
                    mapCameraComponent.backgroundColor = backgroundImageColor;
                }
                else if (this.clearFlags == MapClearFlags.SolidColor)
                {
                    mapCameraComponent.clearFlags = CameraClearFlags.SolidColor;
                    mapCameraComponent.backgroundColor = backgroundImageColor;
                }
                else if (this.clearFlags == MapClearFlags.DepthOnly)
                {
                    mapCameraComponent.clearFlags = CameraClearFlags.Depth;
                }
                else if (this.clearFlags == MapClearFlags.DontClear)
                {
                    mapCameraComponent.clearFlags = CameraClearFlags.Nothing;
                }
            }
        }

        /// <summary>
        /// Returns the background color of the map camera.
        /// </summary>
        public Color GetCameraBackgroundColor()
        {
            return cameraBGColor;
        }

        /// <summary>
        /// Sets the background color of the map camera.
        /// </summary>
        /// <param name="color">New background color of the map camera.</param>
        public void SetCameraBackgroundColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the camera background.\n");
            }
            else
            {
                cameraBGColor = color;
                mapCameraComponent.backgroundColor = color;
            }
        }

        /// <summary>
        /// Initializes the map camera.
        /// </summary>
        private void InitializeCamera()
        {
            SetMapCamera(mapCamera);
            SetMapCameraRotation(cameraRotation);
            SetCameraPosition(cameraPosition);
            SetCameraOrthograpicSize(cameraOrthographicSize);
            SetCameraNearClippingPlane(nearClippingPlane);
            SetCameraFarClippingPlane(farClippingPlane);
        }

        /// <summary>
        /// Finds the Map Background GameObject.
        /// </summary>
        private void FindBackgroundObject()
        {
            if (maskImage != null && backgroundObject == null)
            {
                for (int i = 0; i < maskImage.gameObject.transform.childCount; i++)
                {
                    if (maskImage.gameObject.transform.GetChild(i).name.Equals("Map Background"))
                    {
                        backgroundObject = maskImage.gameObject.transform.GetChild(i).gameObject;
                        break;
                    }
                }
            }

            if (backgroundObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"Map > Map Mask > Map Background\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Finds the Image component on the Map Background GameObject.
        /// </summary>
        private void FindBackgroundImage()
        {
            if (backgroundImageComp == null)
            {
                if (backgroundObject != null)
                {
                    backgroundImageComp = backgroundObject.GetComponent<Image>();

                    if (backgroundImageComp == null)
                    {
                        Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"Map > Map Mask > Map Background\" GameObject could not be found." + errorMessageSuffix);
                        DisableMapOnStart();
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the map background image is enabled and returns false if the map background image is disabled.
        /// </summary>
        public bool IsBackgroundImageEnabled()
        {
            return haveBackgroundImage;
        }

        /// <summary>
        /// Enables the map background image.
        /// </summary>
        public void EnableBackgroundImage()
        {
            haveBackgroundImage = true;
            backgroundObject.SetActive(true);

            InitializeBackgroundImage();

            mapCameraComponent.backgroundColor = new Color(mapCameraComponent.backgroundColor.r, mapCameraComponent.backgroundColor.g, mapCameraComponent.backgroundColor.b, 0F);
        }

        /// <summary>
        /// Disables the map background image.
        /// </summary>
        public void DisableBackgroundImage()
        {
            haveBackgroundImage = false;
            backgroundObject.SetActive(false);
        }

        /// <summary>
        /// Returns the sprite of the map background image.
        /// </summary>
        public Sprite GetBackgroundImageSprite()
        {
            return backgroundImageSprite;
        }

        /// <summary>
        /// Sets the sprite of the map background image.
        /// </summary>
        /// <param name="sprite">New sprite of the map background image.</param>
        public void SetBackgroundImageSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the map background image.\n");
            }
            else
            {
                backgroundImageSprite = sprite;
                backgroundImageComp.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the color of the map background image.
        /// </summary>
        public Color GetBackgroundImageColor()
        {
            return backgroundImageColor;
        }

        /// <summary>
        /// Sets the color of the map background image.
        /// </summary>
        /// <param name="color">New color of the map background image.</param>
        public void SetBackgroundImageColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the map background image.\n");
            }
            else
            {
                backgroundImageColor = color;
                backgroundImageComp.color = color;
            }
        }

        /// <summary>
        /// Initializes the map background image.
        /// </summary>
        private void InitializeBackgroundImage()
        {
            if (haveBackgroundImage)
            {
                if (backgroundImageSprite != null)
                {
                    SetBackgroundImageSprite(backgroundImageSprite);
                }

                if (backgroundImageColor != null)
                {
                    SetBackgroundImageColor(backgroundImageColor);
                }

                if (exitButtonSprite != null)
                {
                    SetExitButtonSprite(exitButtonSprite);
                }

                haveBackgroundImage = true;
                backgroundObject.SetActive(true);
                mapCameraComponent.backgroundColor = new Color(mapCameraComponent.backgroundColor.r, mapCameraComponent.backgroundColor.g, mapCameraComponent.backgroundColor.b, 0F);
            }
            else
            {
                DisableBackgroundImage();
            }
        }

        /// <summary>
        /// Finds the Map Exit Button GameObject.
        /// </summary>
        private void FindExitButtonObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Map Exit Button"))
                {
                    exitButtonObject = transform.GetChild(i).gameObject;
                    break;
                }
            }

            if (exitButtonObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the map because the \"" + gameObject.name + " > Map Exit Button\" GameObject could not be found." + errorMessageSuffix);
                DisableMapOnStart();
            }
        }

        /// <summary>
        /// Finds the Image and Rect Transform components on the Map Exit Button GameObject.
        /// </summary>
        private void FindExitButtonComponents()
        {
            if (exitButtonObject != null)
            {
                exitButtonImage = exitButtonObject.GetComponent<Image>();
                exitButtonRect = exitButtonObject.GetComponent<RectTransform>();

                if (exitButtonImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Image component on the \"Map Exit Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }

                if (exitButtonRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the map because the Rect Transform component on the \"Map Exit Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMapOnStart();
                }
            }
        }

        /// <summary>
        /// Returns true if the map exit button is enabled and returns false if the map exit button is disabled.
        /// </summary>
        public bool IsExitButtonEnabled()
        {
            return haveExitButton;
        }

        /// <summary>
        /// Enables the exit button on the map.
        /// </summary>
        public void EnableExitButton()
        {
            haveExitButton = true;
            exitButtonObject.SetActive(true);

            InitializeExitButton();
        }

        /// <summary>
        /// Disables the exit button on the map.
        /// </summary>
        public void DisableExitButton()
        {
            haveExitButton = false;
            exitButtonObject.SetActive(false);
        }

        /// <summary>
        /// Returns the sprite of the map exit button.
        /// </summary>
        public Sprite GetExitButtonSprite()
        {
            return exitButtonSprite;
        }

        /// <summary>
        /// Sets the sprite of the map exit button.
        /// </summary>
        /// <param name="sprite">New sprite of the map exit button.</param>
        public void SetExitButtonSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the map exit button.\n");
            }
            else
            {
                exitButtonSprite = sprite;
                exitButtonImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Returns the color of the map exit button.
        /// </summary>
        public Color GetExitButtonColor()
        {
            return exitButtonColor;
        }

        /// <summary>
        /// Sets the color of the map exit button.
        /// </summary>
        /// <param name="color">New color of the map exit button.</param>
        public void SetExitButtonColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the map exit button.\n");
            }
            else
            {
                exitButtonColor = color;
                exitButtonImage.color = color;
            }
        }

        /// <summary>
        /// Returns the position of the map exit button.
        /// </summary>
        public Vector2 GetExitButtonPosition()
        {
            return exitButtonPosition;
        }

        /// <summary>
        /// Sets the position of the map exit button.
        /// </summary>
        /// <param name="position">New position of the map exit button.</param>
        public void SetExitButtonPosition(Vector2 position)
        {
            if (position == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the map exit button.\n");
            }
            else
            {
                exitButtonPosition = position;
                exitButtonRect.anchoredPosition = (Vector3)position;
            }
        }

        /// <summary>
        /// Returns the scale of the exit button.
        /// </summary>
        public Vector2 GetExitButtonScale()
        {
            return exitButtonScale;
        }

        /// <summary>
        /// Sets the scale of the exit button.
        /// </summary>
        /// <param name="scale">New scale of the exit button.</param>
        public void SetExitButtonScale(Vector2 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the map exit button.\n");
            }
            else
            {
                exitButtonScale = scale;
                exitButtonRect.sizeDelta = scale;
            }
        }

        /// <summary>
        /// Initializes the map exit button.
        /// </summary>
        private void InitializeExitButton()
        {
            SetExitButtonColor(exitButtonColor);
            SetExitButtonPosition(exitButtonPosition);
            SetExitButtonScale(exitButtonScale);
        }

        /// <summary>
        /// Sets the Render Texture of the map.
        /// </summary>
        /// <param name="renderTexture">New Render Texture of the map.</param>
        public void SetRenderTexture(RenderTexture renderTexture)
        {
            if (renderTexture != null)
            {
                this.renderTexture = renderTexture;
            }
        }

        /// <summary>
        /// Returns true if the minimap is set to be disabled when the map is enabled.
        /// </summary>
        public bool DoesMapDisablesMinimap()
        {
            return disableMinimap;
        }

        /// <summary>
        /// Sets the minimap to be disabled when the map is enabled.
        /// </summary>
        public void DisableMinimap()
        {
            disableMinimap = true;

            if (mapEnabled && minimapGameObject != null)
            {
                minimapGameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Sets the minimap NOT to be disabled when the map is enabled.
        /// </summary>
        public void DontDisableMinimap()
        {
            disableMinimap = false;
        }

        /// <summary>
        /// Returns the Minimap GameObject on the Canvas.
        /// </summary>
        public GameObject GetMinimap()
        {
            return minimapGameObject;
        }

        /// <summary>
        /// Sets the Minimap GameObject reference in this Map Manager instance.
        /// </summary>
        /// <param name="minimap">Minimap GameObject reference.</param>
        public void SetMinimap(GameObject minimap)
        {
            if (minimap != null)
            {
                if (minimap.GetComponent<MinimapManager>() != null)
                {
                    minimapGameObject = minimap;
                    minimapManager = minimap.GetComponent<MinimapManager>();

                    if (mapEnabled && disableMinimap)
                    {
                        minimap.SetActive(false);
                    }
                }
                else
                {
                    Debug.LogWarning(errorMessagePrefix + "The GameObject you tried to assign as the Minimap reference doesn't have a Minimap Manager component on it.\n");
                }
            }
        }

    }

    public enum MapClearFlags
    {
        Skybox,
        SolidColor,
        DepthOnly,
        DontClear
    }
}
