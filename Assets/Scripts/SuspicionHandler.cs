using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionHandler : LevelBase
{
    public Image colourImage;
    public Color startColour = new Color(1, 1, 1, 0);
    public Color endColour = new Color(1, 1, 1, 1);
    public float decreaseRate = 1.5f;

    void Start() {
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;
        DecreaseLevel(decreaseRate * Time.deltaTime);
        colourImage.color = Color.Lerp(Color.clear, Color.white, LevelPercent());
    }

    public override void LevelMaxedOut() {
        return;
    }

    public override void LevelReachedZero() {
        return;
    }

}
