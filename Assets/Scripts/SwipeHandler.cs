using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwipeHandler : MonoBehaviour
{
    //private Point startPoint;
    private double mouseX, mouseY;
    private Vector2 startTouch, swipeDelta;
    private Vector2 SwipeDelta { get { return swipeDelta; } }
    // Use this for initialization

    private double width = Screen.width;
    private double height = Screen.height;
    private double screenRatio;

    private bool tap, swipe;
    private GameObject track;
    private double acceptValue = 0.75f;

    public GameObject wrongSoundEffect;

    private GameObject GetFirstFragment(string tag)
    {
        GameObject[] fragments = GameObject.FindGameObjectsWithTag(tag);

        if (fragments.Length == 0)
        {
            return null;
        }
        else
        {
            return fragments[0];
        }
    }

    void Start()
    {
        track = GameObject.FindGameObjectWithTag("Track");
        screenRatio = width / height;
    }

    void Update()
    {
        if (Input.touches.Length == 1 && gameObject.GetComponent<GameMode>().CheckGameover() == false)
        {
            if (Input.touches[0].phase == TouchPhase.Began && !tap)
            {
                startTouch = Input.touches[0].position;
                tap = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
            else if (Input.touches[0].phase == TouchPhase.Moved && tap)
            {
                swipe = true;
                swipeDelta = (Vector2)Input.touches[0].position - startTouch;

                double g1 = swipeDelta.y / swipeDelta.x;
                double g2 = swipeDelta.x / swipeDelta.y;
                //beware of mutlitouch

                //Right Straight line
                if (Mathf.Abs((float)g1) < acceptValue && startTouch.x < Input.touches[0].position.x && Mathf.Abs(swipeDelta.x) > 20)
                {
                    GameObject frag = GetFirstFragment("Right");
                    if (frag != null)
                    {
                        SuccessfulSwipe(frag);

                    }
                    else
                    {
                        FailureSwipe();
                    }
                }
                //Left Straight line
                else if (Mathf.Abs((float)g1) < acceptValue && startTouch.x > Input.touches[0].position.x && Mathf.Abs(swipeDelta.x) > 20)
                {
                    GameObject frag = GetFirstFragment("Left");
                    if (frag != null)
                    {
                        SuccessfulSwipe(frag);
                    }
                    else
                    {

                        FailureSwipe();
                    }
                }
                //Down Straight line
                else if (Mathf.Abs((float)g2) < acceptValue && startTouch.y > Input.touches[0].position.y && Mathf.Abs(swipeDelta.y) > 20)
                {
                    //can be straight line
                    GameObject frag = GetFirstFragment("Down");
                    if (frag != null)
                    {
                        SuccessfulSwipe(frag);
                    }
                    else
                    {
                        FailureSwipe();
                    }
                }
                //Up Straight line
                else if (Mathf.Abs((float)g2) < acceptValue && startTouch.y < Input.touches[0].position.y && Mathf.Abs(swipeDelta.y) > 20)
                {
                    GameObject frag = GetFirstFragment("Up");
                    if (frag != null)
                    {
                        SuccessfulSwipe(frag);
                    }
                    else
                    {
                        FailureSwipe();
                    }
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Stationary)
            {
                //do nothing yet
            }
        }
    }

    void Reset()
    {
        tap = false;
        swipe = false;
        startTouch = swipeDelta = Vector2.zero;
    }

    void SuccessfulSwipe(GameObject frag)
    {
        GetComponent<GameMode>().AddScore("swipe");
        GameObject.FindGameObjectWithTag("Correct").GetComponent<AudioSource>().Play();
        Reset();
        frag.SendMessage("ClearFragment");

        if (GameObject.FindGameObjectsWithTag("Empty").Length == 6)
        {
            //all clear, change Snoopy pose;
            int pose = GetComponent<GameMode>().GetPose();
            SnoopyAnimationController sac = GameObject.FindGameObjectWithTag("Player").GetComponent<SnoopyAnimationController>();
            sac.SetPose(pose);
        }
    }

    private double Gradient(double x1, double y1, double x2, double y2)
    {
        double g, x, y;
        x = x2 - x1;
        y = y2 - y1;
        g = y / x;

        return g;
    }

    void ClearFragment(int d)
    {
        Component[] comp;
        comp = track.GetComponentsInChildren<FragmentScript>();

        foreach (Component c in comp)
        {
            c.SendMessage("ClearFragment", d);
        }
    }

    IEnumerator Gameover(float t)
    {
        gameObject.GetComponent<GameMode>().SetGameover();
        yield return new WaitForSeconds(t);
        GameObject sm = GameObject.FindGameObjectWithTag("SceneManager");
        sm.SendMessage("ChangeScene");
    }

    void FailureSwipe()
    {
        if (swipe)
        {
            if (wrongSoundEffect == null)
            {
                wrongSoundEffect = GameObject.FindGameObjectWithTag("Wrong");
            }

            wrongSoundEffect.GetComponent<AudioSource>().Play();

            Reset();

            GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");
            float max = 0;
            GameObject life = new GameObject();
            foreach (GameObject l in lifes)
            {
                if (max < l.transform.position.x)
                {
                    max = l.transform.position.x;
                    life = l;
                }
            }
            Destroy(life);

            if (gameObject.GetComponent<GameMode>().LoseLife() == 0)
            {
                StartCoroutine(Gameover(wrongSoundEffect.GetComponent<AudioSource>().clip.length));
            }
        }
    }
}