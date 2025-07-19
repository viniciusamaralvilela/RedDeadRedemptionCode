using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class TypeTextAnimation : MonoBehaviour
{
    public Action TypeFinished;
    public float typeDelay = 0.05f;
    public TextMeshProUGUI textobject;

    public string fullText;

    public void StartTyping()
    {
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }
    public void Skip()
    {
        StopAllCoroutines();
        textobject.maxVisibleCharacters = fullText.Length; // mostra o texto completo instantaneamente
    }


    IEnumerator TypeText()
    {
        textobject.text = fullText;
        textobject.maxVisibleCharacters = 0;

        for (int i = 0; i <= textobject.text.Length; i++)
        {
            textobject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeDelay);
        }
        TypeFinished?.Invoke();

    }
}
