using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerButtonInfo : MonoBehaviour
{
    public bool isCorrectAnswer = false;
    public string answer = string.Empty;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClick()
    {
        if (isCorrectAnswer)
        {
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("worg");
        }
    }

    public void SetInfo(string answer, bool isCorrectAnswer)
    {
        this.answer = answer;
        this.isCorrectAnswer = isCorrectAnswer;

        UpdateInfo();
    }

    private void UpdateInfo()
    {
        text.text = answer;
    }
}
