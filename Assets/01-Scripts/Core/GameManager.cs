using System.Collections;
using System.Collections.Generic;
using Scripts.SO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;

    public List<QuestionDataSO> questionList;
    int currentQuestionIndex;

    public TMP_Text question;
    public AnswerButtonInfo answerButtonA;
    public AnswerButtonInfo answerButtonB;
    public AnswerButtonInfo answerButtonC;
    public AnswerButtonInfo answerButtonD;

    QuestionDataSO currentQuestion;

    #region Mono Function

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        IniQuiz();
        StartQuiz();
    }

    #endregion Mono Function

    private void IniQuiz()
    {
        questionList.Sort((a, b) => 1 - 2 * Random.Range(0, questionList.Count));
        currentQuestionIndex = 0;
    }

    private void StartQuiz()
    {
        currentQuestion = GetQuestionInList();
        SetQuestion();
        SetSuffledAnswer();
    }

    public void NextQuiz()
    {
        if (currentQuestionIndex == questionList.Count - 1)
        {
            Debug.Log("Finish quiz");

            RestartQuiz();
        }
        else
        {
            currentQuestionIndex++;
            StartQuiz();
        }
    }

    public void RestartQuiz()
    {
        IniQuiz();
        StartQuiz();
    }

    private QuestionDataSO GetQuestionInList()
    {
        return questionList[currentQuestionIndex];
    }

    private void SetQuestion()
    {
        question.text = currentQuestion.question;
    }

    private void SetSuffledAnswer()
    {
        List<AnswerButtonInfo> indexButtons = new List<AnswerButtonInfo>();
        indexButtons.Add(answerButtonA);
        indexButtons.Add(answerButtonB);
        indexButtons.Add(answerButtonC);
        indexButtons.Add(answerButtonD);
        indexButtons.Sort((a, b) => 1 - 2 * Random.Range(0, indexButtons.Count));

        indexButtons[0].SetInfo(currentQuestion.correctAnswer, true);
        indexButtons[1].SetInfo(currentQuestion.wrongAnswer1, false);
        indexButtons[2].SetInfo(currentQuestion.wrongAnswer2, false);
        indexButtons[3].SetInfo(currentQuestion.wrongAnswer3, false);
    }
}
