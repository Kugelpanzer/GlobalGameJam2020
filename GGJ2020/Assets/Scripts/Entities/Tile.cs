using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y;

    public TileType type;

    public Tile (int x, int y, TileType tileType)
    {
        this.x = x;
        this.y = y;
        this.type = tileType;
    }

    void UpdateSprite(WallType type)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool areTilesAdjacent (Tile tile1, Tile tile2)
    {
        // horizontal
        if (tile1.x == tile2.x && tile1.y == tile2.y + 1) return true;
        if (tile1.x == tile2.x && tile1.y == tile2.y - 1) return true;
        // slash
        if (tile1.x == tile2.x + 1 && tile1.y == tile2.y) return true;
        if (tile1.x == tile2.x - 1 && tile1.y == tile2.y) return true;
        // backslash
        if (tile1.x == tile2.x - 1 && tile1.y == tile2.y + 1) return true;
        if (tile1.x == tile2.x + 1 && tile1.y == tile2.y - 1) return true;
        return false;
    }
}
