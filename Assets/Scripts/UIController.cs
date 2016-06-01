using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
    public Text healthText;
    public Text crystalsText;
    public Text msgPanelText;
    public GameObject msgPanel;
    public GameObject gameOverPanel;
    public GameObject gameCompletedPanel;
    public GameObject briefingPanel;
    public GameObject pausePanel;

    private float initialTimeShowPanel;

	// Use this for initialization
	void Start () {
        briefingPanel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.time - initialTimeShowPanel > 6)
        {
            HideMsgPanel();
        }
	}

    public void SetHealth(string health)
    {
        healthText.text = "Health: " + health;
    }

    public void SetCrystals(int crystals)
    {
        crystalsText.text = "Crystals: " + crystals + "/5";

        if(crystals == 5)
        {
            // Mission accomplished
            msgPanelText.text = "Great! I got the 5 crystals. Now I gotta go back to my spaceship and get out of here as soon as possible.";
            initialTimeShowPanel = Time.time;
            ShowMsgPanel();
        }
    }
    public void HideMsgPanel()
    {
        msgPanel.SetActive(false);
    }

    public void ShowMsgPanel()
    {
        msgPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowGameCompletedPanel()
    {
        gameCompletedPanel.SetActive(true);
    }

    public void HideBriefingPanel()
    {
        briefingPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ShowPausePanel();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        HidePausePanel();
    }

}
