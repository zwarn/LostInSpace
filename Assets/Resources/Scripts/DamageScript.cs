using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageScript : MonoBehaviour {

	float life = 100;

	[System.Serializable]
	public class DamageAnimationEntry {
		public float Damage;
		public GameObject Effect;
	}
	
	public List<DamageAnimationEntry> damages;
	
	// Update is called once per frame
	void Update () {
		foreach (DamageAnimationEntry entry in damages) {
			if (life < entry.Damage && !entry.Effect.particleSystem.isPlaying) {
				entry.Effect.particleSystem.Play ();
			}
			
			if (life > entry.Damage && entry.Effect.particleSystem.isPlaying) {
				entry.Effect.particleSystem.Stop ();
			}
		}
	}

	public void setLife(float life) {
		this.life = life;
	}
}
