using UnityEngine;
using System.Collections;

public class endFlight : MonoBehaviour {
	float startTime;									// for the time the bullet is shot 
	float liveTime = 4;									// for the amount of time the bullet will live
	int colnum = 0;

	// Use this for initialization
	void Start () {
		//Get time that bullet was shot
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//if 3 seconds have passed since bullet was shot destroy bullet
		if (Time.time > startTime + liveTime) {
			Destroy (gameObject);		
		}
	}
	
	//what happens when bullet touches something
	void OnTriggerEnter2D(Collider2D col) {
		//if bullet touches a collider.
		audio.Play ();
		//if bullet hits a laser make changes to it depending on the color of the laser
		if (col.gameObject.name == "Laser")
		{
			//get the color of the laser
			colnum = col.gameObject.GetComponent<manageLaser> ().colnum;
			//if red
			if (colnum == 1) {
				//reverse bullet direction
				gameObject.rigidbody2D.velocity = gameObject.rigidbody2D.velocity * -1;
			}
			//if yellow
			else if (colnum == 2) {
				//change bullet color
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 1f);
			}
		}
		else if (col.gameObject.name == "LoneLaser")
		{
			//get color of the laser
			colnum = col.gameObject.GetComponent<manageLoneLaser> ().colnum;
			//if red
			if (colnum == 1) {
				//reverse bullet direction
				gameObject.rigidbody2D.velocity = gameObject.rigidbody2D.velocity * -1;
			}
			//if yellow
			else if (colnum == 2) {
				//change bullet color
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 1f);
			}
		}
		//destroy bullet if not laser or enemy
		else if (col.gameObject.tag != "Laser" && col.gameObject.tag != "Enemy")
		{
			Destroy (this.gameObject);
		}
	}
}
