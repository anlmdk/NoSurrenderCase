using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uiManager;

    public bool gameStarted;
    public bool gamePaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Update()
    {
        GameEvent();
    }
    void GameEvent()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        gameStarted = true;
    }
    public void ResumeOrPauseGame()
    {
        if (gamePaused is false)
        {
            gamePaused = true;
            Time.timeScale = 0f;

            uiManager.PauseButton();

            Debug.Log("Game Paused");
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1f;

            uiManager.ResumeButton();

            Debug.Log("Game Resumed");
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        gameStarted = false;

        Invoke(nameof(RestartGame),3f);
    }
}
