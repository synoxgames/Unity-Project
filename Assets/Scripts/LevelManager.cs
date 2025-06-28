using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [Header("Smack Level Settings")]
    [SerializeField] private Slider smackSlider;
    [SerializeField] private float maxSmackLevel = 100f;
    [SerializeField] private float startSmackLevel = 50f;
    [SerializeField] private float smackDecreaseRate = 1f; // Rate at which smack decreases per second
    private float currentSmackLevel = 0f;
    public int smackCount = 0; // Count of smack injections
    public TextMeshProUGUI smackCountText;

    [Header("Suspicion Level Settings")]
    [SerializeField] private Slider suspicionSlider;
    [SerializeField] private float maxSuspicionLevel = 100f;
    [SerializeField] private float suspicionDecreaseRate = 1f; // Rate at which suspicion increases per second
    private float currentSuspicionLevel = 0f;

    [Header("Money Settings")]
    [SerializeField] private int money = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI smackText;

    [Header("UI Elements")]
    [SerializeField] private GameObject debugUI; // UI element to show debug controls
    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSmackLevel = startSmackLevel;
        smackSlider.value = startSmackLevel;
        smackCount = 0; // Reset smack count at the start
        smackCountText = GameObject.Find("SmackCountText").GetComponent<TextMeshProUGUI>();
        currentSuspicionLevel = 0f;
        suspicionSlider.value = 0f;
        moneyText.text = "Money: $" + money.ToString();
        smackText.enabled = false; // Hide smack text initially
        debugUI.SetActive(debug); // Set initial state of debug UI
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease smack level over time
        UpdateSmackLevel(-smackDecreaseRate * Time.deltaTime);

        // Decrease suspicion level over time
        UpdateSuspicionLevel(-suspicionDecreaseRate * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (money < 15)
            {
                Debug.Log("Not enough money to inject smack!");
                return;
            }
            IncreaseSmackCount();
            UpdateMoney(-15); // Decrease money by 15
        }

        // Toggle debug UI
        if (Input.GetKeyDown(KeyCode.P))
        {
            debug = !debug;
            debugUI.SetActive(debug);
        }
    }

    public void UpdateSuspicionLevel(float value)
    {
        currentSuspicionLevel += value;
        currentSuspicionLevel = Mathf.Clamp(currentSuspicionLevel, 0f, maxSuspicionLevel);
        suspicionSlider.value = currentSuspicionLevel;

        if (debug) Debug.Log("Suspicion Level: " + currentSuspicionLevel);

        //Game Over Logic
        if (currentSuspicionLevel >= maxSuspicionLevel)
        {
            Debug.Log("Too sussy baka uwu! Suck shit, you lose!");
            //TODO: Implement game over logic here
        }
    }

    public void UpdateSmackLevel(float value)
    {
        // TODO: have this gradually change from yellow (low - withdrawal gameover) to red (high - overdose gameover)
        currentSmackLevel += value;
        currentSmackLevel = Mathf.Clamp(currentSmackLevel, 0f, maxSmackLevel);
        smackSlider.value = currentSmackLevel;

        if (debug) Debug.Log("Smack Level: " + currentSmackLevel);

        //Game Over Logic
        if (currentSmackLevel >= maxSmackLevel)
        {
            Debug.Log("Too much smack, overdose! Suck shit, you lose!");
            //TODO: Implement game over logic here
        }
        else if (currentSmackLevel <= 0f)
        {
            Debug.Log("Withdrawal symptoms detected! Suck shit, you lose!");
            //TODO: Implement game over logic here
        }
    }

    public void UpdateMoney(int amount)
    {
        if (money + amount < 0)
        {
            Debug.Log("Invalid money update.");
            return;
        }
        money += amount;
        moneyText.text = "Money: $" + money.ToString();
        if (money > 15)
        {
            smackText.enabled = true;
        }
        else
        {
            smackText.enabled = false;
        }
    }

    public void IncreaseSmackCount()
    {
        smackCount++;
        smackCountText.text = "X " + smackCount.ToString();
    }

    public void SpawnFurniture()
    {
        //TODO: Implement furniture spawning logic, should be randomly placed in set locations
    }
}
