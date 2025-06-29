using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Floats")]
    public float moveSpeed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float stepInterval;
    [Space]
    public float sprintInrease = 2f;
    public float sprintMax = 10f;

    [Header("Transform")]
    public Transform groundCheck;

    [Header("Layers")]
    public LayerMask groundMask;
    public SurfaceType currentSurfaceType;

    [Header("Boolean")]
    public bool isWalking;
    public bool isSprinting;
    public bool hasLight;
    bool reachedMaxSprint;
    bool reachedRechargeFailSafe;
    bool changedSpeed;
    public static bool canMove = true;

    [Header("Effects")]
    public WalkSound[] walkSounds;

    CharacterController controller;
    bool isGrounded;
    Vector3 velocity;
    //Animator anim;
    AudioSource aud;
    List<AudioClip> walkingClips = new List<AudioClip>();
    SurfaceType tempSurface = SurfaceType.Wood;
    float nextWalk = 0.0f;
    float sprintDuration;
    AudioClip previousStep;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //anim = GetComponentInChildren<Animator>();
        aud = GetComponent<AudioSource>();
        FindWalkingClips(tempSurface);
    }

    public void Update()
    {
        if (canMove)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            isWalking = (x != 0 | z != 0);

            Vector3 move = transform.right * x + transform.forward * z;

            if (Input.GetButtonDown("Jump") && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

            controller.Move(move * moveSpeed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            //anim.SetBool("isWalking", isWalking);

            if (Input.GetButtonDown("Sprint") && !reachedMaxSprint)
                Sprint(true);
            if (Input.GetButtonUp("Sprint") && !reachedMaxSprint)
                Sprint(false);

            if (isSprinting)
                sprintDuration += Time.deltaTime;
            else if (!isSprinting && sprintDuration > 0)
                sprintDuration -= Time.deltaTime;

            if (reachedRechargeFailSafe)
            {
                if (Input.GetButtonUp("Sprint"))
                {
                    reachedRechargeFailSafe = false;
                    reachedMaxSprint = true;
                }
            }

            if (reachedMaxSprint && sprintDuration <= 0)
            {

                if (!Input.GetButton("Sprint"))
                    reachedMaxSprint = false;
                else
                    reachedRechargeFailSafe = true;

            }

            if (!reachedMaxSprint && sprintDuration >= sprintMax)
            {
                reachedMaxSprint = true;
                Sprint(false);
            }

            if (isWalking)
            {
                CheckGround();

                if (tempSurface != currentSurfaceType)
                {
                    FindWalkingClips(tempSurface);
                    currentSurfaceType = tempSurface;
                }

                if (isGrounded && Time.time > nextWalk)
                {
                    PlaySounds();
                }
            }

            if (isSprinting && changedSpeed == false && isWalking)
            {
                //anim.speed *= 2f;
                changedSpeed = true;
            }
        }
        else
        {
            //anim.SetBool("isWalking", false);
        }

        //anim.SetBool("hasLight", hasLight);
    }

    public void Sprint(bool state)
    {
        isSprinting = state;

        if (state)
        {
            moveSpeed = moveSpeed * sprintInrease;
            stepInterval /= sprintInrease;

            if (isWalking)
            {
                //anim.speed *= 2f;
                changedSpeed = true;
            }
        }
        else if (!state)
        {
            moveSpeed = moveSpeed / sprintInrease;
            stepInterval *= sprintInrease;

            if (changedSpeed)
            {
                //anim.speed /= 2f;
                changedSpeed = false;
            }
        }
    }

    public void CheckGround()
    {
        if (isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(groundCheck.position, -groundCheck.up, out hit, groundMask))
            {
                if (hit.transform.GetComponent<Ground>())
                {
                    if (hit.transform.GetComponent<Ground>().type != tempSurface)
                    {
                        aud.Stop();
                        tempSurface = hit.transform.GetComponent<Ground>().type;
                    }
                }
            }
        }
    }

    public void FindWalkingClips(SurfaceType type)
    {
        foreach (WalkSound sound in walkSounds)
        {
            if (sound.surface == type)
            {
                walkingClips = sound.walkVariants;
                PlaySounds();
                break;
            }
        }
    }

    public void PlaySounds()
    {
        AudioClip newClip = walkingClips[Random.Range(0, walkingClips.Count)];

        if (newClip == previousStep)
        {
            PlaySounds();
        }
        else
        {
            previousStep = newClip;
            aud.PlayOneShot(newClip);
            nextWalk = Time.time + stepInterval;
        }
    }

    public void PickupFlashlight()
    {
        if (hasLight)
        {
            Debug.Log("Player Already Has Flashlight");
            PopupText.SetColour(Color.yellow);
            PopupText.SetText("Your one will do fine.");
            PopupText.DelayTrigger(5);
            return;
        }

        hasLight = true;
        GameObject.FindWithTag("Flashlight").SetActive(true);

    }

    // Attach to player
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        // Ignore if pushing from below
        if (hit.moveDirection.y < -0.3f)
            return;

        // Apply push force
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * 3f; // tweak speed
    }

}

public enum SurfaceType
{
    Concrete,
    BlanketOnConcrete,
    Grass,
    Blood,
    Wood,
    Carpet,
    Tile
}

[System.Serializable]
public class WalkSound
{
    public SurfaceType surface;
    public List<AudioClip> walkVariants = new List<AudioClip>();
}
