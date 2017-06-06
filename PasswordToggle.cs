using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour {

	public UnityEngine.UI.InputField password = null;

	//pretty sure this script does nothing
	public void ToggleInputType()
	{
		if (this.password != null)
		{
			if (this.password.contentType == InputField.ContentType.Password) 
			{
				this.password.contentType = InputField.ContentType.Standard;
			} 
			else 
			{
				this.password.contentType = InputField.ContentType.Password;
			}

			this.password.ForceLabelUpdate ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
