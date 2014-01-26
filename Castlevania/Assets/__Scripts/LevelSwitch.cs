using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour {
	public float endtime = 0;
	public Simon simon;

	void Start() {
		simon = GameObject.Find ("Simon").GetComponent<Simon> ();
	}

	void  OnTriggerEnter2D(Collider2D other) {
		simon.next_level ();
	}
}
