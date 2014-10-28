using UnityEngine;
using System.Collections;

public class OnDeathEffekt : MonoBehaviour {

	public GameObject onDeathEffect;
	bool isQuitting = false;


	void OnApplicationQuit()
	{
		isQuitting = true;
	}

	public void OnDestroy() {

		if (!isQuitting) {
			Instantiate (onDeathEffect, transform.position, transform.rotation);
		}
	}

}
