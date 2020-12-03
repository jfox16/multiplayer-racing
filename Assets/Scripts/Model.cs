using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    [SerializeField] GameObject parent;

    [SerializeField] ControlSphere controlSphere;

    [SerializeField] float rotSlerpRatio = 0.5f;

    void Update()
    {
        // Rotate model to match normal vector. We only rotate on x and z axes because the y-rotation is determined
        // by the parent transform.
        // relativeNormal is the normal vector direction relative to the current rotation.
        // Determine the difference between Vector3.up and the relativeNormal, and use its x- and z-rotations to 
        // rotate the model.
        Vector3 relativeNormal = Quaternion.Inverse(parent.transform.rotation) * controlSphere.normal;
        Vector3 relativeAngles = Quaternion.FromToRotation(Vector3.up, relativeNormal).eulerAngles;
        Quaternion rotation = Quaternion.Euler(relativeAngles.x, parent.transform.rotation.eulerAngles.y, relativeAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSlerpRatio);
    }
}
