using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    //get rigibody and camera
    public Transform cameraFakeTransform;

    //mouse limits
    private float mouseSensitivity = 100.0f;
    private float clampAngle = 80.0f;

    //camera rotation
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    //camera zoom limits
    private float maxZoom = 4f;
    private float minZoom = 10f;
    private float camDistance;
    private const float zoomDampen = 0.5f;
    private const float zoomSpeed = 0.05f;

    void Start()
    {
        //get camera rotation
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        cam();
    }

    //rotates camera using mouse controls
    void cam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseX * mouseSensitivity * Time.deltaTime;
        rotY += mouseY * mouseSensitivity * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
        transform.rotation = localRotation;

        camDistance = Vector3.Distance(cameraFakeTransform.position, transform.position);

        //zoom out (scroll down)
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camDistance < minZoom) cameraFakeTransform.position += -transform.forward / zoomDampen;
        }

        //zoom in (scroll up)
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (camDistance > maxZoom) cameraFakeTransform.position += transform.forward / zoomDampen;
        }

        transform.position = Vector3.Lerp(transform.position, cameraFakeTransform.position, zoomSpeed);
    }
}
