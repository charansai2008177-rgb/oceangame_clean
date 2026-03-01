using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryFramework
{
    public class ItemTooltip : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public Image icon;
        public RectTransform rectTransform;
        public Vector2 offset;

        private Canvas rootCanvas;

        void Awake()
        {
            rootCanvas = GetComponentInParent<Canvas>();
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            Hide();
        }

        public void Show(Item item, Vector2 screenPos)
        {
            if (item == null) return;

            nameText.text = item.itemName;
            descriptionText.text = item.description;
            icon.sprite = item.icon;

            gameObject.SetActive(true);
            UpdatePosition(screenPos);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdatePosition(Vector2 screenPos)
        {
            Vector2 localPoint;
            Camera cam = rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rootCanvas.transform as RectTransform, screenPos, cam, out localPoint);
            rectTransform.anchoredPosition = localPoint + offset;
        }
    }

}
