using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    bool isFullscreen;

    void Start()
    {
        isFullscreen = Screen.fullScreen;
    }


    public void SetScreen()
    {
        Screen.SetResolution(Screen.width, Screen.height, !isFullscreen);
    }
}
