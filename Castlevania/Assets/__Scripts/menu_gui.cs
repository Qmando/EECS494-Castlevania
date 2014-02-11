using UnityEngine;
using System.Collections;

public class menu_gui : MonoBehaviour {
		
	void OnGUI () {
		if (GUI.Button (new Rect (50, 50, 300, 90), "Play Castlevania")) {
			// This code is executed when the Button is clicked
			Application.LoadLevel (0);
		}
		if (GUI.Button (new Rect (50, 150, 300, 90), "Play custom level")) {
			// This code is executed when the Button is clicked
			Application.LoadLevel (3);
		}
	
	}
}
