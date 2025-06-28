using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellZone : MonoBehaviour
{
    public LevelManager levelManager; // Assign in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Furniture"))
        {
            FurnitureValue fv = other.GetComponent<FurnitureValue>();
            if (fv != null)
            {
                levelManager.UpdateMoney(fv.moneyValue);
                levelManager.UpdateSuspicionLevel(fv.suspicionValue);

                Destroy(other.gameObject); // or setInactive if you want effects later
            }
        }
    }
}