using UnityEngine;
using System.Collections;

public class armRotation : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
		// Difference between mouse position on screen relative to player position
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		// Get rotation needed for arm
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		// Rotate arm towards mouse
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
	}
}
