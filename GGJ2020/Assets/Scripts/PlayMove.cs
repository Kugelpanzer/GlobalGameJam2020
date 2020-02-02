using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMove : MonoBehaviour
{
	
    Ability currentAbility;
    bool takingInput;
    Board board;
    public int wallsLeft;

    Ability firstAbility;
    Ability secondAbility;

    public Button firstButton;
    public Button secondButton;
    public Vector3 firstPlace;
    public Vector3 secondPlace;

    public List<Button> allButtons = new List<Button>();


    
    public void SetWall(Tile tile,WallType type)
    {
		GetComponent<AudioMenager>().PlaySound("Wall");
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
       GetComponent<AudioMenager>().PlaySound("Forest"); 
       Tile tile= board.GetTile(x, y);
        if (!board.IsNatureTilePlayable(x, y)) return;
        board.ChangeTileType(x, y, TileType.Nature);
        //tile.UpdateSprite();
		/*if(tile.anim.GetBool("forest1"))
		{Debug.Log("usao");
		tile.anim.SetInteger("Forest grass",2);}*/
		tile.anim.SetBool("forest1",true);
		/* bool forest1 - animacija šuma
		forest grass, 2 */
    }

    public bool AbilityCheck(ColliderCollorScript cc)
    {
        if(cc is WallColliderScript  )
        {
            if (currentAbility!=null && currentAbility.name == "wall")
                return true;
            else return false;
            
        }
        else
        {
            if (currentAbility != null && currentAbility.name == "nature") return true;
            else return false;
        }
    }
    public void ResetAbility()
    {
        currentAbility = null;
        GetComponent<Board>().ExecuteGooMove();
        //call goo play func
    }
    public void WallAbility(int wallCount)
    {
        if (currentAbility == null)
        {
            currentAbility = new Ability("wall", wallCount);
            wallsLeft = wallCount-1;
        }

    }
    public void NatureAbility()
    {
        if (currentAbility == null)
            currentAbility = new Ability("nature", 1);
    }
    public void AbilityUsed()
    {
        if (currentAbility.name == "wall" && wallsLeft > 0)
        {
            wallsLeft--;
        }
        else if (currentAbility.name == "wall" && wallsLeft <= 0)
        {
            ResetAbility();
        }
        else if (currentAbility.name == "nature")
        {
            ResetAbility();
        }

    }
    public void AbilitySelect(Button button)//when button is clicked
    {
		GetComponent<AudioMenager>().PlaySound("ClickUniversal");
        //change too buttons
        if (currentAbility == null)
        {
            Button toChange;
            if (button == firstButton)
            {
                firstButton.transform.position += new Vector3(1000, 0, 0);
                toChange = firstButton;
                firstButton = allButtons[Random.Range(0, allButtons.Count-1)];
                allButtons.Remove(firstButton);
                allButtons.Add(toChange);
                firstButton.transform.position = firstPlace;
            }
            else if(button == secondButton)
            {
                secondButton.transform.position += new Vector3(1000, 0, 0);
                toChange = secondButton;
                secondButton = allButtons[Random.Range(0, allButtons.Count - 1)];
                allButtons.Remove(secondButton);
                allButtons.Add(toChange);
                secondButton.transform.position = secondPlace;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        board = GetComponent<Board>();
        //currentAbility = new Ability("wall", 1);
        firstPlace = firstButton.transform.position;
        secondPlace = secondButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAbility==null)
        {
            GetComponent<ObjectSelector>().allowedAsset = 0;

        }
        else if(currentAbility.name=="nature")
        {
            GetComponent<ObjectSelector>().allowedAsset = 1;

        }
        else if (currentAbility.name == "wall")
        {
            GetComponent<ObjectSelector>().allowedAsset = 2;
        }
    }
}
