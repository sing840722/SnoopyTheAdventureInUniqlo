using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwipeHandler : MonoBehaviour {
	//private Point startPoint;
	private double mouseX, mouseY;
	private Vector2 startTouch, swipeDelta;
	private Vector2 SwipeDelta{get { return swipeDelta; }}
	// Use this for initialization

	private double width = Screen.width;
	private double height = Screen.height;
	private double screenRatio;

	private bool tap, swipe;
	private GameObject track;
	private double acceptValue = 0.75f;

	public GameObject wrongSoundEffect;

	private GameObject GetFirstFragment (string tag)
	{
		GameObject[] fragments = GameObject.FindGameObjectsWithTag (tag);

		if (fragments.Length == 0) {
			return null;
		} else {
			return fragments [0];
		}

	}

	void Start () {
		track = GameObject.FindGameObjectWithTag ("Track");
		screenRatio =  width/height;



	}

	void Update(){
		/*
		if (Input.GetMouseButtonDown (0) && !tap && gameObject.GetComponent<GameMode>().CheckGameover() == false) {
			startTouch = Input.mousePosition;
			tap = true;
			//pen = GameObject.CreatePrimitive (PrimitiveType.Cube);

		}
		if (Input.GetMouseButton (0) && tap && gameObject.GetComponent<GameMode>().CheckGameover() == false) {				
			Swiping ();
		}
		if (Input.GetMouseButtonUp (0)) {
			Reset ();
		}
	*/

		if (Input.touches.Length == 1 && gameObject.GetComponent<GameMode>().CheckGameover() == false) {
			if (Input.touches [0].phase == TouchPhase.Began && !tap) {
				startTouch = Input.touches [0].position;
				tap = true;
				//FailureSwipe();//debug
			} else if (Input.touches [0].phase == TouchPhase.Ended || Input.touches [0].phase == TouchPhase.Canceled) {
				Reset ();

			} else if (Input.touches [0].phase == TouchPhase.Moved &&  tap) {
				swipe = true;
				swipeDelta = (Vector2)Input.touches [0].position - startTouch;


				double g1 = swipeDelta.y / swipeDelta.x;
				double g2 = swipeDelta.x / swipeDelta.y;


				//beware of mutlitouch

				//Right Straight light
				//if (Mathf.Abs ((float)g1) < acceptValue && swipeDelta.x < width / 2 && swipeDelta.x > width / 5) {
				if (Mathf.Abs ((float)g1) < acceptValue && startTouch.x < Input.touches [0].position.x && Mathf.Abs(swipeDelta.x) > 20) {
					//if (startTouch.x < Input.touches [0].position.x) {
						GameObject frag = GetFirstFragment ("Right");
						if (frag != null) {
							//Reset ();
							//frag.SendMessage ("ClearFragment");
							//Debug.Log ("reset");
							SuccessfulSwipe(frag);
						} else {
							//score --;
							//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
							//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
							FailureSwipe ();
						}
					//} 
				}// else if (Mathf.Abs ((float)g1) < acceptValue && swipeDelta.x < width / -5 && swipeDelta.x < width / -2) {
				else if (Mathf.Abs ((float)g1) < acceptValue && startTouch.x > Input.touches [0].position.x && Mathf.Abs(swipeDelta.x) > 20) {
					//if (startTouch.x > Input.touches [0].position.x) {
						GameObject frag = GetFirstFragment ("Left");
						if (frag != null) {
							//Reset ();
							//frag.SendMessage ("ClearFragment");
							//Debug.Log ("reset");
							SuccessfulSwipe(frag);
						} else {
							//score --;
							//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
							//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
							FailureSwipe ();
						}
					//} 

				} //else if (Mathf.Abs ((float)g2) < acceptValue && swipeDelta.y < height / -5 && swipeDelta.y > height / -2) {
				else if (Mathf.Abs ((float)g2) < acceptValue && startTouch.y > Input.touches [0].position.y && Mathf.Abs(swipeDelta.y) > 20) {
					//can be straight line
					//if (startTouch.y > Input.touches [0].position.y) {
						GameObject frag = GetFirstFragment ("Down");
						if (frag != null) {
							//Reset ();
							//frag.SendMessage ("ClearFragment");
							//Debug.Log ("reset");
							SuccessfulSwipe(frag);
						} else {
							//score --;
							//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
							//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
							FailureSwipe ();
						}
					//}
				} //else if (Mathf.Abs ((float)g2) < acceptValue && swipeDelta.y > height / 5 && swipeDelta.y < height / 2) {
				else if (Mathf.Abs ((float)g2) < acceptValue && startTouch.y < Input.touches [0].position.y && Mathf.Abs(swipeDelta.y) > 20) {
					//if (startTouch.y < Input.touches [0].position.y) {
						GameObject frag = GetFirstFragment ("Up");
						if (frag != null) {
							//Reset ();
							//frag.SendMessage ("ClearFragment");
							//Debug.Log ("reset");
							SuccessfulSwipe(frag);
						} else {
							//score --;
							//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
							//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
							FailureSwipe ();
						}
					//}
				} 
			} else if (Input.touches [0].phase == TouchPhase.Stationary) {
				//do nothing yet
			}
		}
	}

	void Reset(){
		//Input.touches [0].phase = TouchPhase.Ended;


		tap = false;
		swipe = false;
		startTouch = swipeDelta = Vector2.zero;
		//Debug.Log ("tap: " + tap + ", swipe: " + swipe);
	}

	void SuccessfulSwipe(GameObject frag){
		GetComponent<GameMode> ().AddScore ("swipe");
		GameObject.FindGameObjectWithTag ("Correct").GetComponent<AudioSource> ().Play ();
		Reset ();
		frag.SendMessage ("ClearFragment");
		//Debug.Log ("reset");

		if (GameObject.FindGameObjectsWithTag ("Empty").Length == 6) {
			//all clear, change Snoopy pose;
			int pose = GetComponent<GameMode>().GetPose();

			SnoopyAnimationController sac=
			GameObject.FindGameObjectWithTag ("Player").GetComponent<SnoopyAnimationController> ();

			sac.SetPose (pose);
		}
	}

	private double Gradient(double x1, double y1, double x2, double y2){
		double g, x, y;
		x = x2 - x1;
		y = y2 - y1;
		g = y / x;

		//return Mathf.Abs((float)g);
		return g;
	}

	void ClearFragment(int d){
		Component[] comp;
		comp = track.GetComponentsInChildren<FragmentScript> ();

		foreach (Component c in comp){
			c.SendMessage ("ClearFragment", d);

		}
	}

	IEnumerator Gameover(float t){
		gameObject.GetComponent<GameMode> ().SetGameover ();
		yield return new WaitForSeconds (t);
		GameObject sm = GameObject.FindGameObjectWithTag ("SceneManager");
		sm.SendMessage ("ChangeScene");	
	}

	void FailureSwipe(){
		
		if (swipe) {
			if (wrongSoundEffect == null) {
				wrongSoundEffect = GameObject.FindGameObjectWithTag ("Wrong");
			}

			wrongSoundEffect.GetComponent<AudioSource> ().Play();

			Reset();

			GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");
			float max = 0;
			GameObject life = new GameObject();
			foreach (GameObject l in lifes) {
				if (max < l.transform.position.x) {
					max = l.transform.position.x;
					life = l;
				}
			}
			Destroy (life);

			if (gameObject.GetComponent<GameMode> ().LoseLife () == 0) {
				StartCoroutine (Gameover (wrongSoundEffect.GetComponent<AudioSource> ().clip.length));
			} 
		}
	}

	void Swiping ()
	{
		swipe = true;
		swipeDelta = (Vector2)Input.mousePosition - startTouch;


		double g1 = swipeDelta.y / swipeDelta.x;
		double g2 = swipeDelta.x / swipeDelta.y;


		//beware of mutlitouch

		//Right Straight light
		if (Mathf.Abs ((float)g1) < acceptValue && swipeDelta.x < width / 2 && swipeDelta.x > width / 15) {
			if (startTouch.x < Input.mousePosition.x) {
				GameObject frag = GetFirstFragment ("Right");
				if (frag != null) {
					//Reset ();
					//frag.SendMessage ("ClearFragment");
					//Debug.Log ("reset");
					SuccessfulSwipe(frag);
				} else {
					//score --;
					//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
					//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
					FailureSwipe ();
				}
				//frag = null;
			} 
		} else if (Mathf.Abs ((float)g1) < acceptValue && swipeDelta.x < width / -15 && swipeDelta.x < width / -2) {
			if (startTouch.x > Input.mousePosition.x) {
				GameObject frag = GetFirstFragment ("Left");
				if (frag != null) {
					//Reset ();
					//frag.SendMessage ("ClearFragment");
					//Debug.Log ("reset");
					SuccessfulSwipe(frag);
				} else {
					//score --;
					//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
					//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
					FailureSwipe ();
				}
				//frag = null;
			} 

		} else if (Mathf.Abs ((float)g2) < acceptValue && swipeDelta.y < height / -15 && swipeDelta.y > height / -2) {
			//can be straight line
			if (startTouch.y > Input.mousePosition.y) {
				GameObject frag = GetFirstFragment ("Down");
				if (frag != null) {
					//Reset ();
					//frag.SendMessage ("ClearFragment");
					//Debug.Log ("reset");
					SuccessfulSwipe(frag);
				} else {
					//score --;
					//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
					//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
					FailureSwipe ();
				}
				//frag = null;
			}
		} else if (Mathf.Abs ((float)g2) < acceptValue && swipeDelta.y > height / 15 && swipeDelta.y < height / 2) {
			if (startTouch.y < Input.mousePosition.y) {
				GameObject frag = GetFirstFragment ("Up");
				if (frag != null) {
					//Reset ();
					//frag.SendMessage ("ClearFragment");
					//Debug.Log ("reset");
					SuccessfulSwipe(frag);
				} else {
					//score --;
					//Debug.Log("4 - start: " + startTouch.x + ", " + startTouch.y);
					//Debug.Log("4 - current: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
					FailureSwipe ();
				}
				//frag = null;
			}
		}
	}
}


/*

		#region Standalone
		if (Input.GetMouseButtonDown (0) && !tap) {
			tap = true;
			startTouch = Input.mousePosition;
			//FailureSwipe ();
		}

		if (Input.GetMouseButton (0)) {
			Plane objPlane = new Plane(Camera.main.transform.forward*-1, this.transform.position);


			Ray mRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			float rayDistance;
			if(objPlane.Raycast(mRay, out rayDistance)){
				this.transform.position=mRay.GetPoint(rayDistance);
			}

			Swiping ();
		}
		else {
			//tap = false;
			//isDraging = false;
			Reset ();
		}
		#endregion


*/