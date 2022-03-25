using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager s_instance;

    [Header("Panels")]
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;

    private void Awake()
    {
        s_instance = this;
    }

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        LosePanel.SetActive(true);
    }

    public void OnClickNextQuestion()
    {
        WinPanel.SetActive(false);
        GameManager.s_instance.NextQuiz();
    }

    public void OnClickRestartQuestion()
    {
        LosePanel.SetActive(false);
        GameManager.s_instance.RestartQuiz();
    }
}
