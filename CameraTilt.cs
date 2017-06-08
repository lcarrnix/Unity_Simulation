using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTilt : MonoBehaviour
{

	public float tiltAngle;
	public Vector2 tiltAngleRange;
	public float rotationSpeed = 0.1f;

	private Vector3 initialPosition;
	private Quaternion initialRotation;


	// Use this for initialization
	void Start ()
	{
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void tilt (bool tiltUp){
		float angle = transform.eulerAngles.x;
		angle = Mathf.Clamp (angle, -30f, 30f);  //sets max and min degrees of rotation
		if (tiltUp){
			angle += rotationSpeed;
		} else {
			angle -= rotationSpeed;
		}
	}

	void pan (bool panRight){
		float angle = transform.eulerAngles.y;
		angle = Mathf.Clamp (angle, -90f, 90f); //sets max and min dregees of rotation
			if(panRight){
				angle += rotationSpeed;
			} else {
				angle -= rotationSpeed;
			}
	}

	void Update ()
	{
		if (Input.GetKey ("w")) {  //calls tilt method to tilt up, max of 30 degrees
			tilt (true);
		} else if (Input.GetKey ("s")) {  //calls tilt method to tilt down, max of -30 degrees
			tilt (false);
		} else if (Input.GetKey ("a")) { //calls pan method to pan to the right, max of 90 degrees
			pan (true);
		} else if (Input.GetKey ("d")) { //calls pan method to pan to the left, max of -90 dregees
			pan (false);
		} 

	}
}


