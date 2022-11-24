using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Disparo : MonoBehaviour
{
    public TMP_Text ammunitionUI;
    public Rigidbody projectile;
    public float fireSpeed = 10f;
    public int ammunition = 10;

    void Start()
    {
        ammunitionUI.text = ammunition.ToString();

    }
    public void Lanzar()
    {
        if (ammunition > 0)
        {
            ammunition -= 1;

            ammunitionUI.text = ammunition.ToString();

            Rigidbody fire = Instantiate(projectile, transform.position, transform.rotation);

            Vector3 worldDirection = transform.rotation * Vector3.forward;

            fire.velocity = worldDirection * fireSpeed;

        }
    }

    public void Reload()
    {
        ammunition += 10;
        ammunitionUI.text = ammunition.ToString();
    }




}


