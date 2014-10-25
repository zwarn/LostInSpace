using UnityEngine;
using System.Collections;

public class WorldGeneration : MonoBehaviour {

	public GameObject turret;
	public GameObject asteroid;
	public GameObject spaceship;
	public float worldSize = 100;
	public float objectNumber = 1200;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < objectNumber; i++) {
			Vector2 position = Random.insideUnitCircle * worldSize;
			if (Random.Range(0,100) > 80) {
				Instantiate (turret, new Vector3(position.x, position.y, 0),  Quaternion.identity);
			} else {
				Instantiate (asteroid, new Vector3(position.x, position.y, 0),  Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
