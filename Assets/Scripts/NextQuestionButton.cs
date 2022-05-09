using UnityEngine;
using UnityEngine.UI;

public class NextQuestionButton : MonoBehaviour
{
    public Sprite imageSmiley;
    public Sprite imageSadFace;

    public void SetHappyFace()
    {
        transform.GetComponent<Image>().sprite = imageSmiley;
    }

    public void SetSadFace()
    {
        transform.GetComponent<Image>().sprite = imageSadFace;
    }

}
