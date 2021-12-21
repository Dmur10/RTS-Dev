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
            Debug.Log("pressed");
            startPos = Input.mousePosition;
            endPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                LayerMask layerHit = hit.transform.gameObject.layer;
                switch (layerHit.value)
                {
                    case 7:
                        DeSelectUnits();
                        SelectUnit(hit.transform);
                        break; 
                    case 6:
                        DeSelect();
                        SelectBuilding(hit.transform);
                        break;
                    default:
                        DeSelectUnits();
                        mouseHeld = true;
                        break;
                }
            }
        }
        if (Input.GetMouseButton(0) && mouseHeld){
            Debug.Log("held");
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (mouseHeld == true) {
                mouseHeld = false;
                ReleaseSelectionBox();
            }
        }
    }

    private void ReleaseSelectionBox()
    {

        DeSelectUnits();
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition;// - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + selectionBox.sizeDelta;/// 2);

        foreach (Transform unit in Player.instance.units)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position); 
         
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                Player.instance.selectedUnits.Add(unit);
                unit.Find("highlight").gameObject.SetActive(true);
            }
        }
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
        Player.instance.selectedUnits.Add(unit);
        unit.Find("highlight").gameObject.SetActive(true);
    }

    void SelectBuilding(Transform building)
    {
        Player.instance.selectedBuilding = building;
        building.Find("highlight").gameObject.SetActive(true);
    }

    void DeSelectUnits()
    {
        for (int i = 0; i < Player.instance.selectedUnits.Count; i++)
        {
            Player.instance.selectedUnits[i].Find("highlight").gameObject.SetActive(false);
        }
        Player.instance.selectedUnits.Clear();
    }

    void DeSelect()
    {
        DeSelectUnits();
        Player.instance.selectedBuilding.Find("highlight").gameObject.SetActive(false);
    }
}
