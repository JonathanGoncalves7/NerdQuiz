using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AnswerButtonInfo : MonoBehaviour
{
    public Color NeutralColor;
    public Color CorrectColor;
    public Color WrongColor;

    public bool isCorrectAnswer = false;
    public string answer = string.Empty;

    Animator _animator;
    Button _button;
    Image _image;
    TMP_Text _text;

    ColorBlock _defaultColors;
    bool _canClick = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TMP_Text>();
        _image = GetComponent<Image>();

        _defaultColors = _button.colors;
        _canClick = true;
    }

    public void OnClick()
    {
        if (!_canClick) return;

        GameManager.s_instance.VerifyClickAnswer(isCorrectAnswer, gameObject);
    }

    public void SetInfo(string answer, bool isCorrectAnswer)
    {
        _button.colors = _defaultColors;

        this.answer = answer;
        this.isCorrectAnswer = isCorrectAnswer;

        _text.text = answer;
        _canClick = true;
    }

    public void PlayClickAnimation()
    {
        _animator.SetBool("PlayAnimation", true);
    }

    public void StopClickAnimation()
    {
        _animator.SetBool("PlayAnimation", false);
    }

    public void ChangeColor()
    {
        Color newColor = isCorrectAnswer ? CorrectColor : WrongColor;

        var colors = _button.colors;
        colors.normalColor = newColor;
        colors.highlightedColor = newColor;
        colors.pressedColor = newColor;
        colors.selectedColor = newColor;
        colors.disabledColor = newColor;
        _button.colors = colors;
    }

    public void DisableClick()
    {
        _canClick = false;
    }
}
