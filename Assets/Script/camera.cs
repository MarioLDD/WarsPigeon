using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private float rotacionInput;
    public float rotacionSpeed = 300;
    public Transform angleCam;
    public float minAngle = 90;
    public float maxAngle = -5;

    float elevation = 0;

    void Start()
    {
        angleCam.localRotation = Quaternion.Euler(0, 0, 0);

    }

    void Update()
    {
        rotacionInput = -Input.GetAxis("Mouse Y");
       // transform.Rotate(Vector3.right * Time.deltaTime * rotacionSpeed * rotacionInput);


        elevation = Mathf.Clamp(elevation + rotacionInput, minAngle, maxAngle);
        angleCam.localRotation = Quaternion.Euler(elevation, 0, 0);

    }

}
