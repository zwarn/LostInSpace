using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

	public GameObject target;
	public GameObject turretbullet;
	public float cooldown = 3;
	float toFire;
	float life = 100;
	public GameObject particleSystemFire;
	public GameObject particleSystemSpark;


	// Use this for initialization
	void Start () {
		toFire = Time.time + cooldown + Random.Range(0f,1f);
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
		}
		if (target == null) {
			target = GameObject.Find("spaceship");
		}

		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		Vector3 bla = transform.eulerAngles;

		if (Time.time > toFire) {
			float random = Random.Range(0f,1f);
			toFire = Time.time + cooldown + random;

			Debug.Log (random);

			Instantiate (turretbullet, transform.Find("BulletSpawner").transform.position, Quaternion.Euler (new Vector3(bla.x, bla.y, bla.z-90)));
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
	}
}
