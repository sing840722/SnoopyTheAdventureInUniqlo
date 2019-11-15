using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class SnoopyAnimationController : MonoBehaviour
{
    private Animator m_animator;
    private int currentPose = -1;

    void Awake()
    {
        m_animator = GetComponent<Animator>();

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //m_animator.SetBool ("isDie", isDie);
    }

    public void SnoopyDie(bool b)
    {

    }

    public void SetPose(int i)
    {
        currentPose = i;
        m_animator.SetInteger("pose", currentPose);

        switch (i)
        {
            case -1:
                gameObject.transform.eulerAngles = new Vector3(
                    gameObject.transform.eulerAngles.x,
                    180,
                    gameObject.transform.eulerAngles.z
                );
                break;
            case 1:
                /*
                gameObject.transform.eulerAngles = new Vector3 (
                    gameObject.transform.eulerAngles.x,
                    -90,
                    gameObject.transform.eulerAngles.z
                );
                */
                break;
            case 2:
                /*
                gameObject.transform.eulerAngles = new Vector3 (
                    gameObject.transform.eulerAngles.x,
                    -30,
                    gameObject.transform.eulerAngles.z
                );
                */
                break;
            case 3:
                gameObject.transform.eulerAngles = new Vector3(
                    gameObject.transform.eulerAngles.x,
                    -90,
                    gameObject.transform.eulerAngles.z
                );
                break;
            default:
                gameObject.transform.eulerAngles = new Vector3(
                    gameObject.transform.eulerAngles.x,
                    180,
                    gameObject.transform.eulerAngles.z
                );
                break;
        }
    }
}
