using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltFeature : MonoBehaviour {
	//attached to the main camera, this script describes the tilting aspect- up, down, left, right controled by the keys WASD 
	//might implement restrictions on whether system needs to be in a "Parked" mode to have a rigid body in order to use tilt feature
	//tilting is separate from rotation both cameras and robot. 
	//restricting degree of tilting 

	public GameObject Player;
	public Camera mainCamera;
	private Vector3 offset;

	public Rigidbody rb; 

	PlayerController PlayerControlInstance;
	ControlMethods ControlMethInstance;
	CameraController CameraControlInstance;

	private float horizontalSpeed = 2.0F; 
	private float turnSpeed = 50f;

	private float moveHorizontal; 
	private float moveVertical;
	public Vector3 movement;

	//private Vector3 initialPosition; //for tilt that is working
	//private Quaternion initialRotation; //for tilt that is working

	// Use this for initialization
	void Start () 
	{
		PlayerControlInstance = GetComponent<PlayerController> ();
		ControlMethInstance = GetComponent<ControlMethods> ();
		CameraControlInstance = GetComponent<CameraController> (); 

		offset = transform.position - Player.transform.position; //setting offset as distance between camera and ball

		mainCamera.enabled = true;


		//initialPosition = transform.position; //tilt that is working
		//initialRotation = transform.rotation; //tilt that is working
	}

	void FixedUpdate()
	{
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical"); 


		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical); //keeps object from rotating on x or z axes 
	}

	// Update is called once per frame
	void Update () {
		/**
		if (Input.GetKey (KeyCode.W)) {  //calls tilt method to tilt up, max of 30 degrees
			tilt (true);
		} else if (Input.GetKey (KeyCode.S)) {  //calls tilt method to tilt down, max of -30 degrees
			tilt (false);
		} else if (Input.GetKey (KeyCode.A)) { //calls pan method to pan to the right, max of 90 degrees
			pan (true);
		} else if (Input.GetKey (KeyCode.D)) { //calls pan method to pan to the left, max of -90 dregees
			pan (false);
		} 

		*/
		//TILT FEATURE FOR MAIN CAMERA VIEW

		//THESE DON'T WORK IN HERE
		//if (Input.GetKey (KeyCode.W)) 
		//{
			//camera tilts up towards ceiling
		//	transform.Rotate (Vector3.left,  turnSpeed * Time.deltaTime); 
			//transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);

		//}
		//if (Input.GetKey (KeyCode.S)) 
		//{
			//camera tilts down towards ground
		//	transform.Rotate(Vector3.left, -turnSpeed * Time.deltaTime); 
		//}
		//if (Input.GetKey (KeyCode.A)) 
		//{
			//camera tilts left/counterclockwise
			//using Space.World to keep rotation level with gound, even when system is also tilting up/down
		//	transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime, Space.World);
		///}
		//if (Input.GetKey (KeyCode.D)) 
		//{
		//	//camera tilts right/clockwise
			//using Space.World to keep rotation level with ground, even when system is also tilting up/down
		//	transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.World);
		//}
	}

	void LateUpdate()
	{
		transform.position = Player.transform.position + offset; //adds the offset to the player at every frame
		//so, as player moves, camera moves at end of that frame
	}

	/**
	void tilt (bool tiltUp){
		float angle = transform.eulerAngles.x;

		if (tiltUp){
			angle += rotationSpeed;
			angle = Mathf.Clamp(angle, -30f, 30f);  //sets max and min degrees of rotation. not working right now
		} else {
			angle -= rotationSpeed;
			angle = Mathf.Clamp(angle, -30f, 30f);  //sets max and min degrees of rotation. not working right now
		}
		//transform.eulerAngles = new Vector3 (transform.eulerAngles.y, angle, transform.eulerAngles.z);
	}

	void pan (bool panRight){
		float angle = transform.eulerAngles.y;
		if(panRight){
			//if(rb.IsSleeping() == true){ //supposed to check if the player is moving but doesn't work. 
			angle += rotationSpeed;
			angle = Mathf.Clamp(angle, -90f, 90f); //sets max and min dregees of rotation. not working right now
			//}
		} else{
			//if(rb.IsSleeping() == true){
			angle -= rotationSpeed;
			angle = Mathf.Clamp(angle, -90f, 90f); //sets max and min dregees of rotation. not working right now
			//}
		}

	}
	*/
}
