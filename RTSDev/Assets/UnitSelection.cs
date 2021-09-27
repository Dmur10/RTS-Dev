using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public RectTransform selectionBox;
    private Vector2 startPos, endPos;
    private RaycastHit hit;
    private bool mouseHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        startPos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("pressed");
                LayerMask layerHit = hit.transform.gameObject.layer;
                switch (layerHit.value)
                {
                    case 7:
                        SelectUnit(hit.transform);
                        break;
                    default:
                        if (!Input.GetMouseButtonUp(0))
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

        float width = mousePosition.x - startPos.x;
        float height = mousePosition.y - startPos.y;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
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
