using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private GameSession gameSession;
    private Text scoreText;


    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}