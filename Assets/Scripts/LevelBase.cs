using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelBase : MonoBehaviour, IEPausable {

    public float levelMax = 100f;
    protected float currentLevel = 50f;
    protected bool isPaused = false;

    public virtual void IncreaseLevel(float amount) {
        currentLevel += amount;
        if (currentLevel >= levelMax) {
            LevelMaxedOut();
            currentLevel = levelMax;
        }
    }

    public virtual void DecreaseLevel(float amount) {
        currentLevel -= amount;
        if (currentLevel <= 0) {
            LevelReachedZero();
            currentLevel = 0;
        }
    }

    public abstract void LevelMaxedOut();

    public abstract void LevelReachedZero();

    public float LevelPercent() {
        return currentLevel / levelMax;
    }

    public void OnPause() {
        isPaused = true;
    }

    public void OnUnPause() {
        isPaused = false;
    }
}
