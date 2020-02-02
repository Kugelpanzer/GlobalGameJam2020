using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToScene(int i )
    {
        SceneManager.LoadScene(i);
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public virtual void TakeTurn()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            GoToScene(0);
        }
    }
}
