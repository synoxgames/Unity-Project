using UnityEngine;
using System.Collections.Generic;

public class KeySequenceDetector : MonoBehaviour
{
    public float inputTimeout = 3f; // seconds to complete the sequence
    public ArmManager armManager; // Reference to the ArmManager to access the kys method
    private List<KeyCode> kysSequence = new List<KeyCode> { KeyCode.K, KeyCode.Y, KeyCode.S };
    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        if (currentIndex > 0)
        {
            timer += Time.deltaTime;
            if (timer > inputTimeout)
            {
                ResetSequence();
            }
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(kysSequence[currentIndex]))
            {
                currentIndex++;
                timer = 0f;

                if (currentIndex >= kysSequence.Count)
                {
                    TriggerSecret();
                    ResetSequence();
                }
            }
            else if (Input.anyKeyDown)
            {
                // wrong key = reset
                ResetSequence();
            }
        }
    }

    void ResetSequence()
    {
        currentIndex = 0;
        timer = 0f;
    }

    void TriggerSecret()
    {
        Debug.Log("KYS sequence entered, triggering death!");
        armManager.KYS();
    }
}
