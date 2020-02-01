using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseObject
{ 

    public WallType type;

    void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = controller.GetComponent<SpriteScript>().wallTypeSpr[type];
    }
    private void Start()
    {
        base.Start();
        UpdateSprite();
    }
}
