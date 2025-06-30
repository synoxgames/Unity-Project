using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

/**
 * This was a script I yoinked from my game project 
 * and slapped into this project
*/
public class CameraShake : MonoBehaviour
{
    // ----- Camera Reference -----
    public GameObject gameCamera;

    // ----- Shaking Variables -----
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.4f;
    private bool isShaking = false;
    private Vector3 originalPos;

    // ----- Blood Effect Variables -----
    public Image bloodEffectImage;
    public float bloodDuration = 0.5f;


    // ----- Debugging -----
    public bool debug = false;

    void Start()
    {
        if (gameCamera == null)
        {
            gameCamera = Camera.main.gameObject;
        }
        isShaking = false;
        originalPos = gameCamera.transform.position;
    }

    public IEnumerator Shake()
    {
        originalPos = gameCamera.transform.position;
        if (debug) Debug.Log("Original position: " + originalPos);

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // Randomize the shake effect using the magnitude
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Set camera position during shake
            gameCamera.transform.position = originalPos + new Vector3(x, y, 0);
            if (debug) Debug.Log("Shaking: " + gameCamera.transform.position);
            elapsed += Time.deltaTime;

            yield return null;
        }

        if (debug) Debug.Log("Returning to original position: " + originalPos +
        "\nTransform position: " + transform.position +
        "\nGame camera position: " + gameCamera.transform.position
        );

        gameCamera.transform.position = originalPos; // Return to original position
    }

    public IEnumerator Shaking()
    {
        originalPos = gameCamera.transform.position;
        if (debug) Debug.Log("Original position: " + originalPos);

        while (isShaking)
        {
            // Randomize the shake effect using the magnitude
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Set camera position during shake
            gameCamera.transform.position = originalPos + new Vector3(x, y, 0);
            yield return null;
        }
    }

    public void Shaking(bool shaking)
    {
        if (shaking)
        {
            isShaking = true;
            StartCoroutine(Shaking());
        }
        else if (!shaking)
        {
            isShaking = false;
            StopCoroutine(Shaking());
            gameCamera.transform.position = originalPos; // Return to original position
            if (debug) Debug.Log("Stopped shaking. Returned camera to position: " + originalPos);
        }
        else
        {
            Debug.LogError("Invalid shaking state.");
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
        StartCoroutine(FadeBloodEffect());
    }

    // This method will increase blood effect alpha to max, then fade it out over the specified duration
    public IEnumerator FadeBloodEffect()
    {
        bloodEffectImage.color = new Color(bloodEffectImage.color.r, bloodEffectImage.color.g, bloodEffectImage.color.b, 1f);
        float elapsed = 0f;
        while (elapsed < bloodDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / bloodDuration);
            bloodEffectImage.color = new Color(bloodEffectImage.color.r, bloodEffectImage.color.g, bloodEffectImage.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

