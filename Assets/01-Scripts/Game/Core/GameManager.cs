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

    [Header("Question UI")]
    public TMP_Text question;

    public AnswerButtonInfo answerButtonA;
    public AnswerButtonInfo answerButtonB;
    public AnswerButtonInfo answerButtonC;
    public AnswerButtonInfo answerButtonD;

    [Header("Audio Clip")]
    public AudioClip ButtonClickClip;
    public List<AudioClip> WinClipList;
    public List<AudioClip> LoseClipList;

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

    public void VerifyClickAnswer(bool isCorrect, GameObject button)
    {
        StartCoroutine(this.CRVerifyClickAnswer(isCorrect, button));
    }

    IEnumerator CRVerifyClickAnswer(bool isCorrect, GameObject button)
    {
        AudioManager.s_instance.PlayAudio(ButtonClickClip);

        yield return new WaitForSeconds(ButtonClickClip.length);

        if (isCorrect)
        {
            AudioManager.s_instance.PlayAudio(GetRandonClip(WinClipList));

            UIManager.s_instance.ShowWinPanel();
        }
        else
        {
            AudioManager.s_instance.PlayAudio(GetRandonClip(LoseClipList));

            UIManager.s_instance.ShowLosePanel();
        }
    }

    private AudioClip GetRandonClip(List<AudioClip> clipList)
    {
        return clipList[Random.Range(0, clipList.Count)];
    }
}
