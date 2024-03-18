using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementController : MonoBehaviour
{
    private Transform target; 
    [SerializeField] private float smoothSpeed = 0.125f;

    private void Update()
    {
        if (target == null) { return; }
        
        Vector3 desiredPosition = target.position;
        desiredPosition.z = transform.position.z;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}
