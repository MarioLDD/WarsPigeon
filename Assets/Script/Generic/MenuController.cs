using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMP_Text score;
    public GameObject buttons;
    public GameObject controls;
    private bool controlMenuState;
    public float hightScoreTime;
    private int levelCont;
    private string level;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            controlMenuState = false;
            controls.SetActive(false);
        }
        levelCont = 0;        

        if (PlayerPrefs.HasKey("HighscoreTime") == false)
        {
            PlayerPrefs.SetFloat("HighscoreTime", 0);
        }
        else
        {
            hightScoreTime = PlayerPrefs.GetFloat("HightScoreTime");
            float minutes = Mathf.FloorToInt(hightScoreTime / 60);
            float seconds = Mathf.FloorToInt(hightScoreTime % 60);
            float miliSeconds = (hightScoreTime % 1) * 1000;
            score.text = "Best time: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliSeconds);
        }
    }
    void Update()
    {
        if (controlMenuState && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)))
        {
            controlMenuState = false;
            buttons.SetActive(true);
            controls.SetActive(false);
        }        
    }
    public void Controls()
    {
        controlMenuState = true;
        buttons.SetActive(false);
        controls.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void NextLevel()
    {
        levelCont += 1;
        level = "Level " + levelCont;
        SceneManager.LoadScene(level);
    }

    public void Restart()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
