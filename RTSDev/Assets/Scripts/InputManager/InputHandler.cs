using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSGame.Units.Player;
using UnityEngine.EventSystems;

namespace RTSGame.InputManager
{
    public class InputHandler: MonoBehaviour
    {
        public static InputHandler instance;

        public RectTransform selectionBox;
        private Vector2 startPos, endPos;
        private RaycastHit hit;
        private bool mouseHeld = false;

        public List<Transform> selectedUnits = new List<Transform>();
        public Transform selectedBuilding = null;
        public Transform selectedResource = null;

        public LayerMask interactabeLayer = new LayerMask();

        private void Awake()
        {
            instance = this;
        } 

        // Update is called once per frame
        public void HandleUnitMovement()
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {

                    startPos = Input.mousePosition;
                    endPos = Input.mousePosition;

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100, interactabeLayer))
                    {
                        if (AddedUnit(hit.transform, Input.GetKey(KeyCode.LeftShift)))
                        {
                            // use unit ui
                            Interactables.IUnit iUnit = hit.transform.GetComponent<Interactables.IUnit>();

                            if (IsWorkerSelected())
                            {
                                iUnit.GetComponent<Interactables.IWorker>().OnBuilderSelect();
                            }
                        }
                        else if (AddedBuilding(hit.transform))
                        {
                            // use building ui
                        }
                        else if (AddedResource(hit.transform))
                        {
                            // show resource stats
                        }
                    }
                    else
                    {
                        mouseHeld = true;
                        DeSelect();
                    }
                }
            }
            if (Input.GetMouseButton(0) && mouseHeld)
            {
                UpdateSelectionBox(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (mouseHeld == true)
                {
                    mouseHeld = false;
                    ReleaseSelectionBox();
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (HaveSelectedUnits())
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        //check if hit resource
                        if (Physics.Raycast(ray, out hit))
                        {
                            Interactables.IResource iResource = hit.transform.GetComponent<Interactables.IResource>();
                            if (iResource)
                            {
                                if (IsScavengerSelected())
                                {
                                    RTSResources.ResourceSource resource = iResource.gameObject.GetComponent<RTSResources.ResourceSource>();
                                    foreach (Transform unit in selectedUnits)
                                    {
                                        Interactables.IScavenger scavenger = unit.gameObject.GetComponent<Interactables.IScavenger>();
                                        resource.GatherResource(10);
                                        scavenger.GatherResource(10, resource.GetResourceType());
                                    }
                                }
                            }
                            else
                            {
                                foreach (Transform unit in selectedUnits)
                                {
                                    PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                    pU.MoveUnit(hit.point);
                                }
                            }
                        }
                    }
                    if (selectedBuilding != null)
                    {
                        selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().SetSpawnMarkerLocation();
                    }
                }
            }

        }

        private void ReleaseSelectionBox()
        {
            selectionBox.gameObject.SetActive(false);
            DeSelect();


            Vector2 min = selectionBox.anchoredPosition;// - (selectionBox.sizeDelta / 2);
            Vector2 max = selectionBox.anchoredPosition + selectionBox.sizeDelta;/// 2);

            foreach (Transform child in Player.PlayerManager.instance.playerUnits)
            {
                foreach (Transform unit in child)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.position);

                    if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                    {
                        AddedUnit(unit, true);
                    }
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
            }
            else
            {
                diffX = mousePosition.x - startPos.x;
                anchorX = startPos.x;
            }

            if (mousePosition.y < startPos.y)
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
            selectionBox.anchoredPosition = new Vector2(anchorX, anchorY);
        }

        void DeSelect()
        {
            if (selectedBuilding)
            {
                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().OnInteractExit();
                selectedBuilding = null;
            }
            else if (selectedResource)
            {
                selectedResource.gameObject.GetComponent<Interactables.IResource>().OnInteractExit();
                selectedResource = null;
            }
            else if (HaveSelectedUnits())
            {
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    selectedUnits[i].gameObject.GetComponent<Interactables.IUnit>().OnInteractExit();
                }
                selectedUnits.Clear();
            }
        }

        private Interactables.IUnit AddedUnit(Transform tf, bool canMultiSelect = false)
        {
            Interactables.IUnit iUnit = tf.GetComponent<Interactables.IUnit>();
            if (iUnit)
            {
                if (!canMultiSelect)
                {
                    DeSelect();
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

        private Interactables.IBuilding AddedBuilding(Transform tf)
        {
            Interactables.IBuilding iBuilding = tf.GetComponent<Interactables.IBuilding>();
            if (iBuilding)
            {
                DeSelect();

                selectedBuilding = iBuilding.gameObject.transform;

                iBuilding.OnInteractEnter();

                return iBuilding;
            }
            else
            {
                return null;
            }
        }

        private Interactables.IResource AddedResource(Transform tf)
        {
            Interactables.IResource iResource = tf.GetComponent<Interactables.IResource>();
            if (iResource)
            {
                DeSelect();

                selectedResource = iResource.gameObject.transform;

                iResource.OnInteractEnter();

                return iResource;
            }
            else
            {
                return null;
            }
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsWorkerSelected()
        {
            foreach(Transform unit in selectedUnits)
            {
                if (!unit.gameObject.GetComponent<Interactables.IWorker>()  )
                {
                    Debug.Log("here");
                    return false;
                }
            }
            return true;
        }

        private bool IsScavengerSelected()
        {
            foreach (Transform unit in selectedUnits)
            {
                if (!unit.gameObject.GetComponent<Interactables.IScavenger>())
                {
                    Debug.Log("here");
                    return false;
                }
            }
            return true;
        }
    }
}

