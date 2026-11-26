using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBob : MonoBehaviour, IEPausable
{
    public float amplitude = 3f;
    public float frequency = 3f;
    bool isPaused = false;

    public void OnPause() {
       isPaused = true;
    }

    public void OnUnPause() {
        isPaused = false;
    }

    public void Update() {
        if (isPaused) return;
        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude, 0);
    }
}
