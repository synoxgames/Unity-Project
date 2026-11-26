using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour, IEPausable {

    [Header("Floats")]
    public float amount;
    public float maximumAmount;
    public float smoothAmount;

    [Header("Vector3")]
    private Vector3 initialPosition;
    private Vector3 positionToUse;

    bool isPaused;
    private static bool LockInPlace = true;

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        initialPosition = transform.localPosition;
        positionToUse = initialPosition;
    }

    private void Update()
    {
        if (isPaused) return;
        float x = -Input.GetAxis("Mouse X") * amount;
        float y = - Input.GetAxis("Mouse Y") * amount;
        x = Mathf.Clamp(x, -maximumAmount, maximumAmount);
        y = Mathf.Clamp(y, -maximumAmount, maximumAmount);


        if (LockInPlace) {
            if (y >= 0)
                y = 0;
        }

        Vector3 finalPosition = new Vector3(x, y, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + positionToUse, Time.deltaTime * smoothAmount);
    }

    public static void SetLockState(bool state) { LockInPlace = state; }

    public void OnPause() {
        isPaused = true;
    }

    public void OnUnPause() {
        isPaused = false;
    }
}
