using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Transform crosshair;
    public GameObject camara;
    public Transform punto;

    void FixedUpdate()
    {
        Ray ray = new Ray(camara.transform.position, camara.transform.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * 30);

        if (Physics.Raycast(ray, out hitInfo))
        {
            punto.transform.LookAt(hitInfo.point);

            if (hitInfo.collider != null)
            {
                crosshair.position = hitInfo.point;
            }
        }
    }
}
