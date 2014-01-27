using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour {
	public float endtime = 0f;
	public Simon simon;

	void Start() {
		simon = GameObject.Find ("Simon").GetComponent<Simon> ();
	}

	void Update() {
		if (endtime != 0f && Time.time > endtime) {
			Application.LoadLevel("level_1-2");
		}
	}

	void  OnTriggerEnter2D(Collider2D other) {
		simon.next_level ();
		endtime = Time.time + .8f;
	}
}
