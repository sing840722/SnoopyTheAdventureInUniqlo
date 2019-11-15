using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentScript : MonoBehaviour
{

    public Material mUp, mDown, mLeft, mRight, mEmpty;

    private enum E_direction
    {
        empty,
        upArrow,
        downArrow,
        leftArrow,
        rightArrow
    }


    // Use this for initialization
    void Start()
    {
        this.tag = "Empty";
        RandomSkin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RandomSkin()
    {
        int i = Random.Range(0, 5); //0~4
        ChangeSkin((E_direction)i);
	}

    void ChangeSkin(E_direction n)
    {
        switch (n)
        {
            case E_direction.empty:
                this.tag = "Empty";
                gameObject.GetComponent<Renderer>().material = mEmpty;
                break;
            case E_direction.upArrow:
                this.tag = "Up";
                gameObject.GetComponent<Renderer>().material = mUp;
                break;
            case E_direction.downArrow:
                this.tag = "Down";
                gameObject.GetComponent<Renderer>().material = mDown;
                break;
            case E_direction.leftArrow:
                this.tag = "Left";
                gameObject.GetComponent<Renderer>().material = mLeft;
                break;
            case E_direction.rightArrow:
                this.tag = "Right";
                gameObject.GetComponent<Renderer>().material = mRight;
                break;
            default:
                this.tag = "Empty";
                gameObject.GetComponent<Renderer>().material = mEmpty;
                break;
        }
	}

	void ClearFragment()
    {
		gameObject.GetComponent<Renderer> ().material = mEmpty;
		this.tag = "Empty";
	}
}
