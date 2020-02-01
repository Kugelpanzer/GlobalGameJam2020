using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : BaseObject
{

    public TileType type;
   /* {
        get
        {
            return type;
        }
        set
        {
            type = value;
            UpdateSprite();
        }

    }*/



    public Tile (int x, int y, TileType tileType)
    {
        this.x = x;
        this.y = y;
        this.type = tileType;
    }

    public void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite=controller.GetComponent<SpriteScript>().typeSpr[type];
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
         controller = GameObject.Find("Controller");
        UpdateSprite();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
