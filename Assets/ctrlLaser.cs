using UnityEngine;
using System.Collections;

public class ctrlLaser : MonoBehaviour {
	//stores if player is in menu
	public bool inMenu = false;
	//stores the color player is on
	public int colnum;
	//maximum number of laser colors
	public int maxcolnum = 3;
	//transform to store color lights
	Transform colred;
	Transform colyel;
	Transform colgre;

	// Use this for initialization
	void Start () {
		//gets gameobjects for each color light
		colred = transform.FindChild("redLED");
		colyel = transform.FindChild("yellowLED");
		colgre = transform.FindChild("greenLED");
	}
	
	// Update is called once per frame
	void Update () {
		//wake up parents rigidbody so movement doesn't need to occur for collision detection to work
		transform.parent.rigidbody2D.WakeUp ();
		//if in menu
		if (inMenu == true) 
		{
			//if right movement key is pressed move color light right
			if (Input.GetKeyDown(KeyCode.D))
			{
				colnum += 1;
				//if pass max light num, go back to the beginning
				if (colnum > 2)
				{
					colnum = 0;
				}
			}
			//if left movement key is pressed move color light left
			else if (Input.GetKeyDown(KeyCode.A))
			{
				colnum -= 1;
				//if pass min light num, go to end
				if (colnum < 0)
				{
					colnum = 3;
				}
			}
			//light up appropriate light
			changeControlPanelColor ();
		}
	}

	//based on color light up proper one and turnoff incorrect ones
	void changeControlPanelColor()
	{
		//change to red
		if (colnum == 0)
		{
			colred.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
			colyel.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			colgre.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		}
		//change to yellow
		else if (colnum == 1)
		{
			colred.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			colyel.GetComponent<SpriteRenderer>().color = new Color(1f, 0.92f, 0.016f, 1f);
			colgre.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);		
		}
		//change to green
		else
		{
			colred.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			colyel.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			colgre.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
		}
	}
	
	void OnTriggerStay2D(Collider2D col) 
	{
		//if player touches control panel
		if (col.gameObject.tag == "Player" && inMenu == false) 
		{
			//if not in menu mode
			if (inMenu == false)
			{
				//if shift(interact key) is pressed turn on menu mode
				if (Input.GetKeyDown(KeyCode.LeftShift))
				{
					inMenu = true;
					col.gameObject.GetComponent<Platformer2DUserControl>().inMenu = inMenu;
				}
			}
		}
		//if shift key is lifted exit menu mode
		if (Input.GetKeyUp(KeyCode.LeftShift) )
		{
			inMenu = false;
			col.gameObject.GetComponent<Platformer2DUserControl>().inMenu = inMenu;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//if player is no longer touching control panel, then inMenu = false
		if (col.gameObject.tag == "Player")
		{
			inMenu = false;
			col.gameObject.GetComponent<Platformer2DUserControl>().inMenu = inMenu;
		}
	}
}
