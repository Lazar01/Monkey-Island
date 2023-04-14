using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject howToPlayPanelStart;
    public GameObject howToPlayPanelPause;

    public TextMeshProUGUI finalScoreText;

    private void Start()
    {
    }
    private void ShowPanel(GameObject panel)
    {
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        howToPlayPanelPause.SetActive(false);
        howToPlayPanelStart.SetActive(false);

        if (panel !=null)
        {
            panel.SetActive(true);
        }
    }

    public void StartGame()
    {
        ShowPanel(null);
    }

    public void GameOver(int n)
    {
        ShowPanel(gameOverPanel);
        finalScoreText.text = "Final score: " + n;
    }
    public void Reset()
    {
        ShowPanel(startPanel);
    }
    public void Pause()
    {
        ShowPanel(pausePanel);
    }
    public void HowToPlayStart()
    {
        ShowPanel(howToPlayPanelStart);
    }
    public void HowToPlayPause()
    {
        ShowPanel(howToPlayPanelPause);
    }
}
