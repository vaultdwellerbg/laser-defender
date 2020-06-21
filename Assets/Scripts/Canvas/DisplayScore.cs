using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private GameSession gameSession;
    private TextMeshProUGUI scoreText;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (gameSession != null)
        {
            scoreText.text = gameSession.GetScore().ToString();
        }  
    }
}
