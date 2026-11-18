using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackPitchController : MonoBehaviour
{
    [Range(-3, 3)] public float minPitch = 0.25f, maxPitch = 1.25f;
    public float pitchDropStart = 0.1f, pitchClimbStart = 0.85f;
    AudioSource musicController;
    LevelManager lm;

    private void Start() {
        lm = FindObjectOfType<LevelManager>();
        musicController = GetComponent<AudioSource>();
    }


    private void Update() {
        float opiumLvl = lm.GetSmackPercent();
        float targetPitch = 1f;

        if (opiumLvl < pitchDropStart) {
            float t = Mathf.InverseLerp(0f, pitchDropStart, opiumLvl);
            targetPitch = Mathf.Lerp(minPitch, 1f, t);
        } else if (opiumLvl > pitchClimbStart) {
            float t = Mathf.InverseLerp(pitchClimbStart, 1f, opiumLvl);
            targetPitch = Mathf.Lerp(1f, maxPitch, t);
        }

        musicController.pitch = Mathf.Lerp(musicController.pitch, targetPitch, Time.deltaTime * 5f);
    }
}
