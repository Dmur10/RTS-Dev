using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public RectTransform selectionBox;
    private Vector2 startPos,endPos;
    private RaycastHit hit;
    private bool mouseHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            endPos = Input.mousePosition;
            Debug.Log("pressed");
        }
        if (Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                LayerMask layerHit = hit.transform.gameObject.layer;
                switch (layerHit.value)
                {
                    case 7:
                        SelectUnit(hit.transform);
                        break;
                    default:
                        if (mouseHeld == true)
                        {
                            Debug.Log("held");
                            UpdateSelectionBox(Input.mousePosition);
                        } else
                        {
                            DeSelectUnits();
                        }  
                        break;
                }
            }
            mouseHeld = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseHeld = false;
            ReleaseSelectionBox();
        }
    }

    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

       // foreach (Transform unit in Player.instance.units)
       // {
           // Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
           //
           // if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
           //? {
           //     Player.instance.selectedUnits.Add(unit);
           //     unit.Find("highlight").gameObject.SetActive(true);
         //   }
       // }
    }
        

private void UpdateSelectionBox(Vector2 mousePosition)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float diffX = 0; 
        float diffY = 0;
        float anchorX;
        float anchorY;

        if (mousePosition.x < startPos.x)
        {
            diffX = endPos.x - mousePosition.x;
            anchorX = mousePosition.x;
        } else
        {
            diffX = mousePosition.x - startPos.x;
            anchorX = startPos.x;
        }

        if(mousePosition.y < startPos.y)
        {
            diffY = endPos.y - mousePosition.y;
            anchorY = mousePosition.y;
        }
        else
        {
            endPos.y = mousePosition.y;
            diffY = mousePosition.y - startPos.y;
            anchorY = startPos.y;
        }

        selectionBox.sizeDelta = new Vector2(diffX, diffY);
        selectionBox.anchoredPosition = new Vector2(anchorX,anchorY);
    }
 

    void SelectUnit(Transform unit)
    {
        DeSelectUnits();
        Player.instance.selectedUnits.Add(unit);
        unit.Find("highlight").gameObject.SetActive(true);
    }

    void DeSelectUnits()
    {
        //for (int i = 0; i < Player.instance.selectedUnits.Count; i++)
        //{
            //Player.instance.selectedUnits[i].Find("highlight").gameObject.SetActive(false);
        //}
    }
}
