using UnityEngine;
public enum STATE
{
    DISABLED,
    WAITING,
    TYPING
}

public class DialogueSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DialogueData dialogueData;
    int currentText = 0;
    bool finished = false;
    TypeTextAnimation typeText;
    public DialogueUI dialogueUI;

    STATE state;

    void Awake()
    {
        typeText = FindObjectOfType<TypeTextAnimation>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        typeText.TypeFinished = OnTypeFinished;
        void Awake()
        {
            if (dialogueUI == null)
            {
                dialogueUI = FindObjectOfType<DialogueUI>();
            }
        }

    }
    void Start()
    {
        state = STATE.DISABLED;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.DISABLED) return;
        switch (state)
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
        }
    }
    public void Next()
    {
        if (currentText == 0)
        {
            dialogueUI.Enable();
        }
        dialogueUI.SetName(dialogueData.talkScript[currentText].name);
        typeText.fullText = dialogueData.talkScript[currentText++].text;
        if (currentText == dialogueData.talkScript.Count) finished = true;

        typeText.StartTyping();
        state = STATE.TYPING;
    }
    void OnTypeFinished()
    {
        state = STATE.WAITING;
    }
    void Waiting()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!finished)
            {
                Next();
            }
            else
            {
                dialogueUI.Disable();
                state = STATE.DISABLED;
                currentText = 0;
                finished = false;
            }
        }
    }
    void Typing()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            typeText.Skip();
            state = STATE.WAITING;
        }
    }
}
