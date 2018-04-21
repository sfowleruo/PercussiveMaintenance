using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : RenderedActor
{
    public TowerData Data;
	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<MeshFilter>().mesh = Data.Mesh;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
