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
    private string mathOperator = "+";  //ct: Expose this later to teacher's setting files.


    private string questionString;  //ct: This is what prints out on screen as the question.
    private int answer;


    public class MathQuestion
    {
        //enum operators { ADD, SUB, DIV, MUL };
        //ct: Just do ADD for now.

        public int maxVal;  //ct: Max value of arguments.
        //public string questionStr;
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

                //GUI.Box.
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


    public int GetCurrCorrectAnswer()
    {
        return answer;
    }


    public int CreateMathQuestion()
    {
        currQnNum++;

        lhs = Random.Range(0, lhsMaxValue+1);
        rhs = Random.Range(0, rhsMaxValue+1);

        MathQuestion question = new MathQuestion();

        if (mathOperator == "+")
        {
            string lhsStr = lhs.ToString();
            string rhsStr = rhs.ToString();

            questionString = lhsStr + " + " + rhsStr + " = ";
            answer = lhs + rhs;
        }
        // else if(mathOperator == "-")
        // {
        //     question.questionStr = lhs + "-" + rhs + "=";
        //     question.answer = lhs - rhs;
        // }
        // else if(mathOperator == "*")
        // {
        //     question.questionStr = lhs + "*" + rhs + "=";
        //     question.answer = lhs * rhs;
        // }
        // else if(mathOperator == "/")
        // {
        //     question.questionStr = lhs + "/" + rhs + "=";
        //     question.answer = lhs / rhs;
        // }    


        return question.answer;
    }


    //int main()
    //{
    //    //cout << "\nWelcome to the math exercise buddy!\n";
    //    MathQuestion question;

    //    string questionString;
    //    int answer;

    //    int correctAns;

    //    //ct: Print out 10 questions
    //    for (int i = 1; i < 3; i++)
    //    {
    //        cout << "\nQuestion " << i << ":\n";

    //        correctAns = question.createMathQuestion("+", 10);
    //        cout << question.questionStr;
    //        cin >> answer;


    //        cout << "\nCorrect Answer: " << correctAns;
    //        if (answer == correctAns)
    //        {
    //            cout << "\nGenius!\n";
    //        }
    //        else
    //        {
    //            cout << "\nYOU CAN DO IT! TRY AGAIN!\n";
    //        }

    //    }

    //}




};


