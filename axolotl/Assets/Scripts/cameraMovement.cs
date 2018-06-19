﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
SOURCE CODE TAKEN FROM: http://wiki.unity3d.com/index.php?title=MouseOrbitImproved
*/

public class cameraMovement : MonoBehaviour
{

    public Transform target;
    public float distance = 7.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float zSpeed = 5.0f;

    public float yMinLimit = -80f;
    public float yMaxLimit = 80f;

    public float distanceMin = 7f;
    public float distanceMax = 12f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 10, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }

            Vector3 negDistance = new Vector3(0.0f, 1.1f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}