using UnityEngine;
using System.Collections;

public class SpaceShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float acceleration;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Up")) {
			rigidbody2D.AddForce(transform.up * acceleration);
		}

		transform.Rotate (Vector3.forward * Input.GetAxis("Horizontal") * rotationSpeed);

		if (Input.GetButtonDown ("Fire")) {
			Instantiate(bullet, transform.Find("BulletSpawnerL").transform.position, transform.rotation );
			Instantiate(bullet, transform.Find("BulletSpawnerR").transform.position, transform.rotation );
		}
	
	}

	void OnTriggerEnter2D (Collider2D obj) {
		var name = obj.gameObject.name;
		
		if (name == "turretbullet(Clone)") {
			Destroy(gameObject);
			Destroy(obj.gameObject);
		}
	}
}
