using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPHandler : MonoBehaviour
{
    [Header("XP Floats")]
    public float currentXP = 0f;
    public int currentLevel = 1;
    private float nextLevelXP = 5f;
    private const float LEVEL_MULTIPLIER = 1.25f;

    [Header("UI")]
    public Slider xpSlider;
    public GameObject levelUpText;

    [Header("Other")]
    public AudioClip levelUpAudio;
    private AudioSource audioSource;

    public static XPHandler instance;
    bool isLevellingUp = false;

    private void Awake() {
        if (instance != this) Destroy(instance);
        instance = this;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        nextLevelXP = 5f;
        UpdateSlider();
    }

    public void AddXP(float amount) {
        currentXP += amount;
        while (currentXP >= nextLevelXP) {
            currentXP -= nextLevelXP; 
            LevelUp();

            if (!isLevellingUp) StartCoroutine(LevelUpUI());
        }

        UpdateSlider();
    }

    public void LevelUp() {
        currentLevel++;
        nextLevelXP = Mathf.Pow(currentLevel, LEVEL_MULTIPLIER) + 5f;
        SkillTreeManager.instance.AddPerkPoint(1);
        UpdateSlider();
    }

    public IEnumerator LevelUpUI() {
        isLevellingUp = true;
        audioSource.PlayOneShot(levelUpAudio);
        levelUpText.SetActive(true);
        yield return new WaitForSeconds(6.648f);
        levelUpText.SetActive(false);
        isLevellingUp = false;
    }

    public void UpdateSlider() {
        xpSlider.maxValue = nextLevelXP;
        xpSlider.value = currentXP;
    }
}
