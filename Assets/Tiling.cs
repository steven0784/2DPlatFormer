using UnityEngine;
using System.Collections;
[RequireComponent (typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour {
	public int offsetX = 2;					// For storing offset on x axis
	// For checking if right or left buddy needs to be instantiated
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;
	public bool reverseScale = false;		// used for when object is not tileable
	private float spriteWidth = 0f;			// For storing width of sprite
	private Camera cam;						// For storing transform of camera
	private Transform myTransform;			// For storing transform of object
	// Use this for initialization
	void Awake(){
		// Store camera's transform
		cam = Camera.main;
		// Store transform
		myTransform = transform;
	}

	void Start () {
		// Get sprite of object
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		// Get sprites width
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		// If it still needs a buddy
		if (hasALeftBuddy == false || hasARightBuddy == false) {
			// Get half the width of the cameras screen view
			float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
			//float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

			// Checking if we can see the edge of the element and then calling MakeNewBuddy if we can
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false) {
				MakeNewBuddy (1);
				hasARightBuddy = true;
			} else if (cam.transform.position.x <= edgeVisiblePositionRight + offsetX && hasALeftBuddy == false) {
				MakeNewBuddy (-1);
				hasALeftBuddy = true;
			}
		}
	}

	// Creates for required side
	void MakeNewBuddy (int rightOrLeft){
		// Calculating the new position for our new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y,myTransform.position.z);
		// Instantiate and store new buddy
		Transform newBuddy = (Transform) Instantiate (myTransform, newPosition, myTransform.rotation);

		// If not tileable reverse x scale
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		// Make newbuddy child of transform's parent
		newBuddy.parent = myTransform.parent;

		// If rightorleft > 0 newbuddy is a left buddy else is a right buddy
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling> ().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling> ().hasARightBuddy = true;
		}
	}
}
