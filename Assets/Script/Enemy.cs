using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public GameObject enemyController;
    public NavMeshAgent navigation;
    private Animator playerAnim;
    public ParticleSystem impactParticle;

    public Transform treeTarget;
    public Transform a;
    public Transform b;
    public Transform c;
    public Transform d;

    public bool enemyState;
    public bool randomState;
    public bool targetState;

    void Start()
    {
        enemyState = true;
        randomState = true;
        targetState = true;

        playerAnim = GetComponent<Animator>();
        navigation = GetComponent<NavMeshAgent>();

        InvokeRepeating("RandomNav", 1f, 6f);
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

            CancelInvoke("RandomNav"); //detego la invocacion al metodo de nevegacion random

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


            enemyController.GetComponent<EnemyController>().EnemyKilled(); //llamo al metodo que se encarga de controlar la cantidad de enemigos restantes y los muestra en el HUB
        }

        if (collision.gameObject.CompareTag("Bounds")) //lo elimino al llegar al borde del mapa
        {

            Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("TargetTree") && targetState) //detecto si el enemigo llego a su target y bloqueo para q no haga multiples golpes
        {
            targetState = false;
            enemyController.GetComponent<EnemyController>().EggsCount(); //llamo al metodo que se encarga de controlar la cantidad de huevos restantes y los muestra en el HUB

            InvokeRepeating(nameof(RandomNav), 0f, 6f);
            randomState = true;

            Invoke("ResetTarget", 5f);
        }
    }

    void RandomNav() //metodo de navegacion random
    {
        if (randomState)
        {
            navigation.destination = new Vector3(Random.Range(d.transform.position.x, b.transform.position.x), 0, Random.Range(c.transform.position.z, a.transform.position.z));

            Walk();
        }


    }
    void ResetTarget()
    {
        targetState = true;
    }

    public void Walk() //animacion de caminar
    {
        // Debug.Log("Walking");
        navigation.speed = 3.5f;
        playerAnim.SetBool("Runing", false);
        playerAnim.SetBool("Walking", true);
    }
    public void Run() //animacion de correr 
    {
        //   Debug.Log("RUNNNNN");
        navigation.destination = treeTarget.position;
        navigation.speed = 5f;
        playerAnim.SetBool("Runing", true);
        randomState = false;
    }
    void Escape() //animacion de escape
    {
        navigation.speed = 10f;
        playerAnim.SetBool("Scaping", true);
    }
}
