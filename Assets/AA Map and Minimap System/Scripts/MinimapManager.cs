// This code has been written by AHMET ALP for the Unity Asset "AA Map and Minimap System".
// Link to the asset store page: https://u3d.as/2V0U
// Publisher contact: ahmetalp.business@gmail.com

using UnityEngine;
using UnityEngine.UI;

namespace AAMAP
{
    public class MinimapManager : MonoBehaviour
    {
        [Tooltip("Shape of the minimap inner display.")] public Sprite minimapShape;
        [Tooltip("Opacity of the minimap inner display. Ranged from 0 (inclusive) to 1 (inclusive).")] public float minimapOpacity = 1F;
        [Tooltip("Color of the minimap inner display.")] public Color minimapColor = Color.white;

        [Tooltip("If this is true, \"North, East, South and West\" signs will be displayed on the minimap.")] public bool displayDirections = false;
        [Tooltip("Distance from the center of the minimap to the positions of the direction signs.")] public float directionsDistance = 220F;
        [Tooltip("Rotation value of the direction signs.")] public float directionsRotation = 0F;
        [Tooltip("Text font of the direction signs.")] public Font directionsTextFont = null;
        [Tooltip("Text font size of the direction signs.")] public int directionsTextFontSize = 40;
        [Tooltip("Text color of the direction signs.")] public Color directionsTextColor = Color.white;
        [Tooltip("If this is true, the direction signs will have background images.")] public bool directionsHaveBackground = false;
        [Tooltip("Background image scales of the direction signs.")] public Vector2 directionsBackgroundImageScale = new Vector2(40F, 40F);
        [Tooltip("Background image sprites of the direction signs. You can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Direction Backgrounds\".")] public Sprite directionsBackgroundSprite = null;
        [Tooltip("Background image color of the direction signs.")] public Color directionsBackgroundColor = Color.black;
        [Tooltip("If this is true, the North direction sign will be displayed on the minimap.")] public bool displayNorth = true;
        [Tooltip("If this is true, the East direction sign will be displayed on the minimap.")] public bool displayEast = true;
        [Tooltip("If this is true, the South direction sign will be displayed on the minimap.")] public bool displaySouth = true;
        [Tooltip("If this is true, the West direction sign will be displayed on the minimap.")] public bool displayWest = true;

        [Tooltip("If this is true, the minimap will have a border.")] public bool haveBorder = false;
        [Tooltip("Sprite of the minimap border. You can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Minimap Borders\".")] public Sprite borderSprite = null;
        [Tooltip("Color of the minimap border.")] public Color borderColor = Color.white;
        [Tooltip("Rotation value of the minimap border.")] public float borderRotation = 0F;
        [Tooltip("If this is true, the minimap border is going to rotate with the inner display.")] public bool rotateWithDisplay = false;

