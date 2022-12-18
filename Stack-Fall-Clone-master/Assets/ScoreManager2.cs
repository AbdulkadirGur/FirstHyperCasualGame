using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager2 : MonoBehaviour
{
    public  static ScoreManager2 instance2;

    public int score;
    
    private Text scoreText;

    private void Awake()
    {
        makeSingleton();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    private void makeSingleton()
    {
        if (instance2 != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance2 = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        
        addScore(0);
    }

    
    void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
    }

    public void addScore(int value)
    {
        score +=value;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);     
        }
        

        scoreText.text = score.ToString();  
    }

    public void resetScore()
    {
        score = 0;
    }
}
