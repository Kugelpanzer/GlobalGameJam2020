using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColliderColor : ColliderCollorScript
{

    List<Transform> childrenList = new List<Transform>();

    bool first, last;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        for(int i = 0; i < transform.childCount; i++)
        {
            childrenList.Add(transform.GetChild(i));
        }
        Board board = controller.GetComponent<Board>();
        Tile tile = transform.parent.gameObject.GetComponent<Tile>();
        if (!board.TileHasLeftTile(tile.x, tile.y))
        {
            GameObject gj = transform.GetChild(0).gameObject;
            childrenList.Remove(gj.transform);
            Destroy(gj);
            first = true;
        }
        if (!board.TileHasBottomRightTile(tile.x, tile.y))
        {
            GameObject gj = transform.GetChild(childrenList.Count-1).gameObject;
            childrenList.Remove(gj.transform);
            Destroy(gj);
            last = true;
        }
        if (!board.TileHasBottomLeftTile(tile.x, tile.y))
        {
            if (first)
            {
                GameObject gj = transform.GetChild(0).gameObject;
                childrenList.Remove(gj.transform);
                Destroy(gj);
            }
            else if (last)
            {
                GameObject gj = transform.GetChild(childrenList.Count - 1).gameObject;
                childrenList.Remove(gj.transform);
                Destroy(gj);
            }
            else if(!first && !last)
            {
                GameObject gj = transform.GetChild(1).gameObject;
                childrenList.Remove(gj.transform);
                Destroy(gj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
