using UnityEngine;
using UnityEngine.AI;

public class RunAwayAI : MonoBehaviour
{
    public Transform player;
    [Header("Flee Settings")]
    public float baseSpeed = 1.5f;
    public float maxSpeed = 6f;
    public float speedScale = 2f; //smaller = faster ramp up
    public float fleeDistance = 5f;
    private NavMeshAgent agent;
    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    private bool isFleeing = false;

    [Header("Audio")]
    public AudioClip[] tauntSounds;
    public float audioCooldown = 1.5f;
    private AudioSource audPlayer;
    float audioTimer = 0.0f;

    void Start()
    {
        audPlayer = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 dir = transform.position - player.position;
        dir.y = 0f; // Keep the flee direction level with the agent

        float distance = dir.magnitude;

        // Update speed exponentially: faster when close
        float speedFactor = Mathf.Exp(-distance / speedScale); // ~1 when close, ~0 when far
        agent.speed = Mathf.Lerp(baseSpeed, maxSpeed, 1f - speedFactor);

        // Flee if close
        isFleeing = distance < fleeDistance;

        if (isFleeing)
        {
            // Output object is fleeing
            Debug.Log("Sprite is fleeing from player!");
            Vector3 target = transform.position + dir.normalized * 4f;
            if (NavMesh.SamplePosition(target, out var hit, 2f, NavMesh.AllAreas))
            {
                if (Time.time > audioTimer) PlayRandomSound();

                agent.SetDestination(hit.position);
            }
        }

        // Detect if stuck
        float movementDelta = (transform.position - lastPosition).magnitude;
        if (movementDelta < 0.05f && isFleeing)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= 1f)
            {
                Debug.Log("Sprite is stuck! Scrambling...");
                ScrambleEscape(dir);
                stuckTimer = 0f;
            }
        }
        else
        {
            if (!isFleeing)
            {
                agent.ResetPath();
                stuckTimer = 0f;
            }
        }
        lastPosition = transform.position;
    }

    void ScrambleEscape(Vector3 dirFromPlayer)
    {
        // Try random offset around original flee direction
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomOffset = Quaternion.Euler(0, Random.Range(-90f, 90f), 0) * dirFromPlayer.normalized;
            Vector3 newTarget = transform.position + randomOffset * 4f;

            if (NavMesh.SamplePosition(newTarget, out var hit, 2f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                return;
            }
        }

        // fallback: teleport a little
        Vector3 emergencyDirection = Random.insideUnitSphere;
        emergencyDirection.y = 0;
        if (NavMesh.SamplePosition(transform.position + emergencyDirection * 2f, out var warpHit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(warpHit.position);
        }
    }

    private void PlayRandomSound() {
        if (audPlayer.isPlaying) return;
        AudioClip clip = tauntSounds[Random.Range(0, tauntSounds.Length)];
        audPlayer.PlayOneShot(clip);
        audioTimer = Time.time + audioCooldown;
    }
}
