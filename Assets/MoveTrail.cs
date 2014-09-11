using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {


	public int moveSpeed = 1000;
	// Update is called once per frame
	void Update () {
		//Vector2 lol = new Vector2 (10, 12);
		//transform.rigidbody2D.velocity = lol;
		transform.Translate(transform.right * Time.deltaTime * moveSpeed);
		//transform.rigidbody2D.velocity = transform.forward*moveSpeed;

		//target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//target.z = transform.position.z;
		//transform.position = Vector3.MoveTowards (transform.position, target, moveSpeed * Time.deltaTime);
		Destroy (gameObject, 1);
	}
	void OnTriggerStay2D(Collider2D lols){
		if(lols.transform.tag == ("Laser")){
			moveSpeed = moveSpeed * -1;
		}
	}
}
