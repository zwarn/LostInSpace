using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 10;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = (transform.up * speed);
	}

}
