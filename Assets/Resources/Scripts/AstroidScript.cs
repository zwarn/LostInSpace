using UnityEngine;
using System.Collections;

public class AstroidScript : MonoBehaviour {

	float rotation;
	float size;

	// Use this for initialization
	void Start () {
		rotation = Random.Range (0, 4)-2;
		size = Random.Range (0.5f, 2);
		transform.localScale = new Vector3(size,size,size);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,0,rotation*1/size));
	}

	void OnTriggerEnter2D (Collider2D obj) {
		var name = obj.gameObject.name;

		if (name == "bullet(Clone)") {
			Destroy(gameObject);
			Destroy(obj.gameObject);
		}

		if (name == "spaceship") {
			Destroy(obj.gameObject);
		}
	}
}
