using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	private Vector3 offset; 

	private float horizontalSpeed = 2.0f;
	private float turnSpeed = 50f;



	// Use this for initialization
	void Start () 
	{
		offset = transform.position - Player.transform.position; //setting offset as distance between camera and ball
	}

	void Update()
	{
		//do nothing

		//THESE DON'T WORK IN HERE
		//if (Input.GetKey (KeyCode.W)) 
		//{
			//camera tilts up towards ceiling
		//	transform.Rotate (Vector3.left,  turnSpeed * Time.deltaTime); 
			//transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);

		//}

		//if (Input.GetKey (KeyCode.S)) {
			//camera tilts down towrads ground
		//	transform.Rotate (Vector3.left, -turnSpeed * Time.deltaTime);
		//}

		//if (Input.GetKey (KeyCode.A)) {
		//	transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		//}

		//if (Input.GetKey (KeyCode.D)) {
		//	transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);
		//}
	}


	// runs at end of every frame- guarunteed to run after anything in frame has happened
	void LateUpdate () {
		transform.rotation = Player.transform.rotation;

		transform.position = Player.transform.position + offset; //adds the offset to the player at every frame
			//so, as player moves, camera moves at end of that frame
	}
}
