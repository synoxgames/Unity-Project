using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBob : MonoBehaviour
{
    public float amplitude = 3f;
    public float frequency = 3f;

    public void Update() {

        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude, 0);
    }
}
