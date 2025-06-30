using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallucinationShake : MonoBehaviour
{
    [Header("Wave One")]
    public float aWaveOne;
    public float fWaveOne;

    [Header("Wave Two")]
    public float aWaveTwo;
    public float fWaveTwo;

    [Header("Wave Three")]
    public float aWaveThree;
    public float fWaveThree;

    public void Update() {
        float waveOne = Mathf.Sin(Time.time * fWaveOne) * aWaveOne;
        float waveTwo = Mathf.Sin(Time.time * fWaveTwo) * aWaveTwo; ;
        float waveThree = Mathf.Sin(Time.time * fWaveThree) * aWaveThree;

        float finalWave = waveOne + waveTwo + waveThree;

        transform.localPosition = new Vector3(finalWave, 0, -finalWave);
    }
}
