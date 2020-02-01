using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{

    public int x, y;

    protected SpriteRenderer objectSpr;
    GameObject controller;

    public void SetOnMap()
    {
        if (y % 2 == 0)
        {
            float sizex = transform.localScale.x * objectSpr.size.x;
            float sizey = transform.localScale.y * objectSpr.size.y ;
            transform.position = /*controller.transform.position +*/ new Vector3(sizex * x, -sizey * y);
        }
        else
        {
            float sizex = transform.localScale.x * objectSpr.size.x * 0.86602540378f ;
            float sizey = transform.localScale.y * objectSpr.size.y ;
            transform.position =/* controller.transform.position + */new Vector3(sizex * x, -sizey * y);
        }
    }
    // Start is called before the first frame update
    protected void Start()
    {
        objectSpr = GetComponent<SpriteRenderer>();
        SetOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
