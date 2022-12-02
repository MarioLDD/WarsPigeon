using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnemyLvl_0_Group : MonoBehaviour
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

    public bool enemyState;
    public bool randomState;
    public bool targetState;

    //  private float time;

    void Start()
    {
        treeTarget = GameObject.FindGameObjectWithTag("TargetTree").transform;
        enemyController = GameObject.FindGameObjectWithTag("EnemyManager");
        a = GameObject.FindGameObjectWithTag("Bounds_A").transform;
        b = GameObject.FindGameObjectWithTag("Bounds_B").transform;
        c = GameObject.FindGameObjectWithTag("Bounds_C").transform;
        d = GameObject.FindGameObjectWithTag("Bounds_D").transform;



        enemyState = true;
        randomState = true;
        targetState = true;

        playerAnim = GetComponent<Animator>();
        navigation = GetComponent<NavMeshAgent>();
        Run();

        // InvokeRepeating("RandomNav", 1f, 6f);
    }

    void Update()
    {





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

        if (collision.gameObject.tag.Contains("Bounds"))
        { 
            Destroy(gameObject);


        }




      

        if (collision.gameObject.CompareTag("TargetTree") && targetState) //detecto si el enemigo llego a su target y bloqueo para q no haga multiples golpes
        {
            targetState = false;
            enemyController.GetComponent<EnemyController>().EggsCount(); //llamo al metodo que se encarga de controlar la cantidad de huevos restantes y los muestra en el HUB





        }

    }

    public void Run() //animacion de correr 
    {
        //   Debug.Log("RUNNNNN");
        navigation.destination = treeTarget.position;
        navigation.speed = 5f;
        playerAnim.SetBool("Runing", true);

    }

    void Escape() //animacion de escape
    {
        navigation.speed = 10f;
        playerAnim.SetBool("Escaping", true);
    }
}
