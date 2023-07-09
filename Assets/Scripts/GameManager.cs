using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager _uiManager;

    public bool gameStarted;
    public bool gamePaused = false;

    public int score = 0;

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
        score = 0;
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
        EnemySpawnner.instance.InstantiateEnemy();
        CollectableSpawnner.instance.InstantiateCollectable();
        CollectableSpawnner.instance.InstantiateCollectableClone();
    }
    public void ResumeOrPauseGame()
    {
        if (gamePaused is false)
        {
            gamePaused = true;
            Time.timeScale = 0f;

            _uiManager.PauseButton();

            Debug.Log("Game Paused");
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1f;

            _uiManager.ResumeButton();

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
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCameraTracking>().enabled = false;
        Invoke(nameof(RestartGame),2f);
    }
}
