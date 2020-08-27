using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    public List<Building> buildings = new List<Building>();

    public void Save_Game()
    {
        buildings = FindObjectsOfType<Building>().ToList();

        if(buildings.Count <= 0)
            return;

        SaveGame.Save<List<Building>>("Buildings", buildings);
    }

    public void Load_Game()
    {

        FindObjectsOfType<Building>().ToList().ForEach(b => Destroy(b.gameObject));

        buildings = SaveGame.Load<List<Building>>("Buildings");

        foreach (var building in buildings)
        {
            Building temp = Instantiate(Resources.Load<GameObject>("Prefabs/" + building.prefabName)).GetComponent<Building>();
            temp.LoadConstructor(building.postition, building.rotation, building.GetColor(), building.layer);
        }
    }

}
