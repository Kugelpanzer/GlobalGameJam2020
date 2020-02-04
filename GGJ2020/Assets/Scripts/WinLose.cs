using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    public int tilesToLose=25;
    public int tilesToWin=7;
    Board board;

    public void VictoryCheck()
    {
        if(tilesToWin<= board.tiles.FindAll(tileX => tileX.type == TileType.Nature).Count)
        {
            Victory();
        }
    }

    public void LoseCheck()
    {
        if (tilesToLose <= board.tiles.FindAll(tileX => tileX.type == TileType.Goo).Count)
        {
            GetComponent<AudioMenager>().PlaySound("GameOver");
            GetComponent<LevelController>().GoToScene(3);
        }
    }
    public void Victory()
    {
        GetComponent<AudioMenager>().PlaySound("Win");
        GetComponent<LevelController>().GoToScene(2);
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
