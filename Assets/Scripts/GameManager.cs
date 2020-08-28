using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Managers
    private BuildingManager buildingManager;
    private UIManager uiManager;

    // Raycast variables
    public LayerMask layerMask;
    public float maxRayDistance;
    public Vector3 hitPoint;
    public Vector3 faceVector;
    private RaycastHit hit;

    // Grid size
    public float gridSize = 1;
    // Currently targeted object by raycast
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
