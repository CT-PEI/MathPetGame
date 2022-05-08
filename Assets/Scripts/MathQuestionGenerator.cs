using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MathQuestionGenerator
{
    private int currQnNum = 0;  //ct: Which question is the player currently on?

    //ct: TODO: Maybe expose this to 1. component value, 2. text file setting, 3. mutiplayer, and teacher can control live while student is using app.
    private int totalNumQns = 10;  //ct: Total number of questions for this set of exercise. Can be set outside of code by teacher later.

    //ct: This is for question equation values.
    private int lhs = 0;
    private int rhs = 0;

    //ct: TODO: Maybe expose this to 1. component value, 2. text file setting, 3. mutiplayer, and teacher can control live while student is using app.
    //ct: Purpose of separating the values is to give more control to the teacher.
    private int lhsMaxValue = 20;  
    private int rhsMaxValue = 20;
    private Operators mathOperator = Operators.ADD;  //ct: Expose this later to teacher's setting files.

    private string questionString;  //ct: This is what prints out on screen as the question.
    private int answer;

    private enum Operators { ADD, SUB, DIV, MUL };

    public class MathQuestion
    {
        public int maxVal;  //ct: Max value of arguments.
        public int answer;        
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
        string qnNumStr = "Question " + currQnNum + " of " + totalNumQns + ":";
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


    public int GetCurrCorrectAnswer()
    {
        return answer;
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
                questionOperator = "/";
                answer = lhs / rhs;
                break;
            case Operators.MUL:
                questionOperator = "*";
                answer = lhs * rhs;
                break;
            default:
                Debug.Log("Operator not found");
                break;
        }

        questionString = lhsStr + " " + questionOperator + " " +  rhsStr + " = ";
        
        return question.answer;
    }

};


