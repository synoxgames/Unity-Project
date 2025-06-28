using UnityEngine;
using UnityEngine.AI;

public class RunAwayAI : MonoBehaviour
{
    public Transform player;
    public float fleeDistance = 5f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        Vector3 dir = transform.position - player.position;
        dir.y = 0;

        if (dir.magnitude < fleeDistance)
        {
            Vector3 fleeDir = dir.normalized;
            Vector3 right = Vector3.Cross(Vector3.up, fleeDir); // perpendicular to flee direction
            Vector3 offset = right * Mathf.Sin(Time.time * 3f); // oscillate left/right

            Vector3 fleeTarget = transform.position + (fleeDir + offset).normalized * 4f;

            if (NavMesh.SamplePosition(fleeTarget, out var hit, 2f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }
}
