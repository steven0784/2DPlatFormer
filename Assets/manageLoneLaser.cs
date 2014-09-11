using UnityEngine;
using System.Collections;

public class manageLoneLaser : MonoBehaviour {

	//color of laser
	public int colnum = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//change laser color
		changeColor ();
	}

	//change color based on colnum value
	void changeColor()
	{
		//Change laser color based on colnumm
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
