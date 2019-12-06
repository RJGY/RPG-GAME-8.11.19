using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueOption : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] text;
    public int index;
    public int option;
    public bool showDialogue;
    public Text dialogueText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        dialogueText.text = text[index];
        if (showDialogue)
        {
            Vector2 screen = new Vector2(Screen.width / 16, Screen.height / 9);
            GUI.Box(new Rect(0, screen.y * 6, Screen.width, screen.y * 3), "");
            dialogueText.gameObject.SetActive(true);

            if ( !(index >= text.Length-1 || index == option))
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 7.85f, screen.x, screen.y), "Next"))
                {
                    index++;
                    dialogueText.text = text[index];
                }
            }

            else if (index == option)
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 7.85f, screen.x, screen.y), "Decline"))
                {
                    index = text.Length - 1;
                    dialogueText.text = text[index];
                }
                if (GUI.Button(new Rect(screen.x * 12.5f, screen.y * 7.85f, screen.x, screen.y), "Accept"))
                {
                    index++;
                    dialogueText.text = text[index];
                }
            }
            else
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 7.85f, screen.x, screen.y), "Exit"))
                {
                    index = 0;
                    showDialogue = false;
                    dialogueText.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        else
        {
            dialogueText.gameObject.SetActive(false);
        }
    }
}
