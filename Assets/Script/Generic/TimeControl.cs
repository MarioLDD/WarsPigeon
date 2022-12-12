using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeControl : MonoBehaviour
{
    public float time;
    public TMP_Text timeUI;

    void Update()
    {
        time += Time.deltaTime;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float miliSeconds = (time % 1) * 1000;
        timeUI.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliSeconds);        
    }

    public void Score()
    {
        if (time < PlayerPrefs.GetFloat("HightScoreTime") || PlayerPrefs.GetFloat("HightScoreTime") == 0)
        {
            PlayerPrefs.SetFloat("HightScoreTime", time);
        }

        SceneManager.LoadScene("WinMenu");
    }
}
