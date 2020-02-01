using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{

    public GameObject selectedTile;

    public ColliderCollorScript checkingObject;
    public int allowedAsset=1;

    Vector3 mousePos;
    Vector2 mousePos2D;
    RaycastHit2D hit;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos2D = new Vector2(mousePos.x, mousePos.y);

            hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
               selectedTile=hit.collider.gameObject;
            }
        }


        //checker

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = new Vector2(mousePos.x, mousePos.y);

        hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {

            if (hit.collider.gameObject.GetComponent<ColliderCollorScript>() != null)
            {
                //if(check color)
                checkingObject = hit.collider.gameObject.GetComponent<ColliderCollorScript>();
                checkingObject.ChangeColliderColor(allowedAsset);
            }
        }
        else
        {
           /* if (checkingObject != null)
                checkingObject.GetComponent<ColliderCollorScript>().ResetColor();*/
            checkingObject = null;
        }
    }



}
