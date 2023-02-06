using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private int mainMenuIndex = 0;
    public static GameManager Instance;
    [SerializeField] private float buffTime;
    [SerializeField] private UIManager uiManager;
    public bool IsPaused { get; private set; } = false;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        PlayerStats p = (PlayerStats)FindFirstObjectByType(typeof(PlayerStats));
        p?.OnDeath.AddListener(HandleDeath);
    }
    public float GetBuffTime()
    {
        return buffTime;
    }
    public void HandleDeath()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        uiManager.ShowGameOverScreen();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        uiManager.SetPauseScreen(true);

    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
        uiManager.SetPauseScreen(false);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BossDefeated()
    {
        Time.timeScale = 0f;
        uiManager.ShowWinScreen();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
    public void GoToNextLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}