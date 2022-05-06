using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextQuestionButton : MonoBehaviour
{
    public Sprite imageSmiley;
    public Sprite imageSadFace;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowNextQuestionButton()
    {
        transform.gameObject.SetActive(true);
    }

    public void HideNextQuestionButton()
    {
        transform.gameObject.SetActive(false);
    }

    public void SetHappyFace()
    {
        transform.GetComponent<Image>().sprite = imageSmiley;
        transform.GetComponentInChildren<Text>.text = "GET IT!";
    }

    public void SetSadFace()
    {
        transform.GetComponent<Image>().sprite = imageSadFace;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
