
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
	private GameObject gameHandler;
	// Use this for initialization
	void Start () {
		gameHandler = GameObject.FindGameObjectWithTag ("GameHandler");
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "Level") {
			if (gameHandler != null) {
				SceneManager.DontDestroyOnLoad (gameHandler);
			}
			//SceneManager.DontDestroyOnLoad (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name != "Level") {
			if (Input.GetMouseButtonDown (0)) {
				ChangeScene ();
			}
		}

	}

	void ChangeScene(){
		string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
		switch (currentScene) {
		case "MainMenu":
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Level", LoadSceneMode.Single);
			break;
		case "Level":
			UnityEngine.SceneManagement.SceneManager.LoadScene ("GameOver", LoadSceneMode.Single);
			//UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("GameOver", LoadSceneMode.Single);
			break;
		case "GameOver":
			if (gameHandler != null) {
				Destroy (gameHandler);
			}

			UnityEngine.SceneManagement.SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
			break;
		}
	}	
}
