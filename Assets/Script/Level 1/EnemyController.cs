using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    private GameObject[] enemyList;
    private GameObject actualEnemy;
    public GameObject timeControl;

    public TMP_Text killsUI;
    public TMP_Text eggsUI;

    private int enemySelect;
    private int enemyCounter;
    private int Kills;
    private int eggsRemaining;
    public float firstAttackTime = 15f;
    public float attackFrequency = 8f;

    void Start()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter = enemyList.Length;
        InvokeRepeating("EnemyToTarget", firstAttackTime, attackFrequency);
        Kills = 0;
        killsUI.text = "Kills: " + Kills.ToString() + " / " + enemyList.Length.ToString();
        eggsRemaining = 3;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P)) EnemyToTarget();
    }

    public void EnemyToTarget()
    {
        enemySelect = Random.Range(0, enemyList.Length);

        if (enemyList[enemySelect].GetComponent<Enemy>().randomState)

        {
            enemyList[enemySelect].GetComponent<Enemy>().Run();
            actualEnemy = enemyList[enemySelect];
            Debug.Log(actualEnemy.name);                //sacar
        }
        else EnemyToTarget();
    }

    public void EnemyKilled()
    {
        Kills++;
        killsUI.text = "Kills: " + Kills.ToString() + " / " + enemyList.Length.ToString();
        enemyCounter--;
        if (enemyCounter == 0)
        {
            timeControl.GetComponent<TimeControl>().Score();
        }
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