using UnityEngine;
using System.Collections;

public class SpaceShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float acceleration;
	public GameObject bullet;
	public GameObject particleSystemR;
	public GameObject particleSystemL;
	public GameObject particleSystemShield;
	public GameObject particleSystemFire;
	public GameObject particleSystemFireSmoke;
	public GameObject particleSystemSpark;
	public AudioClip fireSound;
	public AudioClip shieldSound;
	public AudioClip onBeingHit;

	public float shield = 3f;
	public float maxShield = 3f;

	public float life = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (life < 80) {
			if (!particleSystemSpark.particleSystem.isPlaying) {
				particleSystemSpark.particleSystem.Play();
			}
		}
		if (life < 35) {
			if (!particleSystemFire.particleSystem.isPlaying) {
				particleSystemFire.particleSystem.Play();
			}
			if (!particleSystemFireSmoke.particleSystem.isPlaying) {
				particleSystemFireSmoke.particleSystem.Play();
			}
		}

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

		transform.Rotate (Vector3.forward * Input.GetAxis("Horizontal") * rotationSpeed);

		if (Input.GetButtonDown ("Fire")) {
			AudioSource.PlayClipAtPoint(fireSound, transform.Find("Spaceship").transform.position);
			Instantiate(bullet, transform.Find("BulletSpawnerL").transform.position, transform.rotation );
			Instantiate(bullet, transform.Find("BulletSpawnerR").transform.position, transform.rotation );
		}
	
	}

	void OnTriggerEnter2D (Collider2D obj) {
		var name = obj.gameObject.name;
		
		if (name == "turretbullet(Clone)") {

			if (shield > 0f) {
				shield = shield - 1;
				particleSystemShield.particleSystem.Play();
				AudioSource.PlayClipAtPoint(shieldSound, transform.Find("Spaceship").transform.position);
			} else {
				life = life - 10;
				AudioSource.PlayClipAtPoint(onBeingHit, transform.Find("Spaceship").transform.position);
				//I would like a short shake of the camera here.
				if (life <= 0) {
					Destroy(gameObject);
				}
			}

			//Destroy(gameObject);
			Destroy(obj.gameObject);
		}
	}
}
