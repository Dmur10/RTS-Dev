using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.UI
{
    public class CursorObject : MonoBehaviour
    {

        [SerializeField] private CursorManager.CursorType cursorType;

        private void OnMouseEnter()
        {
            CursorManager.Instance.SetActiveCursorAnimation(cursorType);
        }

        private void OnMouseExit()
        {
            CursorManager.Instance.SetActiveCursorAnimation(CursorManager.CursorType.Arrow);
        }
    }
}

