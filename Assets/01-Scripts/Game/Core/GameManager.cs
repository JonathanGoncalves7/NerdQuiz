using System.Collections;
using System.Collections.Generic;
using Scripts.SO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;

    [SerializeField] List<QuestionDataSO> questionList;
    int _currentQuestionIndex;

    [Header("Question UI")]
    [SerializeField] TMP_Text question;

    [SerializeField] List<AnswerButtonInfo> answerButtonList;

    [Header("Audio Clip")]
    [SerializeField] AudioClip ButtonClickClip;
    [SerializeField] List<AudioClip> WinClipList;
    [SerializeField] List<AudioClip> LoseClipList;

    [Header("Transaction")]
    [SerializeField] Animator transactionAnimator;

    [Header("Finish")]
    [SerializeField] ParticleSystem FinishEffect;

    QuestionDataSO _currentQuestion;
    UIShake _UIShake;

    public static event System.Action<int, int> OnQuestionChanged;

    #region Mono Function

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        _UIShake = GetComponent<UIShake>();

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

        _currentQuestion = GetQuestionInList();
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
        question.text = _currentQuestion.question;
    }

    private void SetSuffledAnswer()
    {
        answerButtonList.Sort((a, b) => 1 - 2 * Random.Range(0, answerButtonList.Count));

        answerButtonList[0].SetInfo(_currentQuestion.correctAnswer, true);
        answerButtonList[1].SetInfo(_currentQuestion.wrongAnswer1, false);
        answerButtonList[2].SetInfo(_currentQuestion.wrongAnswer2, false);
        answerButtonList[3].SetInfo(_currentQuestion.wrongAnswer3, false);
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
                StartCoroutine(this.CRFinishEffect());
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

            _UIShake.Shake();

            UIManager.s_instance.ShowLosePanel();
        }
    }

    private AudioClip GetRandonClip(List<AudioClip> clipList)
    {
        return clipList[Random.Range(0, clipList.Count)];
    }

    IEnumerator CRFinishEffect()
    {
        FinishEffect.Play();

        yield return new WaitForSeconds(2);

        FinishEffect.Stop();
    }
}
