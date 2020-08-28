using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject buildingMenu;

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

    public GameObject container;
    public GameObject dynamicButton;

    private Vector3 nextButtonPos;


    private void Start()
    {
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

        PopulateButtons();
    }
    /// <summary>
    /// Selects building for manipulating it
    /// </summary>
    /// <param name="building">Building ref</param>
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

    /// <summary>
    /// Function used with ColorPicker plugin
    /// </summary>
    /// <param name="color"></param>
    public void ChangeColor(Color color)
    {
        if (selectedBuilding != null)
        {
            selectedBuilding.SetColor(color);
            selectedColor = color;
        }
    }

    /// <summary>
    /// Dynamically populated building selection with available buildings from "Resources/Prefabs" folder
    /// </summary>
    private void PopulateButtons()
    {
        List<GameObject> items = Resources.LoadAll<GameObject>("Prefabs").ToList();
        container.GetComponent<RectTransform>().sizeDelta = new Vector2(179.4f, dynamicButton.GetComponent<RectTransform>().rect.height*items.Count);
        container.GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, -dynamicButton.GetComponent<RectTransform>().rect.height*items.Count);
        nextButtonPos = new Vector3(container.transform.position.x, container.transform.position.y + (container.GetComponent<RectTransform>().rect.height / 2) + 25, container.transform.position.z);
        foreach (var item in items)
        {
            Button btn = Instantiate(dynamicButton).GetComponent<Button>();
            btn.transform.SetParent(container.transform, false);
            nextButtonPos -= new Vector3(0, btn.GetComponent<RectTransform>().rect.height);
            btn.transform.position = nextButtonPos;
            btn.transform.GetChild(0).GetComponent<Text>().text = item.GetComponent<Building>().Name;
            btn.onClick.AddListener(delegate { SelectBuilding(item);});
        }

    }

}
