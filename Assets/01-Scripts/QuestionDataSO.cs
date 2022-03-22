using Scripts.Enums;
using UnityEngine;

namespace Scripts.SO
{
    [CreateAssetMenu(fileName = "QuestionDataSO", menuName = "NerdQuiz/QuestionDataSO", order = 0)]
    public class QuestionDataSO : ScriptableObject
    {
        [Header("Question")]
        public string question = string.Empty;

        [Header("Answer")]
        public string correctAnswer = string.Empty;

        public string wrongAnswer1 = string.Empty;
        public string wrongAnswer2 = string.Empty;
        public string wrongAnswer3 = string.Empty;

        [Header("Others")]
        public ThemeQuestion theme;
    }
}