using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmManager : MonoBehaviour
{
    [Header("Arm Sprites")]
    public Image armImage;
    public Sprite idleSprite;
    public Sprite injectSprite;
    public Sprite grabSprite;
    public Sprite kysSprite;
    [Header("Injection Settings")]
    public float injectDuration = 1.5f;
    public KeyCode injectKey = KeyCode.Mouse0;
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip[] punchSounds;
    public AudioClip bangSound;
    [Header("Script References")]
    public LevelManager levelManager;
    public CameraShake cameraShake;
    private bool isInjecting = false;
    private bool grabbing = false;

    public static ArmManager instance;

    private void Awake() {
        if (instance != this) Destroy(instance);
        instance = this;
    }

    void Update()
    {
        if (!isInjecting && Input.GetKeyDown(injectKey) && levelManager.smackCount > 0 && !grabbing)
        {
            StartCoroutine(Inject());
            levelManager.smackCount--; // Decrease smack count
            levelManager.smackCountText.text = "X " + levelManager.smackCount.ToString(); // Update UI text
        }
    }

    IEnumerator Inject()
    {
        AudioClip clip = punchSounds[Random.Range(0, punchSounds.Length)];
        audioSource.PlayOneShot(clip);
        cameraShake.ShakeCamera();
        isInjecting = true;
        armImage.sprite = injectSprite;
        yield return new WaitForSeconds(injectDuration);
        armImage.sprite = idleSprite;
        isInjecting = false;
        levelManager.UpdateSmackLevel(20f); // Increase smack level by 10
    }

    public void SetGrabbing(bool isGrabbing)
    {
        grabbing = isGrabbing;
        if (grabbing)
        {
            armImage.sprite = grabSprite;
        }
        else
        {
            armImage.sprite = idleSprite;
        }
    }

    public void KYS()
    {
        // sets arm sprite to kys, waits 2 seconds and shakes the image, plays a bang sound, screen goes black, scene resets
        armImage.sprite = kysSprite;
        StartCoroutine(KYSCoroutine());
    }

    private IEnumerator KYSCoroutine()
    {
        cameraShake.Shaking(true);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(bangSound);
        cameraShake.Shaking(false);
        levelManager.blackScreen.gameObject.SetActive(true); // Ensure black screen is active
        levelManager.blackScreen.color = new Color(0, 0, 0, 1f); // Set black screen to opaque
        
        yield return new WaitForSeconds(1f);
        // Reset the scene or perform any other action needed after KYS
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        StartCoroutine(levelManager.FadeOut()); // Fade in after reset
    }
}
