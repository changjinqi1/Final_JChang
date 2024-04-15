using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("Score");
        SceneManager.LoadScene("SampleScene");
    }
}
