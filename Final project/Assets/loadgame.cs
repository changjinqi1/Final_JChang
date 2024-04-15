using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadgame : MonoBehaviour
{
    public string sceneName;

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
