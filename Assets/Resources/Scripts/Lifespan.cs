using UnityEngine;
using System.Collections;

public class Lifespan : MonoBehaviour {

	public float timeToLife;
	float timeOfDeath;

	// Use this for initialization
	void Start () {
		timeOfDeath = Time.time + timeToLife;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeOfDeath) {
			Destroy(gameObject);
		}
	}
}
