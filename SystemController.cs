﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour {

	public GameObject System; //self
	public Rigidbody rb; //for collisions and parking system in place

	void Start()
	{
		Debug.Log ("hello");
		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		//do nothing
	}
}
