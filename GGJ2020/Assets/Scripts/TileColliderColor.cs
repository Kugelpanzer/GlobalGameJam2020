using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColliderColor : ColliderCollorScript
{

    List<Transform> childrenList = new List<Transform>();

    bool first,second, third;
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

        if (!board.TileHasBottomLeftTile(tile.x, tile.y))
        {
            GameObject gj = transform.GetChild(2).gameObject;
            childrenList.Remove(gj.transform);
            Destroy(gj);
        }
        if (!board.TileHasBottomRightTile(tile.x, tile.y))
        {
            GameObject gj = transform.GetChild(1).gameObject;
            childrenList.Remove(gj.transform);
            Destroy(gj);
        }
        if (!board.TileHasLeftTile(tile.x, tile.y))
        {
            GameObject gj = transform.GetChild(0).gameObject;
            childrenList.Remove(gj.transform);
            Destroy(gj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
