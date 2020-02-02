using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{

    public GameObject selectedTile;

    public ColliderCollorScript checkingObject;
    public int allowedAsset=1;
    PlayMove pMove;

    Vector3 mousePos;
    Vector2 mousePos2D;
    RaycastHit2D hit;
	

    // Start is called before the first frame update
    void Start()
    {
        pMove=GetComponent<PlayMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (checkingObject != null && checkingObject.currentColor != checkingObject.disabledColor)
            {
				GetComponent<AudioMenager>().PlaySound("ClickUniversal");
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos2D = new Vector2(mousePos.x, mousePos.y);

                hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    selectedTile = hit.collider.gameObject.transform.parent.gameObject;
                    if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<Tile>() != null)
                    {
                        Debug.Log(selectedTile);
                        Tile tile = selectedTile.GetComponent<Tile>();
                        GetComponent<PlayMove>().AbilityUsed();
                        GetComponent<PlayMove>().SetTile(tile.x, tile.y);
                    }
                    else
                    {
                        Debug.Log(selectedTile);
                        WallColliderScript wc = hit.collider.gameObject.GetComponent<WallColliderScript>();
                        GetComponent<PlayMove>().AbilityUsed();
                       GetComponent<PlayMove>().SetWall(hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Tile>(), wc.type);

                    }

                }
            }
			else 
			{
				GetComponent<AudioMenager>().PlaySound("ClickError");
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
                if (GetComponent<PlayMove>().AbilityCheck(checkingObject)) allowedAsset = 1;
                else allowedAsset = 2;
                if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<Tile>() == null && allowedAsset != 2)
                {
                    WallColliderScript wc = hit.collider.gameObject.GetComponent<WallColliderScript>();
                    if (GetComponent<Board>().IsNatureWallPlayable(wc.x, wc.y, wc.type)) allowedAsset = 1;
                    else allowedAsset = 2;
                }
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
