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
        if (isCorrectAnswer)
        {
            UIManager.s_instance.ShowWinPanel();
        }
        else
        {
            UIManager.s_instance.ShowLosePanel();
        }
    }

    public void SetInfo(string answer, bool isCorrectAnswer)
    {
        this.answer = answer;
        this.isCorrectAnswer = isCorrectAnswer;

        text.text = answer;
    }
}
