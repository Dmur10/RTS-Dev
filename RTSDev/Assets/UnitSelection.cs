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

    public List<Transform> selectedUnits = new List<Transform>();  
    public Transform selectedBuilding = null;

    public LayerMask interactabeLayer = new LayerMask();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {     
            Debug.Log("pressed");
            startPos = Input.mousePosition;
            endPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, interactabeLayer))
            {
                if (addedUnit(hit.transform))
                {
                    // use unit ui
                } else if(addedBuilding(hit.transform))
                {
                    // use building ui
                }
                /*LayerMask layerHit = hit.transform.gameObject.layer;
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
                }*/
            } else
            {
                mouseHeld = true;
                DeSelectUnits();
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
        selectionBox.gameObject.SetActive(false);
        DeSelectUnits();
        

        Vector2 min = selectionBox.anchoredPosition;// - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + selectionBox.sizeDelta;/// 2);

        foreach (Transform unit in Player.instance.units)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position); 
         
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectedUnits.Add(unit);
                unit.gameObject.GetComponent<IUnit>().OnInteractEnter();
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

    void DeSelectUnits()
    {
        if (selectedBuilding) {
            selectedBuilding.gameObject.GetComponent<IBuilding>().OnInteractExit();
            selectedBuilding = null;
        }
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].gameObject.GetComponent<IUnit>().OnInteractExit();
        }
        selectedUnits.Clear();
    }

    private IUnit addedUnit(Transform tf, bool canMultiSelect = false)
    {
        IUnit iUnit = tf.GetComponent<IUnit>();
        if (iUnit)
        {
            if (!canMultiSelect)
            {
                DeSelectUnits();
            }
            selectedUnits.Add(iUnit.gameObject.transform);

            iUnit.OnInteractEnter();
            return iUnit;
        }
        else
        {
            return null;
        }
    }

    private IBuilding addedBuilding(Transform tf)
    {
        IBuilding iBuilding = tf.GetComponent<IBuilding>();
        if (iBuilding)
        {
            DeSelectUnits();

            selectedBuilding = iBuilding.gameObject.transform;

            iBuilding.OnInteractEnter();

            return iBuilding;
        }
        else
        {
            return null;
        }
    }
}
