using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Fire : MonoBehaviour
{
    public TMP_Text ammunitionUI;
    public Rigidbody projectile;
    public float fireSpeed = 10f;
    public int initialAmmunition = 10;
    public int ammunitionAmountPerFood = 5;

    void Start()
    {
        ammunitionUI.text = "Ammo: " + initialAmmunition.ToString();
    }
    public void Lanzar()
    {
        if (initialAmmunition > 0)
        {
            initialAmmunition -= 1;
            ammunitionUI.text = "Ammo: " + initialAmmunition.ToString();
            Rigidbody fire = Instantiate(projectile, transform.position, transform.rotation);
            Vector3 worldDirection = transform.rotation * Vector3.forward;
            fire.velocity = worldDirection * fireSpeed;
        }
    }

    public void Reload()
    {
        initialAmmunition += ammunitionAmountPerFood;
        ammunitionUI.text = initialAmmunition.ToString();
    }




}


