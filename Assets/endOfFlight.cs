using UnityEngine;
using System.Collections;

public class endOfFlight : MonoBehaviour {

	float startTime;									// for the time the bullet is shot 
	float liveTime = 4;									// for the amount of time the bullet will live
	int damage = 10;
	
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
		//if bullet hits a laser
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlatformerCharacter2D>().hp -= damage;
			Debug.Log(col.gameObject.GetComponent<PlatformerCharacter2D>().hp);
			Destroy(gameObject);
		}
	}
}
