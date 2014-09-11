using UnityEngine;
using System.Collections;

public class manageLaser : MonoBehaviour {
	Transform ctrlpanel;				//For storing controlPanel gameobject
	public int colnum;					//For storing color
	public float center;				//For storing y position between two elebox's
	public float scaler;				//For storing how much laser needs to be scaled by
	Vector3 newpos;						//For new position of laser
	Vector3 newsca;						//For new scale of laser
	Vector2 newboxsca;					//For new scale for collider
	SpriteRenderer sr;					//For storing gameobjects spriterenderer
	BoxCollider2D laserbox;				//For storing lasers box collider

	// Use this for initialization
	void Start () {
		//Store controlPanel child gameobject
		ctrlpanel = transform.parent.parent.FindChild ("controlPanel");
		//Center and set length of the laser
		laserLen ();
	}
	
	// Update is called once per frame
	void Update () {
		//change color if in menu mode
		if (ctrlpanel.GetComponent<ctrlLaser>().inMenu == true)
		{
			changeColor();
		}
	}

	// Make laser go from ele1 to ele2, make sure to change box collider as well
	void laserLen()
	{
		//scales laser
		sr = GetComponent<SpriteRenderer>();
		newsca = transform.localScale;
		newsca.y = 1;	//****
		scaler = (transform.parent.position.y - transform.parent.parent.position.y) / sr.bounds.size.y;
		newsca.y = newsca.y*scaler;
		transform.localScale = newsca;
		//change scale of boxcollider
		laserbox = collider2D as BoxCollider2D;
		newboxsca = laserbox.size;
		scaler = scaler - 0.2f;
		newboxsca.y = 7;//*****
		newboxsca.y = laserbox.size.y * scaler;
		laserbox.size = newboxsca;
		//centers laser
		//center = (transform.parent.position.y - transform.parent.parent.position.y) / 2;
		//newpos = transform.position;
		//newpos.y = center;
		//transform.position = newpos;
	}

	//change color based on controlpanel value
	void changeColor()
	{
		//Get color from ctrlLaser script and based on it change color
		colnum = ctrlpanel.GetComponent<ctrlLaser> ().colnum;
		if (colnum == 0)
		{
			transform.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
		}
		else if(colnum == 1)
		{
			transform.GetComponent<SpriteRenderer>().color = new Color(1f, 0.92f, 0.016f, 1f);
		}
		else
		{
			transform.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
		}
	}
}
