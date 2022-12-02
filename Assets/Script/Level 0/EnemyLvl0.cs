using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnemyLvl0 : MonoBehaviour
{
    public GameObject enemyController;
    public NavMeshAgent navigation;
    private Animator playerAnim;
    public ParticleSystem impactParticle;
    public Transform spawnPoint;
    public GameObject enemy;
    public Transform treeTarget;
    public Transform a;
    public Transform b;
    public Transform c;
    public Transform d;
    public GameObject cameraPlayer;
    public GameObject cameraFija;
    public GameObject player;
    public bool enemyState;
    public bool randomState;
    public bool targetState;

    //  private float time;

    void Start()
    {
        enemyState = true;

        playerAnim = GetComponent<Animator>();
        navigation = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        playerAnim.SetFloat("Head_Vertical_f", 1, 2, 0.01f);

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnEnemy();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //detecto si es impactado por un proyectil y si no fue impactado ya antes
        if (collision.gameObject.CompareTag("Bomb") && enemyState)
        {
            enemyState = false;
            Instantiate(impactParticle, collision.transform);


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


            enemyController.GetComponent<EnemyControllerLvl0>().EnemyKilled(); //llamo al metodo que se encarga de controlar la cantidad de enemigos restantes y los muestra en el HUB
        }

        if (collision.gameObject.tag.Contains("Bounds")) //lo elimino al llegar al borde del mapa
        {
            this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

            cameraPlayer.SetActive(false);
            cameraFija.GetComponent<Camera>().enabled = true;
            SpawnEnemy();
            Invoke(nameof(ResetCamera), 4);
        }
    }

    void ResetCamera()
    {
        cameraFija.GetComponent<Camera>().enabled = false;
        cameraPlayer.SetActive(true);
        player.GetComponent<PlayerController>().LookAtEnemy();
        Destroy(gameObject);
    }
    public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.transform);

    }

    void Escape() //animacion de escape
    {
        navigation.speed = 10f;
        playerAnim.SetFloat("Speed_f", 0.51f);
        playerAnim.SetBool("Static_b", true);
    }
}
