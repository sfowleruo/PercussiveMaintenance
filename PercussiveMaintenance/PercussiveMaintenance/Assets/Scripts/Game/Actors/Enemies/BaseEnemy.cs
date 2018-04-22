using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EnemyType
{
    GroundNormal,
    GroundFast,
    GroundHeavy,

    FlyingNormal,
    FlyingFast,
    FlyingHeavy,
}

public class BaseEnemy : RenderedActor
{
    public float Speed;
    public EnemyType EnemyType;
    public string ID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    void Move()
    {
        float step = Speed * Time.deltaTime;
       // transform.position = Vector3.MoveTowards(transform.position, .position, step);
    }
}
