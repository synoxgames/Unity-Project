using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChaseManager : LevelBase {

    public Transform dragonTail;
    public Vector3 startPosition;
    public Vector3 endPosition;

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
        DecreaseLevel(chaseDecreaseRate * Time.deltaTime);

        dragonTail.localPosition = Vector3.Lerp(endPosition, startPosition, LevelPercent());
    }



    public override void LevelMaxedOut() {
        return;
    }

    public override void LevelReachedZero() {
       ArmManager.instance.KYS();
    }
}
