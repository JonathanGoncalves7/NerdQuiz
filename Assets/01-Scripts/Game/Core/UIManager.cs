using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CRLoadScene))]
public class UIManager : MonoBehaviour
{
    public static UIManager s_instance;

    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject FinishPanel;
    [SerializeField] GameObject LosePanel;

    [SerializeField] TMP_Text ProgressText;

    CRLoadScene _loadScene;

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        _loadScene = GetComponent<CRLoadScene>();
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

    public void ShowFinishPanel()
    {
        FinishPanel.SetActive(true);
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
        FinishPanel.SetActive(false);
        GameManager.s_instance.RestartQuiz();
    }

    public void OnClickMenu()
    {
        _loadScene.OnCLickLoadScene(0);
    }

    public void OnClickConfig()
    {
        _loadScene.OnCLickLoadAdditiveScene(1);
    }

    private void GameManager_OnQuestionChanged(int currentIndex, int totalIndex)
    {
        ProgressText.text = (currentIndex + 1) + "/" + totalIndex;
    }
}
