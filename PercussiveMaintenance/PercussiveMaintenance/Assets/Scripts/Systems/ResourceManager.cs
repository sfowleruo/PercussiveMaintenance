using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Manager
{
    public static ResourceManager Inst;
    Dictionary<string, TowerData> TowerData = new Dictionary<string, TowerData>();

    GameObject TowerPrefab;
    public override void Init()
    {
        Inst = this;
        LoadTowers();
        Status = ManagerStatus.Loaded;
    }

    void LoadTowers()
    {
        TowerPrefab = Resources.Load<GameObject>("Prefabs/Tower");
        var towers = Resources.LoadAll<TowerData>("DataAssets/TowerData");
        foreach (var tower in towers)
        {
            TowerData[tower.name] = tower;
        }
    }

    public BaseTower GetNewTower(string name)
    {
        BaseTower tower = null;
        TowerData towerData = null;
        if (TowerData.TryGetValue(name, out towerData))
        {
            var towerGO = new GameObject();

            tower = towerGO.AddComponent<BaseTower>();
            tower.Data = towerData;
            tower.name = towerData.ID;

            towerData.SetCollider(towerGO);
            towerGO.AddComponent<MeshFilter>().mesh = towerData.Mesh;
            towerGO.AddComponent<MeshRenderer>().material = towerData.Material;
            
        }else
        {
            Debug.LogError(name + " is not in the dictionary.");
        }
        return tower;
    }
}
