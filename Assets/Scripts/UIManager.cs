using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject buildingMenu;

    public Button buildingBoxButton;
    public Button buildingConeButton;
    public Button buildingCylinderButton;


    public GameObject editorMenu;
    public Button editorMove;
    public Button editorColor;
    public Button editorCancel;

    public GameObject colorEditor;
    public Button colorEditorApply;

    public Building selectedBuilding;

    private Color selectedColor;

    // Load Save

    public Button saveButton;
    public Button loadButton;

    private void Start()
    {
        buildingBoxButton.onClick.AddListener(delegate { SelectBuilding(FindObjectOfType<BuildingManager>().availableBuildings[0]);});
        buildingConeButton.onClick.AddListener(delegate { SelectBuilding(FindObjectOfType<BuildingManager>().availableBuildings[1]);});
        buildingCylinderButton.onClick.AddListener(delegate { SelectBuilding(FindObjectOfType<BuildingManager>().availableBuildings[2]);});
        editorCancel.onClick.AddListener(delegate { ShowEditor(false);
            selectedBuilding = null;
        });
        editorMove.onClick.AddListener(delegate { FindObjectOfType<BuildingManager>().EditPosition(selectedBuilding); editorMenu.SetActive(false); });
        
        editorColor.onClick.AddListener(delegate
        {
            editorMenu.SetActive(false);
            buildingMenu.SetActive(false);
            colorEditor.SetActive(true);
        });

        colorEditorApply.onClick.AddListener(delegate
        {
            selectedBuilding.ApplyColor(selectedColor);
            editorMenu.SetActive(true);
            colorEditor.SetActive(false);
        });


        saveButton.onClick.AddListener(delegate { FindObjectOfType<SaveLoadManager>().Save_Game();});
        loadButton.onClick.AddListener(delegate { FindObjectOfType<SaveLoadManager>().Load_Game();});
    }

    public void SelectBuilding(GameObject building)
    {
        buildingMenu.SetActive(false);
        FindObjectOfType<BuildingManager>().PreviewBuilding(building);
        
    }

    public void ShowEditor(bool show, Building selected = null)
    {
        if (show)
        {
            selectedBuilding = selected;
            buildingMenu.SetActive(false);
            editorMenu.SetActive(true);
        }
        else
        {
            buildingMenu.SetActive(true);
            editorMenu.SetActive(false);
            selectedBuilding = selected;
        }
    }

    public void ChangeColor(Color color)
    {
        if (selectedBuilding != null)
        {
            selectedBuilding.SetColor(color);
            selectedColor = color;
        }
    }

}
