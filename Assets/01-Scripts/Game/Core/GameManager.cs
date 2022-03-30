using System.Collections;
using System.Collections.Generic;
using Scripts.SO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;

    public List<QuestionDataSO> questionList;
    int _currentQuestionIndex;

    [Header("Question UI")]
    public TMP_Text question;

    public List<AnswerButtonInfo> answerButtonList;

    [Header("Audio Clip")]
    public AudioClip ButtonClickClip;
    public List<AudioClip> WinClipList;
    public List<AudioClip> LoseClipList;

    [Header("Transaction")]
    [SerializeField] Animator transactionAnimator;

    QuestionDataSO currentQuestion;
    UIShake uiShake;

    public static event System.Action<int, int> OnQuestionChanged;

    #region Mono Function

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        uiShake = GetComponent<UIShake>();

        IniQuiz();
        StartQuiz(false);
    }

    #endregion Mono Function

    private void IniQuiz()
    {
        questionList.Sort((a, b) => 1 - 2 * Random.Range(0, questionList.Count));

        _currentQuestionIndex = 0;
    }

    private void StartQuiz(bool playTransition = true)
    {
        StartCoroutine(this.CRStartQuiz(playTransition));
    }

    IEnumerator CRStartQuiz(bool playTransition)
    {
        if (playTransition)
        {
            transactionAnimator.SetTrigger("Start");

            yield return new WaitForSeconds(1.5f);
        }

        currentQuestion = GetQuestionInList();
        SetQuestion();
        SetSuffledAnswer();

        OnQuestionChanged?.Invoke(_currentQuestionIndex, questionList.Count);

        if (playTransition)
        {
            transactionAnimator.SetTrigger("End");
        }
    }

    public void NextQuiz()
    {
        _currentQuestionIndex++;
        StartQuiz();
    }

    public void RestartQuiz()
    {
        IniQuiz();
        StartQuiz();
    }

    private QuestionDataSO GetQuestionInList()
    {
        return questionList[_currentQuestionIndex];
    }

    private void SetQuestion()
    {
        question.text = currentQuestion.question;
    }

    private void SetSuffledAnswer()
    {
        answerButtonList.Sort((a, b) => 1 - 2 * Random.Range(0, answerButtonList.Count));

        answerButtonList[0].SetInfo(currentQuestion.correctAnswer, true);
        answerButtonList[1].SetInfo(currentQuestion.wrongAnswer1, false);
        answerButtonList[2].SetInfo(currentQuestion.wrongAnswer2, false);
        answerButtonList[3].SetInfo(currentQuestion.wrongAnswer3, false);
    }

    public void VerifyClickAnswer(bool isCorrect, GameObject button)
    {
        StartCoroutine(this.CRVerifyClickAnswer(isCorrect, button));
    }

    IEnumerator CRVerifyClickAnswer(bool isCorrect, GameObject button)
    {
        AudioManager.s_instance.PlaySFX(ButtonClickClip);

        answerButtonList.ForEach(button =>
        {
            button.DisableClick();
            button.PlayClickAnimation();
        });

        yield return new WaitForSeconds(2);

        answerButtonList.ForEach(button =>
        {
            button.StopClickAnimation();
            button.ChangeColor();
        });

        yield return new WaitForSeconds(2);

        if (isCorrect)
        {
            AudioManager.s_instance.PlaySFX(GetRandonClip(WinClipList));

            if (_currentQuestionIndex == questionList.Count - 1)
            {
                UIManager.s_instance.ShowFinishPanel();
            }
            else
            {
                UIManager.s_instance.ShowWinPanel();
            }
        }
        else
        {
            AudioManager.s_instance.PlaySFX(GetRandonClip(LoseClipList));

            uiShake.Shake();

            UIManager.s_instance.ShowLosePanel();
        }
    }

    private AudioClip GetRandonClip(List<AudioClip> clipList)
    {
        return clipList[Random.Range(0, clipList.Count)];
    }
}
