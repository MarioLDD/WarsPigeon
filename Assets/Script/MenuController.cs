using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMP_Text score;
    public float hightScoreTime;


        void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
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
