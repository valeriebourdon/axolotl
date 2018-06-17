using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	//get rigibody and camera
	private Rigidbody rb;
	public Transform cameraTransform;

	//movement speed
	private float walkSpeed = 6f;
	private float sprintSpeed = 10f;

    public bool deliverOrbs = false;


	void Start () {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

		//get rigidbody and camera
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		navigation();
        //trigger delivery
        if (Input.GetKey(KeyCode.Space)) deliverOrbs = true;
        else deliverOrbs = false;
	}

	//move freely in 3 dimensions
	void navigation() {

		float speed;

		if (Input.GetKey(KeyCode.LeftShift)) speed = sprintSpeed;
		else speed = walkSpeed;

        if (Input.GetMouseButton(0)) {
            rb.AddForce((cameraTransform.forward) * speed);

            float tilt = cameraTransform.eulerAngles.x + 90 / 8;
            if (tilt > 90) tilt = 0;

            Quaternion axisTilt = Quaternion.Euler(tilt, 0, 0);
            transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * axisTilt;
        } else {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
	}
}
