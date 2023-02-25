using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;

    [Header("In Game")]
    [SerializeField]
    private GameObject inGamePanel;
    [SerializeField]
    private TextMeshProUGUI textScore;

    [Header("Game Over")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI textHighScore;

    public void GameStart()
    {
        mainPanel.SetActive(false);
        inGamePanel.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        textHighScore.text = $"HIGH SCORE : {PlayerPrefs.GetInt("HIGHSCORE")}";
    }

    public void UpdateScore(int score)
    {
        if (score < 10 )
        {
            textScore.text = score.ToString("D2");
        }
        else
        {
            textScore.text = score.ToString();
        }
    }
}
