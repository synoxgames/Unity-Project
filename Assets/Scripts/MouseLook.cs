using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Floats")]
    public float mouseSensitivity = 100f;
    public Vector2 mouseClamp = new Vector2(-90, 90);
    public Vector2 armClamp = new Vector2(-90, 90);

    [Header("Transform")]
    public Transform playerBody;
    public Transform cameraBody;

    public static bool canUse = true;

    public static MouseLook current;

    float xRotation = 0f;
    float xBodyRotation;
    Quaternion offset;

    void Awake() {
        if (current)
            current = null;
        current = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        offset = transform.localRotation;
    }

    void Update()
    {
        if (canUse) {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xBodyRotation = Mathf.Clamp(xRotation, armClamp.x, armClamp.y);
            xRotation = Mathf.Clamp(xRotation, mouseClamp.x, mouseClamp.y);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            cameraBody.localRotation = Quaternion.Euler(-1.369f, 0, xBodyRotation);

            playerBody.Rotate(Vector3.up * (mouseX));

        }
        
    }

    public static void ChangeMouseState(bool state) {
        canUse = state;

        switch (state) {
            case true:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case false:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
