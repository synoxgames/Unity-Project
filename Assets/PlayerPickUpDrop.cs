using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] public bool debug = false;
    private ObjectGrabbable objectGrabbable;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                // carrying fuck all, try n grab
                float pickUpDistance = 3f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
                {
                    if (debug) Debug.Log("Raycast hit: " + raycastHit.transform.name);
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        if (debug) Debug.Log("Picked up object: " + raycastHit.transform.name);
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                // currently raxing shit, try n drop
                if (debug) Debug.Log("Dropped object: " + objectGrabbable.gameObject.name);
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
}