using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause")) 
        {
            TogglePaused();
        }
    }

    private void TogglePaused()
    {
        Time.timeScale = Time.timeScale > 0f ? 0f : 1f;
    }
}
