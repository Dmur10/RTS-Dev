using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame
{
    public class ToolTipBox : MonoBehaviour
    {
        private static ToolTipBox instance;

        [SerializeField]
        private RectTransform canvasRectTransform;

        private Text nameText;
        private Text foodAmt;
        private Text scrapAmt;
        private Text fuelAmt;
        private Image image;

        private RectTransform backgroundRectTransform;

        private void Awake()
        {
            instance = this;
            backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
            nameText = transform.Find("nameText").GetComponent<Text>();
            foodAmt = transform.Find("foodAmt").GetComponent<Text>();
            scrapAmt = transform.Find("scrapAmt").GetComponent<Text>();
            fuelAmt = transform.Find("fuelAmt").GetComponent<Text>();
            image = transform.Find("Image").GetComponent<Image>();
            HideToolTip();
        }

        private void Update()
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            transform.localPosition = localPoint;

            Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
            if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            {
                anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
            }
            if (anchoredPosition.y - backgroundRectTransform.rect.height < 0)
            {
                anchoredPosition.y = 0 + backgroundRectTransform.rect.height;
            }
            transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        }

        private void ShowTooltip(string objectName, float foodAmt, float scrapAmt, float fuelAmt)
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            nameText.text = objectName;
            this.foodAmt.text = foodAmt.ToString();
            this.scrapAmt.text = scrapAmt.ToString();
            this.fuelAmt.text = fuelAmt.ToString();
            Update();
        }

        private void HideToolTip()
        {
            gameObject.SetActive(false);
        }

        public static void ShowToolTip_Static(string objectName, float foodAmt, float scrapAmt, float fuelAmt)
        {
            instance.ShowTooltip(objectName, foodAmt, scrapAmt, fuelAmt);
        }

        public static void HideToolTip_Static()
        {
            instance.HideToolTip();
        }
    }
}
