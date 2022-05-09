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
        RestartGameState();
    }

    public void RestartGameState()
    {
        ShowStartScreen(true);
        ShowGameScreen(false);
        ShowCongratulationsScreen(false);
        gen.ResetCurrQnNum();
    }

    public void StartMathGame()
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
        if (nextQnButtonGO != null)
        {
            nextQnButtonGO.SetActive(false);
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

    public void ButtonPressedSetNumQuestions(GameObject textGO)
    {
        int numOfQns;
        string numberInText = textGO.GetComponent<Text>().text;
        bool success = int.TryParse(numberInText, out numOfQns);
        if(!success)
        {
            Debug.LogError("Could not parse numberInText!");
        }
        gen.SetTotalNumQuestions(numOfQns);
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

            //Prep for next round.
            scoreGO.GetComponent<ScoreKeeper>().ResetScore();
            EnableAllAnswerButtons(true);  
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
