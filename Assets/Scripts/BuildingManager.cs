using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private Building prevObject = null;

    public bool buildingSelected = false;

    public List<GameObject> availableBuildings = new List<GameObject>();

    public Collider[] hit;

    public Color intersectingColor;
    private Color objColor;

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

    public void PreviewBuilding(GameObject selectedBuilding)
    {
        prevObject = Instantiate(selectedBuilding, gameManager.hitPoint, Quaternion.identity).GetComponent<Building>();
        prevObject.SetRotation(Quaternion.FromToRotation(transform.up, gameManager.faceVector) * transform.rotation);
        buildingSelected = true;
    }

    private void MoveBuilding()
    {
        if(prevObject == null)
            return;

        prevObject.SetPosition(gameManager.hitPoint);
        if (prevObject.transform.rotation != Quaternion.FromToRotation(prevObject.transform.up, gameManager.faceVector) * prevObject.transform.rotation)
            prevObject.SetRotation(Quaternion.FromToRotation(prevObject.transform.up, gameManager.faceVector) * prevObject.transform.rotation);
    }

    private void BuildBuilding()
    {
        if(prevObject == null)
            return;
        prevObject.SetLayer(9);
        buildingSelected = false;
        prevObject = null;
        FindObjectOfType<UIManager>().ShowEditor(false);

    }

    private void CancelBuilding()
    {
        Destroy(prevObject.gameObject);
        buildingSelected = false;
        prevObject = null;
        FindObjectOfType<UIManager>().ShowEditor(false);
    }

    private bool IsIntersecting()
    {
        
        hit = Physics.OverlapBox(prevObject.GetCollider().bounds.center, new Vector3(prevObject.GetCollider().bounds.size.x / extentDivider, prevObject.GetCollider().bounds.size.y / extentDivider, prevObject.GetCollider().bounds.size.z / extentDivider), Quaternion.identity);
        

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
        prevObject.ResetColor();
        return false;
    }

    private void OnDrawGizmos()
    {
        if(prevObject != null){
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(prevObject.GetCollider().bounds.center,
                new Vector3(prevObject.GetCollider().bounds.size.x, prevObject.GetCollider().bounds.size.y,
                    prevObject.GetCollider().bounds.size.z));
        }
    }


    public void EditPosition(Building building)
    {
        if(buildingSelected)
            return;
        building.SetLayer(0);
        prevObject = building.GetComponent<Building>();
        buildingSelected = true;
        
    }

    public void RotateBuilding(bool rotateRight)
    {
        if(prevObject == null)
            return;

        if (rotateRight)
        {
            prevObject.transform.Rotate(Vector3.up, 90);
        }
        else
        {
            prevObject.transform.Rotate(Vector3.up, -90);
        }

    }
}
