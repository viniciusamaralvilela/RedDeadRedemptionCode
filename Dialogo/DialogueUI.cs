using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI talkText;
    public float speed = 10.0f;
    bool open = false;

    void Awake()
    {
        // Opcional: verificar se está tudo atribuído
        if (background == null || nameText == null || talkText == null)
        {
            Debug.LogError("DialogueUI: Componentes não atribuídos no Inspector!");
        }
    }

    void Update()
    {
        float target = open ? 1f : 0f;
        background.fillAmount = Mathf.Lerp(background.fillAmount, target, speed * Time.deltaTime);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void Enable()
    {
        background.fillAmount = 0;
        open = true;
    }

    public void Disable()
    {
        open = false;
        nameText.text = "";
        talkText.text = "";
    }
}
