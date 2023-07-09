using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    // Timer Variables
    private bool timerStarted;
    [SerializeField] private float timerValue;

    // Text Variables
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI enemyText;

    // Sprite Variables
    private Image button;
    public Sprite[] pauseAndResumeButton;

    void Start()
    {
        button = GameObject.Find("Resume_And_Pause_Button").GetComponent<Image>();

        // Timer text value seen on game scene
        int gameTimerValue = Convert.ToInt32(timerValue);
        timerText.text = gameTimerValue.ToString();
    }
    void Update()
    {
        Timer();
        EnemyCount();
        ScoreCount();
    }
    public void Timer()
    {
        if (GameManager.instance.gameStarted is true)
        {
            timerStarted = true;

            timerValue -= Time.deltaTime;
            int second = Convert.ToInt32(timerValue);
            timerText.text = second.ToString();

            if (timerValue <= 0)
            {
                timerValue = 0;
                GameManager.instance.EndGame();
            }
        }
        else
        {
            return;
        }
    }
    public void EnemyCount()
    {
        enemyText.text = EnemySpawnner.instance.enemyCount.ToString();
    }
    public void ScoreCount()
    {
        scoreText.text = GameManager.instance.score.ToString();
    }
    public void PauseButton() 
    {
       button.sprite = pauseAndResumeButton[0];
    }
    public void ResumeButton()
    {
        button.sprite = pauseAndResumeButton[1];
    }
}
