using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovementController : MonoBehaviour
{
    public Transform Target;

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }
}
