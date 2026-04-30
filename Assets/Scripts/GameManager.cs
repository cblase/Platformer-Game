using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player, Menu, StartBtn, RestartBtn, ResumeBtn;
    public float TimeLimit = 120;
    public TMP_Text TimerText, ScoreText, EndMSG;

    private bool GameRunning = false;
    private PlayerInventory playerInventory;
    private PlayerController playerController;
    private PlayerInput playerInput;
    private float gameTimer;
    private bool gameStarted = false;

    private void Awake()
    {
        playerInventory = Player.gameObject.GetComponent<PlayerInventory>();
        playerController = Player.gameObject.GetComponent<PlayerController>();
        playerInput = Player.gameObject.GetComponent<PlayerInput>();
        TimerText.text = "Seconds: " + TimeLimit;

    }

    //manages game timer, and game over states
    void Update()
    {
        if (GameRunning)
        {
            gameTimer -= Time.deltaTime * 1000;
            TimerText.text = "Seconds: " + (int)Mathf.Floor(gameTimer/1000);

            if (gameTimer <= 0 || playerController.IsDead)
            {
                gameTimer = 0;
                TimerText.text = "Seconds: " + 0;
                GameRunning = false;
                GameOver();
            }
        }
    }

    //begins the game
    public void StartGame()
    {
        Menu.SetActive(false);
        gameStarted = true;
        playerInput.enabled = true;
        playerInventory.NumCoins = 0;
        playerInventory.NumKeys = 0;
        gameTimer = TimeLimit * 1000;
        GameRunning = true;
        Time.timeScale = 1f;
    }

    //reloads the game scene
    public void RestartGame()
    {
        gameStarted=false;
        Menu.SetActive(false);
        SceneManager.LoadScene("GameplayScene");
    }

    //pauses the game (timescale = 0)
    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.started && gameStarted)
        {
            playerInput.enabled = false;
            Time.timeScale = 0f;
            StartBtn.SetActive(false);
            RestartBtn.SetActive(true);
            ResumeBtn.SetActive(true);
            EndMSG.text = "PAUSED";
            ScoreText.text = "Coins: " + playerInventory.NumCoins;
            Menu.SetActive(true);
            GameRunning = false;
        }     
    }

    //unpauses and resumes the game
    public void ResumeGame()
    {
        playerInput.enabled = true;
        Menu.SetActive(false);
        GameRunning = true;
        ResumeBtn.SetActive(false);
        Time.timeScale = 1f;
    }

    //game end condition based on time, death, or win condition
    public void GameOver()
    {
        if(playerController.IsDead)
            EndMSG.text = "YOU DIED";
        else if(gameTimer <= 0)
            EndMSG.text = "TIMES UP!";
        else
            EndMSG.text = "WINNER!";
        gameStarted = false;
        playerInput.enabled = false;
        StartBtn.SetActive(false);
        RestartBtn.SetActive(true);
        ScoreText.text = "Coins: " + playerInventory.NumCoins;
        Menu.SetActive(true);
        //Time.timeScale = 0;
    }

    //handles multiple game modes including editor, application, and webGL platform
    public void QuitGame()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif

        #if(UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #elif (UNITY_WEBGL)
            SceneManger.LoadScene("QuitScene");
        #endif
    }
}
