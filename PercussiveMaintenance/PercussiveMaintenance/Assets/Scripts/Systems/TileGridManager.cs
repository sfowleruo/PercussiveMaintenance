using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridManager : Manager
{
    public static TileGridManager Inst;
    public Dictionary<Vector2Int, Tile> Tiles = new Dictionary<Vector2Int, Tile>();
    public Vector2Int Size;
    public int TileSize;

    int NumTiles;

    public override void Init()
    {
        Inst = this;

        for (int y = 0; y < Size.y; y++)
        {
            for (int x = 0; x < Size.x; x++)
            {
                var tile = new Tile(x, y);
                Tiles[new Vector2Int(x, y)] = tile;
            }
        }

        Status = ManagerStatus.Loaded;
    }

    public Tile GetTileAt(Vector3 pos)
    {
        var x = (int)(pos.x / TileSize);
        var y = (int)(pos.z / TileSize);

        if (x > Size.x - 1 || x < 0 || y > Size.y - 1 || y < 0)
        {
            Debug.LogError("Tile (" + x + ", " + y + ") is out of range");
            return null;
        }

        Tile tile = null;
        if (!Tiles.TryGetValue(new Vector2Int(x, y), out tile))
        {
            Debug.LogError("Tile: " + x + " " + y + " is not in the dictionary.");
            return null;
        }

        return tile;
    }

    public void OnDrawGizmos()
    {
        if (Tiles.Count == 0)
            return;

        var tileSize = TileGridManager.Inst.TileSize;
        foreach (var tile in Tiles.Values)
        {
            Gizmos.DrawWireCube(
                tile.GetWorldPos(),
                new Vector3(tileSize, .1f, tileSize) * .999f);//a little padding with .1f
        }
    }
}
