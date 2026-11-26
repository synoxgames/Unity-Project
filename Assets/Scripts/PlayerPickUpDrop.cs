using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour, IEPausable
{
    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] public ArmManager armManager;
    [SerializeField] public bool debug = false;
    private ObjectGrabbable objectGrabbable;
    bool isPaused = false;

    public static PlayerPickUpDrop instance;

    private void Awake() {
        // Creating a cheeky singleton
        if (instance != this) Destroy(instance);
        instance = this;
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && !isPaused) {
            if (objectGrabbable == null) {
                // carrying fuck all, try n grab
                PickupObject();
            } else {
                // currently raxing shit, try n drop
                DropObject();
            }
        }
    }

    public void PickupObject() {
        float pickUpDistance = 3f;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance)) {
            if (debug) Debug.Log("Raycast hit: " + raycastHit.transform.name);
            if (raycastHit.transform.TryGetComponent(out objectGrabbable)) {
                if (debug) Debug.Log("Picked up object: " + raycastHit.transform.name);
                objectGrabbable.Grab(objectGrabPointTransform);
                armManager.SetGrabbing(true);
            }
        }
    }


    public void DropObject() {
        if (objectGrabbable == null) return;

        if (debug) Debug.Log("Dropped object: " + objectGrabbable.gameObject.name);
        objectGrabbable.Drop();
        objectGrabbable = null;
        armManager.SetGrabbing(false);
    }

    public void OnPause() {
        isPaused = true;
    }

    public void OnUnPause() {
        isPaused = false;
    }
}