using UnityEngine;
using System.Collections;

public class SpaceShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float acceleration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Up")) {
			rigidbody2D.AddForce(transform.up * acceleration);
		}

		transform.Rotate (Vector3.forward * Input.GetAxis("Horizontal") * rotationSpeed);
	
	}
}
