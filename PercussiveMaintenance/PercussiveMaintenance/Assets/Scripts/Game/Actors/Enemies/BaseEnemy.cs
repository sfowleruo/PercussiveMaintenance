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
    public List<Vector3> Waypoints;
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
        if(Waypoints.Count == 0)
        {
            return;
        }
        var target = Waypoints[0];
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if(transform.position == target)
        {
            Waypoints.RemoveAt(0);
            if(Waypoints.Count == 0)
            {
                KillSelf();
            }
        }
    }

    public void KillSelf()
    {
        Destroy(gameObject);
        Debug.Log(ID + " Reached End Goal");
    }
}
