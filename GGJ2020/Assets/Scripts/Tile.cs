using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int x, y;

    public TileType type
    {
        get { return type; }   // get method
        set {
            if (this.type != value)
            {
                UpdateSprite((WallType)value);
                this.type = value;
            }

        }  // set method
    }


    public override bool Equals(object other)
    {
        Tile otherTile = (Tile)other;
        return (this.x == otherTile.x) && (this.y == otherTile.y) && (this.type == otherTile.type);
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
