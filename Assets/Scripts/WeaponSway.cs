using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {

    [Header("Floats")]
    public float amount;
    public float maximumAmount;
    public float smoothAmount;
    [Space]

    [Header("Aiming")]
    public bool useAim = false;
    [Space]
    public bool isAiming;
    public float fovIncrease;
     public float fovSmooth;
    public float sensitivityChange;

    [Header("Vector3")]
    public Vector3 aimingPosition;
    private Vector3 initialPosition;
    private Vector3 positionToUse;

    Animator anim;
    PlayerMovement player;
    Camera mainCam;
    float initialFOV;
    float currentFOV;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        mainCam = Camera.main;
    }

    private void Start()
    {
        initialPosition = transform.localPosition;
        positionToUse = initialPosition;

        if (useAim)
        {
            initialFOV = mainCam.fieldOfView;
            currentFOV = mainCam.fieldOfView;
        }
    }

    private void Update()
    {
            float x = -Input.GetAxis("Mouse X") * amount;
            float y = -Input.GetAxis("Mouse Y") * amount;
            x = Mathf.Clamp(x, -maximumAmount, maximumAmount);
            y = Mathf.Clamp(y, -maximumAmount, maximumAmount);

            Vector3 finalPosition = new Vector3(x, y, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + positionToUse, Time.deltaTime * smoothAmount);

           anim.SetBool("isWalking", player.isWalking);


            if (useAim)
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, currentFOV, fovSmooth * Time.deltaTime);

            if (Input.GetButtonDown("Fire2"))
            {
                if (useAim)
                {
                    if (!isAiming)
                    {
                        isAiming = true;
                        positionToUse = aimingPosition;
                        amount /= 2;
                        maximumAmount /= 2;
                        currentFOV = (initialFOV / fovIncrease);
                    }
                    else
                    {
                        isAiming = false;
                        positionToUse = initialPosition;
                        amount *= 2;
                        maximumAmount *= 2;
                        currentFOV = initialFOV;
                    }
                }
            }
    }
}
