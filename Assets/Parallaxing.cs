using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {
	public Transform[] backgrounds;					// Array for storing back and foregrounds
	private float[] parallaxScales;					// Array for length of back and foregrounds
	public float smoothing = 1f;					// For making parallaxing smooth
	private Transform cam;							// For storing camera
	private Vector3 previousCamPos;					// For storing the previous camera position

	//called before start good for assigning references
	void Awake(){
		// Store camera transform
		cam = Camera.main.transform;
	}
	// Use this for initialization
	void Start () {
		// Store camera's position
		previousCamPos = cam.position;

		// Store backgrounds lengths
		parallaxScales = new float[backgrounds.Length];
		// Set parallax scales
		for (int i = 0; i< backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move backgrounds to create parallax effect
		for (int i = 0; i< backgrounds.Length; i++) {
			float parallax = (previousCamPos.x - cam.position.x)*parallaxScales[i];
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos,smoothing * Time.deltaTime);
		}

		// Store camera's position
		previousCamPos = cam.position;
	}
}