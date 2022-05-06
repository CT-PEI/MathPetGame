using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public void SetText(string text)
    {
        Button tgl = transform.GetComponent<Button>();
        Image img = GetComponent<Image>();
        Text txt = transform.Find("Text").GetComponent<Text>();

        txt.text = "11";


    }

    public Button ansButton;

    private void Start()
    {
        //Button
    }
}
