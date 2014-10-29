using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (DamageScript))]
public class SpaceShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float acceleration;

	public AudioClip fireSound;
	public AudioClip shieldSound;
	public AudioClip onBeingHit;

	public float maxShield = 3f;
	public float shield = 3f;
	public float maxlife = 100f;
	public float life = 100f;

	List<WeaponScript> weapons;
	List<ParticleSystem> engineEffects;
	List<ParticleSystem> shieldEffects;

	DamageScript damagescript;

	bool thrust = false;



	// Use this for initialization
	void Start () {

		damagescript = GetComponent<DamageScript> ();

		weapons = new List<WeaponScript> ();
		WeaponScript[] weaponArray = GetComponentsInChildren<WeaponScript> ();
		for (int i = 0; i < weaponArray.Length; i++) {
			weapons.Add(weaponArray[i]);
			weaponArray[i].register(gameObject);
		}

		engineEffects = new List<ParticleSystem> ();
		ParticleSystem[] engineArray = transform.FindChild ("EngineEffects").GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < engineArray.Length; i++) {
			engineEffects.Add(engineArray[i]);
		}

		shieldEffects = new List<ParticleSystem> ();
		ParticleSystem[] shieldArray = transform.FindChild ("ShieldEffects").GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < shieldArray.Length; i++) {
			shieldEffects.Add(shieldArray[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Up")) {
			if (!thrust) {
				startEngineEffect();
			}
			thrust = true;
			// Caps the maximum speed of the player
			if (!(rigidbody2D.velocity.sqrMagnitude > 15)) {
				rigidbody2D.AddForce(transform.up * acceleration);
			} 
		} else {
			if (thrust) {
				stopEngineEffect();
			}
			thrust = false;
		}
		
		transform.Rotate (Vector3.forward * Input.GetAxis("Horizontal") * rotationSpeed);

		if (Input.GetButtonDown ("Fire")) {
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
			foreach (WeaponScript weapon in weapons) {
				weapon.fire();
			}
		}
	
	}

	void OnTriggerEnter2D (Collider2D obj) {
		var name = obj.gameObject.name;
		
		if (name == "turretbullet(Clone)") {

			gettingHit();

			Destroy(obj.gameObject);
		}
	}

	void gettingHit() {
		if (shield > 0f) {
			shield = shield - 1;
			startShieldEffect();
			AudioSource.PlayClipAtPoint(shieldSound, transform.position);
		} else {
			life = life - 10;
			AudioSource.PlayClipAtPoint(onBeingHit, transform.position);
			//I would like a short shake of the camera here.
			if (life <= 0) {
				Destroy(gameObject);
			}
		}
		damagescript.setLife (life * 100 / maxlife);
	}

	void startEngineEffect() {
		foreach (ParticleSystem particleSystem in engineEffects) {
			particleSystem.Play();
		}
	}
	
	void stopEngineEffect() {
		foreach (ParticleSystem particleSystem in engineEffects) {
			particleSystem.Stop();
		}
	}

	void startShieldEffect() {
		foreach (ParticleSystem particleSystem in shieldEffects) {
			particleSystem.Play();
		}
	}

}
