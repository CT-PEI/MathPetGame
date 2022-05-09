using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MathQuestionGenerator
{
    private int currQnNum = 0;  //ct: Which question is the player currently on?

    //ct: TODO: Maybe expose this to 1. component value, 2. text file setting, 3. mutiplayer, and teacher can control live while student is using app.
    private int totalNumQns = 5;  //ct: Total number of questions for this set of exercise. Can be set outside of code by teacher later.

    //ct: This is for question equation values.
    private int lhs = 0;
    private int rhs = 0;

    //ct: TODO: Maybe expose this to 1. component value, 2. text file setting, 3. mutiplayer, and teacher can control live while student is using app.
    //ct: Purpose of separating the values is to give more control to the teacher.
    private int lhsMaxValue = 20;  
    private int rhsMaxValue = 20;
    private Operators mathOperator = Operators.ADD;

    private string questionString;  //ct: This is what prints out on screen as the question.
    private int answer;

    private enum Operators { ADD, SUB, DIV, MUL };

    public class MathQuestion
    {
        public int maxVal;  //ct: Max value of arguments.
        public int answer;        
    }
    

    public void SetTotalNumQuestions(int i)
    {
        totalNumQns = i;
    }

    public void SetMathOperator(int operatorEnumIndex)
    {
        switch (operatorEnumIndex)
        {
            case 0:
                mathOperator = Operators.ADD;
                break;
            case 1:
                mathOperator = Operators.SUB;
                break;
            case 2:
                mathOperator = Operators.DIV;
                break;
            case 3:
                mathOperator = Operators.MUL;
                break;
        }
    }

    public bool IsAnswerCorrect(string selectedAnswer)
    {
        int result;
        bool successful = int.TryParse(selectedAnswer, out result);  //ct: int.TryParse gives result in out result.
        if (successful)
        {
            if (result == answer)
            {
                return true;
            }
            
        }
        else
        {
            //ct: TODO: Handle this gracefully in game.
            Debug.Log("int.TryParse failed");
        }

        return false;
    }

    public string GetQuestionString()
    {
        return questionString;
    }

    public string GetQuestionNumberString()
    {
        string qnNumStr = "Question " + currQnNum + ":";
        return qnNumStr;
    }

    public string GetTotalNumQuestionsString()
    {
        return totalNumQns.ToString();
    }

    public int GetTotalNumQuestions()
    {
        return totalNumQns;
    }

    public void ResetCurrQnNum()
    {
        currQnNum = 0;
    }

    public int CreateMathQuestion()
    {
        currQnNum++;

        //Range that includes the value itself.
        lhs = Random.Range(0, lhsMaxValue+1);
        rhs = Random.Range(0, rhsMaxValue+1);

        MathQuestion question = new MathQuestion();

        //String comparison is expensive in general, but since the string is always just 1 character, it does not really matter.
        //Better practice to use ENUM here, but it will not make much of a difference here.

        string lhsStr = lhs.ToString();
        string rhsStr = rhs.ToString();
        string questionOperator = "";

        switch (mathOperator)
        {
            case Operators.ADD:
                questionOperator = "+";
                answer = lhs + rhs;
                break;
            case Operators.SUB:
                questionOperator = "-";
                answer = lhs - rhs;
                break;
            case Operators.DIV:
                questionOperator = "÷";
                answer = lhs / rhs;
                break;
            case Operators.MUL:
                questionOperator = "x";
                answer = lhs * rhs;
                break;
            default:
                Debug.Log("Operator not found");
                break;
        }

        questionString = lhsStr + " " + questionOperator + " " +  rhsStr + " = ";
        
        return question.answer;
    }


    public int GetCurrCorrectAnswer()
    {
        return answer;
    }


    public int[] GetAnswerOptions()
    {
        int correctAns = answer;

        //Avoid 0 - so that you don't get the same answer as the correct answer.
        //Have staggered range so that the 2 wrong answers will not be the same.
        int randomNumber1 = Random.Range(1, 6);
        int randomNumber2 = Random.Range(6, 11);

        //So that it is not predicatble that the correct answer is always the min / middle value.
        int wrongAns1;
        int signRandNum = Random.Range(1, 3);
        switch (signRandNum)
        {
            case 1:
                wrongAns1 = correctAns + randomNumber1;
                break;
            case 2:
            default:
                wrongAns1 = correctAns - randomNumber1;
                break;
        }

        int wrongAns2 = correctAns + randomNumber2;

        //Randomize position of correct answer.
        int positionOfCorrectAnswer = Random.Range(1, 4); 
        switch (positionOfCorrectAnswer)
        {
            case 1:
                return new int[] { correctAns, wrongAns1, wrongAns2 };
            case 2:
                return new int[] { wrongAns1, correctAns, wrongAns2 };
            case 3:
            default:
                return new int[] { wrongAns1, wrongAns2, correctAns };
        }


    }

};


