using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	//get rigibody and camera
	private Rigidbody rb;
	public Transform cameraTransform;

	//movement speed
	private float walkSpeed = 5f;
	private float sprintSpeed = 11f;

	void Start () {
		//get rigidbody and camera
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		navigation();
	}

	//move freely in 3 dimensions
	void navigation() {

		float speed;

		if (Input.GetKey(KeyCode.LeftShift)) speed = sprintSpeed;
		else speed = walkSpeed;

        if (Input.GetMouseButtonDown(0)) {
            rb.AddForce((cameraTransform.forward) * speed);
        }
	}
}
