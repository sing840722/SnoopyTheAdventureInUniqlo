
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
    private GameObject gameHandler;
    // Use this for initialization
    void Start()
    {
        //Find GameHandler object
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler");
        //The object should be found/initialise in Level
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level")
        {
            //If the object already exist
            if (gameHandler != null)
            {
                //Do not destroy this object on changing scene, use it again after transition from Menu->(Level)->Gameover->Menu->(Level)
                SceneManager.DontDestroyOnLoad(gameHandler);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Level")
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeScene();
            }
        }

    }

    void ChangeScene()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "MainMenu":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level", LoadSceneMode.Single);
                break;
            case "Level":
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                break;
            case "GameOver":
                if (gameHandler != null)
                {
                    Destroy(gameHandler);
                }
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
        }
    }
}
