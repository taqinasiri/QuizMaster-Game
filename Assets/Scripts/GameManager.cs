using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Quiz quiz;
    private EndScreen endScreen;

    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    private void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(quiz.isComplete)
        {
            endScreen.ShowFinalScore();
            endScreen.gameObject.SetActive(true);
            quiz.gameObject.SetActive(false);
        }
    }

    public void OnReplayLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}