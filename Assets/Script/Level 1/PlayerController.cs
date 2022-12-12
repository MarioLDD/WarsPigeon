using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject pointFire;
    private AudioSource playerAudio;
    public AudioClip foodSound;

    public float speedUp = 8f;
    public float speed = 4f;
    public float verticalSpeed = 5f;
    public float rotacionSpeed = 30f;
    private float rotacionInput;
    private float verticalInput;
    private float minAltitud = 1f;
    private float maxAltitud = 10f;
    private float xMaxMove = 75f;
    private float zMaxMove = 50f;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
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
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xMaxMove, xMaxMove), Mathf.Clamp(transform.position.y, minAltitud, maxAltitud), Mathf.Clamp(transform.position.z, -zMaxMove, zMaxMove));


            transform.Rotate(Vector3.up * Time.deltaTime * rotacionSpeed * rotacionInput);

            if (Input.GetMouseButtonDown(0))
            {
                pointFire.GetComponent<Fire>().Lanzar();
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
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(foodSound, 0.45f);
            pointFire.GetComponent<Fire>().Reload();
        }
    }
}