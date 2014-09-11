using UnityEngine;
using System.Collections;

public class DestoryScript : MonoBehaviour {
	public float hp = 1;				//hp of object

	void OnTriggerEnter2D(Collider2D Colls)
	{
		// If gets hit by a bullet trail, destroy bullet and damage object
		if (Colls.tag == "BulletTrail") {
			Destroy (Colls.gameObject);
			hit (1);
		}

	}

	// For when player gets hit
	void hit(float damage){
		// Decrease hp by damage
		hp -= damage;
		// If hp < 0 destroy gameobject
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
	}
}
