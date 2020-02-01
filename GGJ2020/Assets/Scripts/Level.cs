using UnityEngine;

public class Level : MonoBehaviour
{
    public Board board;
    public LevelLogic levelLogic;

    public void initLevel ()
    {
        levelLogic = new LevelLogic();
        board = new Board();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
