using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class UserPosition : MonoBehaviour
{

	string fileName = "userPosition.XML"; //saves data to an XML file called userPosition
	int time = 0;
	int interval = 50; //interval for checking the users position (every 50 units)


	// Use this for initialization
	void Start ()
	{
	 //do nothing here
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (time > interval) {
			float x = transform.position.x; //the user's current x position
			//float y = transform.position.y; //the user's current y position
			float z = transform.position.z; //the user's current z position

			StreamWriter sw = File.AppendText(fileName); //or use File.AppendText to just add all data to one file
			sw.WriteLine (x + " " + z); //outputs a line with the user's current x position seperated from z position with single space
			//sw.WriteLine ("Z: " + z); //outputs a line with the user's current z position
			sw.Close (); 
			Debug.Log ("writing to file");
			time = 0;
		} else {
			time++;
		}
	}
}

