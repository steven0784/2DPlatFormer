using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;							// Firerate of weapon
	public float damage = 10;							// Damage of weapon
	Vector3 fwd;										// Direction to shoot
	public LayerMask whatToHit;							// Hit only 
	public Transform BulletTrailPrefab;					// Transform for bullet trail
	public float effectSpawnRate = 1;					// Spawn rate of effect
	// Use this for initialization
	float timeToFire = 0;								// Duration to fire
	float timeToSpawnEffect = 0;						// Duration to spawn effect
	Transform firePoint;								// Transform for point to fire from

	void Awake(){
		// Get point to fire from
		firePoint = transform.FindChild ("FirePoint");
		// If fire point does not exist error
		if (firePoint == null) {
			Debug.LogError ("no fire point");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Get direction to fire too
		fwd = transform.TransformDirection (Vector3.right);
		// If fire rate = 0
		if (fireRate == 0) {
			// Shoot without any buffer	
			if (Input.GetButton("Fire1")) {
				Shoot ();
				fireRate = 1;
			}
		}
		// If firerate is not 0
		else
		{
			// Fire with buffer
			if(Input.GetButton("Fire1") && Time.time > timeToFire)
			{
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot(){
		// Get position of mouse
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		// Get fire point position
		Vector2 firePointPosition = new Vector2 (firePoint.position.x,firePoint.position.y);
		//create raycast in direction of firepoint to mouse position
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100,whatToHit);
		// If timeToSpawnEffect time has passed since last shot do effect
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
		// Draw a yellow debug line in direction of firepoint to mouse position
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100,Color.yellow);
		// If does not hit anything line is red
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
		}
	}
	void Effect(){
		//Create bullet
		Transform bPrefab = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
		//Shoot bullet
		bPrefab.transform.rigidbody2D.AddForce (fwd * 50, ForceMode2D.Impulse);
	}
}