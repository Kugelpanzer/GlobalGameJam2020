using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour
{

    Ability currentAbility;
    bool takingInput;
    Board board;



    
    private void SetWall(int x,int y,WallType type)
    {
        Wall wall=Instantiate(GetComponent<TilemapController>().wallAsset).GetComponent<Wall>();
        wall.x = x;
        wall.y = y;
        wall.type = type;
        wall.SetOnMap();
       board.AddWall(wall);

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
