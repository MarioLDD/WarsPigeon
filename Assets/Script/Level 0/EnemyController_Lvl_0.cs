using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController_Lvl_0 : MonoBehaviour
{
    public GameObject[] enemyList;
    public GameObject timeControl;

    public TMP_Text killsUI;
    public TMP_Text eggsUI;

    public int enemyCounter;
    private int Kills;
    private int eggsRemaining;
    public int totalEnemy;
    public int enemySelect;


    void Start()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter = 4;
        Kills = 0;
        totalEnemy = enemyList.Length;
        killsUI.text = "Kills: " + Kills.ToString() + " / " + totalEnemy.ToString();
        eggsRemaining = 3;
    }

    public void EnemyKilled()
    {
        Kills++;
        killsUI.text = "Kills: " + Kills.ToString() + " / " + totalEnemy.ToString();
        enemyCounter--;
        if (enemyCounter == 0)
        {
            // timeControl.GetComponent<TimeControl>().Score();
            SceneManager.LoadScene("WinMenu");
        }
    }

    public void HudUpdate()
    {
        killsUI.text = "Kills: " + Kills.ToString() + " / " + totalEnemy.ToString();
    }

    public void EggsCount()
    {
        eggsRemaining--;

        if (eggsRemaining == 2)
        {
            eggsUI.text = "Eggs: " + eggsRemaining.ToString();
            eggsUI.color = new Color32(255, 121, 0, 255);
        }
        if (eggsRemaining == 1)
        {
            eggsUI.text = "Eggs: " + eggsRemaining.ToString();
            eggsUI.color = new Color32(255, 0, 0, 255);
        }
        if (eggsRemaining == 0)
        {
            SceneManager.LoadScene("GameOverMenu");
        }
    }
}
