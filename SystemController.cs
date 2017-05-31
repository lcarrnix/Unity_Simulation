using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour {
	//controls overall robot system, if optional stand component is implemented
	//*** not currently used in the simulation
	
	//may be useful in future once design of environemnt and player are incorporated

	public GameObject System; //self
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
