﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform objToFollow; // The Object we're following.

    public float lerpScale = 2f; // How much we scale our smooth lerp movement.

    public float yOffset = -2f;

    // We use FixedUpdate because our target object is probably moving via physics.
    void FixedUpdate()
    {
        if (objToFollow == null)
        {
            return; // Don't try to follow if we don't have a target.
        }
        Vector3 targetPos = Vector3.Lerp(transform.position, objToFollow.transform.position, Time.fixedDeltaTime * lerpScale);
        //new Vector3 (objToFollow.transform.position.x, objToFollow.transform.position.y - yOffset, objToFollow.transform.position.z)
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }

    public float farLeft, farRight; //end of screen left and right

    void Update()
    {
        if (this.transform.position.x > farRight)
        {
            transform.position = new Vector3(farRight, transform.position.y, transform.position.z);
        }

        if (this.transform.position.x < farLeft)
        {
            transform.position = new Vector3(farLeft, transform.position.y, transform.position.z);
        }
    }
}
