using UnityEngine;
using System.Collections.Generic;
using System;

public class bomb : MonoBehaviour {
	public Animator anim;
	public GameObject simon;
	public bool exploded = false;
	public float exploded_time;
	public bool shooting = false;
	public Vector2 exploded_pos = new Vector2(0, 0);
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		simon = GameObject.Find ("Simon");
	}
	
	// Update is called once per frame
	void Update () {
		if (exploded) {
			transform.position = exploded_pos;
			if (Time.time > exploded_time) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D with) {
		explode ();
	}

	void explode(){
		anim.SetTrigger ("boom");
		rigidbody2D.gravityScale = 0;
		exploded = true;
		exploded_pos = transform.position;
		exploded_time = Time.time + .5f;
		simon.SendMessage ("bomb", this.gameObject);
	}

	void shoot(){
		anim = GetComponent<Animator> ();
		simon = GameObject.Find ("Simon");
		anim.SetBool ("horizontal", true);
		rigidbody2D.gravityScale = .1f;
		rigidbody2D.velocity = new Vector2 (10, 0);
		transform.localScale = new Vector3 (.5f, .5f, .5f);
	}

	void attack_on_fall() {}
		//nothing
	
}
