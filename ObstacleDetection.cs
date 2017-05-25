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
	private RaycastHit hit;
	public float detectionDistance; //for obstacle detection
	private float speed;
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

	// Use this for initialization
	void Start () 
	{
		PlayerControlInstance = GetComponent<PlayerController> ();
		ControlMethInstance = GetComponent<ControlMethods> ();

		rb = PlayerControlInstance.rb;

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
		speed = 5.0f;
		decreasedSpeed = 1.0f;

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
		if ((Physics.Raycast (transform.position, forward, detectionDistance * 2)) || (Physics.Raycast (transform.position, back, detectionDistance * 2)) ||
		    (Physics.Raycast (transform.position, left, detectionDistance * 1.7f)) || (Physics.Raycast (transform.position, right, detectionDistance * 1.7f)) ||
		    (Physics.Raycast (transform.position, diagonal1, detectionDistance * 1.5f)) || (Physics.Raycast (transform.position, diagonal2, detectionDistance * 1.5f)) ||
		    (Physics.Raycast (transform.position, diagonal3, detectionDistance * 1.5f)) || (Physics.Raycast (transform.position, diagonal4, detectionDistance * 1.5f))) 
		{
			//there is an obstacle near user 
			//slowing down user's speed
			speedDecrease(ref speed);

			//checking which direction obstacle is in relative to player
			if (Physics.Raycast (transform.position, forward, out hit, detectionDistance * 2)) 
			{
				//Debug.Log ("Obstacle in front!");
				theHitObject = hit.collider.gameObject; //sets to obstacle at hand

				frontBackObstacleWarning (theHitObject); //changes color to red
			}
			if (Physics.Raycast (transform.position, back, out hit, detectionDistance * 2)) 
			{
				//Debug.Log ("Obstacle behind!");
				theHitObject = hit.collider.gameObject;

				frontBackObstacleWarning (theHitObject); //changes color to red
			}
			if (Physics.Raycast (transform.position, left, out hit, detectionDistance * 1.7f))
			{
				//Debug.Log ("Obstacle to the left!");
				theHitObject = hit.collider.gameObject; 

				leftRightObstacleWarning (theHitObject); //changes color to yellow
			}
			if (Physics.Raycast (transform.position, right, out hit, detectionDistance * 1.7f))
			{
				//Debug.Log ("Obstacle to the right!");
				theHitObject = hit.collider.gameObject; 

				leftRightObstacleWarning (theHitObject); //changes color to yellow
			}
			if (Physics.Raycast (transform.position, diagonal1, out hit, detectionDistance * 1.5f)) 
			{
				//Debug.Log ("Obstacle in front right diagonal!");
				theHitObject = hit.collider.gameObject; 

				frontBackObstacleWarning (theHitObject); //changes color to red
			}
			if (Physics.Raycast (transform.position, diagonal2, out hit, detectionDistance * 1.5f)) 
			{
				//Debug.Log ("Obstacle in back right diagonal!");
				theHitObject = hit.collider.gameObject;

				leftRightObstacleWarning (theHitObject); //changes color to yellow
			}
			if (Physics.Raycast (transform.position, diagonal3, out hit, detectionDistance * 1.5f)) 
			{
				//Debug.Log ("Obstacle in back left diagonal!");
				theHitObject = hit.collider.gameObject; 

				leftRightObstacleWarning (theHitObject); //changes color to yellow
			}
			if (Physics.Raycast (transform.position, diagonal4, out hit, detectionDistance * 1.5f)) 
			{
				//Debug.Log ("Obstacle in front right diagonal!");
				theHitObject = hit.collider.gameObject;

				frontBackObstacleWarning (theHitObject); //changes color to red
			}
		}
		checkObstacleWarning (); //if all obstacles are far enough away, will turn white again
	}

	//if an obstacle has been detected as a warning in front/back of player
	GameObject frontBackObstacleWarning(GameObject objectWarning)
	{
		//visual warnings take place here- red to front and back obstacles (most severe) 
		objectWarning.GetComponent<Renderer>().material.color = new Color(1,0,0,1); //red

		return objectWarning;
	}

	//if an obstacle has been detected as a warning on left/right of player
	GameObject leftRightObstacleWarning(GameObject objectWarning)
	{
		//visual warnings take place here- yellow to left and right obstacles (less severe)
		//converting doubles to floats for color
		float gValue = (float) 0.92;
		float bValue = (float)0.016;
		objectWarning.GetComponent<Renderer> ().material.color = new Color (1, gValue, bValue, 1); //yellow- from docs.unity3d.com

		return objectWarning;
	}

	//speed is decreased substantially to assist user in avoiding an obstacle
	float speedDecrease(ref float speed)
	{
		//near obstacle in any direction - need to decrease speed to help warn user, help them detect the obstacle, and avoid the obstacle itself
		speed = decreasedSpeed;
		return speed;
	}

	void checkObstacleWarning() //overwriting
	{
		//checking if all desks are far away from user to not be a warning anymore- checks front, back, left, right, and diagonals
		if (!Physics.Raycast (transform.position, forward, out hit, detectionDistance * 2) && !Physics.Raycast(transform.position, back, out hit, detectionDistance * 2) 
			&& !Physics.Raycast(transform.position, left, out hit, detectionDistance * 1.7f) && !Physics.Raycast(transform.position, right, out hit, detectionDistance * 1.7f)
			&& !Physics.Raycast(transform.position, diagonal1, out hit, detectionDistance * 1.5f) && !Physics.Raycast(transform.position, diagonal2, out hit, detectionDistance * 1.5f)
			&& !Physics.Raycast(transform.position, diagonal3, out hit, detectionDistance * 1.5f) && !Physics.Raycast(transform.position, diagonal4, out hit, detectionDistance * 1.5f)) 
		{
			//restoring speed to normal 
			speed = 5.0f;

			deskObstacles = GameObject.FindGameObjectsWithTag ("desk"); 

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
		speed = 5.0f; //restoring max speed
		return objectWarning;
	}

	//these tags will be changed when the environment is changed to a living room setting
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "desk")
		{
			//Destroy (col.gameObject);
			rb.AddForce(0,0,0, ForceMode.VelocityChange);
		}

		if(col.gameObject.tag == "wall")
		{
			//rend.material.color = Color.red;
			rb.AddForce (0,0,0, ForceMode.VelocityChange); 
		}
	}
		
	// Update is called once per frame
	void Update () {
		
		//if no obstacle is too close in front, can move forward
		if (!Physics.Raycast (transform.position, forward, detectionDistance)) 
		{
			if (!Physics.Raycast (transform.position, left, detectionDistance) && !Physics.Raycast (transform.position, right, detectionDistance)) 
			{
				//need to check 1st and 4th quadrant diagonals (x is positive)
				//player isn't too close to obstacle in front
				if (!Physics.Raycast (transform.position, diagonal1, detectionDistance) && !Physics.Raycast (transform.position, diagonal4, detectionDistance)) 
				{
					if (Input.GetKey (KeyCode.UpArrow)) 
					{
						//Debug.Log ("in the all clear to move forward");
						//camera/player move forward
						transform.Translate (Vector3.forward * speed * Time.deltaTime); //move forwards
					} 
				} 
				else 
				{ //obstacle near diagonals 
					//for testing accuracy
					if (Physics.Raycast (transform.position, diagonal1, detectionDistance)) 
					{
						//Debug.Log ("OBSTACLE IN FRONT RIGHT DIAGONAL");
					} 
					if (Physics.Raycast (transform.position, diagonal4, detectionDistance)) 
					{
						//Debug.Log ("OBSTACLE IN FRONT LEFT DIAGONAL");
					} //end testing for accuracy
					if (Input.GetKey (KeyCode.DownArrow))
					{ //free to move backwards
						transform.Translate (-Vector3.forward * speed * Time.deltaTime); //move backwards away from obstacle
					}
				}//end of else for diagonals
			}//end of left right- there is an obstacle on either side
			else 
			{ //obstacle on left or right, but not forward
				if (Physics.Raycast (transform.position, right, detectionDistance)) 
				{
					//obstacle on the right
					//Debug.Log ("OBSTACLE ON THE RIGHT SIDE");

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
					else 
					{ //obstacle hits diagonal too
						//Debug.Log ("OBSTACLE IN FRONT RIGHT DIAGONAL");
					}
				}
			//end of if on right side
				if (Physics.Raycast (transform.position, left, detectionDistance)) 
				{
						//obstacle on the left
						//Debug.Log ("OBSTACLE ON THE LEFT SIDE");
					
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
					else 
					{//obstacle hits diagonal too
						//Debug.Log ("OBSTACLE IN FRONT LEFT DIAGONAL");
					}
				}//end of if on left side
					
			}//end of else statement- left/right obstacles
		}//end of forward is clear if statement

		//there is an obstacle in front of user
		else
		{
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
			if (Input.GetKey (KeyCode.DownArrow)) //can still go backwards
			{
				transform.Translate (-Vector3.forward * speed * Time.deltaTime);
			}
		}//end of else statement- went through everything for front 


		//checks if player is too close to obstacle in back, left or right sides- restricts backward movement if so
		//still able to rotate and move forward away from obstacle 

		//if backward distance to obstacle increases (player turns away/moves from obstacle enough) backward movement will be available again
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
				Debug.Log ("OBSTACLE IN BACK RIGHT DIAGONAL!");
			}
			if (Physics.Raycast (transform.position, diagonal3, detectionDistance)) 
			{
				Debug.Log ("OBSTACLE IN BACK LEFT DIAGONAL!");
			}//end testing for accuracy
			if (Physics.Raycast (transform.position, right, detectionDistance))
			{
				Debug.Log ("OBSTACLE TO THE BACK RIGHT!");
			}
			if (Physics.Raycast (transform.position, left, detectionDistance)) 
			{
				Debug.Log ("OBSTACLE TO THE BACK LEFT");
			}

			if (Input.GetKey (KeyCode.UpArrow)) 
			{ //can still go forwards
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}
		}//end of else statement- went through everything for back

		checkObstacleWarning (); //idk about this

	}//end of Update()
} 

