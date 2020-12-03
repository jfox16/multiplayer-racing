using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float positionSlerpRatio;

    [SerializeField] float rotationSlerpRatio;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player) {
            transform.position = Vector3.Slerp(transform.position, player.transform.position, positionSlerpRatio);
            transform.rotation = Quaternion.Slerp(transform.rotation, player.transform.rotation, rotationSlerpRatio);
        }
    }
}
