using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private GameManager gameManager;
    private Building prevObject = null;
    private Collider[] hit;

    public bool buildingSelected = false;
    // The color that will be used to color the selected object in the event of an intersection with an obstacle
    public Color intersectingColor;

    // Variable used to resize OrevlapBox bounds so that it would be just slightly smaller than the object
    public float extentDivider;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (buildingSelected)
        {
            MoveBuilding();
            if (!IsIntersecting() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                BuildBuilding();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelBuilding();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                RotateBuilding(false);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                RotateBuilding(true);
            }
        }

    }

    /// <summary>
    /// Preview selected object
    /// </summary>
    /// <param name="selectedBuilding">Building ref</param>
    public void PreviewBuilding(GameObject selectedBuilding)
    {
        prevObject = Instantiate(selectedBuilding, gameManager.hitPoint, Quaternion.identity).GetComponent<Building>();
        prevObject.SetRotation((Quaternion.FromToRotation(transform.up, gameManager.faceVector) * transform.rotation).eulerAngles);
        buildingSelected = true;
    }
    /// <summary>
    /// Move the selected building to match the mouse position
    /// </summary>
    private void MoveBuilding()
    {
        if(prevObject == null)
            return;

        prevObject.SetPosition(gameManager.hitPoint);
        if (prevObject.transform.rotation != Quaternion.FromToRotation(prevObject.transform.up, gameManager.faceVector) * prevObject.transform.rotation)
            prevObject.SetRotation((Quaternion.FromToRotation(prevObject.transform.up, gameManager.faceVector) * prevObject.transform.rotation).eulerAngles);
            //prevObject.SetRotation((Quaternion.FromToRotation(prevObject.transform.up, gameManager.faceVector) * prevObject.transform.rotation).eulerAngles);
    }

    /// <summary>
    /// Finalize the building process (Build it)
    /// </summary>
    private void BuildBuilding()
    {
        if(prevObject == null)
            return;
        prevObject.SetLayer(9);
        buildingSelected = false;
        prevObject = null;
        FindObjectOfType<UIManager>().ShowEditor(false);

    }
    /// <summary>
    /// Cancel/Delete currently selected building
    /// </summary>
    private void CancelBuilding()
    {
        Destroy(prevObject.gameObject);
        buildingSelected = false;
        prevObject = null;
        FindObjectOfType<UIManager>().ShowEditor(false);
    }
    /// <summary>
    /// Checks whether the building is intersecting with something
    /// </summary>
    /// <returns>Returns true if the building is intersecting with the ground and.or other buildings</returns>
    private bool IsIntersecting()
    {
        
        hit = Physics.OverlapBox(prevObject.GetCollider().bounds.center, new Vector3(prevObject.GetCollider().bounds.size.x / extentDivider, prevObject.GetCollider().bounds.size.y / extentDivider, prevObject.GetCollider().bounds.size.z / extentDivider), Quaternion.identity);
        
        Debug.Log("UP: " + (prevObject.transform.up != Vector3.up));

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.GetComponent<Collider>() != prevObject.GetCollider())
            {
                if (hit[i].transform.gameObject.GetComponent<Building>() || hit[i].transform.gameObject.layer == 9)
                {
                    prevObject.SetColor(intersectingColor);
                    return true;
                }
            }
        }

        if (!prevObject.canBePlacedOnWall && prevObject.transform.up != Vector3.up)
        {
            prevObject.SetColor(intersectingColor);
            return true;
        }

        prevObject.ResetColor();
        return false;
    }

    /// <summary>
    /// Enables manipulation of the previously built buildings
    /// </summary>
    /// <param name="building">selected building</param>
    public void EditPosition(Building building)
    {
        if(buildingSelected)
            return;
        building.SetLayer(0);
        prevObject = building.GetComponent<Building>();
        buildingSelected = true;
        
    }
    /// <summary>
    /// Rotate selected building on Y axis by either 90 or -90 degree
    /// </summary>
    /// <param name="rotateRight">if rotateRight is true, then building will be rotated by 90 degree on Y axis, else rotated by -90 degree on Y axis</param>
    public void RotateBuilding(bool rotateRight)
    {
        if(prevObject == null)
            return;

        if (rotateRight)
        {
            prevObject.transform.Rotate(Vector3.up, 90);
            prevObject.SetRotation(prevObject.transform.rotation.eulerAngles);
        }
        else
        {
            prevObject.transform.Rotate(Vector3.up, -90);
            prevObject.SetRotation(prevObject.transform.rotation.eulerAngles);
        }

    }
}
