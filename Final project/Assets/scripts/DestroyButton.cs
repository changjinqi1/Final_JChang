using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{

    public void OnButtonClick()
    {
        Destroy(this.gameObject);
    }
}