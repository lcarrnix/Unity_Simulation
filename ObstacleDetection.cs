using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObstacleDetection : MonoBehaviour {

	public Canvas warningMenu;
	public Toggle warningToggle;

	PlayerController PlayerControlInstance; //instance of PlayerController class
	ControlMethods ControlMethInstance; //instance of ControlMethods class

	public Rigidbody rb; //for collisions and parking system in place

	public GameObject Player;
	public GameObject trigger;
	private GameObject theHitObject; //for obstacle detection

	public GameObject[] deskObstacles; //for clearing obstacle warnings- desks
	public GameObject[] wallObstacles; //for clearing obstacle warnings- walls
	public GameObject[] pathObjects; 

	private RaycastHit hit;

	public float speed;
	public float detectionDistance; //for obstacle detection
	private float decreasedSpeed; //for obstacle avoidance

	//direction vectors
	private Vector3 forward;
	private Vector3 back; 
	private Vector3 left;
	private Vector3 right;

	private Vector3 diagonal1;
	private Vector3 diagonal2;
	private Vector3 diagonal3;
	private Vector3 diagonal4;

	public Slider speedSlider;

	public void changeSpeedSlider(float value) {
		speed = value;
	}

	// Use this for initialization
	void Start ()
	{
		warningMenu = warningMenu.GetComponent<Canvas> ();
		warningMenu.enabled = false;
		warningToggle = warningToggle.GetComponent<Toggle> ();
		warningToggle.enabled = false;

		trigger = GameObject.Find ("Trigger");
		speedSlider = GameObject.Find ("Speed Slider").GetComponent<Slider> ();

		PlayerControlInstance = GetComponent<PlayerController> ();
		ControlMethInstance = GetComponent<ControlMethods> ();

		rb = PlayerControlInstance.rb;

		decreasedSpeed = 1.0f;
		speed = speedSlider.value; //current value of the speed slider (default 1)

		//direction vectors from space.world
		forward = transform.TransformDirection (Vector3.forward);
		back = transform.TransformDirection (Vector3.back);
		left = transform.TransformDirection (Vector3.left);
		right = transform.TransformDirection (Vector3.right);

		diagonal1 = transform.TransformDirection (1, 0, 1); //first quadrant diagonal for obstacle detection
		diagonal2 = transform.TransformDirection(1, 0, -1); //2nd quadrant
		diagonal3 = transform.TransformDirection (-1, 0, -1); //3rd quadrant
		diagonal4 = transform.TransformDirection (-1, 0, 1); //4th quadrant
	}

	void FixedUpdate() //before physics
	{
		Vector3 movement = ControlMethInstance.movement;

		//updating vectors every frame
		forward = transform.TransformDirection (Vector3.forward);
		back = transform.TransformDirection (Vector3.back);
		left = transform.TransformDirection (Vector3.left);
		right = transform.TransformDirection (Vector3.right);

		diagonal1 = transform.TransformDirection (1, 0, 1); //first quadrant diagonal for obstacle detection
		diagonal2 = transform.TransformDirection(1, 0, -1); //2nd quadrant
		diagonal3 = transform.TransformDirection (-1, 0, -1); //3rd quadrant
		diagonal4 = transform.TransformDirection (-1, 0, 1); //4th quadrant

		rb.AddForce (movement * speed); 

		//just warning
		obstacleDetection(ref speed);
			
		checkObstacleWarning (); //checks if obstacle were avoided every frame before any physics takes place
	}

	//main function for detecting obstacles
	//will call functions to display colors corresponding to obstacles and decrease speed for obstacle avoidance
	void obstacleDetection(ref float speed) {
		//Debug.Log ("im in obstacleDetection and the speed is " + speed);
		if ( (Physics.Raycast (transform.position, forward, detectionDistance * 2f)) || (Physics.Raycast (transform.position, back, detectionDistance * 2f)) ||
			(Physics.Raycast (transform.position, left, detectionDistance * 1.7f)) || (Physics.Raycast (transform.position, right, detectionDistance * 1.7f)) ||
			(Physics.Raycast (transform.position, diagonal1, detectionDistance * 1.5f)) || (Physics.Raycast (transform.position, diagonal2, detectionDistance * 1.5f)) ||
			(Physics.Raycast (transform.position, diagonal3, detectionDistance * 1.5f)) || (Physics.Raycast (transform.position, diagonal4, detectionDistance * 1.5f)) ) {

			//checking which direction obstacle is in relative to player
			//front, front-right diagonal, front-left diagonal
			if(Physics.Raycast (transform.position, forward, out hit, detectionDistance * 2) || Physics.Raycast (transform.position, diagonal1, out hit, detectionDistance * 1.5f) || Physics.Raycast (transform.position, diagonal4, out hit, detectionDistance * 1.5f)){
				theHitObject = hit.collider.gameObject; //sets to obstacle at hand
				//pathObjects = GameObject.FindGameObjectsWithTag("path");
				if (theHitObject.CompareTag("furniture") || theHitObject.CompareTag("wall")){
					//there is an obstacle near user, so slowing down user's speed
					speed = decreasedSpeed;
					redObstacleWarning (theHitObject); //changes color to red
					if (warningToggle.isOn) {
						warningMenu.enabled = true;
						warningToggle.enabled = true;
					}
				}
			}
			if(Physics.Raycast (transform.position, back, out hit, detectionDistance * 2) || Physics.Raycast (transform.position, left, out hit, detectionDistance * 1.7f) || Physics.Raycast (transform.position, right, out hit, detectionDistance * 1.7f)
				|| Physics.Raycast (transform.position, diagonal2, out hit, detectionDistance * 1.5f) || Physics.Raycast (transform.position, diagonal3, out hit, detectionDistance * 1.5f)){
				theHitObject = hit.collider.gameObject;
				//pathObjects = GameObject.FindGameObjectsWithTag("path");
				if (theHitObject.CompareTag("furniture") || theHitObject.CompareTag("wall")){
					speed = decreasedSpeed;
					yellowObstacleWarning (theHitObject); //changes color to yellow
					if (warningToggle.isOn) {
						warningMenu.enabled = true;
						warningToggle.enabled = true;
					}
				}
			}

			checkObstacleWarning (); //if all obstacles are far enough away, will turn white again
		}
	}

	//if an obstacle has been detected as a warning in front/back of player
	GameObject redObstacleWarning(GameObject objectWarning)
	{
		//visual warnings take place here- red to front and back obstacles (most severe) 
		objectWarning.GetComponent<Renderer>().material.color = new Color(1,0,0,1); //red
		return objectWarning;
	}

	//if an obstacle has been detected as a warning on left/right of player
	GameObject yellowObstacleWarning(GameObject objectWarning)
	{
		//visual warnings take place here- yellow to left and right obstacles (less severe)
		//converting doubles to floats for color
		float gValue = (float) 0.92;
		float bValue = (float) 0.016;
		objectWarning.GetComponent<Renderer> ().material.color = new Color (1, gValue, bValue, 1); //yellow- from docs.unity3d.com
		return objectWarning;
	}

	void checkObstacleWarning() //overwriting
	{
		//checking if all desks are far away from user to not be a warning anymore- checks front, back, left, right, and diagonals
		if (!Physics.Raycast (transform.position, forward, out hit, detectionDistance * 2f) && !Physics.Raycast(transform.position, back, out hit, detectionDistance * 2f) 
			&& !Physics.Raycast(transform.position, left, out hit, detectionDistance * 1.7f) && !Physics.Raycast(transform.position, right, out hit, detectionDistance * 1.7f)
			&& !Physics.Raycast(transform.position, diagonal1, out hit, detectionDistance * 1.5f) && !Physics.Raycast(transform.position, diagonal2, out hit, detectionDistance * 1.5f)
			&& !Physics.Raycast(transform.position, diagonal3, out hit, detectionDistance * 1.5f) && !Physics.Raycast(transform.position, diagonal4, out hit, detectionDistance * 1.5f)) 
		{
			//restoring speed to normal 
			speed = speedSlider.value;

			//Testing purposes: deskObstacles = GameObject.FindGameObjectsWithTag ("desk");
			deskObstacles = GameObject.FindGameObjectsWithTag ("furniture");
			foreach (GameObject deskObstacle in deskObstacles) //turns all desks back to white
			{
				deskObstacle.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, 1); //white
			}

			wallObstacles = GameObject.FindGameObjectsWithTag ("wall");
			foreach(GameObject wallObstacle in wallObstacles) //turns all walls back to white
			{
				wallObstacle.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, 1); //white
			}
		}
	}

	void checkObstacleWarning(Vector3 direction, GameObject objectWarning)
	{
		//check if object is far enough away
		if (!Physics.Raycast (transform.position, direction, out hit, detectionDistance * 2)) 
		{
			clearObstacleWarning (theHitObject);
		}
	}

	GameObject clearObstacleWarning(GameObject objectWarning)
	{
		//object is far enough away from player after a warning
		objectWarning.GetComponent<Renderer>().material.color = new Color(1,1,1,1); //white
		speed = speedSlider.value; //restoring max speed
		return objectWarning;
	}

	//these tags will be changed when the environment is changed to a living room setting
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "desk" || col.gameObject.tag == "wall" || col.gameObject.tag == "furniture")
		{
			rb.AddForce(0,0,0, ForceMode.VelocityChange);  //changes speed to zero
		}
	}

	//checks if it's all good to move forward and moves forward if it is
	void moveForward (){
		//there is nothing in front
		if (Input.GetKey (KeyCode.UpArrow) && !Physics.Raycast (transform.position, forward, detectionDistance) && !Physics.Raycast (transform.position, diagonal1, detectionDistance) && !Physics.Raycast (transform.position, diagonal4, detectionDistance)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
		//can still move forward if object in front is dock
		else if(Input.GetKey (KeyCode.UpArrow) && (Physics.Raycast (transform.position, forward, out hit, detectionDistance) || Physics.Raycast (transform.position, diagonal1, out hit, detectionDistance) || Physics.Raycast (transform.position, diagonal4, out hit, detectionDistance))){
			if(hit.collider.gameObject == trigger){
				//Debug.Log ("trigger is HIT OBJECT");
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}
		}
	}

	//checks if it's all good to move backward and moves backward if it is
	void moveBackward(){
		//there is nothing in back
		if (Input.GetKey (KeyCode.DownArrow) && !Physics.Raycast (transform.position, back, detectionDistance) && !Physics.Raycast (transform.position, diagonal2, detectionDistance) && !Physics.Raycast (transform.position, diagonal3, detectionDistance)) {
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);
		}
		//can still move backward if object in back is dock
		else if(Input.GetKey (KeyCode.DownArrow) && (Physics.Raycast (transform.position, back, out hit, detectionDistance) || Physics.Raycast (transform.position, diagonal2, out hit, detectionDistance) || Physics.Raycast (transform.position, diagonal3, out hit, detectionDistance)) ){
			if (hit.collider.gameObject == trigger) {
				transform.Translate (-Vector3.forward * speed * Time.deltaTime);
			}
		}
	}

	// Update is called once per frame
	void Update(){
		moveForward ();
		moveBackward ();
		//Debug.Log ("Current speed: " + speed);
		checkObstacleWarning (); //idk about this
	}
}