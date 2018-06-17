using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	//get rigibody and camera
	private Rigidbody rb;
	private Transform cameraTransform;
	private Transform cameraFakeTransform;

	//movement speed
	private float walkSpeed = 5f;
	private float sprintSpeed = 11f;

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

	void Start () {
		//get rigidbody and camera
		rb = GetComponent<Rigidbody>();
		cameraTransform = Camera.main.transform;
		cameraFakeTransform = GameObject.Find("CameraFake").transform;

		//get camera rotation
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;

		//lock cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void Update () {
		camera();
		navigation();
	}

	//move freely in 3 dimensions
	void navigation() {

		float speed;

		if (Input.GetKey(KeyCode.LeftShift)) speed = sprintSpeed;
		else speed = walkSpeed;

		if (Input.GetKey("w")) {
			rb.AddForce((cameraTransform.forward) * speed);
		}

		if (Input.GetKey("s")) {
			rb.AddForce((cameraTransform.forward * -1) * speed);
		}

		if (Input.GetKey("a")) {
			Vector3 left = Vector3.Cross(cameraTransform.forward, cameraTransform.up);
			rb.AddForce(left * speed);
		}

		if (Input.GetKey("d")) {
			Vector3 left = Vector3.Cross(cameraTransform.forward, cameraTransform.up);
			Vector3 right = -left;
			rb.AddForce(right * speed);
		}

		if (Input.GetKey(KeyCode.Space)) {
			rb.AddForce(cameraTransform.up * speed);
		}

		if (Input.GetKey(KeyCode.LeftControl)) {
			rb.AddForce(-cameraTransform.up * speed);
		}
	}

	//rotates camera using mouse controls
	void camera() {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotX += mouseX * mouseSensitivity * Time.deltaTime;
		rotY += mouseY * mouseSensitivity * Time.deltaTime;

		rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
		transform.rotation = localRotation;

		camDistance = Vector3.Distance(cameraFakeTransform.position, transform.position);

		//zoom out (scroll down)
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			if (camDistance < minZoom) cameraFakeTransform.position += -cameraTransform.forward / zoomDampen;
		}

		//zoom in (scroll up)
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			if (camDistance > maxZoom) cameraFakeTransform.position += cameraTransform.forward / zoomDampen;
		}

		cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraFakeTransform.position, zoomSpeed);
	}
}
