using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] new Rigidbody rigidbody = null;

    [SerializeField] ControlSphere controlSphere = null;

    [SerializeField] Model model = null;

    [SerializeField] float maxSpeed = 10.0f;

    [SerializeField] float acceleration = 0.1f;

    [SerializeField] float frictionCoefficient = 0.5f;

    [SerializeField] float gravitationalAcceleration = 10.0f;

    [SerializeField] float turnAcceleration = 0.5f;

    [SerializeField] float slopeSensitivity = 0.2f;

    [SerializeField] float groundStickDistance = 0.5f;

    float currentSpeed = 0.0f;

    float currentGravity = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Read Inputs
        float intensity = Input.GetAxis("Vertical");
        
        float turnIntensity = Input.GetAxis("Horizontal");

        // Calculate rotation from turning
        transform.rotation *= Quaternion.AngleAxis(turnAcceleration * turnIntensity * currentSpeed, Vector3.up);

        // Calculate speed from acceleration
        currentSpeed = Mathf.Clamp(currentSpeed + acceleration * intensity, -maxSpeed, maxSpeed);

        // Apply slope coefficient
        float slopeCoefficient = Vector3.Dot(Vector3.up, controlSphere.forward * Mathf.Sign(currentSpeed)) * slopeSensitivity;
        currentSpeed -= currentSpeed * slopeCoefficient;

        // Apply friction
        currentSpeed -= currentSpeed * frictionCoefficient;

        if (controlSphere.isGrounded) {
            currentGravity = gravitationalAcceleration;
        }
        else {
            currentGravity += gravitationalAcceleration;
        }

        Vector3 moveVector = controlSphere.forward * currentSpeed;

        Vector3 gravityVector = Vector3.down * currentGravity;

        rigidbody.velocity = moveVector + gravityVector;

        if (controlSphere.groundDistance < groundStickDistance) {
            transform.position += Vector3.down * controlSphere.groundDistance;
        }
    }
}
