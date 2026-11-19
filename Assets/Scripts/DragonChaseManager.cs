using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChaseManager : MonoBehaviour {

    public float chaseDecreaseRate = 0.75f;
    public float maxChase = 100f;
    private float currentChase = 100f;

    public static DragonChaseManager instance;

    private void Awake() {
        if (instance != this) Destroy(instance);
        instance = this;
    }

    private void Start() {
        currentChase = maxChase / 2;
    }

    private void Update() {
        DecreaseChase(chaseDecreaseRate * Time.deltaTime);
    }

    public void IncreaseChase(float amount) {
        currentChase += amount;
        print("u");
    }

    public void DecreaseChase(float amount) {
        currentChase -= amount;

        if (currentChase <= 0) ArmManager.instance.KYS();
    }

}
