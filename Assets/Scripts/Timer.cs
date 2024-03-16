using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToComplateQuestoin = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion = false;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    private float timerValue;

    private void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer() => timerValue = 0;

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToComplateQuestoin;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToComplateQuestoin;
                loadNextQuestion = true;
            }
        }
    }
}