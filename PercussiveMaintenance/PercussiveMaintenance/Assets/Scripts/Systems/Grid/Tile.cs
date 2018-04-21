using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public Vector2 GridPos;
    public GameObject Object;

    public Tile(int x, int y)
    {
        GridPos.x = x;
        GridPos.y = y;
    }

    public override string ToString()
    {
        return GridPos.ToString();
    }

    public Vector3 GetWorldPos()
    {
        var x = GridPos.x * TileGridManager.Inst.TileSize;
        var y = GridPos.y * TileGridManager.Inst.TileSize;
        return new Vector3(x, 0, y);
    }
}
