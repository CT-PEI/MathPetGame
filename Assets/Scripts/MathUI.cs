using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MathUI : MonoBehaviour
{
    [Tooltip("Text to hold full question.")]
    public Text questionNumberText = null;

    [Tooltip("Text to hold full question.")]
    public Text questionText = null;

    [Tooltip("Text for MCQ from left to right.")]
    public Text[] mcqAnswers = null;

    [Tooltip("Next question button that also indicates if player's answer is right or wrong.")]
    public GameObject nextQnButtonGO = null;

    [Tooltip("Text to hold full question.")]
    public Text nextQnTextGO = null;

    [Tooltip("Timer slider.")]
    public GameObject timerGO = null;

    [Tooltip("Time allowed per question.")]
    public float fillTimeInSecs = 10.0f;

    [Tooltip("Game object that keeps track of score.")]
    public GameObject scoreGO = null;

    [Tooltip("Congratulations screen when score target is met.")]
    public GameObject congratulationsScreenGO = null;

    MathQuestionGenerator gen = new MathQuestionGenerator();

    // Start is called before the first frame update
    void Start()
    {
        ShowCongratulationsScreen(false);
        SetupQuestion();
    }

    private void SetupQuestion()
    {
        UpdateScoreText();  //Initialize with 0/10 to start with.

        timerGO.GetComponent<SliderTimer>().StartTimer(PopulateWithCorrectAnswer);

        if (nextQnButtonGO.activeInHierarchy)
        {
            nextQnButtonGO.SetActive(false);  //if next button is not showing, don't bother to hide it.
        }

        gen.CreateMathQuestion();

        if (questionNumberText != null)
        {
            questionNumberText.text = gen.GetQuestionNumberString();
        }

        if (questionText != null)
        {
            questionText.text = gen.GetQuestionString();
        }

        PopulateAnswerButtons();

        timerGO.GetComponent<SliderTimer>().SetTimerValue(fillTimeInSecs);  //ct: Reset timer at the start of each question.

    }


    private void UpdateScoreText()
    {
        string scoreText = "Score: " + scoreGO.GetComponent<ScoreKeeper>().GetScore().ToString() + " / " + gen.GetTotalNumQuestionsString();
        scoreGO.GetComponent<Text>().text = scoreText;
    }


    public void ButtonPressedWithIndex(int i)
    {
        timerGO.GetComponent<SliderTimer>().StopTimer();

        if ( i < mcqAnswers.Length)  //ct: prevent error when hooking up via unity component. Ensure that value passed in is not bigger than array size.
        {
            EnableAllAnswerButtons(false);

            string selectedAns = mcqAnswers[i].text;

            questionText.text = gen.GetQuestionString() + selectedAns;

            bool isAnsRight = gen.IsAnswerCorrect(selectedAns);
            if (isAnsRight)
            {
                nextQnButtonGO.GetComponent<NextQuestionButton>().SetHappyFace();
                scoreGO.GetComponent<ScoreKeeper>().IncrementScore();
                UpdateScoreText();
            }
            else
            {
                nextQnButtonGO.GetComponent<NextQuestionButton>().SetSadFace();
            }
            nextQnButtonGO.SetActive(true);

            Debug.Log("Button " + i + " was pressed! With selectedAns " + selectedAns);
        }
    }

    private void EnableAllAnswerButtons(bool enabled)
    {
        for (int i=0; i < mcqAnswers.Length; i++)
        {
            Button but = mcqAnswers[i].GetComponentInParent<Button>();

            if (but != null)
            {
                but.interactable = enabled;
            }
            
        }
    }

    public void NextQnButtonPressed()
    {
        Debug.Log("Next Button Pressed in math ui");

        //New weird idea... Keep going until you reach the goal you/the teacher set for yourself today.
        if (scoreGO.GetComponent<ScoreKeeper>().GetScore() >= gen.GetTotalNumQuestions())
        {
            Debug.Log("Show CONGRATULATIONS screen");
            ShowCongratulationsScreen(true);
        }
        else
        {
            SetupQuestion();
        }
        
    }

    public void ShowCongratulationsScreen(bool show)
    {
        if (congratulationsScreenGO != null)
            congratulationsScreenGO.SetActive(show);
    }
   
    public void PopulateAnswerButtons()
    {
        int correctAns = gen.GetCurrCorrectAnswer();

        int randomNumber1 = Random.Range(1, 6);
        int randomNumber2 = Random.Range(6, 11);

        int wrongAns1 = correctAns + randomNumber1;
        int wrongAns2 = correctAns + randomNumber2;

        int positionOfCorrectAnswer = Random.Range(1, 4);  //So that position of correct answer is not predictable.

        int[] options;

        switch(positionOfCorrectAnswer)
        {
            case 1:
                options = new int[] { correctAns, wrongAns1, wrongAns2 };
                break;
            case 2:
                options = new int[] { wrongAns1, correctAns, wrongAns2 };
                break;
            case 3:
                options = new int[] { wrongAns1, wrongAns2, correctAns };
                break;
            default:
                Debug.Log("Position of correct answer is out of range!");  //todo: Error instead of just log.
                options = new int[] { wrongAns1, wrongAns2, correctAns };
                break;
        }

        for (int i = 0; i < options.Length; i++)
        {
            mcqAnswers[i].text = options[i].ToString();
        }

        EnableAllAnswerButtons(true);  //ct: Enable buttons after new values are populated.
    }

    private void PopulateWithCorrectAnswer()
    {
        int correctAns = gen.GetCurrCorrectAnswer();
        questionText.text = gen.GetQuestionString() + correctAns;
        nextQnButtonGO.GetComponent<NextQuestionButton>().SetSadFace();
        nextQnButtonGO.SetActive(true);
    }
    
}
