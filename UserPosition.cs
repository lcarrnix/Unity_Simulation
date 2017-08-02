using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class UserPosition : MonoBehaviour
{

	string fileName = "userPosition.XML"; //saves data to an XML file called userPosition
	string currentFile = @"C:\Users\Rebecca\Documents\Roll a Ball example\userPosition.XML"; //file path may change****
	int time = 0;
	int interval = 50; //interval for checking the users position (every 50 units)

	// Use this for initialization
	void Start ()
	{
		File.Delete (currentFile); //deletes old user data by deleting the file so MAKE SURE TO SAVE FILE SOMEWHERE BETWEEN TESTS!!
		Debug.Log ("Deleted old data");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (time > interval) {
			float x = transform.position.x; //the user's current x position
			float z = transform.position.z; //the user's current z position
		
			StreamWriter sw = File.AppendText (fileName);
			sw.WriteLine (x + " " + z);
			sw.Close ();
			Debug.Log ("writing to file");
			time = 0;

		} else {
			time++;
		}
	}
}

