using UnityEngine;
using System.Collections;

public class OnDeathEffekt : MonoBehaviour {

	public GameObject onDeathEffect;

	public void OnDestroy() {

		Instantiate (onDeathEffect, transform.position, transform.rotation);

	}

}
