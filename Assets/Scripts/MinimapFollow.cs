using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 initialPosition;
    public float smoothing = 1.5f;
    private Vector3 targetVector;

    private void Start() {
        initialPosition = transform.position;
    }

    public void LateUpdate() {
        targetVector = new Vector3(player.position.x, initialPosition.y, player.position.z);
        transform.position = Vector3.Lerp(transform.position, targetVector, smoothing * Time.deltaTime);
    }
}
