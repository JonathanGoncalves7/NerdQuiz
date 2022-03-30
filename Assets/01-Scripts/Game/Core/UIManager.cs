using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CRLoadScene))]
public class UIManager : MonoBehaviour
{
    public static UIManager s_instance;

    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;

    [SerializeField] TMP_Text ProgressText;

    CRLoadScene loadScene;

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        loadScene = GetComponent<CRLoadScene>();
    }

    private void OnEnable()
    {
        GameManager.OnQuestionChanged += GameManager_OnQuestionChanged;
    }

    private void OnDisable()
    {
        GameManager.OnQuestionChanged -= GameManager_OnQuestionChanged;
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

    public void OnClickMenu()
    {
        loadScene.OnCLickLoadScene(0);
    }

    public void OnClickConfig()
    {
        loadScene.OnCLickLoadAdditiveScene(1);
    }

    private void GameManager_OnQuestionChanged(int currentIndex, int totalIndex)
    {
        ProgressText.text = (currentIndex + 1) + "/" + totalIndex;
    }
}
