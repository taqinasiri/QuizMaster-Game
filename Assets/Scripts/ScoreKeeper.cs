using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionsSeen = 0;

    public int GetCorrectAnswers() => correctAnswers;

    public void IncrementCorrectAnswers() => correctAnswers++;

    public int GetQuestionsSeen() => questionsSeen;

    public void IncrementQuestionsSeen() => questionsSeen++;

    public int CalculateScore() => Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
}