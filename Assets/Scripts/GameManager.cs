using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BuildingManager buildingManager;
    private UIManager uiManager;

    public LayerMask layerMask;
    public float maxRayDistance;

    public Vector3 hitPoint;

    public float gridSize = 1;

    public Vector3 faceVector;
    RaycastHit hit;

    public GameObject targetObject;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

        ProcessMouseInput();

        if (!buildingManager.buildingSelected && targetObject != null)
        {

            if (uiManager.selectedBuilding == null && targetObject.GetComponent<Building>() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Building building = targetObject.GetComponent<Building>();
                uiManager.ShowEditor(true, building);
            }
        }

    }

    private void ProcessMouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxRayDistance, layerMask))
        {
            faceVector = hit.normal;
            hitPoint = new Vector3(Mathf.Round(hit.point.x / gridSize) * gridSize, Mathf.Round(hit.point.y / gridSize) * gridSize, Mathf.Round(hit.point.z / gridSize) * gridSize);
            targetObject = hit.transform.gameObject;
        }
        else
        {
            targetObject = null;
        }
    }

}
