using UnityEngine;
using System.Collections;

public class level_switch2 : MonoBehaviour {
	public float endtime = 0f;
	public Simon simon;
	
	void Start() {
		simon = GameObject.Find ("Simon").GetComponent<Simon> ();
	}
	
	void Update() {
		if (endtime != 0f && Time.time > endtime) {
			Application.LoadLevel("level_1-3");
		}
	}
	
	void  OnTriggerStay2D(Collider2D other) {
		if (Input.GetKey (KeyCode.DownArrow)){
			simon.next_level2 ();
			endtime = Time.time + .8f;
		}
	}
}
