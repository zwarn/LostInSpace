using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public GameObject bullet;
	GameObject owner;

	public void fire() {
		Debug.Log ("fire!");
		Instantiate(bullet, transform.position, transform.rotation );
	}

	public void register(GameObject owner) {
		this.owner = owner;
	}
}
