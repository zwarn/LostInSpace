using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

	public GameObject target;
	public GameObject turretbullet;
	public float cooldown = 3;
	float toFire;


	// Use this for initialization
	void Start () {
		toFire = Time.time + cooldown;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		Vector3 bla = transform.eulerAngles;

		if (Time.time > toFire) {
			toFire = Time.time + cooldown;
			Instantiate (turretbullet, transform.Find("BulletSpawner").transform.position, Quaternion.Euler (new Vector3(bla.x, bla.y, bla.z-90)));
		}

	}
}
