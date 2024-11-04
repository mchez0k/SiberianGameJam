using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TExt : MonoBehaviour
{
    public Canvas canvas;
    public TMP_Text text;
    public float delayBeforeTyping = 6.5f;
    public float typingSpeed = 0.1f;
    private string fullText;
    private string currentText = "";

    void Start()
    {
        canvas.gameObject.SetActive(false);
        fullText = text.text;
        text.text = "";
        Invoke("ActivateCanvas", delayBeforeTyping);
    }

    void ActivateCanvas()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            text.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
