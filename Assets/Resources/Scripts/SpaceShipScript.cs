using UnityEngine;
using System.Collections;

public class SpaceShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float acceleration;
	public GameObject bullet;
	public GameObject particleSystemR;
	public GameObject particleSystemL;
	public GameObject particleSystemShield;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Up")) {

			// Caps the maximum speed of the player
			if (rigidbody2D.velocity.sqrMagnitude > 15) {
				if (!particleSystemR.particleSystem.isPlaying) {
					particleSystemR.particleSystem.Play();
					particleSystemL.particleSystem.Play();
				}
			}
			else {
				rigidbody2D.AddForce(transform.up * acceleration);
			}
			if (!particleSystemR.particleSystem.isPlaying) {
				particleSystemR.particleSystem.Play();
				particleSystemL.particleSystem.Play();
			}
		} else {
			if (particleSystemR.particleSystem.isPlaying) {
				particleSystemR.particleSystem.Stop();
				particleSystemL.particleSystem.Stop();
			}
		}

		//This reduces the players velocity over time, so that he eventually stands still

		if (rigidbody2D.velocity.x > 0.1f) {
						rigidbody2D.AddForce (new Vector3 (-1f, 0f, 0f) * acceleration / 10);
				} else if (rigidbody2D.velocity.x < -0.1f) {
						rigidbody2D.AddForce (new Vector3 (1f, 0f, 0f) * acceleration / 10);
				} else if (rigidbody2D.velocity.y > 0.1f) {
						rigidbody2D.AddForce (new Vector3 (0f, -1f, 0f) * acceleration / 10);
				} else if (rigidbody2D.velocity.x < 0.1f) {
						rigidbody2D.AddForce (new Vector3 (0f, 1f, 0f) * acceleration / 10);
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
			particleSystemShield.particleSystem.Play();
			//Destroy(gameObject);
			Destroy(obj.gameObject);
		}
	}
}
