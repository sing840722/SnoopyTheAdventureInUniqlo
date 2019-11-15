using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    private GameMode gameMode;
    private int score;
    private static int highScore;

    // Use this for initialization
    void Start()
    {
        //get game mode script from game object game handler
        gameMode = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameMode>();

        if (gameMode != null)
        {
            if (highScore < gameMode.GetScore())
            {
                highScore = gameMode.GetScore();
            }
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if (gameMode != null)
        {
            score = gameMode.GetScore();
        }
        if (gameObject.name == "Score")
        {
            GetComponent<Text>().text = score.ToString();
        }
        if (gameObject.name == "HighScore")
        {
            GetComponent<Text>().text = highScore.ToString();
        }
    }
}
