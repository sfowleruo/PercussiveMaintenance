using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    public BaseTower CurrentTower { get; private set; }

    public void SetTower(BaseTower tower)
    {
        CurrentTower = tower;
        tower.transform.position = transform.position;
    }
}
