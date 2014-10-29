using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float acceleration;

	public AudioClip fireSound;
	public AudioClip shieldSound;
	public AudioClip onBeingHit;

	public float maxShield = 3f;
	public float shield = 3f;
	public float life = 100f;

	List<WeaponScript> weapons;
	List<ParticleSystem> engineEffects;
	List<ParticleSystem> shieldEffects;

	[System.Serializable]
	public class DamageAnimationEntry {
		public float Damage;
		public GameObject Effect;
	}

	public List<DamageAnimationEntry> damages;

	bool thrust = false;



	// Use this for initialization
	void Start () {
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


		foreach (DamageAnimationEntry entry in damages) {
			if (life < entry.Damage && !entry.Effect.particleSystem.isPlaying) {
				entry.Effect.particleSystem.Play ();
			}

			if (life > entry.Damage && entry.Effect.particleSystem.isPlaying) {
				entry.Effect.particleSystem.Stop ();
			}
		}

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
			AudioSource.PlayClipAtPoint(fireSound, transform.Find("Spaceship").transform.position);
			foreach (WeaponScript weapon in weapons) {
				weapon.fire();
			}
		}
	
	}

	void OnTriggerEnter2D (Collider2D obj) {
		var name = obj.gameObject.name;
		
		if (name == "turretbullet(Clone)") {

			if (shield > 0f) {
				shield = shield - 1;
				startShieldEffect();
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
