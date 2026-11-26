using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningHandler : MonoBehaviour, IEPausable
{
    public float blinkSpeed = 0.33f;
    private string warningMessage = "";
    private TextMeshProUGUI warningText;
    float blinkTimer = 0.0f;
    bool isBlinking = false;
    bool blink = false;
    bool isPaused = false;

    private void Start() {
        warningText = GetComponent<TextMeshProUGUI>();
        warningText.enabled = false;
    }

    public void Update() {
        if (isBlinking && Time.time > blinkTimer) {
            Blink();
        }
    }

    public void Blink() {
        blink = !blink;
        warningText.enabled = blink;
        warningText.text = warningMessage;
        blinkTimer = Time.time + blinkSpeed;
    }

    public void StartWarning(string message, float speed = .33f) {
        if (isBlinking) {
            warningMessage = message;
            return;
        }

        warningText.enabled = true;
        warningMessage = message;
        blinkSpeed = speed;
        isBlinking = true;
    }

    public void StopWarning() {
        if (!isBlinking) return;

        isBlinking = false;
        warningText.enabled = false;
        blinkSpeed = 0.33f;
    }

    public void OnPause() {
        isPaused = true;
    }

    public void OnUnPause() {
        isPaused = false;
    }
}
