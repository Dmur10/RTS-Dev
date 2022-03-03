using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables {
    public class ICapturable : Interactable
    {
        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }

        public virtual void capture()
        {
            throw new NotImplementedException();
        }
    }

}

