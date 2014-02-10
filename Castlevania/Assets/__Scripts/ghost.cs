using UnityEngine;
using System.Collections;

public class ghost : MonoBehaviour {
	public GameObject simon;
	public int moving;

	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
		float simon_x = simon.transform.position.x;
		if (transform.position.x - simon_x < 0)
			moving = 4;
		else
			moving =-4;
			
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 vel = new Vector2 (0, 0);
		vel.x = moving;
		rigidbody2D.velocity = vel;

	}

	void die(){
		Destroy (this.gameObject);
	}
}
