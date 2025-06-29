using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    private bool isHoldingItem = false;
    private GameObject heldItem;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHoldingItem)
        {
            TryPickUpItem();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isHoldingItem)
        {
            DropItem();
        }
    }

    private void TryPickUpItem()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hit, 2f))
        {
            if (hit.collider.CompareTag("Furniture"))
            {
                PickUpItem(hit.collider.gameObject);
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        isHoldingItem = true;
        heldItem = item;
        Debug.Log("Picked up: " + item.name);
    }

    private void DropItem()
    {
        isHoldingItem = false;
        heldItem.SetActive(true);
        heldItem = null;
    }
}
