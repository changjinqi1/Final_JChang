using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoresystem : MonoBehaviour
{
    public TMP_Text winText;
    public TMP_Text scoreText;
    public GameObject pauseUI;
    public GameObject pauseUI2;
    private int score = 0;
    private bool isPaused = false;

    private const string ScoreKey = "Score";

    void Start()
    {
        LoadScore();
        UpdateScoreText();
        winText.gameObject.SetActive(false);
        pauseUI.SetActive(false);
        pauseUI2.SetActive(false);
    }

    void Update()
    {
        // Check if the score has reached 25 and the game is not already paused
        if (score >= 20 && !isPaused)
        {
            ChoosePowerups1();
            isPaused = true;
        }

        if (score > 70)
        {
            ChoosePowerups1();
            isPaused = true;
        }

    }


    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score > 200)
        {
            ShowWinText();
        }

        SaveScore();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void ShowWinText()
    {
        winText.gameObject.SetActive(true);
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            score = PlayerPrefs.GetInt(ScoreKey);
        }
    }

    void ChoosePowerups1()
    {
        pauseUI.SetActive(true);
    }

    void ChoosePowerups2()
    {
        pauseUI2.SetActive(true);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseUI.SetActive(true);
    }

    void PauseGame2()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseUI2.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseUI.SetActive(false);
        pauseUI2.SetActive(false);
    }
}