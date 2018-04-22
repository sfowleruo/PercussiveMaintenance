using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SpawnData
{
    public float TimeIntoGame;
    public int SpawnIndex;
    public EnemyType EnemyType;
}

[Serializable]
public class EnemyPrefabMapper
{
    public BaseEnemy GroundNormalPrefab;
}

[Serializable]
public class TowerPrefabManager
{
    public BaseTower KickDrumPrefab;
    public BaseTower HighHatPrefab;
}

public class GameClient : MonoBehaviour
{
    public List<SpawnData> SpawnDataList;
    public List<SpawnPoint> SpawnPoints;

    public List<TowerPoint> TowerPoints;

    public List<BaseTower> Towers;

    public EnemyPrefabMapper EnemyPrefabMapper;
    public TowerPrefabManager TowerPrefabMapper;

    public float GameStartTime;

    public void Awake()
    {
        SortEnemySpawns();

        //DEBUG
        Towers.Add(GetTowerOfType(TowerType.BaseDrum));
        Towers.Add(GetTowerOfType(TowerType.HighHat));

        InitTowers();
        StartGame();
    }

    public void InitTowers()
    {
        for(int i = 0; i < Towers.Count; i++)
        {
            TowerPoints[i].SetTower(Towers[i]);
        }
    }

    public void SortEnemySpawns()
    {
        SpawnDataList.Sort((x, y) => x.TimeIntoGame < y.TimeIntoGame ? -1 : 1);
    }

    public void StartGame()
    {
        SortEnemySpawns();
        GameStartTime = Time.time;
    }

    public void SpawnEnemy(SpawnData data)
    {
        var enemy = GetEnemyOfType(data.EnemyType);
        SpawnPoints[data.SpawnIndex].SpawnEnemy(enemy);
    }

    private void Update()
    {
        while(SpawnDataList.Count > 0 && 
            SpawnDataList[0].TimeIntoGame <= Time.time - GameStartTime)
        {
            SpawnEnemy(SpawnDataList[0]);
            SpawnDataList.RemoveAt(0);
        }
    }

    Dictionary<EnemyType, int> GlobalEnemyTypeCount = new Dictionary<EnemyType, int>();
    Dictionary<TowerType, int> GlobalTowerTypeCount = new Dictionary<TowerType, int>();

    public void IncrementGlobalEnemyCount(EnemyType enemyType)
    {
        if (GlobalEnemyTypeCount.ContainsKey(enemyType))
        {
            GlobalEnemyTypeCount[enemyType]++;
        }
        else
        {
            GlobalEnemyTypeCount[enemyType] = 0;
        }
    }
    public void IncrementGlobalTowerCount(TowerType towerType)
    {
        if (GlobalTowerTypeCount.ContainsKey(towerType))
        {
            GlobalTowerTypeCount[towerType]++;
        }
        else
        {
            GlobalTowerTypeCount[towerType] = 0;
        }
    }

    public BaseEnemy GetEnemyOfType(EnemyType enemyType)
    {
        BaseEnemy newEnemy = null;
        switch (enemyType)
        {
            case EnemyType.GroundNormal:
            {
                newEnemy = Instantiate(EnemyPrefabMapper.GroundNormalPrefab);
                break;
            }
        }
        IncrementGlobalEnemyCount(enemyType);
        var enemyName = enemyType.ToString() + GlobalEnemyTypeCount[enemyType];
        newEnemy.EnemyType = enemyType;
        newEnemy.ID = enemyName;
        newEnemy.gameObject.name = enemyName;
        return newEnemy;
    }

    public BaseTower GetTowerOfType(TowerType towerType)
    {
        BaseTower newTower = null;
        switch (towerType)
        {
            case TowerType.BaseDrum:
                {
                    newTower = GameObject.Instantiate(TowerPrefabMapper.KickDrumPrefab);
                    break;
                }
            case TowerType.HighHat:
                {
                    newTower = GameObject.Instantiate(TowerPrefabMapper.HighHatPrefab);
                    break;
                }
        }
        IncrementGlobalTowerCount(towerType);
        var towerName = towerType.ToString() + GlobalTowerTypeCount[towerType];
        newTower.Data.TowerType = towerType;
        newTower.Data.ID = towerName;
        newTower.gameObject.name = towerName;
        return newTower;
    }
}
