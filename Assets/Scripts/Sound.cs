using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(0.3f, 3f)]
    public float pitch;

    [Range(0, 256)]
    public int priority = 128;

    [Range(-1, 1)]
    public float pan = 0;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
