using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentScript : MonoBehaviour {
	
	public Material mUp, mDown, mLeft, mRight, mEmpty;

	private enum E_direction {
		empty,
		upArrow,
		downArrow,
		leftArrow,
		rightArrow
	}


	// Use this for initialization
	void Start () {
		this.tag = "Empty";

		//ChangeSkin (E_direction.rightArrow);
		RandomSkin();
	}
	
	// Update is called once per frame
	void Update () {
		
		//ChangeSkin (E_direction.empty);
	}

	public void RandomSkin(){
		int i = Random.Range (0,5);	//0~4
		ChangeSkin ((E_direction)i);

	}

	void ChangeSkin(E_direction n){
		

		switch (n) {
		case E_direction.empty:
			//mat = mEmpty;
			//Debug.Log(n);
			this.tag = "Empty";
			gameObject.GetComponent<Renderer>().material = mEmpty;
			break;

		case E_direction.upArrow:
			//mat = mUp;
			//mat.color = Color.red;

			//set texture;
			//Debug.Log (n);
			this.tag = "Up";
			gameObject.GetComponent<Renderer>().material = mUp;
			break;

		case E_direction.downArrow:
			//mat = mDown;
			this.tag = "Down";
			//Debug.Log(n);
			gameObject.GetComponent<Renderer>().material = mDown;
			break;

		case E_direction.leftArrow:
			//mat = mLeft;
			//Debug.Log(n);
			this.tag = "Left";
			gameObject.GetComponent<Renderer>().material = mLeft;
			break;

		case E_direction.rightArrow:
			//mat = mRight;
			this.tag = "Right";
			gameObject.GetComponent<Renderer>().material = mRight;
			break;

		default:
			//mat = mEmpty;
			this.tag = "Empty";
			gameObject.GetComponent<Renderer>().material = mEmpty;
			//Debug.Log(n);
			break;
		}

	}

	void ClearFragment(){
		gameObject.GetComponent<Renderer> ().material = mEmpty;
		this.tag = "Empty";
	}
}
