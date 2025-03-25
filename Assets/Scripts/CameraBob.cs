using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour {

	[Header("Transforms")]
	public Transform headTransform;
	public Transform cameraTransform;

	[Header("Floats")]
	public float bobFrequency = 5f;
	public float bobHorizontalAmplitude = 0.1f;
	public float bobVerticalAmplitude = 0.1f;
	[Range(0, 1)]
	public float bobSmoothing;

	private float walkingTime;
	private Vector3 targetCameraPosition;
	bool changedSmooth;

	PlayerMovement playerController;

	public void Awake() {
		playerController = GetComponentInParent<PlayerMovement>();
		
    }

	public void Update() {
		if (!playerController.isWalking)
			walkingTime = 0;
		else
			walkingTime += Time.deltaTime;

		targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);

		cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, bobSmoothing);

		if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001f)
			cameraTransform.position = targetCameraPosition;

		if (playerController.isSprinting && !changedSmooth) {
			bobFrequency *= 2;
			changedSmooth = true;
        } else if (!playerController.isSprinting && changedSmooth) {
			bobFrequency /= 2;
			changedSmooth = false;
        }
    }

	private Vector3 CalculateHeadBobOffset(float t) {
		float _h = 0;
		float _v = 0;
		Vector3 offset = Vector3.zero;

		if (t > 0) {
			_h = Mathf.Cos(t * bobFrequency) * bobHorizontalAmplitude;
			_v = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmplitude;

			offset = headTransform.right * _h + headTransform.up * _v;
        }

		return offset;
    }
}
