using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMethods : MonoBehaviour {
	//this class describes the movement behaviors of user's driving abilities for the robot

	public GameObject Player;

	//cameras for toggling option
	public Camera mainCamera;
	public Camera downCamera;
	public Camera downCamera2; 
	public Camera mapCamera;

	PlayerController PlayerControlInstance; //instance of PlayerController class
	SystemController SystemControlInstance; //instance of SystemController class

	public Rigidbody rb;

	private float horizontalSpeed = 2.0F; 
	private float turnSpeed = 50f;

	private float moveHorizontal; 
	private float moveVertical;
	public Vector3 movement;
	private float speed;
	private float decreasedSpeed; //obstacle avoidance slows down system's speed

	private float rotationZ; //testing for resetting after tilt feature 
	private float mainCamZ; //testing for resetting after tilt feature

	// Use this for initialization
	void Start () {
		PlayerControlInstance = GetComponent<PlayerController> ();
		rb = PlayerControlInstance.rb;
		speed = 4.0f; //adjustable
		decreasedSpeed = 1.0f;
		mainCamZ = mainCamera.transform.position.z; 
		mainCamZ = 0.0f;
	
		//sets just main camera and centered down camera as on when user starts simulation
		//should be able to toggle options on/off during play- currently not working
		mainCamera.enabled = true;
		downCamera.enabled = false;
		downCamera2.enabled = true;
		mapCamera.enabled = true;
	}

	void FixedUpdate()
	{
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical"); 

		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical); //keeps object from rotating on x or z axes 
	}

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (Vector3.left * 0); //corrects shift of camera at start of program 

		//rotation is never restricted for obstacle detection/avoidance- how player will avoid obstacles

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) 
		{
			//user is moving/rotation system and cameras associated **NOT TILTING SYSTEM**
			//need to reset x and z axes rotation and restrict the y axis to .8
			//Player's position restrictions- just on Y, setting x and z same as before reset

			//none of this is working

			mainCamZ = 0;
			float mainCamX = mainCamera.transform.position.x; 
			mainCamX = 0;
			float mainCamY = mainCamera.transform.position.y;
			Vector3 mainCamPos = new Vector3 (mainCamX, mainCamY, mainCamZ);
			mainCamera.transform.position = mainCamPos;
			float mainCamRotateZ = mainCamera.transform.rotation.z; 
			mainCamRotateZ = 0; 

			//Player's rotation restrictions- just on Z, setting x and y same as before reset
			//this is not working
			float temp3 = Player.transform.rotation.y;
			float temp4 = Player.transform.rotation.z; 
			float temp5 = Player.transform.rotation.x;
			temp5 = 0;
			temp4 = 0;

		}
		if (Input.GetKey (KeyCode.R)) 
		{
			//resetting position after using tilt feature- for testing purposes
			//this is not working
			float resetXpos = Player.transform.position.x;
			float resetYpos = Player.transform.position.y;
			float resetZpos = Player.transform.position.z; 

			resetYpos = 0.8f; 
			Vector3 resetPos = new Vector3 (resetXpos, resetYpos, resetZpos);
		//	Player.transform.position = resetPos;

			Quaternion originalRotation = transform.rotation;

			transform.rotation = originalRotation * Quaternion.AngleAxis (0, Vector3.up);

		}
		//end of things not working

		//way user moves the system- arrow keys
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			//camera rotates to the left
			transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);
		} 	
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			//camera rotates to the right
			transform.Rotate(Vector3.left, 0f); //trying to reset on z axis, not responding
			transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		}

		//speed increase- shift button
		if(Input.GetKey (KeyCode.RightShift))
		{
			rb.velocity = new Vector3 (10, 0, 0); //this needs to be fixed
		}

		//not working
		if (Input.GetKey (KeyCode.M) && mapCamera.enabled == false) 
		{
			mapCamera.enabled = !mapCamera.enabled;
		}

		if (Input.GetKey (KeyCode.M) && mapCamera.isActiveAndEnabled == true)
		{
			//this is not working

			//mapCamera.enabled == false;
			//mapCamera.SetActive(true);
			//mapCamera.isActiveAndEnabled = false; 
		}
			
		//THESE WORK IN HERE!
		if (Input.GetKey (KeyCode.W)) 
		{
			//restrict degree of tilt possible 
			//for testing purposes- 45 degrees up
			//camera tilts up towards ceiling
			Debug.Log(moveVertical);
			if (moveVertical < 0.34f) 
			{
				//transform.Rotate (Vector3.left, turnSpeed * Time.deltaTime);
				transform.Rotate(-1, 0,0);
			}
		}

		if (Input.GetKey (KeyCode.S)) 
		{
			//restricts degree of tilt possible
			//for testing purposes- want 45 degrees down
			//camera tilts down towards ground
			Debug.Log(moveVertical);
			if (moveVertical > -0.34f) 
			{
				transform.Rotate (1, 0, 0);
			}
		//	transform.Rotate(Vector3.left, -turnSpeed * Time.deltaTime); 
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			//restricts degree of tilt possible
			//for testing purposes- want 45 degrees to left
			//camera tilts left/counterclockwise
			Debug.Log(moveHorizontal);
			if (moveHorizontal > -0.34f){
				transform.Rotate (0, -1, 0);
			}
				
		}

		if (Input.GetKey (KeyCode.D)) 
		{
			//restricts degree of tilt possible
			//for testing purposes- want 45 degrees to right
			//camera tilts right/clockwise
			Debug.Log(moveHorizontal);
			if (moveHorizontal < 0.34f){
				transform.Rotate (0, 1, 0);
			}
		}

		//keeps system from roaming/rolling when idle
		if(!Input.anyKeyDown)
		{
			//no buttons are pressed
			rb.velocity = new Vector3 (0,0,0); //no velocity
			//no rotation
			rb.angularVelocity = Vector3.zero;
		}
	}
}
