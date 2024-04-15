using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidepanel : MonoBehaviour

{
    public GameObject panelToHide;

    private void Start()
    {
        // Ensure that a panel is assigned to hide
        if (panelToHide == null)
        {
            enabled = false; // Disable the script to prevent further errors
        }
    }

    public void OnButtonClick()
    {
        // Check if the panel to hide is not null
        if (panelToHide != null)
        {
            // Hide the panel by setting it inactive
            panelToHide.SetActive(false);
        }
    }
}