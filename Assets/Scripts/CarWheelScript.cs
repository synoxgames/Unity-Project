using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarWheelScript : MonoBehaviour
{
    [Header("Wheel Rotations")]
    public Transform wheelImage;

    [Header("Float")]
    public float rotateAmount = 90f;
    public float smoothSpeed = 3f;

    void Update() {
        float axis = Input.GetAxis("Horizontal");

        float targetZ = -rotateAmount * axis;
        float currentZ = wheelImage.eulerAngles.z;

        if (currentZ > 180f) currentZ -= 360f;

        float newZ = Mathf.Lerp(currentZ, targetZ, smoothSpeed * Time.deltaTime);

        wheelImage.transform.eulerAngles = new Vector3(0, 0, newZ);
    }

}

