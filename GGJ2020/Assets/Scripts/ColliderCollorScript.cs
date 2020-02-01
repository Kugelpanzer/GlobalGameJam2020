using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCollorScript : MonoBehaviour
{
    GameObject controller;
    public Color defaultColor;
    public Color disabledColor;
    public Color allowedColor;
    SpriteRenderer spr;
    List<Transform> childrenList = new List<Transform>();
    public void ChangeColliderColor(int colorCase)
    {
       // Debug.Log(gameObject.name);
        switch (colorCase)
        {
            case 0:
                spr.color = defaultColor;
               // ChangeColorToWalls(colorCase);
                break;
            case 1:
                spr.color = allowedColor;
               // ChangeColorToWalls(colorCase);
                break;
            case 2:
                spr.color = disabledColor;
               // ChangeColorToWalls(colorCase);
                break;
        }
    }
   public void ResetColor()
    {
        spr.color = defaultColor;
    }
    /*private void ChangeColorToWalls(int colorCase)
    {
        if (childrenList.Count == 0) return;
        foreach(Transform t in childrenList)
        {
            Transform child=t.GetChild(0);
            child.gameObject.GetComponent<ColliderCollorScript>().ChangeColliderColor(colorCase);
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Controller");
        spr = GetComponent<SpriteRenderer>();
        spr.color = defaultColor;

        for(int i = 0; i < transform.childCount; i++)
        {
            childrenList.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<ObjectSelector>().checkingObject == this)
            ResetColor();
    }
}
