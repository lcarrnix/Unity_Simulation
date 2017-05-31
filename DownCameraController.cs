using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCameraController : MonoBehaviour {
	//links to player and creates an offset between them to maintain during movement, points down at stand of robot

	public GameObject Player;
	private Vector3 offset; //distance between camera and player

	public Transform target; //for lookAt() function. target is set in game- will be player below the down-angled camera at all times

	private float horizontalSpeed = 2.0f;
	private float turnSpeed = 50f; 

	// Use this for initialization
	void Start () {
		offset = transform.position - Player.transform.position; //sets desired distance between player and down-angled camera 
	}
	
	// Update is called once per frame
	void Update () {
		//do nothing
	}

	void LateUpdate() {
		transform.LookAt (target); //keeps camera looking/following player
		transform.rotation = Player.transform.rotation; //camera rotates with player
		transform.Rotate(90,0,0, Space.Self); //fixes camera rotating at start

		transform.position = Player.transform.position + offset; //adds the offset to the player at every frame
		//so, as player moves, camera moves at end of that frame
	}
}
