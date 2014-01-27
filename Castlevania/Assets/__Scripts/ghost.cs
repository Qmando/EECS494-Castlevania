using UnityEngine;
using System.Collections;

public class ghost : MonoBehaviour {
	public GameObject simon;

	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
	}
	
	// Update is called once per frame
	void Update () {
		// Move left when simon gets close
		float x = transform.position.x;
		float simon_x = simon.transform.position.x;
		if (x - simon_x < 10f) {
			Vector2 loc = this.transform.position;
			loc.x -= 4*Time.deltaTime;
			this.transform.position = loc;
		}
	}

	void die(){
		Destroy (this.gameObject);
	}
}
