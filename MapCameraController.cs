using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour {
	//links to player and creates an offset between them to maintain during movement, points down at entire environment
	//for a bird's eye/map view, will be toggled on/off by user

	public GameObject Player; //setting object as player
	private Vector3 offset; //maintains distance between camera and player

	private float horizontalSpeed = 2.0f;
	private float turnSpeed = 50f; 

	// Use this for initialization
	void Start () {
		offset = transform.position - Player.transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
		//do nothing- no rotation/movement necessary for map view
	}

}


