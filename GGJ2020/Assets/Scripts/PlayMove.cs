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

    public bool AbilityCheck(ColliderCollorScript cc)
    {
        if(cc is WallColliderScript  )
        {
            if (currentAbility.name == "wall")
                return true;
            else return false;
            
        }
        else
        {
            if (currentAbility.name == "nature") return true;
            else return false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        board = GetComponent<Board>();
        currentAbility = new Ability("wall", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
