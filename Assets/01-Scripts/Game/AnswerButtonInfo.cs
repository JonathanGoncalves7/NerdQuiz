using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AnswerButtonInfo : MonoBehaviour
{
    Animator animator;
    Button button;
    Image image;

    public Color NeutralColor;
    public Color CorrectColor;
    public Color WrongColor;

    public bool isCorrectAnswer = false;
    public string answer = string.Empty;

    private TMP_Text text;

    ColorBlock defaultColors;

    bool isCanClick = false;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        image = GetComponent<Image>();

        defaultColors = button.colors;
        isCanClick = true;
    }

    public void OnClick()
    {
        if (!isCanClick) return;

        GameManager.s_instance.VerifyClickAnswer(isCorrectAnswer, gameObject);
    }

    public void SetInfo(string answer, bool isCorrectAnswer)
    {
        button.colors = defaultColors;

        this.answer = answer;
        this.isCorrectAnswer = isCorrectAnswer;

        text.text = answer;
        isCanClick = true;
    }

    public void PlayClickAnimation()
    {
        animator.SetBool("PlayAnimation", true);
    }

    public void StopClickAnimation()
    {
        animator.SetBool("PlayAnimation", false);
    }

    public void ChangeColor()
    {
        Color newColor = isCorrectAnswer ? CorrectColor : WrongColor;

        var colors = button.colors;
        colors.normalColor = newColor;
        colors.highlightedColor = newColor;
        colors.pressedColor = newColor;
        colors.selectedColor = newColor;
        colors.disabledColor = newColor;
        button.colors = colors;
    }

    public void DisableClick()
    {
        isCanClick = false;
    }
}
