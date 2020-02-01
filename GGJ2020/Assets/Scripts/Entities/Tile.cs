using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y;

    public TileType type
    {
        get { return type; }
        set {
            if (this.type != value)
            {
                UpdateSprite((WallType)value);
                this.type = value;
            }
        }
    }

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
}
