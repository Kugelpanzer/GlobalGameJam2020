using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{

    public int x, y;

    protected SpriteRenderer objectSpr;
    GameObject controller;
    public Grid gridMap;

    public void SetOnMap()
    {
       transform.position = gridMap.GetCellCenterWorld(new Vector3Int(x, y, 0));
       // float sizex = transform.localScale.x * objectSpr.size.x ;
       //  float sizey = transform.localScale.y * objectSpr.size.y * (0.86602540378f/2f);
       //transform.position = new Vector3(sizex * x, -sizey * y);
       /*  if (y % 2 == 0)
         {
             float sizex = transform.localScale.x * objectSpr.size.x;
             float sizey = transform.localScale.y * objectSpr.size.y * 0.86602540378f;
             transform.position = new Vector3(sizex * x, -sizey * y);
         }
         else
         {
             float sizex = transform.localScale.x * objectSpr.size.x - 0.5f * (transform.localScale.x * objectSpr.size.x);
             float sizey = transform.localScale.y * objectSpr.size.y * 0.86602540378f;
             transform.position = new Vector3(sizex * x, -sizey * y);
         }*/
    }
    // Start is called before the first frame update
    protected void Start()
    {
        objectSpr = GetComponent<SpriteRenderer>();
        gridMap = GameObject.Find("MainGrid").GetComponent<Grid>();
        gridMap.cellSize = new Vector3(objectSpr.size.x, objectSpr.size.y, 0);

        SetOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
