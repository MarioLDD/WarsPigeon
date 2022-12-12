using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform angleCam;

    private float rotacionInput;
    public float rotacionSpeed = 300;
    public float minAngle = 90;
    public float maxAngle = -5;
    private float angleRestriction = 0;

    void Start()
    {
        angleCam.localRotation = Quaternion.Euler(0, 0, 0);

    }

    void Update()
    {
        if (!PauseControl.gameIsPaused)
        {
            rotacionInput = -Input.GetAxis("Mouse Y");

            angleRestriction = Mathf.Clamp(angleRestriction + rotacionInput, minAngle, maxAngle);
            angleCam.localRotation = Quaternion.Euler(angleRestriction, 0, 0);
        }
    }
}
