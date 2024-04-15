using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingscriptManager : MonoBehaviour
{
    public GameObject shootingScriptA;
    public GameObject shootingScriptB;
    public GameObject shootingScriptC;

    void Start()
    {

        shootingScriptA.SetActive(false);
        shootingScriptB.SetActive(false);
        shootingScriptC.SetActive(false);
    }

    public void ToggleSplitbullets()
    {

        shootingScriptA.SetActive(!shootingScriptA.activeSelf);

    }

    public void Toggleplus4bullets()
    {
        shootingScriptB.SetActive(!shootingScriptB.activeSelf);
    }

    public void ToggleGiganticbullets()
    {
        shootingScriptC.SetActive(!shootingScriptC.activeSelf);
    }
}