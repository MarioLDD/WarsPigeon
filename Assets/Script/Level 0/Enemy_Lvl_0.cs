using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Lvl_0 : MonoBehaviour
{
    private NavMeshAgent navigation;
    private Animator enemyAnim;
    public ParticleSystem impactParticle;
    private AudioSource playerAudio;
    public AudioClip audioImpact;

    public Transform spawnPoint;
    public Transform treeTarget;
    private Transform a;
    private Transform b;
    private Transform c;
    private Transform d;
    public Transform hand;

    public GameObject enemy;
    private GameObject enemyController;
    public GameObject cameraPlayer;
    public GameObject cameraFija;
    public GameObject player;
    public GameObject rock;
    public GameObject foodParticles;
    public GameObject crosshair;

    public bool enemyState;
    public bool randomState;
    public bool targetState;
    public bool foodCollection;
    private bool enemyKilled;
    public float rockVel;

    void Start()
    {
        enemyController = GameObject.FindGameObjectWithTag("EnemyManager");
        a = GameObject.FindGameObjectWithTag("Bounds_A").transform;
        b = GameObject.FindGameObjectWithTag("Bounds_B").transform;
        c = GameObject.FindGameObjectWithTag("Bounds_C").transform;
        d = GameObject.FindGameObjectWithTag("Bounds_D").transform;

        enemyState = true;
        foodCollection = false;
        enemyKilled = false;

        enemyAnim = GetComponent<Animator>();
        navigation = GetComponent<NavMeshAgent>();
        playerAudio = GetComponent<AudioSource>();

        InvokeRepeating("RockAnim", 4f, 2.75f);
    }

    void Update()
    {
        enemyAnim.SetFloat("Head_Vertical_f", 1, 2, 0.01f);

        if (foodCollection && enemyKilled)
        {
            enemyKilled = false;
            SpawnEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //detecto si es impactado por un proyectil y si no fue impactado ya antes
        if (collision.gameObject.CompareTag("Bomb") && enemyState)
        {
            enemyState = false;
            Instantiate(impactParticle, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(audioImpact, 0.45f);

            Escape(); //activo animacion de escape

            //busco el borde mas cercano del mapa para que se retire
            float oA = Vector3.Distance(transform.position, a.transform.position);
            float oB = Vector3.Distance(transform.position, b.transform.position);
            float oC = Vector3.Distance(transform.position, c.transform.position);
            float oD = Vector3.Distance(transform.position, d.transform.position);

            if (oA < oC)
            {
                if (oB < oD)
                {
                    if (oA < oB)
                    {
                        // Debug.Log("cerca de A");
                        navigation.SetDestination(new Vector3(transform.position.x, transform.position.y, a.position.z));
                    }
                    else
                    {
                        // Debug.Log("cerca de B");
                        navigation.SetDestination(new Vector3(b.position.x, transform.position.y, transform.position.z));
                    }
                }
                else
                {
                    if (oA < oD)
                    {
                        // Debug.Log("cerca de A");
                        navigation.SetDestination(new Vector3(transform.position.x, transform.position.y, a.position.z));
                    }
                    else
                    {
                        // Debug.Log("cerca de D");
                        navigation.SetDestination(new Vector3(d.position.x, transform.position.y, transform.position.z));
                    }
                }
            }
            else
            {
                if (oB < oD)
                {
                    if (oC < oB)
                    {
                        // Debug.Log("cerca de C");
                        navigation.SetDestination(new Vector3(transform.position.x, transform.position.y, c.position.z));
                    }
                    else
                    {
                        // Debug.Log("cerca de B");
                        navigation.SetDestination(new Vector3(b.position.x, transform.position.y, transform.position.z));
                    }
                }
                else
                {
                    if (oC < oD)
                    {
                        // Debug.Log("cerca de C");
                        navigation.SetDestination(new Vector3(transform.position.x, transform.position.y, c.position.z));
                    }
                    else
                    {
                        // Debug.Log("cerca de D");
                        navigation.SetDestination(new Vector3(d.position.x, transform.position.y, transform.position.z));
                    }
                }
            }

            enemyController.GetComponent<EnemyController_Lvl_0>().EnemyKilled(); //llamo al metodo que se encarga de controlar la cantidad de enemigos restantes y los muestra en el HUB

            if (!foodCollection)
            {
                foodParticles.SetActive(true);
            }
        }

        if (collision.gameObject.tag.Contains("Bounds")) //lo elimino al llegar al borde del mapa
        {
            this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            enemyKilled = true;
        }
    }

    public void SpawnEnemy()
    {
        crosshair.SetActive(false);
        cameraPlayer.SetActive(false);
        cameraFija.GetComponent<Camera>().enabled = true;
        cameraFija.GetComponent<AudioListener>().enabled = true;
        Instantiate(enemy, spawnPoint.transform);
        enemyController.GetComponent<EnemyController_Lvl_0>().totalEnemy += 3;
        enemyController.GetComponent<EnemyController_Lvl_0>().HudUpdate();
        Invoke(nameof(ResetCamera), 4);
    }

    void ResetCamera()
    {
        cameraFija.GetComponent<Camera>().enabled = false;
        cameraFija.GetComponent<AudioListener>().enabled = false;
        cameraPlayer.SetActive(true);
        crosshair.SetActive(true);
        player.GetComponent<PlayerController_Lvl_0>().LookAtEnemy();
        Destroy(gameObject);
    }

    void Escape() //animacion de escape
    {
        navigation.speed = 10f;
        enemyAnim.SetBool("Run", true);
    }

    void RockAnim()
    {
        enemyAnim.SetTrigger("Lanzamiento");
        Invoke("RockThrow", 1.25f);
    }

    private void RockThrow()
    {
        GameObject projectile = Instantiate(rock, hand.transform.position, Quaternion.identity);
        Vector3 rockDirection = (treeTarget.position - projectile.transform.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity = rockDirection * rockVel;
        Destroy(projectile, 1);
    }
}
