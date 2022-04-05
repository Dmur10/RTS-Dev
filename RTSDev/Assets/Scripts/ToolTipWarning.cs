using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame
{
    public class ToolTipWarning : MonoBehaviour
    {
        private static ToolTipWarning instance;

        [SerializeField]
        private RectTransform canvasRectTransform;

        private Text tooltipText;
        private RectTransform backgroundRectTransform;

        private float showTimer;

        private void Awake()
        {
            instance = this;
            backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
            tooltipText = transform.Find("text").GetComponent<Text>();
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
            if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            {
                anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
            }
            transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

            showTimer -= Time.deltaTime;
            if(showTimer <= 0f)
            {
                HideToolTip();
            }
        }

        private void ShowTooltip(string toolTipString, float showTimerMax = 2f)
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            tooltipText.text = toolTipString;
            float textPaddingSize = 4f;
            Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
            backgroundRectTransform.sizeDelta = backgroundSize;
            showTimer = showTimerMax;
        }

        private void HideToolTip()
        {
            gameObject.SetActive(false);
        }

        public static void ShowToolTip_Static(string toolTipString, float showTimerMax = 2f)
        {
            instance.ShowTooltip(toolTipString, showTimerMax);
        }

        public static void HideToolTip_Static()
        {
            instance.HideToolTip();
        }
    }
}

