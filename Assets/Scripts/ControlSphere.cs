using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSphere : MonoBehaviour
{
    [SerializeField] SphereCollider sphereCollider;

    public bool isGrounded {get; private set;}

    public Vector3 normal {get; private set;}

    public float groundDistance {get; private set;}

    // moveDirection is the calculated forward direction based on the ground's normal vector.
    // e.g. if the ground slopes upward, this moveDirection will go parallel with the ground.
    public Vector3 forward {
        get {
            Vector3 cross = Vector3.Cross(transform.rotation * Vector3.right, normal).normalized;
            return cross;
        }
    }

    void Awake()
    {
        normal = Vector3.up;
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo;

        if (Physics.SphereCast(
            transform.position + sphereCollider.center,
            sphereCollider.radius - 0.01f,
            Vector3.down,
            out hitInfo,
            1.0f))
        {
            isGrounded = hitInfo.distance <= 0.02f;
            groundDistance = hitInfo.distance;
        }
        else
        {
            isGrounded = false;
            groundDistance = Mathf.Infinity;
        }
            
        if (isGrounded) {
            normal = hitInfo.normal;
        }
        
        Debug.DrawLine(transform.position, transform.position + normal * 10);
        Debug.DrawLine(transform.position, transform.position + forward * 10, Color.magenta);
    }
}