        [Tooltip("If this is true, the minimap is going to have zoom in and zoom out buttons on it.")] public bool haveZoomButtons = false;
        [Tooltip("The zooming sensitivity is the strength of the zoom in and zoom out actions.")] public float zoomingSensitivity = 5F;
        [Tooltip("Minimum range the player can get while zooming in.")] public float minimumRange = 0F;
        [Tooltip("Maximum range the player can get while zooming out.")] public float maximumRange = 1000F;
        [Tooltip("Sprite of the zoom in button. You can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".")] public Sprite zoomInButtonSprite = null;
        [Tooltip("Sprite of the zoom out button. You can locate these sprites at \"Assets > AA Map and Minimap System > Sprites > Zooming Icons\".")] public Sprite zoomOutButtonSprite = null;
        [Tooltip("Position of the zoom in button.")] public Vector2 zoomInButtonPosition = new Vector2(-65F, 0F);
        [Tooltip("Scale of the zoom in button.")] public Vector2 zoomInButtonScale = new Vector2(50F, 50F);
        [Tooltip("Position of the zoom out button.")] public Vector2 zoomOutButtonPosition = new Vector2(0F, 0F);
        [Tooltip("Scale of the zoom out button.")] public Vector2 zoomOutButtonScale = new Vector2(50F, 50F);
        [Tooltip("Color of the zoom in button.")] public Color zoomInButtonColor = Color.white;
        [Tooltip("Color of the zoom out button.")] public Color zoomOutButtonColor = Color.white;
        
        [Tooltip("If this is true, a grid will be displayed on the minimap.")] public bool displayGrid = false;
        [Tooltip("Sprite of the minimap grid. You can find these sprites at \"Assets > AA Map and Minimap System > Sprites > Grids\".")] public Sprite gridSprite = null;
        [Tooltip("Color of the minimap grid.")] public Color gridColor = Color.white;
        [Tooltip("Opacity of the minimap grid.")] public float gridOpacity = 1F;
        [Tooltip("Scale of the minimap grid.")] public Vector3 gridScale = Vector3.one;
        [Tooltip("Rotation value of the minimap grid.")] public float gridRotation = 0F;
        [Tooltip("If this is true, the minimap grid will rotate with the camera.")] public bool gridRotateWithCamera = false;

        [Tooltip("If you assign a GameObject to this, the minimap camera is going to follow it. Most of the time, the Target Object is the player GameObject. Having a Target Object is optional.")] public GameObject targetObject = null;
        [Tooltip("If this is true, the minimap camera is going to rotate on the Y axis with the target GameObject.")] public bool rotateWithTarget = true;
        [Tooltip("Default rotation value of the camera.")] public float defaultRotation = 0F;

        [Tooltip("The \"Minimap Camera\" GameObject. This GameObject is generated with the Minimap.")] public GameObject minimapCamera;
        [Tooltip("Position of the minimap camera on the Y axis. It is recommended to set this value above the shadow distance in your scene.")] public float minimapHeight = 50F;
        [Tooltip("The closest point to the Minimap Camera where drawing occurs.")] public float nearClippingPlane = 0.3F;
        [Tooltip("The furthest point from the Minimap Camera that drawing occurs.")] public float farClippingPlane = 1000;
        [Tooltip("Range of the minimap camera. Increase this value to display larger parts on the map.")] public float minimapRange = 150F;
        [Tooltip("What to display in empty areas of the minimap camera's view. Choose Skybox to display a skybox in the empty areas, defaulting to a background color if no skybox is found. Choose Solid Color to display a solid background color in empty areas. Choose Depth Only to display nothing in empty areas. Choose Don't Clear to display whatever was displayed in the previous frame in empty areas.")] public MinimapClearFlags clearFlags = MinimapClearFlags.Skybox;
        [Tooltip("Minimap camera clears the screen to this color before rendering.")] public Color backgroundColor = Color.white;

        [Tooltip("Raw Image component on the \"Minimap > Minimap Mask > Minimap Display\" GameObject.")] private RawImage displayImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Mask\" GameObject.")] private Image maskImage = null;
        [Tooltip("\"Minimap > Minimap Directions\" GameObject.")] private GameObject minimapDirectionsObject = null;
        [Tooltip("\"Minimap > Minimap Border\" GameObject.")] private GameObject minimapBorderObject = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Border\" GameObject.")] private RectTransform minimapBorderRect = null;
        [Tooltip("Image component on the \"Minimap > Minimap Border\" GameObject.")] private Image minimapBorderImage = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Border\" GameObject.")] private RectTransform minimapDirectionsRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > North\" GameObject.")] private RectTransform northRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > East\" GameObject.")] private RectTransform eastRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > South\" GameObject.")] private RectTransform southRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Directions > West\" GameObject.")] private RectTransform westRect = null;
        [Tooltip("\"Minimap > Minimap Directions > North\" GameObject.")] private GameObject northObject = null;
        [Tooltip("\"Minimap > Minimap Directions > East\" GameObject.")] private GameObject eastObject = null;
        [Tooltip("\"Minimap > Minimap Directions > South\" GameObject.")] private GameObject southObject = null;
        [Tooltip("\"Minimap > Minimap Directions > West\" GameObject.")] private GameObject westObject = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > North\" GameObject.")] private Text northText = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > East\" GameObject.")] private Text eastText = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > South\" GameObject.")] private Text southText = null;
        [Tooltip("Text component on the \"Minimap > Minimap Directions > West\" GameObject.")] private Text westText = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > North\" GameObject.")] private Image northImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > East\" GameObject.")] private Image eastImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > South\" GameObject.")] private Image southImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Directions > West\" GameObject.")] private Image westImage = null;
        [Tooltip("Camera component on the \"Minimap Camera\" GameObject.")] private Camera minimapCameraComponent = null;
        [Tooltip("Transform component on the \"Minimap Camera\" GameObject.")] private Transform minimapCameraTransform = null;
        [Tooltip("Transform component on the target GameObject.")] private Transform targetTransform = null;
        [Tooltip("\"Minimap > Minimap Zoom Buttons\" GameObject")] private GameObject minimapZoomButtons = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject.")] private RectTransform zoomInRect = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject.")] private RectTransform zoomOutRect = null;
        [Tooltip("Image component on the \"Minimap > Minimap Zoom Buttons > Zoom In Button\" GameObject.")] private Image zoomInImage = null;
        [Tooltip("Image component on the \"Minimap > Minimap Zoom Buttons > Zoom Out Button\" GameObject.")] private Image zoomOutImage = null;
        [Tooltip("\"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private GameObject gridObject = null;
        [Tooltip("Image component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private Image gridImage = null;
        [Tooltip("Rect Transform component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject.")] private RectTransform gridRect = null;
        [Tooltip("Image component on the \"Minimap > Minimap Mask > Minimap Background Filler\" GameObject.")] private Image backgroundFillerImage = null;

        [Tooltip("Render Texture of the minimap.")] public RenderTexture renderTexture;

        private readonly string errorMessagePrefix = "<color=orange>AA Map and Minimap System : </color>";
        private readonly string errorMessageSuffix = " Please delete and re-create the minimap.\n";

        private void Start()
        {
            FindMaskImage();
            FindDisplayRawImage();
            FindBackgroundFillerImage();
            FindDirectionsObject();
            FindDirectionsRectTransform();
            FindNESWComponents();
            FindBorderObject();
            FindBorderRectTransform();
            FindBorderImage();
            FindZoomButtonsObject();
            FindZoomButtonsRectAndImage();
            FindGridComponents();
            FindCameraComponents();
            FindTargetTransform();
            
            SetMinimapShape(minimapShape);
            SetMinimapOpacity(minimapOpacity);
            SetMinimapColor(minimapColor);
            InitializeDirections();
            InitializeBorder();
            InitializeZoomButtons();
            InitializeGrid();
            InitializeCamera();
            SetClearFlags(clearFlags);
        }

        private void Update()
        {
            if (targetObject != null && minimapCamera != null)
            {
                minimapCameraTransform.position = new Vector3(targetTransform.position.x, minimapHeight, targetTransform.position.z);

                if (rotateWithTarget)
                {
                    minimapCameraTransform.rotation = Quaternion.Euler(90F, targetTransform.eulerAngles.y + defaultRotation, 0F);

                    if (displayDirections)
                    {
                        minimapDirectionsRect.localRotation = Quaternion.Euler(0F, 0F, minimapCameraTransform.eulerAngles.y + directionsRotation);
                        northRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                        eastRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                        southRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                        westRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                    }
                }
            }

            if (displayGrid && gridRotateWithCamera)
            {
                gridRect.localRotation = Quaternion.Euler(0F, 0F, minimapCameraTransform.eulerAngles.y + gridRotation);
            }

            if (haveBorder && rotateWithDisplay)
            {
                minimapBorderRect.localRotation = Quaternion.Euler(0F, 0F, minimapCameraTransform.eulerAngles.y + borderRotation);
            }
        }

        /// <summary>
        /// Disables the minimap.
        /// </summary>
        private void DisableMinimap()
        {
            if (minimapCamera != null)
            {
                minimapCamera.SetActive(false);
            }

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Finds the Image component on the Minimap Mask GameObject.
        /// </summary>
        private void FindMaskImage()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Minimap Mask"))
                {
                    maskImage = transform.GetChild(i).GetComponent<Image>();
                    break;
                }
            }

            if (maskImage == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > Minimap Mask\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Finds the Raw Image component on the Minimap Display GameObject.
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
                    if (maskImage.transform.GetChild(i).name.Equals("Minimap Display"))
                    {
                        displayImage = maskImage.transform.GetChild(i).GetComponent<RawImage>();
                        break;
                    }
                }
            }

            if (displayImage == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Raw Image component on the \"" + gameObject.name + " > Minimap Mask > Minimap Display\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Finds the Image component on the Minimap Background Filler GameObject.
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
                    if (maskImage.transform.GetChild(i).name.Equals("Minimap Background Filler"))
                    {
                        backgroundFillerImage = maskImage.transform.GetChild(i).GetComponent<Image>();
                        break;
                    }
                }
            }

            if (backgroundFillerImage == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > Minimap Mask > Minimap Background Filler\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Returns the shape of the minimap.
        /// </summary>
        public Sprite GetMinimapShape()
        {
            return minimapShape;
        }

        /// <summary>
        /// Sets the shape of the minimap.
        /// </summary>
        /// <param name="shape">New shape of the minimap.</param>
        public void SetMinimapShape(Sprite shape)
        {
            if (shape == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the shape of the minimap.\n");
                return;
            }

            if (maskImage == null)
            {
                FindMaskImage();
            }

            minimapShape = shape;

            if (maskImage != null)
            {
                maskImage.sprite = shape;
            }
        }

        /// <summary>
        /// Returns the opacity of the inner display. Ranged from 0 (inclusive) to 1 (inclusive).
        /// </summary>
        public float GetMinimapOpacity()
        {
            return minimapOpacity;
        }

        /// <summary>
        /// Sets the opacity of the inner display.
        /// </summary>
        /// <param name="opacity">New opacity of the inner display. Ranged from 0 (inclusive) to 1 (inclusive).</param>
        public void SetMinimapOpacity(float opacity)
        {
            if (displayImage == null)
            {
                FindDisplayRawImage();
            }

            if (opacity < 0F)
            {
                opacity = 0F;
            }
            else if (opacity > 1F)
            {
                opacity = 1F;
            }

            minimapOpacity = opacity;
            displayImage.color = new Color(minimapColor.r, minimapColor.g, minimapColor.b, opacity);
            backgroundFillerImage.color = new Color(0F, 0F, 0F, opacity);
        }

        /// <summary>
        /// Returns the color of the minimap inner display.
        /// </summary>
        public Color GetMinimapColor()
        {
            return minimapColor;
        }

        /// <summary>
        /// Sets the color of the minimap inner display.
        /// </summary>
        /// <param name="color">New color of the minimap inner display.</param>
        public void SetMinimapColor(Color color)
        {
            if (color == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the minimap inner display.\n");
            }
            else
            {
                minimapColor = color;

                if (displayImage == null)
                {
                    FindDisplayRawImage();
                }

                if (displayImage != null)
                {
                    displayImage.color = new Color(color.r, color.g, color.b, minimapOpacity);
                }
            }
        }

        /// <summary>
        /// Finds the Minimap Directions GameObject.
        /// </summary>
        private void FindDirectionsObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Minimap Directions"))
                {
                    minimapDirectionsObject = transform.GetChild(i).gameObject;
                    break;
                }
            }

            if (minimapDirectionsObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > Minimap Directions\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Finds the Rect Transform component on the Minimap Directions GameObject.
        /// </summary>
        private void FindDirectionsRectTransform()
        {
            if (minimapDirectionsObject != null)
            {
                minimapDirectionsRect = minimapDirectionsObject.GetComponent<RectTransform>();

                if (minimapDirectionsRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > Minimap Directions\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
        }

        /// <summary>
        /// Returns true if the minimap direction signs are enabled and returns false if the minimap direction signs are disabled.
        /// </summary>
        public bool AreDirectionsEnabled()
        {
            return displayDirections;
        }

        /// <summary>
        /// Enables the direction signs on the minimap.
        /// </summary>
        public void EnableDirections()
        {
            displayDirections = true;
            InitializeDirections();
        }

        /// <summary>
        /// Disables the direction signs on the minimap.
        /// </summary>
        public void DisableDirections()
        {
            displayDirections = false;
            minimapDirectionsObject.SetActive(false);
        }

        /// <summary>
        /// Initializes the minimap directions.
        /// </summary>
        private void InitializeDirections()
        {
            if (displayDirections)
            {
                minimapDirectionsObject.SetActive(true);

                northRect.anchoredPosition = new Vector3(0F, directionsDistance, 0F);
                eastRect.anchoredPosition = new Vector3(directionsDistance, 0F, 0F);
                southRect.anchoredPosition = new Vector3(0F, directionsDistance * -1F, 0F);
                westRect.anchoredPosition = new Vector3(directionsDistance * -1F, 0F, 0F);

                northRect.sizeDelta = directionsBackgroundImageScale;
                eastRect.sizeDelta = directionsBackgroundImageScale;
                southRect.sizeDelta = directionsBackgroundImageScale;
                westRect.sizeDelta = directionsBackgroundImageScale;

                northObject.SetActive(displayNorth);
                eastObject.SetActive(displayEast);
                southObject.SetActive(displaySouth);
                westObject.SetActive(displayWest);

                if (directionsTextFont != null)
                {
                    northText.font = directionsTextFont;
                    eastText.font = directionsTextFont;
                    southText.font = directionsTextFont;
                    westText.font = directionsTextFont;
                }

                northText.fontSize = directionsTextFontSize;
                eastText.fontSize = directionsTextFontSize;
                southText.fontSize = directionsTextFontSize;
                westText.fontSize = directionsTextFontSize;

                if (directionsTextColor != null)
                {
                    northText.color = directionsTextColor;
                    eastText.color = directionsTextColor;
                    southText.color = directionsTextColor;
                    westText.color = directionsTextColor;
                }

                if (directionsHaveBackground && directionsBackgroundSprite != null)
                {
                    northImage.sprite = directionsBackgroundSprite;
                    eastImage.sprite = directionsBackgroundSprite;
                    southImage.sprite = directionsBackgroundSprite;
                    westImage.sprite = directionsBackgroundSprite;

                    northImage.color = directionsBackgroundColor;
                    eastImage.color = directionsBackgroundColor;
                    southImage.color = directionsBackgroundColor;
                    westImage.color = directionsBackgroundColor;
                }
                else
                {
                    northImage.color = Color.clear;
                    eastImage.color = Color.clear;
                    southImage.color = Color.clear;
                    westImage.color = Color.clear;
                }

                northImage.enabled = (displayDirections && displayNorth);
                northText.enabled = (displayDirections && displayNorth);
                eastImage.enabled = (displayDirections && displayEast);
                eastText.enabled = (displayDirections && displayEast);
                southImage.enabled = (displayDirections && displaySouth);
                southText.enabled = (displayDirections && displaySouth);
                westImage.enabled = (displayDirections && displayWest);
                westText.enabled = (displayDirections && displayWest);
            }
            else
            {
                minimapDirectionsObject.SetActive(false);
            }
        }

        /// <summary>
        /// Finds the Rect Transform, Text and Image components on the North, East, South and West GameObjects.
        /// </summary>
        private void FindNESWComponents()
        {
            if (minimapDirectionsObject == null)
            {
                return;
            }

            for (int i = 0; i < minimapDirectionsObject.transform.childCount; i++)
            {
                if (minimapDirectionsObject.transform.GetChild(i).name.Equals("North"))
                {
                    northObject = minimapDirectionsObject.transform.GetChild(i).gameObject;
                    northRect = minimapDirectionsObject.transform.GetChild(i).transform.GetComponent<RectTransform>();
                    northText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                    northImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                }
                else if (minimapDirectionsObject.transform.GetChild(i).name.Equals("East"))
                {
                    eastObject = minimapDirectionsObject.transform.GetChild(i).transform.gameObject;
                    eastRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                    eastText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                    eastImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                }
                else if (minimapDirectionsObject.transform.GetChild(i).name.Equals("South"))
                {
                    southObject = minimapDirectionsObject.transform.GetChild(i).transform.gameObject;
                    southRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                    southText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                    southImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                }
                else if (minimapDirectionsObject.transform.GetChild(i).name.Equals("West"))
                {
                    westObject = minimapDirectionsObject.transform.GetChild(i).transform.gameObject;
                    westRect = minimapDirectionsObject.transform.GetChild(i).GetComponent<RectTransform>();
                    westText = minimapDirectionsObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>();
                    westImage = minimapDirectionsObject.transform.GetChild(i).GetComponent<Image>();
                }
            }

            if (northObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > North\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
            else
            {
                if (northRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > North\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (northText == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Text component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > North > Text\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (northImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > North\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }

            if (eastObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > East\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
            else
            {
                if (eastRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > East\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (eastText == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Text component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > East > Text\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (eastImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > East\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }

            if (southObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > South\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
            else
            {
                if (southRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > South\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (southText == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Text component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > South > Text\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (southImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > South\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }

            if (westObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > West\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
            else
            {
                if (westRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > West\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (westText == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Text component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > West > Text\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (westImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > " + minimapDirectionsObject.name + " > West\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
        }

        /// <summary>
        /// Returns the distance from the center of the minimap to the positions of the direction signs.
        /// </summary>
        public float GetDirectionsDistance()
        {
            return directionsDistance;
        }

        /// <summary>
        /// Sets the distance from the center of the minimap to the positions of the direction signs.
        /// </summary>
        /// <param name="distance">New distance of the minimap directions.</param>
        public void SetDirectionsDistance(float distance)
        {
            directionsDistance = distance;

            northRect.anchoredPosition = new Vector3(0F, distance, 0F);
            eastRect.anchoredPosition = new Vector3(distance, 0F, 0F);
            southRect.anchoredPosition = new Vector3(0F, distance * -1F, 0F);
            westRect.anchoredPosition = new Vector3(distance * -1F, 0F, 0F);
        }

        /// <summary>
        /// Returns the rotation value of the minimap direction signs.
        /// </summary>
        public float GetDirectionsRotation()
        {
            return directionsRotation;
        }

        /// <summary>
        /// Sets the rotation value of the minimap direction signs
        /// </summary>
        /// <param name="rotation">New rotation value of the minimap direction signs. From -360 (inclusive) to 360 (inclusive).</param>
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

            if (minimapCamera != null && !rotateWithTarget)
            {
                minimapDirectionsRect.localRotation = Quaternion.Euler(0F, 0F, minimapCamera.transform.eulerAngles.y + defaultRotation);

                northRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
                eastRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
                southRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
                westRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCamera.transform.eulerAngles.y + defaultRotation) * -1F);
            }
        }

        /// <summary>
        /// Returns the text font of the minimap direction signs.
        /// </summary>
        public Font GetDirectionsFont()
        {
            return directionsTextFont;
        }

        /// <summary>
        /// Sets the text font of the minimap direction signs.
        /// </summary>
        /// <param name="font">New text font of the minimap direction signs.</param>
        public void SetDirectionsFont(Font font)
        {
            if (font != null)
            {
                directionsTextFont = font;

                northText.font = font;
                eastText.font = font;
                southText.font = font;
                westText.font = font;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new text font of the minimap direction signs.\n");
            }
        }

        /// <summary>
        /// Returns the text font size of the minimap direction signs.
        /// </summary>
        public float GetDirectionsFontSize()
        {
            return directionsTextFontSize;
        }

        /// <summary>
        /// Sets the text font size of the minimap direction signs.
        /// </summary>
        /// <param name="fontSize">New text font size of the minimap direction signs.</param>
        public void SetDirectionsFontSize(int fontSize)
        {
            if (fontSize < 0)
            {
                fontSize = 0;
            }

            directionsTextFontSize = fontSize;

            northText.fontSize = fontSize;
            eastText.fontSize = fontSize;
            southText.fontSize = fontSize;
            westText.fontSize = fontSize;
        }

        /// <summary>
        /// Returns the text color of the minimap direction signs.
        /// </summary>
        public Color GetDirectionsTextColor()
        {
            return directionsTextColor;
        }

        /// <summary>
        /// Sets the text color of the minimap direction signs.
        /// </summary>
        /// <param name="color">New text color of the minimap direction signs.</param>
        public void SetDirectionsTextColor(Color color)
        {
            if (color != null)
            {
                directionsTextColor = color;

                northText.color = color;
                eastText.color = color;
                southText.color = color;
                westText.color = color;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new text color of the minimap direction signs.\n");
            }
        }

        /// <summary>
        /// Returns true if the direction signs have a background image and returns false if the direction signs don't have a background image.
        /// </summary>
        public bool DoesDirectionsHaveBackgroundImages()
        {
            return directionsHaveBackground;
        }

        /// <summary>
        /// Enables the background images for the minimap direction signs.
        /// </summary>
        public void EnableDirectionsBackgroundImages()
        {
            directionsHaveBackground = true;

            northImage.color = directionsBackgroundColor;
            eastImage.color = directionsBackgroundColor;
            southImage.color = directionsBackgroundColor;
            westImage.color = directionsBackgroundColor;

            if (directionsBackgroundSprite != null)
            {
                northImage.sprite = directionsBackgroundSprite;
                eastImage.sprite = directionsBackgroundSprite;
                southImage.sprite = directionsBackgroundSprite;
                westImage.sprite = directionsBackgroundSprite;
            }
        }

        /// <summary>
        /// Disables the background images of the minimap direction signs.
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
            if (scale != null)
            {
                directionsBackgroundImageScale = scale;

                northRect.sizeDelta = directionsBackgroundImageScale;
                eastRect.sizeDelta = directionsBackgroundImageScale;
                southRect.sizeDelta = directionsBackgroundImageScale;
                westRect.sizeDelta = directionsBackgroundImageScale;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the directions background images.\n");
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
            if (sprite != null)
            {
                directionsBackgroundSprite = sprite;

                northImage.sprite = sprite;
                eastImage.sprite = sprite;
                southImage.sprite = sprite;
                westImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the directions background images.\n");
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
            if (color != null)
            {
                directionsBackgroundColor = color;

                northImage.color = color;
                eastImage.color = color;
                southImage.color = color;
                westImage.color = color;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the directions background images.\n");
            }
        }

        /// <summary>
        /// Enables the North sign on the minimap.
        /// </summary>
        public void EnableNorthSign()
        {
            displayNorth = true;
            northObject.SetActive(true);
        }

        /// <summary>
        /// Disables the North sign on the minimap.
        /// </summary>
        public void DisableNorthSign()
        {
            displayNorth = false;
            northObject.SetActive(false);
        }

        /// <summary>
        /// Enables the East sign on the minimap.
        /// </summary>
        public void EnableEastSign()
        {
            displayEast = true;
            eastObject.SetActive(true);
        }

        /// <summary>
        /// Disables the East sign on the minimap.
        /// </summary>
        public void DisableEastSign()
        {
            displayEast = false;
            eastObject.SetActive(false);
        }

        /// <summary>
        /// Enables the South sign on the minimap.
        /// </summary>
        public void EnableSouthSign()
        {
            displaySouth = true;
            southObject.SetActive(true);
        }

        /// <summary>
        /// Disables the South sign on the minimap.
        /// </summary>
        public void DisableSouthSign()
        {
            displaySouth = false;
            southObject.SetActive(false);
        }

        /// <summary>
        /// Enables the West sign on the minimap.
        /// </summary>
        public void EnableWestSign()
        {
            displayWest = true;
            westObject.SetActive(true);
        }

        /// <summary>
        /// Disables the West sign on the minimap.
        /// </summary>
        public void DisableWestSign()
        {
            displayWest = false;
            westObject.SetActive(false);
        }

        /// <summary>
        /// Returns true if the North icon is enabled and returns false if the North icon is disabled.
        /// </summary>
        public bool IsNorthEnabled()
        {
            return displayNorth;
        }

        /// <summary>
        /// Returns true if the East icon is enabled and returns false if the East icon is disabled.
        /// </summary>
        public bool IsEastEnabled()
        {
            return displayEast;
        }

        /// <summary>
        /// Returns true if the South icon is enabled and returns false if the South icon is disabled.
        /// </summary>
        public bool IsSouthEnabled()
        {
            return displaySouth;
        }

        /// <summary>
        /// Returns true if the West icon is enabled and returns false if the West icon is disabled.
        /// </summary>
        public bool IsWestEnabled()
        {
            return displayWest;
        }

        /// <summary>
        /// Finds the Minimap Border GameObject.
        /// </summary>
        private void FindBorderObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Minimap Border"))
                {
                    minimapBorderObject = transform.GetChild(i).gameObject;
                    break;
                }
            }

            if (minimapBorderObject == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > Minimap Border\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Finds the Rect Transform component on the Minimap Border GameObject.
        /// </summary>
        private void FindBorderRectTransform()
        {
            if (minimapBorderObject != null)
            {
                minimapBorderRect = minimapBorderObject.GetComponent<RectTransform>();

                if (minimapBorderRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > Minimap Border\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
        }

        /// <summary>
        /// Finds the Image component on the Minimap Border GameObject.
        /// </summary>
        private void FindBorderImage()
        {
            if (minimapBorderObject != null)
            {
                minimapBorderImage = minimapBorderObject.GetComponent<Image>();

                if (minimapBorderImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > Minimap Border\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
        }

        /// <summary>
        /// Returns true if the minimap border is enabled and returns false if the minimap border is disabled.
        /// </summary>
        public bool IsBorderEnabled()
        {
            return haveBorder;
        }

        /// <summary>
        /// Enables the minimap border.
        /// </summary>
        public void EnableBorder()
        {
            haveBorder = true;
            minimapBorderObject.SetActive(true);

            InitializeBorder();
        }

        /// <summary>
        /// Disables the minimap border.
        /// </summary>
        public void DisableBorder()
        {
            haveBorder = false;
            minimapBorderObject.SetActive(false);
        }

        /// <summary>
        /// Returns the sprite of the minimap border.
        /// </summary>
        public Sprite GetBorderSprite()
        {
            return borderSprite;
        }

        /// <summary>
        /// Sets the sprite of the minimap border.
        /// </summary>
        /// <param name="sprite">New sprite of the minimap border.</param>
        public void SetBorderSprite(Sprite sprite)
        {
            if (sprite != null)
            {
                borderSprite = sprite;
                minimapBorderImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the minimap border.\n");
            }
        }

        /// <summary>
        /// Returns the color of the minimap border.
        /// </summary>
        public Color GetBorderColor()
        {
            return borderColor;
        }

        /// <summary>
        /// Sets the color of the minimap border.
        /// </summary>
        /// <param name="color">New color of the minimap border.</param>
        public void SetBorderColor(Color color)
        {
            if (color != null)
            {
                borderColor = color;
                minimapBorderImage.color = color;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the minimap border.\n");
            }
        }

        /// <summary>
        /// Returns the rotation value of the minimap border.
        /// </summary>
        public float GetBorderRotation()
        {
            return borderRotation;
        }

        /// <summary>
        /// Sets the rotation value of the minimap border.
        /// </summary>
        /// <param name="rotation">New rotation value of the minimap border. From -360 (inclusive) to 360 (inclusive).</param>
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
            minimapBorderRect.rotation = Quaternion.Euler(0F, 0F, rotation);
        }

        /// <summary>
        /// Returns true if the minimap border rotates with the inner display and returns false if the minimap border doesn't rotate with the inner display.
        /// </summary>
        public bool DoesBorderRotate()
        {
            return rotateWithDisplay;
        }

        /// <summary>
        /// Makes the minimap border rotate with the inner display.
        /// </summary>
        public void EnableBorderRotation()
        {
            rotateWithDisplay = true;
        }

        /// <summary>
        /// Disables the minimap border from rotating with the inner display.
        /// </summary>
        public void DisableBorderRotation()
        {
            rotateWithDisplay = false;
        }

        /// <summary>
        /// Initializes the minimap border.
        /// </summary>
        private void InitializeBorder()
        {
            if (haveBorder)
            {
                minimapBorderObject.SetActive(true);
                minimapBorderImage.enabled = true;

                if (borderSprite != null)
                {
                    minimapBorderImage.sprite = borderSprite;
                    minimapBorderImage.color = borderColor;

                    if (rotateWithDisplay)
                        minimapBorderRect.localRotation = Quaternion.Euler(0F, 0F, minimapCameraTransform.eulerAngles.y + borderRotation);
                    else
                        minimapBorderRect.localRotation = Quaternion.Euler(0F, 0F, borderRotation);
                }
            }
            else
            {
                minimapBorderObject.SetActive(false);
            }
        }

        /// <summary>
        /// Finds the Minimap Zoom Buttons GameObject.
        /// </summary>
        private void FindZoomButtonsObject()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.Equals("Minimap Zoom Buttons"))
                {
                    minimapZoomButtons = transform.GetChild(i).gameObject;
                    break;
                }
            }

            if (minimapZoomButtons == null)
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"" + gameObject.name + " > Minimap Zoom Buttons\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Finds the Rect Transform and Image components on the Zoom In Button and Zoom Out Button GameObjects.
        /// </summary>
        private void FindZoomButtonsRectAndImage()
        {
            if (minimapZoomButtons != null)
            {
                for (int i = 0; i < minimapZoomButtons.transform.childCount; i++)
                {
                    if (minimapZoomButtons.transform.GetChild(i).name.Equals("Zoom In Button"))
                    {
                        zoomInRect = minimapZoomButtons.transform.GetChild(i).transform.GetComponent<RectTransform>();
                        zoomInImage = minimapZoomButtons.transform.GetChild(i).transform.GetComponent<Image>();
                    }
                    else if (minimapZoomButtons.transform.GetChild(i).name.Equals("Zoom Out Button"))
                    {
                        zoomOutRect = minimapZoomButtons.transform.GetChild(i).transform.GetComponent<RectTransform>();
                        zoomOutImage = minimapZoomButtons.transform.GetChild(i).transform.GetComponent<Image>();
                    }
                }

                if (zoomInRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > " + minimapZoomButtons.name + " > " + "Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (zoomInImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > " + minimapZoomButtons.name + " > " + "Zoom In Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (zoomOutRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"" + gameObject.name + " > " + minimapZoomButtons.name + " > " + "Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (zoomOutImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"" + gameObject.name + " > " + minimapZoomButtons.name + " > " + "Zoom Out Button\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
        }

        /// <summary>
        /// Returns true if the minimap zoom buttons are enabled and returns false if the minimap zoom buttons are disabled.
        /// </summary>
        public bool AreZoomButtonsEnabled()
        {
            return haveZoomButtons;
        }

        /// <summary>
        /// Enables the zoom buttons on the minimap.
        /// </summary>
        public void EnableZoomButtons()
        {
            haveZoomButtons = true;
            InitializeZoomButtons();
        }

        /// <summary>
        /// Disables the zoom buttons on the minimap.
        /// </summary>
        public void DisableZoomButtons()
        {
            haveZoomButtons = false;
            minimapZoomButtons.SetActive(false);
        }

        /// <summary>
        /// Returns the minimap zooming sensitivity.
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
        /// Returns the minimum range of the minimap while zooming in.
        /// </summary>
        public float GetMinimumRange()
        {
            return minimumRange;
        }

        /// <summary>
        /// Sets the minimum range while zooming in on the minimap.
        /// </summary>
        /// <param name="minimumRange">New minimum range of the minimap.</param>
        public void SetMinimumRange(float minimumRange)
        {
            this.minimumRange = minimumRange;
        }

        /// <summary>
        /// Returns the maximum range of the minimap while zooming out.
        /// </summary>
        public float GetMaximumRange()
        {
            return maximumRange;
        }

        /// <summary>
        /// Sets the maximum range while zooming out of the minimap.
        /// </summary>
        /// <param name="maximumRange">New maximum range of the minimap.</param>
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
            if (sprite != null)
            {
                zoomInButtonSprite = sprite;
                zoomInImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the zoom in button.\n");
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
            if (sprite != null)
            {
                zoomOutButtonSprite = sprite;
                zoomOutImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the zoom out button.\n");
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
            if (position != null)
            {
                zoomInButtonPosition = position;
                zoomInRect.anchoredPosition = (Vector3)zoomInButtonPosition;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the zoom in button.\n");
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
            if (scale != null)
            {
                zoomInButtonScale = scale;
                zoomInRect.sizeDelta = scale;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the zoom in button.\n");
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
            if (position != null)
            {
                zoomOutButtonPosition = position;
                zoomOutRect.anchoredPosition = (Vector3)zoomOutButtonPosition;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new position of the zoom out button.\n");
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
            if (scale != null)
            {
                zoomOutButtonScale = scale;
                zoomOutRect.sizeDelta = scale;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the zoom out button.\n");
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
            if (color != null)
            {
                zoomInButtonColor = color;

                if (zoomInImage == null)
                {
                    FindZoomButtonsRectAndImage();
                }

                zoomInImage.color = color;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the zoom in button.\n");
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
            if (color != null)
            {
                zoomOutButtonColor = color;

                if (zoomOutImage == null)
                {
                    FindZoomButtonsRectAndImage();
                }

                zoomOutImage.color = color;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the zoom out button.\n");
            }
        }

        /// <summary>
        /// Initializes the minimap zoom buttons.
        /// </summary>
        private void InitializeZoomButtons()
        {
            minimapZoomButtons.SetActive(haveZoomButtons);

            zoomInRect.sizeDelta = zoomInButtonScale;
            zoomOutRect.sizeDelta = zoomOutButtonScale;

            if (zoomInButtonSprite != null)
                zoomInImage.sprite = zoomInButtonSprite;

            if (zoomOutButtonSprite != null)
                zoomOutImage.sprite = zoomOutButtonSprite;

            zoomInRect.anchoredPosition = (Vector3)zoomInButtonPosition;
            zoomOutRect.anchoredPosition = (Vector3)zoomOutButtonPosition;

            zoomInImage.color = zoomInButtonColor;
            zoomOutImage.color = zoomOutButtonColor;

            zoomInImage.enabled = true;
            zoomOutImage.enabled = true;
        }

        /// <summary>
        /// Zooms in on the minimap.
        /// </summary>
        /// <param name="zoomingValue">The strength of the zooming action. Keep in mind that you cannot go lower than the minimum range.</param>
        public void ZoomIn(int zoomingValue)
        {
            if (minimapRange - zoomingValue >= minimumRange)
            {
                minimapRange -= zoomingValue;
            }
            else
            {
                minimapRange = minimumRange;
            }

            if (minimapCameraComponent == null)
            {
                FindCameraComponents();
            }

            minimapCameraComponent.orthographicSize = minimapRange;
        }

        /// <summary>
        /// Zooms out on the Minimap.
        /// </summary>
        /// <param name="zoomingValue">The strength of the zooming action. Keep in mind that you cannot go higher than the maximum range.</param>
        public void ZoomOut(int zoomingValue)
        {
            if (minimapRange + zoomingValue <= maximumRange)
            {
                minimapRange += zoomingValue;
            }
            else
            {
                minimapRange = maximumRange;
            }
            
            if (minimapCameraComponent == null)
            {
                FindCameraComponents();
            }

            minimapCameraComponent.orthographicSize = minimapRange;
        }

        /// <summary>
        /// Zooms in on the minimap. This method is called when the player press the zoom in button but it can also be called independently.
        /// </summary>
        public void ZoomIn()
        {
            if (minimapRange - zoomingSensitivity >= minimumRange)
            {
                minimapRange -= zoomingSensitivity;
            }
            else
            {
                minimapRange = minimumRange;
            }

            if (minimapCameraComponent == null)
            {
                FindCameraComponents();
            }

            minimapCameraComponent.orthographicSize = minimapRange;
        }

        /// <summary>
        /// This method zooms out on the Minimap. This method is called when the player press the zoom out button but it can also be called independently.
        /// </summary>
        public void ZoomOut()
        {
            if (minimapRange + zoomingSensitivity <= maximumRange)
            {
                minimapRange += zoomingSensitivity;
            }
            else
            {
                minimapRange = maximumRange;
            }

            if (minimapCameraComponent == null)
            {
                FindCameraComponents();
            }

            minimapCameraComponent.orthographicSize = minimapRange;
        }

        /// <summary>
        /// Finds all the necessary GameObjects and components about the Minimap Grid.
        /// </summary>
        private void FindGridComponents()
        {
            if (maskImage == null)
            {
                return;
            }

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
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"Minimap > Minimap Mask > Minimap Grid\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
            else
            {
                gridImage = gridObject.GetComponent<Image>();
                gridRect = gridObject.GetComponent<RectTransform>();

                if (gridImage == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Image component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }

                if (gridRect == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Rect Transform component on the \"Minimap > Minimap Mask > Minimap Grid\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
        }

        /// <summary>
        /// Returns true if the minimap grid is enabled and returns false if the minimap grid is disabled
        /// </summary>
        public bool IsGridEnabled()
        {
            return displayGrid;
        }

        /// <summary>
        /// Enables the minimap grid.
        /// </summary>
        public void EnableGrid()
        {
            displayGrid = true;
            InitializeGrid();
        }

        /// <summary>
        /// Disables the minimap grid.
        /// </summary>
        public void DisableGrid()
        {
            displayGrid = false;
            gridObject.SetActive(false);
        }

        /// <summary>
        /// Returns the sprite of the minimap grid.
        /// </summary>
        public Sprite GetGridSprite()
        {
            return gridImage.sprite;
        }

        /// <summary>
        /// Sets the sprite of the minimap grid.
        /// </summary>
        /// <param name="sprite">New sprite of the minimap grid.</param>
        public void SetGridSprite(Sprite sprite)
        {
            if (sprite != null)
            {
                gridSprite = sprite;
                gridImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new sprite of the minimap grid.\n");
            }
        }

        /// <summary>
        /// Returns the color of the minimap grid.
        /// </summary>
        public Color GetGridColor()
        {
            return gridImage.color;
        }

        /// <summary>
        /// Sets the color of the minimap grid.
        /// </summary>
        /// <param name="color">New color of the minimap grid.</param>
        public void SetGridColor(Color color)
        {
            if (color != null)
            {
                gridColor = new Color(color.r, color.g, color.b, gridOpacity);
                gridImage.color = new Color(color.r, color.g, color.b, gridOpacity);
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new color of the minimap grid.\n");
            }
        }

        /// <summary>
        /// Returns the opacity of the minimap grid. Ranged from 0 (inclusive) to 1 (inclusive).
        /// </summary>
        public float GetGridOpacity()
        {
            return gridImage.color.a;
        }

        /// <summary>
        /// Sets the opacity of the minimap grid.
        /// </summary>
        /// <param name="opacity">New opacity of the minimap grid. Ranged 0 (inclusive) and 1 (inclusive)./param>
        public void SetGridOpacity(float opacity)
        {
            if (opacity < 0F)
            {
                opacity = 0F;
            }

            if (opacity > 1F)
            {
                opacity = 1F;
            }

            gridOpacity = opacity;
            gridImage.color = new Color(gridColor.r, gridColor.g, gridColor.b, opacity);
        }

        /// <summary>
        /// Returns the scale of the minimap grid.
        /// </summary>
        public Vector3 GetGridScale()
        {
            return gridScale;
        }

        /// <summary>
        /// Sets the scale of the minimap grid.
        /// </summary>
        /// <param name="scale">New scale of the minimap grid.</param>
        public void SetGridScale(Vector3 scale)
        {
            if (scale == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new scale of the minimap grid.\n");
            }
            else
            {
                gridScale = scale;
                gridRect.sizeDelta = scale;
            }
        }

        /// <summary>
        /// Returns the rotation value of the minimap grid.
        /// </summary>
        public float GetGridRotation()
        {
            return gridRotation;
        }

        /// <summary>
        /// Sets the rotation value of the minimap grid.
        /// </summary>
        /// <param name="rotation">New rotation value of the minimap grid.</param>
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
        /// Returns true if the minimap grid rotates with the Minimap Camera and returns false if the minimap grid doesn't rotate with the Minimap Camera.
        /// </summary>
        public bool DoesGridRotatesWithCamera()
        {
            return gridRotateWithCamera;
        }

        /// <summary>
        /// Enables the minimap grid to rotate with the Minimap Camera.
        /// </summary>
        public void RotateGridWithCamera()
        {
            gridRotateWithCamera = true;
        }

        /// <summary>
        /// Disables the minimap grid from rotating with the Minimap Camera.
        /// </summary>
        public void DontRotateGridWithCamera()
        {
            gridRotateWithCamera = false;
        }

        /// <summary>
        /// Initializes the minimap grid.
        /// </summary>
        private void InitializeGrid()
        {
            gridImage.sprite = gridSprite;
            gridImage.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            gridRect.localScale = gridScale;

            if (gridRotateWithCamera && minimapCamera != null)
            {
                gridRect.localRotation = Quaternion.Euler(0F, 0F, minimapCameraTransform.eulerAngles.y + gridRotation);
            }
            else
            {
                gridRect.localRotation = Quaternion.Euler(0F, 0F, gridRotation);
            }

            gridObject.SetActive(displayGrid);
        }

        /// <summary>
        /// Finds the Transform component on the Target GameObject.
        /// </summary>
        private void FindTargetTransform()
        {
            if (targetObject != null)
            {
                targetTransform = targetObject.GetComponent<Transform>();
            }
        }

        /// <summary>
        /// Returns the target GameObject of the minimap.
        /// </summary>
        public GameObject GetTargetObject()
        {
            return targetObject;
        }

        /// <summary>
        /// Sets the target GameObject of the minimap. The minimap camera is going to follow the target GameObject.
        /// </summary>
        /// <param name="target">New target GameObject of the minimap.</param>
        public void SetTargetObject(GameObject target)
        {
            targetObject = target;

            if (targetObject != null)
            {
                targetTransform = target.transform;
            }
            else
            {
                targetTransform = null;
            }
        }

        /// <summary>
        /// Returns true if the minimap camera rotates with the target GameObject and returns false if the minimap camera doesn't rotate with the target GameObject.
        /// </summary>
        public bool DoesCameraRotateWithTarget()
        {
            return rotateWithTarget;
        }

        /// <summary>
        /// This method enables the minimap to rotate with the target.
        /// </summary>
        public void EnableRotationWithTarget()
        {
            rotateWithTarget = true;
        }

        /// <summary>
        /// This method disables the minimap from rotating with the target.
        /// </summary>
        public void DisableRotationWithTarget()
        {
            rotateWithTarget = false;
        }

        /// <summary>
        /// Returns the default rotation value of the minimap camera.
        /// </summary>
        public float GetCameraDefaultRotation()
        {
            return defaultRotation;
        }

        /// <summary>
        /// Sets the default rotation value of the minimap camera.
        /// </summary>
        /// <param name="defaultRotation">New default rotation value of the minimap camera. From -360 (inclusive) to 360 (inclusive).</param>
        public void SetCameraDefaultRotation(float defaultRotation)
        {
            if (defaultRotation < -360F)
            {
                defaultRotation %= -360F;
            }

            if (defaultRotation > 360F)
            {
                defaultRotation %= 360F;
            }

            this.defaultRotation = defaultRotation;

            if (targetObject == null || !rotateWithTarget)
            {
                minimapCameraTransform.rotation = Quaternion.Euler(90F, defaultRotation, 0F);
            }
        }

        /// <summary>
        /// Finds the Transform and the Camera components on the Minimap Camera GameObject.
        /// </summary>
        private void FindCameraComponents()
        {
            if (minimapCamera != null)
            {
                minimapCameraTransform = minimapCamera.GetComponent<Transform>();
                minimapCameraComponent = minimapCamera.GetComponent<Camera>();

                if (minimapCameraComponent == null)
                {
                    Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the Camera component on the \"Minimap Camera\" GameObject could not be found." + errorMessageSuffix);
                    DisableMinimap();
                }
            }
            else
            {
                Debug.LogError(errorMessagePrefix + "Failed to generate the minimap because the \"Minimap Camera\" GameObject could not be found." + errorMessageSuffix);
                DisableMinimap();
            }
        }

        /// <summary>
        /// Returns the minimap camera.
        /// </summary>
        public GameObject GetCamera()
        {
            return minimapCamera;
        }

        /// <summary>
        /// Sets the camera of the minimap. The minimap camera can be found in the Hierarchy. By default, the minimap camera has already been created and assigned.
        /// </summary>
        /// <param name="camera">New minimap camera of the minimap.</param>
        public void SetCamera(GameObject camera)
        {
            if (camera == null)
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null GameObject as the new minimap camera.\n");
            }
            else
            {
                if (camera.GetComponent<Camera>() == null)
                {
                    Debug.LogWarning(errorMessagePrefix + "The GameObject you want to assign as the new minimap camera doesn't have a Camera component on it.\n");
                }
                else
                {
                    minimapCamera = camera;
                    minimapCameraComponent = camera.GetComponent<Camera>();
                    minimapCameraTransform = camera.transform;

                    if (minimapCameraComponent != null && renderTexture != null)
                    {
                        minimapCameraComponent.targetTexture = renderTexture;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the height of the minimap camera. Minimap camera's position on the Y axis.
        /// </summary>
        /// <returns>Height of the minimap camera.</returns>
        public float GetCameraHeight()
        {
            return minimapHeight;
        }

        /// <summary>
        /// Sets the height of the minimap camera. Minimap camera's position on the Y axis. It is recommended to set this value above the shadow distance.
        /// </summary>
        /// <param name="height">New height of the camera.</param>
        public void SetCameraHeight(float height)
        {
            minimapHeight = height;

            if (targetObject == null)
            {
                minimapCameraTransform.position = new Vector3(minimapCameraTransform.position.x, height, minimapCameraTransform.position.z);
            }
        }

        /// <summary>
        /// Returns the near clipping plane of the minimap camera.
        /// </summary>
        public float GetCameraNearClippingPlane()
        {
            return nearClippingPlane;
        }

        /// <summary>
        /// Sets the near clipping plane of the minimap camera.
        /// </summary>
        /// <param name="nearClippingPlane">New near clipping plane of the minimap camera.</param>
        public void SetCameraNearClippingPlane(float nearClippingPlane)
        {
            this.nearClippingPlane = nearClippingPlane;

            if (minimapCameraComponent != null)
            {
                minimapCameraComponent.nearClipPlane = nearClippingPlane;
            }
        }

        /// <summary>
        /// Returns the far clipping plane of the minimap camera.
        /// </summary>
        public float GetCameraFarClippingPlane()
        {
            return farClippingPlane;
        }

        /// <summary>
        /// Sets the far clipping plane of the minimap camera.
        /// </summary>
        /// <param name="farClippingPlane">New far clipping plane of the minimap camera.</param>
        public void SetCameraFarClippingPlane(float farClippingPlane)
        {
            this.farClippingPlane = farClippingPlane;

            if (minimapCameraComponent != null)
            {
                minimapCameraComponent.farClipPlane = farClippingPlane;
            }
        }

        /// <summary>
        /// Returns the range of the minimap camera.
        /// </summary>
        public float GetCameraRange()
        {
            return minimapRange;
        }

        /// <summary>
        /// Sets the range of the minimap camera.
        /// </summary>
        /// <param name="range">New range of the minimap camera.</param>
        public void SetCameraRange(float range)
        {
            minimapRange = range;

            if (minimapCameraComponent != null)
            {
                minimapCameraComponent.orthographicSize = range;
            }
        }

        /// <summary>
        /// Returns the clear flags of the minimap camera.
        /// </summary>
        public MinimapClearFlags GetClearFlags()
        {
            return clearFlags;
        }

        /// <summary>
        /// Sets the Clear Flags of the minimap camera.
        /// </summary>
        /// <param name="clearFlags">New Clear Flags of the camera.</param>
        public void SetClearFlags(MinimapClearFlags clearFlags)
        {
            this.clearFlags = clearFlags;

            if (minimapCameraComponent != null)
            {
                if (this.clearFlags == MinimapClearFlags.Skybox)
                {
                    minimapCameraComponent.clearFlags = CameraClearFlags.Skybox;
                    minimapCameraComponent.backgroundColor = backgroundColor;
                }
                else if (this.clearFlags == MinimapClearFlags.SolidColor)
                {
                    minimapCameraComponent.clearFlags = CameraClearFlags.SolidColor;
                    minimapCameraComponent.backgroundColor = backgroundColor;
                }
                else if (this.clearFlags == MinimapClearFlags.DepthOnly)
                {
                    minimapCameraComponent.clearFlags = CameraClearFlags.Depth;
                }
                else if (this.clearFlags == MinimapClearFlags.DontClear)
                {
                    minimapCameraComponent.clearFlags = CameraClearFlags.Nothing;
                }
            }
        }

        /// <summary>
        /// Returns the background color of the minimap camera.
        /// </summary>
        public Color GetCameraBackgroundColor()
        {
            return backgroundColor;
        }

        /// <summary>
        /// Sets the background color of the minimap camera.
        /// </summary>
        /// <param name="color">New background color of the minimap camera.</param>
        public void SetCameraBackgroundColor(Color color)
        {
            if (color != null)
            {
                backgroundColor = color;

                if (minimapCameraComponent != null)
                {
                    minimapCameraComponent.backgroundColor = color;
                }
            }
            else
            {
                Debug.LogWarning(errorMessagePrefix + "You cannot assign a null value as the new background color of the minimap camera.\n");
            }
        }

        /// <summary>
        /// Initializes the minimap camera.
        /// </summary>
        private void InitializeCamera()
        {
            if (targetObject != null && minimapCamera != null)
            {
                minimapCameraTransform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + minimapHeight, targetTransform.position.z);

                if (rotateWithTarget)
                {
                    minimapCameraTransform.rotation = Quaternion.Euler(90F, targetTransform.eulerAngles.y + defaultRotation, 0F);
                    minimapDirectionsRect.localRotation = Quaternion.Euler(0F, 0F, minimapCameraTransform.eulerAngles.y + directionsRotation);
                    northRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                    eastRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                    southRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                    westRect.localRotation = Quaternion.Euler(0F, 0F, (minimapCameraTransform.eulerAngles.y + directionsRotation) * -1F);
                }
            }

            if (targetObject == null && minimapCamera != null)
            {
                minimapCameraTransform.SetPositionAndRotation(new Vector3(minimapCameraTransform.position.x, minimapHeight, minimapCameraTransform.position.z), Quaternion.Euler(90F, defaultRotation, 0F));
            }

            if (minimapCamera != null)
            {
                minimapCameraComponent.orthographicSize = minimapRange;
                minimapCameraComponent.nearClipPlane = nearClippingPlane;
                minimapCameraComponent.farClipPlane = farClippingPlane;
            }

            if (minimapCameraComponent != null && renderTexture != null)
            {
                minimapCameraComponent.targetTexture = renderTexture;
            }
        }

        /// <summary>
        /// Sets the Render Texture of the minimap.
        /// </summary>
        /// <param name="renderTexture">New Render Texture of the minimap.</param>
        public void SetRenderTexture(RenderTexture renderTexture)
        {
            if (renderTexture != null)
            {
                this.renderTexture = renderTexture;
            }
        }

        /// <summary>
        /// Disables the Minimap when the Map is enabled.
        /// </summary>
        public void DisableMinimapForMapEnabling()
        {
            maskImage.gameObject.SetActive(false);
            minimapBorderObject.SetActive(false);
            minimapDirectionsObject.SetActive(false);
            minimapZoomButtons.SetActive(false);

            minimapBorderImage.enabled = false;

            northImage.enabled = false;
            northText.enabled = false;
            eastImage.enabled = false;
            eastText.enabled = false;
            southImage.enabled = false;
            southText.enabled = false;
            westImage.enabled = false;
            westText.enabled = false;

            zoomInImage.enabled = false;
            zoomOutImage.enabled = false;
        }

        /// <summary>
        /// Enables the Minimap when the Map is disabled.
        /// </summary>
        public void EnableMinimapForMapDisabling()
        {
            maskImage.gameObject.SetActive(true);
            minimapBorderObject.SetActive(true);
            minimapDirectionsObject.SetActive(true);
            minimapZoomButtons.SetActive(true);

            minimapBorderImage.enabled = haveBorder;

            northImage.enabled = (displayDirections && displayNorth);
            northText.enabled = (displayDirections && displayNorth);
            eastImage.enabled = (displayDirections && displayEast);
            eastText.enabled = (displayDirections && displayEast);
            southImage.enabled = (displayDirections && displaySouth);
            southText.enabled = (displayDirections && displaySouth);
            westImage.enabled = (displayDirections && displayWest);
            westText.enabled = (displayDirections && displayWest);

            zoomInImage.enabled = true;
            zoomOutImage.enabled = true;
        }
    }

    public enum MinimapClearFlags
    {
        Skybox,
        SolidColor,
        DepthOnly,
        DontClear
    }
}
