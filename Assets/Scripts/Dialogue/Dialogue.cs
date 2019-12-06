using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] text;
    public int index;
    public bool showDialogue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (showDialogue)
        {
            Vector2 screen = new Vector2(Screen.width / 16, Screen.height / 9);
            GUI.Box(new Rect(0, screen.y * 6, Screen.width, screen.y * 3), "");
            

            if (!(index >= text.Length-1))
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 7.85f, screen.x, screen.y), "Next"))
                {
                    index++;
                }
            }

            else
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 7.85f, screen.x, screen.y), "Exit"))
                {
                    index = 0;
                    showDialogue = false;
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}
