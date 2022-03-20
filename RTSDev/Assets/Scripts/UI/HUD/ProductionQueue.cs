using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class ProductionQueue : MonoBehaviour
    {
        public static ProductionQueue instance = null;
        [SerializeField]
        private List<Image> images;

        [SerializeField]
        private Image progressBar;

        private void Awake()
        {
            instance = this;
            progressBar.fillAmount = 0;
        }

        public void SetProgressAmount(float amount)
        {
            progressBar.fillAmount = amount;
        }

        public IEnumerator StartProgressCountdown(float Duration)
        {
            float time = 0;
            while(time < Duration)
            {
                time += Time.deltaTime;
                SetProgressAmount(time / Duration);
            }
            yield return null;
        }
    }
}

