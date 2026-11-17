using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementScript : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float accelerationSpeed = 10f;
    public float deccelerationSpeed = 5f;

    public float rotateSpeed = 10f;
    Vector3 rotateVector;
    float speed = 0;
    Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {

        float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical");
        if (Mathf.Approximately(z, 0f)) speed = Mathf.MoveTowards(speed, 0f, deccelerationSpeed * Time.deltaTime); 
        else speed += (z * accelerationSpeed) * Time.deltaTime;
        speed = Mathf.Clamp(speed, -maxSpeed / 2f, maxSpeed);
        rotateVector.y += ((rotateSpeed * x) * Time.deltaTime) % 360;
        transform.eulerAngles = rotateVector;
    }

    private void FixedUpdate() {
        Vector3 forwardVelocity = transform.forward * speed;
        rb.velocity = new Vector3(forwardVelocity.x, rb.velocity.y, forwardVelocity.z);
    }
}
