using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Interactables {
    public class ICapturable : Interactable
    {

        public float offset;
        public GameObject prefab;
        public Transform parent;

        private int captureTick;
        [SerializeField]
        private int captureTickMax;

        public Image Progress;

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }

        public void AddCaptureTick()
        {
            captureTick++;
            Progress.fillAmount = (float)captureTick / captureTickMax;

            if (captureTick >= captureTickMax)
            {
                GameObject temp = Instantiate(prefab, transform.position, transform.rotation);
                temp.transform.SetParent(parent);
                Destroy(gameObject);
            }
        }

        public bool IsCaptured()
        {
            if(captureTick >= captureTickMax)
            {
                return true;
            }
            return false;
        }

        public void capture()
        {
            GameObject temp = Instantiate(prefab, transform.position, transform.rotation);
            temp.transform.SetParent(parent);
            Destroy(gameObject);
        }
    }

}

