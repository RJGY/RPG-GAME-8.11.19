using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    bool audio = true;

    public void ToggleAudio()
    {
        // Toggles audio depending on the bool status.
        if (audio)
        {
            audio = false;
            AudioListener.volume = 0;
        }
        else
        {
            audio = true;
            AudioListener.volume = 1;
        }
    }
}
