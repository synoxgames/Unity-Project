using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
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
    public Gradient colourGradient;
    public Image smackFillSprite;
    private bool hasDied;

    public SuspicionHandler suspicion;

    [Header("Money Settings")]
    [SerializeField] private int money = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI smackText;

    [Header("UI Elements")]
    [SerializeField] private GameObject debugUI; // UI element to show debug controls
    public Image blackScreen;
    public bool debug = false;

    [Header("UI Elements - Warnings")]
    public TextMeshProUGUI warningText;
    public float withdrawlWarnThreshold = 0.1f;
    public float overdoseWarnThreshold = 0.7f;
    public float withdrawlBlinkSpeed = 1f;
    float wBT = 0.0f;

    [Header("Other")]
    public GameObject hallucinationsHolder;
    public PostProcessVolume postProcessVol;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.gameObject.SetActive(true);
        StartCoroutine(FadeIn()); // Start fade-in effect
        currentSmackLevel = startSmackLevel;
        smackSlider.value = startSmackLevel;
        smackCount = 0; // Reset smack count at the start
        smackCountText = GameObject.Find("SmackCountText").GetComponent<TextMeshProUGUI>();
        moneyText.text = "Broke Ass";
        smackText.enabled = false; // Hide smack text initially
        debugUI.SetActive(debug); // Set initial state of debug UI
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease smack level over time
        UpdateSmackLevel(-smackDecreaseRate * Time.deltaTime);

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

        UpdateUI();
    }

    public void UpdateSmackLevel(float value)
    {
        currentSmackLevel += value;
        currentSmackLevel = Mathf.Clamp(currentSmackLevel, 0f, maxSmackLevel);
        smackSlider.value = currentSmackLevel;

        smackFillSprite.color = colourGradient.Evaluate(currentSmackLevel / maxSmackLevel);
        postProcessVol.weight = currentSmackLevel / maxSmackLevel;


        if ((currentSmackLevel / maxSmackLevel) <= withdrawlWarnThreshold) {
            warningText.text = "HEROIN REQUIRED ASAP";
            if (Time.time >= wBT) {
                warningText.gameObject.SetActive(!warningText.gameObject.activeSelf);
                wBT = Time.time + withdrawlBlinkSpeed;
            }
        } else if ((currentSmackLevel / maxSmackLevel) >= overdoseWarnThreshold) {
            warningText.text = "OVERDOSE IMMINENT";
            if (Time.time >= wBT) {
                warningText.gameObject.SetActive(!warningText.gameObject.activeSelf);
                wBT = Time.time + withdrawlBlinkSpeed;
            }
        } else if (warningText.gameObject.activeSelf) warningText.gameObject.SetActive(false);

        if ((currentSmackLevel / maxSmackLevel) <= 0.25f)
        {
            if (!hallucinationsHolder.activeSelf)
                hallucinationsHolder.SetActive(true);
        }
        else
        {
            if (hallucinationsHolder.activeSelf)
                hallucinationsHolder.SetActive(false);
        }

        if (debug) Debug.Log("Smack Level: " + currentSmackLevel.ToString("0.#"));

        //Game Over Logic
        if (currentSmackLevel > maxSmackLevel)
        {
            if (debug) Debug.Log("Too much smack, overdose! Suck shit, you lose!");
            //TODO: Implement game over logic here
        }
        else if (currentSmackLevel <= 0f)
        {
            if (debug) Debug.Log("Withdrawal symptoms detected! Suck shit, you lose!");
            if (!hasDied) {
                ArmManager.instance.KYS();
                hasDied = true;
            }
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
    }

    public void IncreaseSmackCount()
    {
        smackCount++;
    }

    public void DecreaseSmackCount(int amount = 1) {
        if ((smackCount - amount) <= 0) amount = smackCount;
        smackCount--;
    }

    public void SpawnFurniture()
    {
        //TODO: Implement furniture spawning logic, should be randomly placed in set locations
    }
    
     // This coroutine is called at the start of the game to fade in from black.
    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);

        float elapsedTime = 0f;
        float fadeTime = 2f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeTime;
            float easedAlpha = 1 - (progress * progress); // Quadratic ease-out

            blackScreen.color = new Color(0, 0, 0, easedAlpha);
            yield return null;
        }

        // Ensure alpha is exactly 0 at the end
        blackScreen.color = new Color(0, 0, 0, 0);
    }

    // This coroutine is called at the end of the game to fade out to black.
    public IEnumerator FadeOut()
    {
        // ensure black screen object is active
        if (!blackScreen.gameObject.activeSelf)
        {
            // Make sure black screen is clear 
            blackScreen.color = new Color(0, 0, 0, 0);
            blackScreen.gameObject.SetActive(true);
        }

        float elapsedTime = 0f;
        float fadeTime = 2f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeTime;
            float easedAlpha = progress * progress; // Quadratic ease-in

            blackScreen.color = new Color(0, 0, 0, easedAlpha);
            yield return null;
        }

        // Ensure alpha is exactly 1 at the end
        blackScreen.color = new Color(0, 0, 0, 1);
    }

    public float GetSmackPercent() {
        return currentSmackLevel / maxSmackLevel;
    }

    public void UpdateUI() {
        smackCountText.text = smackCount.ToString();

        moneyText.text = "Money: $" + money.ToString();

        if (money > 15) {
            smackText.enabled = true;
        } else {
            smackText.enabled = false;
        }
    }
}
