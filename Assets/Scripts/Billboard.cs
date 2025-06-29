using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;
    [SerializeField] public Transform camera;

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;
    
    private Vector3 originalRotation;
    public enum BillboardType { LookAtCamera, CameraForward }
    // Start is called before the first frame update
    void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
// if camera hasnt been assigned, search for player camera
        if (camera == null)
        {
            camera = GameObject.Find("Camera")?.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(camera.position, Vector3.up);
                break;
            case BillboardType.CameraForward:
                transform.forward = camera.forward;
                break;
            default:
                break;
        }

        Vector3 rotation = transform.rotation.eulerAngles;
        if (lockX) rotation.x = originalRotation.x;
        if (lockY) rotation.y = originalRotation.y;
        if (lockZ) rotation.z = originalRotation.z;
        transform.rotation = Quaternion.Euler(rotation);
    }
}