using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RTSGame
{
    public class ToolTip : MonoBehaviour
    {
        private Text tooltipText;
        private RectTransform backgroundRectTransform;

        private void Awake()
        {
            backgroundRectTransform = transform.Find("BackGround").GetComponent<RectTransform>();
            tooltipText = transform.Find("Text").GetComponent<Text>();
        }
        public void ShowTooltip()
        {
            gameObject.SetActive(true);

            tooltipText.text = transform.parent.GetComponent<Button>().name;
            float textPaddingSize = 4f;
            Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
            backgroundRectTransform.sizeDelta = backgroundSize;
        }

        public void HideToolTip()
        {
            gameObject.SetActive(false);
        }
    }
}

