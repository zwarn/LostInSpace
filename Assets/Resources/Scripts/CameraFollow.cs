using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject toFollow;
	public GameObject backGround;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y, -10);
		transform.rotation = toFollow.transform.rotation;

		backGround.transform.position = toFollow.transform.position;
	}
}
