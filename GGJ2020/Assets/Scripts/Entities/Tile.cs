using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : BaseObject
{

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
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
