using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public GameObject furniturePrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.debug)
        {
            switch (Input.inputString)
            {
                case "1":
                    IncreaseSuspicion();
                    break;
                case "2":
                    DecreaseSuspicion();
                    break;
                case "3":
                    IncreaseSmack();
                    break;
                case "4":
                    DecreaseSmack();
                    break;
                case "5":
                    IncreaseMoney();
                    break;
                case "6":
                    DecreaseMoney();
                    break;
                case "7":
                    SpawnTestFurniture();
                    break;
            }
        }
    }

    public void IncreaseSmack()
    {
        levelManager.UpdateSmackLevel(10f);
    }

    public void DecreaseSmack()
    {
        levelManager.UpdateSmackLevel(-10f);
    }

    public void IncreaseSuspicion()
    {
        levelManager.UpdateSuspicionLevel(10f);
    }

    public void DecreaseSuspicion()
    {
        levelManager.UpdateSuspicionLevel(-10f);
    }

    public void IncreaseMoney()
    {
        levelManager.UpdateMoney(10);
    }

    public void DecreaseMoney()
    {
        levelManager.UpdateMoney(-10);
    }

    public void SpawnTestFurniture()
    {
        Vector3 spawnPosition = new Vector3(7f, 1f, 0f);
        Quaternion spawnRotation = Quaternion.identity;

        if (furniturePrefab != null)
        {
            GameObject newFurniture = Instantiate(furniturePrefab, spawnPosition, spawnRotation);

            // parent under "Furniture" GameObject
            GameObject furnitureContainer = GameObject.Find("Furniture");
            if (furnitureContainer != null)
            {
                newFurniture.transform.SetParent(furnitureContainer.transform);
            }
        }
        else
        {
            Debug.LogWarning("Furniture prefab not assigned!");
        }
    }
}
