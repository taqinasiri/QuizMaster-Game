using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question",fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] private string question = "Enter new question text here";

    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private byte correctAnswerIndex;

    public string GetQuestion() => question;

    public string GetAnswers(byte index) => answers[index];

    public string[] GetAnswers() => answers;

    public byte GetCorrectAnswerIndex() => correctAnswerIndex;
}