using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerData : ScriptableObject
{
    public TowerType TowerType;
    public string ID;
    public float Damage;
    public float Range;
    public Mesh Mesh;
    public Material Material;
    public ColliderType ColliderType;

    public void SetCollider(GameObject go)
    {
       switch (ColliderType)
        {
            case ColliderType.BoxCollider:
                go.AddComponent<BoxCollider>();
                break;
            case ColliderType.SphereCollider:
                go.AddComponent<SphereCollider>();
                break;
            case ColliderType.CapsuleCollider:
                go.AddComponent<CapsuleCollider>();
                break;
            case ColliderType.MeshCollider:
                go.AddComponent<MeshCollider>();
                break;
        }
    }
}

public enum ColliderType
{
    BoxCollider, SphereCollider, CapsuleCollider, MeshCollider
}

