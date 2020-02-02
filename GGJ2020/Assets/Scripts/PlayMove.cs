using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour
{

    Ability currentAbility;
    bool takingInput;
    Board board;



    
    public void SetWall(Tile tile,WallType type)
    {
        Wall wall=Instantiate(GetComponent<TilemapController>().wallAsset).GetComponent<Wall>();
        wall.x = tile.x;
        wall.y = tile.y;
        wall.type = type;
        wall.SetOnMap();
        board.AddWall(wall);
      // board.AddWall(wall);

    }

    public void SetTile(int x,int y)
    {
        
       Tile tile= board.GetTile(x, y);
        if (!board.IsNatureTilePlayable(x, y)) return;
        board.ChangeTileType(x, y, TileType.Nature);
        tile.UpdateSprite();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        board = GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
