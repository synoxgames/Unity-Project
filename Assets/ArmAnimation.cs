using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmAnimation : MonoBehaviour
{
    public Image armImage;
    public Sprite idleSprite;
    public Sprite injectSprite;
    public float injectDuration = 1.5f;
    public KeyCode injectKey = KeyCode.Mouse0;
    public AudioSource audioSource;
    public AudioClip[] punchSounds;
    private bool isInjecting = false;

    void Update()
    {
        if (!isInjecting && Input.GetKeyDown(injectKey))
        {
            StartCoroutine(Inject());
        }
    }

    IEnumerator Inject()
    {
        AudioClip clip = punchSounds[Random.Range(0, punchSounds.Length)];
        audioSource.PlayOneShot(clip);
        isInjecting = true;
        armImage.sprite = injectSprite;
        yield return new WaitForSeconds(injectDuration);
        armImage.sprite = idleSprite;
        isInjecting = false;
    }
}
