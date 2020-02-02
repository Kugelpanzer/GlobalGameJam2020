using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : BaseObject
{
	public Animator anim;
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
        //GetComponent<SpriteRenderer>().sprite=controller.GetComponent<SpriteScript>().typeSpr[type];
		if(type==TileType.Nature)
			anim.SetInteger("base int",3);
		if(type==TileType.Goo)
			anim.SetInteger("base int",2);
		if(type==TileType.Mountain)
			anim.SetInteger("base int",1);
		
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        gridMap.cellSize = new Vector3(objectSpr.size.x, objectSpr.size.y, 0);
		anim=GetComponent<Animator>();
		//GetComponent<SpriteRenderer>().sortingOrder=-y;
        SetOnMap();
        UpdateSprite();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
