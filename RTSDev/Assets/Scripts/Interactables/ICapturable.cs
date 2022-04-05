using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables {
    public class ICapturable : Interactable
    {

        public float offset;
        public GameObject prefab;
        public Transform parent;

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }
   
        public void capture()
        {
            GameObject temp = Instantiate(prefab, transform.position, transform.rotation);
            temp.transform.SetParent(parent);
            Destroy(gameObject);
        }
    }

}

