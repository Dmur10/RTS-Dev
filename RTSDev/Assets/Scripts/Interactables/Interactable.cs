using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public bool isInteracting = false;
        public GameObject highlight = null;

        public virtual void Awake()
        {
            highlight.SetActive(false);
        }

        public virtual void OnInteractEnter()
        {
            ShowHighLight();
            isInteracting = true;
        }

        public virtual void OnInteractExit()
        {
            HideHighLight();
            isInteracting = false;
        }

        private void ShowHighLight()
        {
            highlight.SetActive(true);
        }

        private void HideHighLight()
        {
            highlight.SetActive(false);
        }
    }
}

