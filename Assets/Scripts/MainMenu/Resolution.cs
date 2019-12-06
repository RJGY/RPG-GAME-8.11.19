using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    
    public void SetResolution4K()
    {
        //4k
        Screen.SetResolution(3840, 2160, Screen.fullScreen);
    }
    public void SetResolution2K()
    {
        //2560×1440
        Screen.SetResolution(2560, 1440, Screen.fullScreen);
    }

    public void SetResolution1080()
    {
        //1920×1080
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
    public void SetResolution900()
    {
        //1600×900
        Screen.SetResolution(1600, 900, Screen.fullScreen);
    }
    public void SetResolution768()
    {
        //1366×768
        Screen.SetResolution(1366, 768, Screen.fullScreen);
    }
    public void SetResolution720()
    {
        //1280×720
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }
    public void SetResolution576()
    {
        //1024×576
        Screen.SetResolution(1024, 576, Screen.fullScreen);
    }

}
