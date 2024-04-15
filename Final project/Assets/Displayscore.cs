using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Displayscore : MonoBehaviour

{
    public TMP_Text scoreText;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            int score = PlayerPrefs.GetInt("Score");

            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            scoreText.text = "Score: Not Available";
        }
    }
}