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

    public void GameStart()
    {
        mainPanel.SetActive(false);
        inGamePanel.SetActive(true);
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
