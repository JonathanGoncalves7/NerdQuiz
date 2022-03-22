using System.Collections;
using System.Collections.Generic;
using Scripts.SO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;

    public List<QuestionDataSO> questionList;

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
        StartQuiz();
    }

    #endregion Mono Function

    private void StartQuiz()
    {
        currentQuestion = GetQuestionInList();
        SetQuestion();
        SetSuffledAnswer();
    }

    private QuestionDataSO GetQuestionInList()
    {
        return questionList[Random.Range(0, questionList.Count)];
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

        indexButtons[0].isCorrectAnswer = true;

        indexButtons[0].SetInfo(currentQuestion.correctAnswer, true);
        indexButtons[1].SetInfo(currentQuestion.wrongAnswer1, false);
        indexButtons[2].SetInfo(currentQuestion.wrongAnswer2, false);
        indexButtons[3].SetInfo(currentQuestion.wrongAnswer3, false);
    }
}
