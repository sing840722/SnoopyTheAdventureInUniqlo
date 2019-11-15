using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    private GameObject track;
    private GameObject printerSpawnPoint;
    public GameObject[] printerPrefabs;

    private int level = 1, passed;
    private float speed = 0.1f;
    private int score;
    private int life = 2;
    private bool gameover = false;
    private int currentBoard = -1;

    // Use this for initialization
    void Start()
    {
        track = GameObject.FindGameObjectWithTag("Track");
        printerSpawnPoint = GameObject.FindGameObjectWithTag("PrinterSpawnPoint");
        SpawnNewPrinter();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            AddScore("alive");
        }
    }

    //Instantiate new printer object
    public void SpawnNewPrinter()
    {
        if (printerPrefabs != null && printerSpawnPoint != null)
        {
            GameObject newPrinter;
            switch (currentBoard = Random.Range(1, printerPrefabs.Length + 1))
            {    //1~3
                case 1:
                    newPrinter = Instantiate(printerPrefabs[0], printerSpawnPoint.transform.position, Quaternion.identity);
                    break;
                case 2:
                    newPrinter = Instantiate(printerPrefabs[1], printerSpawnPoint.transform.position, Quaternion.identity);
                    break;
                case 3:
                    newPrinter = Instantiate(printerPrefabs[2], printerSpawnPoint.transform.position, Quaternion.identity);
                    break;
                default:
                    newPrinter = Instantiate(printerPrefabs[0], printerSpawnPoint.transform.position, Quaternion.identity);
                    break;
            }
            Printer ps = newPrinter.GetComponent<Printer>();
            if (ps != null)
            {
                ps.SetSpeed(speed);
            }
        }
        ReloadTrack();
    }

    private void ReloadTrack()
    {
        if (track != null)
        {
            Transform pTr = track.transform;
            foreach (Transform tr in pTr)
            {
                if (tr.tag == "Empty")
                {
                    FragmentScript fs = tr.GetComponent<FragmentScript>();
                    if (fs != null)
                    {
                        fs.RandomSkin();
                    }
                }
            }
        }
    }


    public void AddLevel()
    {
        level++;
        passed = 0;
        speed *= 1.5f;
    }

    public void AddScore(string s)
    {
        //score += s;
        if (s == "swipe") {
            score += Mathf.RoundToInt((life + 1) * 0.25f + 50);
        } else if (s == "level") {
            score += Mathf.RoundToInt((life + 1) * level * 250);
        } else if (s == "alive") {
            score += Mathf.RoundToInt(level * 0.25f * (life + 2));//or life +3
        }
    }

    //Passed printer counter
    public void AddPassed()
    {
        passed++;
    }

    public int GetPassed()
    {
        return passed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLife()
    {
        return life;
    }

    public int LoseLife()
    {
        return life--;
    }

    public bool CheckGameover()
    {
        return gameover;
    }

    public void SetGameover()
    {
        gameover = true;
    }

    public int GetPose()
    {
        return currentBoard;
    }
}
