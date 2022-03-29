using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CRLoadScene))]
public class UIManager : MonoBehaviour
{
    public static UIManager s_instance;

    [Header("Panels")]
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject PausePanel;

    CRLoadScene loadScene;

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        loadScene = GetComponent<CRLoadScene>();
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

    public void OnClickPause()
    {
        AudioManager.s_instance.PlayButtonClick();
        PausePanel.SetActive(true);
    }

    public void OnClickResume()
    {
        AudioManager.s_instance.PlayButtonClick();
        PausePanel.SetActive(false);
    }

    public void OnClickMenu()
    {
        loadScene.OnCLickLoadScene(0);
    }

    public void OnClickConfig()
    {
        loadScene.OnCLickLoadAdditiveScene(1);
    }
}
