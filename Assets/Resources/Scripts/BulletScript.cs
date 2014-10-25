using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 10;
	public float lifetime = 1;
	float time;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = (transform.up * speed);
		time = Time.time + lifetime;
	}

	// Update is called once per frame
	void Update () {

		if (Time.time > time) {
			Destroy(gameObject);
		}
	}
}
