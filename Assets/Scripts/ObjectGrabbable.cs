using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] public bool debug = false;
    [SerializeField] public float lerpSpeed = 10f; // Speed at which the object moves to the grab point
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        if (objectRigidbody == null)
        {
            Debug.LogError("ObjectGrabbable requires a Rigidbody component.");
        }
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        if (debug) Debug.Log("Object grabbed: " + gameObject.name);
        // changes the point to move to
        this.objectGrabPointTransform = objectGrabPointTransform;
        // disables the gravity of the object
        objectRigidbody.useGravity = false;
        // freezes the rotation of the object
        // objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Drop()
    {
        if (debug) Debug.Log("Object dropped: " + gameObject.name);
        // resets the point to move to
        this.objectGrabPointTransform = null;
        // enables the gravity of the object
        objectRigidbody.useGravity = true;
        // unfreezes the rotation of the object
        // objectRigidbody.constraints = RigidbodyConstraints.None;
    }

    private void LateUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            // lerp to smoothly move the object to the grab point
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.fixedDeltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
