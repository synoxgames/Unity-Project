using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChaseManager : LevelBase, IEUpgradable {

    public Transform dragonTail;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public WarningHandler warningText;
    public float warningThreshold = 0.25f;
    public float chaseDecreaseRate = 0.75f;

    public static DragonChaseManager instance;

    private void Awake() {
        if (instance != this) Destroy(instance);
        instance = this;
    }

    private void Start() {
        currentLevel = levelMax;
    }

    private void Update() {
        if (isPaused) return;

        DecreaseLevel(chaseDecreaseRate * Time.deltaTime);
        dragonTail.localPosition = Vector3.Lerp(endPosition, startPosition, LevelPercent());

        if (LevelPercent() <= warningThreshold) warningText.StartWarning("Chase Me", 0.1f);
        else warningText.StopWarning();
    }



    public override void LevelMaxedOut() {
        return;
    }

    public override void LevelReachedZero() {
       ArmManager.instance.KYS();
    }

}
