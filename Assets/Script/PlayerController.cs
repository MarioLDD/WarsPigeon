using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedUp = 8f;
    public float speed = 4f;
    public float verticalSpeed = 5f;
    public float rotacionSpeed = 150f;
    private float rotacionInput;
    private float verticalInput;
    public GameObject pointFire;
    private float minAltitud = 1f;
    private float maxAltitud = 10f;
    private float xMaxMove = 75f;
    private float zMaxMove = 50f;
  //  public Rigidbody player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
       // player = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (!PauseControl.gameIsPaused)
        {
            rotacionInput = Input.GetAxis("Mouse X");

            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * verticalSpeed);

            if (Input.GetKey(KeyCode.LeftShift))
            {

                transform.Translate(Vector3.forward * speedUp * Time.deltaTime);
            }

            else
            {
                //  player.velocity = Vector3.forward * speed;
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xMaxMove, xMaxMove), Mathf.Clamp(transform.position.y, minAltitud, maxAltitud), Mathf.Clamp(transform.position.z, -zMaxMove, zMaxMove));


            transform.Rotate(Vector3.up * Time.deltaTime * rotacionSpeed * rotacionInput);

            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("bomba va");

                pointFire.GetComponent<Disparo>().Lanzar();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)//esto no anda
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            speed = 0;
            speedUp = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("comidaaaa");
            Destroy(other.gameObject);
            pointFire.GetComponent<Disparo>().Reload();
        }
    }


   
}


