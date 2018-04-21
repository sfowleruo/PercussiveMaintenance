using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager
{
    public float MaxHealth;
    [HideInInspector]
    public float Health;


    public override void Init()
    {
        Health = MaxHealth;
        Status = ManagerStatus.Loaded;
    }
}
