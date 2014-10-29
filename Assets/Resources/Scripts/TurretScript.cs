using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (DamageScript))]
public class TurretScript : MonoBehaviour {

	public GameObject target;
	public float cooldown = 3;
	float toFire;
	float life = 100;
	float maxlife = 100;

	DamageScript damagescript;

	List<WeaponScript> weapons;

	// Use this for initialization
	void Start () {
		toFire = Time.time + cooldown + Random.Range(0f,1f);
		weapons = new List<WeaponScript>();
		WeaponScript[] weaponArray = GetComponentsInChildren<WeaponScript> ();
		for (int i = 0; i < weaponArray.Length; i++) {
			weapons.Add(weaponArray[i]);
			weaponArray[i].register(gameObject);
		}
		damagescript = GetComponent<DamageScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (target == null) {
			target = GameObject.Find("Spaceship");
		}

		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		Vector3 bla = transform.eulerAngles;

		if (Time.time > toFire) {
			float random = Random.Range(0f,1f);
			toFire = Time.time + cooldown + random;
			foreach (WeaponScript weapon in weapons) {
				weapon.fire();
			}
		}

	}

	void OnTriggerEnter2D (Collider2D obj) {
		var name = obj.gameObject.name;
		
		if (name == "bullet(Clone)") {
			life = life - 20;
			Destroy(obj.gameObject);
		}
		if (life <= 0) {
			Destroy(gameObject);
		}
		damagescript.setLife (life * 100 / maxlife);
	}
}
