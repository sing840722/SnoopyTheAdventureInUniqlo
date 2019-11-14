using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Printer : MonoBehaviour {
	private float speed = 0.1f;
	private GameObject track;
	private GameObject gameHandler;

	void Start () {
		track = GameObject.FindGameObjectWithTag ("Track");
		gameHandler = GameObject.FindGameObjectWithTag ("GameHandler");


	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (0.0f, 0.0f, speed);
	}

	public void SetSpeed(float s){
		speed = s;
	}



	void OnCollisionEnter(Collision col){
		//
		if (col.gameObject.tag == "Player") {
			if (this != null) {
				if (track.GetComponent<Track> ().CountEmpty () == 6 && gameHandler.GetComponent<GameMode>().CheckGameover() == false) {
					StartCoroutine (CompleteLevel ());
				} 
				else {
					//Debug.Log ("die");
					/*
					SnoopyAnimationController sac;
					sac = col.gameObject.GetComponent <SnoopyAnimationController> ();
					if (sac != null) {
						sac.SnoopyDie (true);
					}
					*/

					StartCoroutine (PlaySoundThenLoad ());
				}
			}
		}
	}


	void Gameover(){
		speed = 0;
		GameObject sm = GameObject.FindGameObjectWithTag("SceneManager");
		Destroy (gameObject);
	}

	IEnumerator PlaySoundThenLoad(){
		gameHandler.GetComponent<GameMode> ().SetGameover ();
		speed = 0;
		AudioSource audio = GameObject.FindGameObjectWithTag("SnoopyDie").GetComponent<AudioSource>();
		audio.Play ();
		GameObject sm = GameObject.FindGameObjectWithTag("SceneManager");
		yield return new WaitForSeconds (audio.clip.length);
		sm.SendMessage ("ChangeScene");

	}

	IEnumerator CompleteLevel(){

		//speed = 0;
		GetComponent<BoxCollider>().isTrigger = true;
		AudioSource audio = GameObject.FindGameObjectWithTag ("LevelComplete").GetComponent<AudioSource> ();
		if(!audio.isPlaying)
		audio.Play ();
		GameMode gm;
		gm = gameHandler.GetComponent<GameMode> ();

		yield return new WaitForSeconds(audio.clip.length);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<SnoopyAnimationController> ().SetPose (-1);
		if (gm != null) {
			gm.SpawnNewPrinter();

			gm.AddScore ("level");
			gm.AddPassed ();

			if (gm.GetPassed () == 7) {
				gm.AddLevel ();
				//speed = gm.GetSpeed ();
			}
			Destroy (gameObject);
		}

	}
}
