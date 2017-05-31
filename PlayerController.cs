using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//initalizes the player and rigidbody; sets rigidbody equal to its component
	
	//player name should be eventually replaced with system or robot in all scripts and documentation

	public GameObject Player; //self
	public Rigidbody rb; //for collisions and parking system in place

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}
}


	

	

