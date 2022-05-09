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

    [Tooltip("Starting screen.")]
    public GameObject startScreenGO = null;

    [Tooltip("Game screen that contains game buttons and text.")]
    public GameObject gameScreenGO = null;

    [Tooltip("Congratulations screen when score target is met.")]
    public GameObject congratulationsScreenGO = null;

    MathQuestionGenerator gen = new MathQuestionGenerator();

    // Start is called before the first frame update
    void Start()
    {
        ShowStartScreen(true);
        ShowGameScreen(false);
        ShowCongratulationsScreen(false);
    }

    public void StartGame()
    {
        //When user clicked on number of question button and operator button, the buttons already took care of setting the values in MathQuestionGenerator.
        SetupQuestion();
        ShowStartScreen(false);
        ShowGameScreen(true);
        ShowCongratulationsScreen(false);
    }

    public void ShowStartScreen(bool show)
    {
        if (startScreenGO != null)
            startScreenGO.SetActive(show);
    }

    public void ShowGameScreen(bool show)
    {
        if (gameScreenGO != null)
            gameScreenGO.SetActive(show);

    }

    public void ShowCongratulationsScreen(bool show)
    {
        if (congratulationsScreenGO != null)
            congratulationsScreenGO.SetActive(show);
    }

    public void ShowNextQuestionButton(bool show)
    {
        if (nextQnButtonGO != null && nextQnButtonGO.activeInHierarchy)
        {
            nextQnButtonGO.SetActive(false);  //if next button is not showing, don't bother to hide it.
        }
    }

    private void SetupQuestion()
    {
        UpdateScoreText();  //Initialize with 0/10 to start with.

        timerGO.GetComponent<SliderTimer>().StartTimer(PopulateWithCorrectAnswer);

        ShowNextQuestionButton(false);

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
        string scoreText = "Getting closer to target: " + scoreGO.GetComponent<ScoreKeeper>().GetScore().ToString() + " / " + gen.GetTotalNumQuestionsString();
        scoreGO.GetComponent<Text>().text = scoreText;
    }

    public void ButtonPressedSetNumQuestions(int i)
    {
        gen.SetTotalNumQuestions(i);
    }

    public void ButtonPressedSetOperator(int i)
    {
        gen.SetMathOperator(i);
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

            //Debug.Log("Button " + i + " was pressed! With selectedAns " + selectedAns);
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
        //New weird idea... Keep going until you reach the goal you/the teacher set for yourself today.
        if (scoreGO.GetComponent<ScoreKeeper>().GetScore() >= gen.GetTotalNumQuestions())
        {
            ShowCongratulationsScreen(true);
        }
        else
        {
            SetupQuestion();
        }
        
    }

    public void PopulateAnswerButtons()
    {
        int[] possibleAnswers = gen.GetAnswerOptions();  //Todo: Might want to move it to passing in parameters later.

        for (int i = 0; i < possibleAnswers.Length; i++)
        {
            mcqAnswers[i].text = possibleAnswers[i].ToString();
        }

        EnableAllAnswerButtons(true);  //ct: Enable buttons after new values are populated.
    }

    private void PopulateWithCorrectAnswer()
    {
        int correctAns = gen.GetCurrCorrectAnswer();
        questionText.text = gen.GetQuestionString() + correctAns;
        EnableAllAnswerButtons(false);  //ct: Disable buttons after correct answer is shown.
        nextQnButtonGO.GetComponent<NextQuestionButton>().SetSadFace();
        nextQnButtonGO.SetActive(true);

    }
    
}
