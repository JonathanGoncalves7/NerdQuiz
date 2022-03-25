using TMPro;
using UnityEngine;

public class AnswerButtonInfo : MonoBehaviour
{
    public bool isCorrectAnswer = false;
    public string answer = string.Empty;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void OnClick()
    {
        GameManager.s_instance.VerifyClickAnswer(isCorrectAnswer, gameObject);
    }

    public void SetInfo(string answer, bool isCorrectAnswer)
    {
        this.answer = answer;
        this.isCorrectAnswer = isCorrectAnswer;

        text.text = answer;
    }
}
