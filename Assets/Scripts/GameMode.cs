using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {
	private GameObject track;
	private GameObject printerSpawnPoint;
	//public GameObject printerPrefab;
	//public GameObject printerPrefab2;
	public GameObject[] printerPrefabs;

	private int level = 1, passed;
	private float speed = 0.1f;
	//private float speed = 0.0000000000000125f;
	private int score;
	private int life = 2;
	private bool gameover = false;
	private int currentBoard = -1;

	// Use this for initialization
	void Start () {
		track = GameObject.FindGameObjectWithTag ("Track");
		printerSpawnPoint = GameObject.FindGameObjectWithTag ("PrinterSpawnPoint");
		SpawnNewPrinter ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (life+1);
		if (!gameover) {
			AddScore ("alive");
		}
	}

	public void SpawnNewPrinter(){
		//Debug.Log (printerPrefabs.Length);
		//initiate prefab (printer);
		if (printerPrefabs != null && printerSpawnPoint != null) {
			GameObject newPrinter;

			switch(currentBoard = Random.Range (1, printerPrefabs.Length+1)){	//1~3
			case 1:
				newPrinter = Instantiate (printerPrefabs [0], printerSpawnPoint.transform.position, Quaternion.identity);
				//Debug.Log ("1");
				break;
			case 2:
				newPrinter = Instantiate (printerPrefabs[1], printerSpawnPoint.transform.position, Quaternion.identity);
				//Debug.Log ("2");
				break;
			case 3:
				newPrinter = Instantiate (printerPrefabs[2], printerSpawnPoint.transform.position, Quaternion.identity);
				//Debug.Log ("3");
				break;
			default:
				newPrinter = Instantiate (printerPrefabs[0], printerSpawnPoint.transform.position, Quaternion.identity);
				break;
			}
			//Debug.Log (currentBoard);
			//GameObject newPrinter = Instantiate (printerPrefab, printerSpawnPoint.transform.position, Quaternion.identity);
			Printer ps = newPrinter.GetComponent<Printer> ();
			if (ps != null) {
				ps.SetSpeed (speed);
			}
			//Debug.Log (speed);
			//newPrinter.GetComponent<Renderer> ().material = ;	//set skin
		}
			
		ReloadTrack();
	}

	private void ReloadTrack(){
		if (track != null) {
			Transform pTr = track.transform;
			foreach (Transform tr in pTr) {
				if (tr.tag == "Empty") {
					FragmentScript fs = tr.GetComponent<FragmentScript> ();
					if (fs != null) {
						fs.RandomSkin ();	
					}
				}
			}
		}
	}


	public void AddLevel(){
		level++;
		passed = 0;
		speed *= 1.5f;
	}

	public void AddScore(string s){
		//score += s;
		if (s == "swipe") {
			score += Mathf.RoundToInt((life + 1) * 0.25f + 50);
		} else if (s == "level") {
			score += Mathf.RoundToInt ((life+1) * level*250);
		} else if (s == "alive"){
			score += Mathf.RoundToInt (level * 0.25f*(life + 2));//or life +3
		}
	}

	public void AddPassed(){
		passed++;

	}

	public int GetPassed(){
		return passed;
	}

	public float GetSpeed(){
		return speed;
	}

	public int GetScore(){
		return score;
	}

	public int GetLife (){
		return life;
	}

	public int LoseLife(){
		return life--;
	}

	public bool CheckGameover(){
		return gameover;
	}

	public void SetGameover(){
		gameover = true;
	}

	public int GetPose(){
		return currentBoard;
	}
}
