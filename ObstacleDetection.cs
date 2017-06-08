using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour {

	public GameObject Player;
	PlayerController PlayerControlInstance; //instance of PlayerController class
	ControlMethods ControlMethInstance; //instance of ControlMethods class

	public Rigidbody rb; //for collisions and parking system in place
	private GameObject theHitObject; //for obstacle detection
	public GameObject[] deskObstacles; //for clearing obstacle warnings- desks
	public GameObject[] wallObstacles; //for clearing obstacle warnings- walls
	public GameObject[] pathObjects; 
	private RaycastHit hit;
	private float speed;
	public float detectionDistance; //for obstacle detection
	private float decreasedSpeed; //for obstacle avoidance
	private float increasedSpeed;

	//direction vectors
	private Vector3 forward;
	private Vector3 back; 
	private Vector3 left;
	private Vector3 right;

	private Vector3 diagonal1;
	private Vector3 diagonal2;
	private Vector3 diagonal3;
	private Vector3 diagonal4;

	// Use this for initialization
	void Start () 
	{
		PlayerControlInstance = GetComponent<PlayerController> ();
		ControlMethInstance = GetComponent<ControlMethods> ();

		rb = PlayerControlInstance.rb;

		increasedSpeed = 5.0f;
		decreasedSpeed = 1.0f;
		speed = increasedSpeed;

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
	void obstacleDetection(ref float speed)
	{
		//Debug.Log ("im in obstacleDetection and the speed is " + speed);
		if ((Physics.Raycast (transform.position, forward, detectionDistance * 2f)) || (Physics.Raycast (transform.position, back, detectionDistance * 2f)) ||
		    (Physics.Raycast (transform.position, left, detectionDistance * 1.7f)) || (Physics.Raycast (transform.position, right, detectionDistance * 1.7f)) ||
		    (Physics.Raycast (transform.position, diagonal1, detectionDistance * 1.5f)) || (Physics.Raycast (transform.position, diagonal2, detectionDistance * 1.5f)) ||
		    (Physics.Raycast (transform.position, diagonal3, detectionDistance * 1.5f)) || (Physics.Raycast (transform.position, diagonal4, detectionDistance * 1.5f))) 
		{
			//there is an obstacle near user 
			//slowing down user's speed
			speed = decreasedSpeed;

			//checking which direction obstacle is in relative to player
			//front, front-right diagonal, front-left diagonal
			if(Physics.Raycast (transform.position, forward, out hit, detectionDistance * 2) || Physics.Raycast (transform.position, diagonal1, out hit, detectionDistance * 1.5f) || Physics.Raycast (transform.position, diagonal4, out hit, detectionDistance * 1.5f)){
				theHitObject = hit.collider.gameObject; //sets to obstacle at hand
				//pathObjects = GameObject.FindGameObjectsWithTag("path");
				if (theHitObject.CompareTag("furniture") || theHitObject.CompareTag("wall")){
					redObstacleWarning (theHitObject); //changes color to red
				}
			}
			if(Physics.Raycast (transform.position, back, out hit, detectionDistance * 2) || Physics.Raycast (transform.position, left, out hit, detectionDistance * 1.7f) || Physics.Raycast (transform.position, right, out hit, detectionDistance * 1.7f)
				|| Physics.Raycast (transform.position, diagonal2, out hit, detectionDistance * 1.5f) || Physics.Raycast (transform.position, diagonal3, out hit, detectionDistance * 1.5f)){
				theHitObject = hit.collider.gameObject;
				//pathObjects = GameObject.FindGameObjectsWithTag("path");
				if (theHitObject.CompareTag("furniture") || theHitObject.CompareTag("wall")){
						yellowObstacleWarning (theHitObject); //changes color to yellow
					}
			}
		}
		checkObstacleWarning (); //if all obstacles are far enough away, will turn white again
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
		float bValue = (float)0.016;
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
			speed = increasedSpeed;

			//Testing purposes: deskObstacles = GameObject.FindGameObjectsWithTag ("desk");
			//FIXME: Check out why we have 2 arrays (one for walls and another for desks). Can we just use one?
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

			//Debug.Log ("Obstacle avoided!");
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
		speed = increasedSpeed; //restoring max speed
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

	/*****************************************************************************************************************************************************************************************************************/
	//FIXME: The purpose of this method is to cut down on the long code in in Update() for forward, back, and <new> left, right, diagonals! :)
	//This only gets  called when there is nothing forward
	void moveForward () {
		//FIXME: figure out why user still lmoves forward even though there is an obstacle for certain angles
		if (!Physics.Raycast (transform.position, left, detectionDistance) && !Physics.Raycast (transform.position, right, detectionDistance))
		{
			//need to check 1st and 4th quadrant diagonals; player isn't too close to obstacle in front
			//FIXME: Why 4th? Why not 2nd? Note- doesn't really make a difference when tested on 06/07 at 11 am
			if (Input.GetKey (KeyCode.UpArrow) && !Physics.Raycast (transform.position, diagonal1, detectionDistance) && !Physics.Raycast (transform.position, diagonal4, detectionDistance)) 
			{
				Debug.Log ("All clear to move forward");
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}
			else 
			{
				//FIXME: We need to check if we are actually free to move backwards before moving
				if (Input.GetKey (KeyCode.DownArrow) && !Physics.Raycast (transform.position, back, detectionDistance))
				{
					transform.Translate (-Vector3.forward * speed * Time.deltaTime); //move backwards away from obstacle
				}
			}
		}//end of left, right, and back
		else 
		{ //obstacle on left or right, but not forward
			if (Physics.Raycast (transform.position, right, detectionDistance)) 
			{
				if (Input.GetKey (KeyCode.DownArrow)) 
				{
					transform.Translate (-Vector3.forward * speed * Time.deltaTime);
				}

				if (!Physics.Raycast (transform.position, diagonal1, detectionDistance)) 
				{ //not near obstacle on diagonal, so can move forward

					if (Input.GetKey (KeyCode.UpArrow)) 
					{
						transform.Translate (Vector3.forward * speed * Time.deltaTime);
					}
				}
			}
			if (Physics.Raycast (transform.position, left, detectionDistance)) 
			{
				if (Input.GetKey (KeyCode.DownArrow)) 
				{
					transform.Translate (-Vector3.forward * speed * Time.deltaTime);
				}

				if(!Physics.Raycast(transform.position, diagonal4, detectionDistance))
				{
					if (Input.GetKey (KeyCode.UpArrow)) 
					{
						transform.Translate (Vector3.forward * speed * Time.deltaTime); 
					}
				} 
			}//end of if on left side
		}//end of else statement- left/right obstacles
	}

	void obstacleInFront(){
		//for testing accuracy
		if(Physics.Raycast(transform.position, diagonal1, detectionDistance))
		{
			//Debug.Log("OBSTACLE IN FRONT RIGHT DIAGONAL!");
		}
		if(Physics.Raycast(transform.position, diagonal4, detectionDistance))
		{
			//Debug.Log("OBSTACLE IN FRONT LEFT DIAGONAL!");
		} //end testing for accuracy
		if (Physics.Raycast (transform.position, right, detectionDistance)) 
		{
			//Debug.Log ("OBSTACLE TO THE FRONT RIGHT");
		}
		if (Physics.Raycast (transform.position, left, detectionDistance)) 
		{
			//Debug.Log ("OBSTACLE TO THE FRONT LEFT!");
		}
		//FIXME: should the user still be allowed to go backwards? Shouldn't we check if there's something behind them?
		if (Input.GetKey (KeyCode.DownArrow)) //can still go backwards
		{
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);
		}
	}

	// Update is called once per frame
	void Update () {
		//if no obstacle is too close in front, can move forward
		if (!Physics.Raycast (transform.position, forward, detectionDistance)) {
			moveForward ();
		}
		//there is an obstacle in front of user
		else {
			obstacleInFront ();
		}//went through everything for front

		//checks if player is too close to obstacle in back, left or right sides- restricts backward movement if so; still able to rotate and move forward away from obstacle
		//if backward distance to obstacle increases (player t	urns away/moves from obstacle enough) backward movement will be available again
		if (!Physics.Raycast (transform.position, back, detectionDistance)) 
		{ //checking distance in back
			if (!Physics.Raycast (transform.position, left, detectionDistance) && !Physics.Raycast (transform.position, right, detectionDistance)) 
			{
				//need to check 2nd and 3rd quadrant diagonals (x is negative) 
				//player isn't too close to obstacle in front
				if (!Physics.Raycast (transform.position, diagonal2, detectionDistance) && !Physics.Raycast (transform.position, diagonal3, detectionDistance)) 
				{
					if (Input.GetKey (KeyCode.DownArrow)) 
					{
						//Debug.Log ("all clear to move backward");
						transform.Translate (-Vector3.forward * speed * Time.deltaTime); //camera/player free to move backwards
					}
				} 
				else 
				{ //obstacle is near a back diagonal 
					//for testing accuracy
					if (Physics.Raycast (transform.position, diagonal2, detectionDistance)) 
					{
						//Debug.Log ("OBSTACLE IN BACK RIGHT DIAGONAL!");
					} 
					if (Physics.Raycast (transform.position, diagonal3, detectionDistance))
					{
						//Debug.Log ("OBSTACLE IN BACK LEFT DIAGONAL");
					} 
					if (Input.GetKey (KeyCode.UpArrow))
					{//free to move forwards still
						transform.Translate (Vector3.forward * speed * Time.deltaTime);
					}
				}//end of else for diagonals
			}//end of left right check- there is an obstacle on either side 
		
			else 
			{ //obstacle is on either left or right side of player but not backward//could still be near a diagonal AND a side
				if (Physics.Raycast (transform.position, right, detectionDistance)) 
				{
					//obstacle on the right
					//Debug.Log ("OBSTACLE ON THE RIGHT SIDE");

					if (Input.GetKey (KeyCode.UpArrow)) 
					{
						transform.Translate (Vector3.forward * speed * Time.deltaTime);
					}

					if (!Physics.Raycast (transform.position, diagonal2, detectionDistance)) 
					{
						//not near obstacle on diagonal, so can move backward
						if (Input.GetKey (KeyCode.DownArrow)) 
						{
							transform.Translate (-Vector3.forward * speed * Time.deltaTime);
						}
					}
					else 
					{
						//obstacle hits diagonal too
						//Debug.Log ("OBSTACLE IN BACK RIGHT DIAGONAL");
					}
				} //end of if on right side
		
				if (Physics.Raycast (transform.position, left, detectionDistance)) 
				{
					//obstacle on the left
					//Debug.Log ("OBSTACLE ON THE LEFT SIDE");

					if (Input.GetKey (KeyCode.UpArrow)) 
					{
						transform.Translate (Vector3.forward * speed * Time.deltaTime);
					}

					if (!Physics.Raycast (transform.position, diagonal3, detectionDistance))
					{
						if (Input.GetKey (KeyCode.DownArrow)) 
						{
							transform.Translate (-Vector3.forward * speed * Time.deltaTime); 
						}
					} 
					else 
					{//obstacle hits diagonal too
						//Debug.Log ("OBSTACLE IN BACK LEFT DIAGONAL");
					}
				}//end of if on left side

			}//end of else statement- left/right obstacles
		}//end of forward is clear if statement

		//there is an obstacle behind of user
		else 
		{
			//for testing accuracy
			if (Physics.Raycast (transform.position, diagonal2, detectionDistance)) 
			{
				//Debug.Log ("OBSTACLE IN BACK RIGHT DIAGONAL!");
			}
			if (Physics.Raycast (transform.position, diagonal3, detectionDistance)) 
			{
				//Debug.Log ("OBSTACLE IN BACK LEFT DIAGONAL!");
			}//end testing for accuracy
			if (Physics.Raycast (transform.position, right, detectionDistance))
			{
				//Debug.Log ("OBSTACLE TO THE BACK RIGHT!");
			}
			if (Physics.Raycast (transform.position, left, detectionDistance)) 
			{
				//Debug.Log ("OBSTACLE TO THE BACK LEFT");
			}

			if (Input.GetKey (KeyCode.UpArrow)) 
			{ //can still go forwards
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}
		}//end of else statement- went through everything for back

		checkObstacleWarning (); //idk about this

	}//end of Update()


} 

