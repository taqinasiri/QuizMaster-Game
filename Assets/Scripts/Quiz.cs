using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private List<QuestionSO> questions = new();
    private QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] private GameObject[] answerButtons;

    private byte correctAnswerIndex;
    private bool hasAnsverdEarly = true;

    [Header("Button Colors")]
    [SerializeField] private Sprite currectAnswerSprite;

    [SerializeField] private Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Image tiemrImage;

    [Header("Scoring")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("ProgressBar")]
    [SerializeField] private Slider progressBar;

    public bool isComplete = false;

    private ScoreKeeper scoreKeeper;

    private Timer timer;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        tiemrImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsverdEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!timer.isAnsweringQuestion && !hasAnsverdEarly)
        {
            DisplayAnswer(-1);
            SetButtonsState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsverdEarly = true;
        DisplayAnswer(index);
        SetButtonsState(false);
        timer.CancelTimer();
        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}%";
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;
        if(index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = currectAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string corrctAnswer = currentQuestion.GetAnswers(correctAnswerIndex);
            questionText.text = $"No ,Correct answer is : \n {corrctAnswer}";
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    private void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonsState(true);
            SetDefaultButtonSprites();
            GetRandomQuestoin();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestoin()
    {
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        TextMeshProUGUI buttonText;
        for(byte i = 0; i < answerButtons.Length; i++)
        {
            buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswers(i);
        }
    }

    private void SetButtonsState(bool state)
    {
        Button button;
        for(byte i = 0; i < answerButtons.Length; i++)
        {
            button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for(byte i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}