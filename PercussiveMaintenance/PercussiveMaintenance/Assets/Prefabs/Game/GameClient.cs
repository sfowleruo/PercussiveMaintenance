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

public class GameClient : MonoBehaviour
{
    public List<SpawnData> SpawnDataList;
    public List<PH_SpawnPoint> SpawnPoints;

    public float GameStartTime;

    public void Awake()
    {
        SortEnemySpawns();
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

    Dictionary<EnemyType, int> GlobalTypeCount = new Dictionary<EnemyType, int>();

    public void IncrementGlobalEnemyCount(EnemyType enemyType)
    {
        if (GlobalTypeCount.ContainsKey(enemyType))
        {
            GlobalTypeCount[enemyType]++;
        }
        else
        {
            GlobalTypeCount[enemyType] = 1;
        }
    }

    public BaseEnemy GetEnemyOfType(EnemyType enemyType)
    {
        IncrementGlobalEnemyCount(enemyType);
        var newObject = new GameObject();
        var newEnemy = newObject.AddComponent<BaseEnemy>();
        newEnemy.EnemyType = enemyType;
        newEnemy.ID = enemyType.ToString() + GlobalTypeCount[enemyType];
        return newEnemy;
    }
}
