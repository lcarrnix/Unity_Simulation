using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandController : MonoBehaviour {
	//controls optional stand component of the system
	//*** not currently implemented into the simulation

	//may be useful in future once design of environemnt and player are incorporated

	public GameObject Stand; //self
	public Rigidbody rb; //for collisions and parking system in place

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		//do nothing
	}
}
