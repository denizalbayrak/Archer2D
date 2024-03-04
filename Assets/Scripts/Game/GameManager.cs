using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float startTime;
    private float elapsedTime;
    private bool gameIsOver;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    int killedMonsters = 0;
    int collectedItems = 0;
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI itemCountText;
    public TextMeshProUGUI itemCountGamePanelText;
    public TextMeshProUGUI timerText;
    public PlayfabManager playfabController;

    public enum GameState { Starting, Playing, Paused, GameOver };
    public GameState currentState;

    void Awake()
    {     
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        playfabController = FindObjectOfType<PlayfabManager>();
        StartGame();
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Starting:
                break;
            case GameState.Playing:
                UpdateTimer();
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
    }

    void StartGame()
    {
        Time.timeScale = 1;
        currentState = GameState.Playing;
        startTime = Time.time;
    }

    void UpdateTimer()
    {
        elapsedTime = Time.time - startTime;
        
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        EndGame();
    }
    void EndGame()
    {
        gameOverPanel.SetActive(true);
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = "Survive Time: "+ string.Format("{0:00}:{1:00}", minutes, seconds);
        monsterCountText.text = "Killed Monster: " + killedMonsters.ToString();
        itemCountText.text = "Collected Gold: " + collectedItems.ToString();

        playfabController.SendLeaderBoard(collectedItems, Mathf.FloorToInt(elapsedTime), killedMonsters);
    }
    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.Paused;
            Time.timeScale = 0; 

            pausePanel.SetActive(true);
        }
        else if (currentState == GameState.Paused)
        {
            currentState = GameState.Playing;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }  
    
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

    }
    public void MonsterKilled()
    {
        killedMonsters++;
    }
    public void ItemCollected()
    {
        collectedItems++;
        itemCountGamePanelText.text = collectedItems.ToString();
    }
  
}
