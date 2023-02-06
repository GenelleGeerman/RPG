using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pausedScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Image currentStatusImage;
    [SerializeField] private List<StatusSprites> statusSprites;
    [SerializeField] private Text currentStatusText;

    void Start()
    {
        FindObjectOfType<EssenceManager>()?.OnMaxEssence.AddListener(SetStatusImage);
    }
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void ShowWinScreen()
    {

        winScreen.SetActive(true);
    }

    public void SetPauseScreen(bool state)
    {

        pausedScreen.SetActive(state);
    }

    public void NextLevel()
    {
        winScreen.SetActive(false);
    }

    public void SetStatusImage(Status type)
    {
        currentStatusImage.gameObject.SetActive(true);
        currentStatusText.text = type.ToString();
        foreach (StatusSprites item in statusSprites)
        {
            if (item.Status == type)
            {
                currentStatusImage.sprite = item.Sprite;
                return;
            }
        }
    }
}