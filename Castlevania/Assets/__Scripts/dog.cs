using UnityEngine;
using System.Collections;
using System;

public class dog : MonoBehaviour {
	public GameObject simon;
	public float trigger_x;
	public bool jumping = false;
	public bool grounded = false;
	public bool idle = true;
	public bool running = false;
	public float jump_time;
	Animator anim;
	SpriteRenderer sr;
	public Sprite jump_sprite;
	public bool dir;
	
	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
		float simon_x = simon.transform.position.x;
		trigger_x = transform.position.x - 5f;
		anim = this.GetComponent<Animator> ();
		sr = this.GetComponent<SpriteRenderer> ();
		anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0)
			anim.SetInteger("direction", 0);
		else
			anim.SetInteger("direction", 1);

		Vector2 vel = new Vector2 (0, 0);
		if (idle && simon.transform.position.x > trigger_x
		    && transform.position.x > trigger_x) {
			dir = (transform.position.x > simon.transform.position.x);
			attack ();
		}
		else if (idle && simon.transform.position.x < trigger_x
		         && transform.position.x < trigger_x) {
			dir = (transform.position.x > simon.transform.position.x);
			attack ();
		}
		else if (!idle) {
			attack ();
		}
	}

	void attack() {
		idle = false;
		if (!grounded && !jumping) {
			jump();
		}
		else if (!jumping && !running){
			anim.enabled = true;
			running = true;
			Vector2 vel = new Vector2(0, 0);
			if (dir)
				vel.x = -6;
			else
				vel.x = 6;
			rigidbody2D.velocity = vel;
		}
	}

	void jump() {
		Vector2 vel = new Vector2(0, 0);
		jumping = true;
		running = false;
		if (dir)
			vel.x = -8;
		else 
			vel.x = 8;
		vel.y = 3;
		jump_time = Time.time+.5f;
		rigidbody2D.velocity = vel;
		anim.enabled = false;
		sr.sprite = jump_sprite;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "platform"){
			grounded = true;
			if (jumping) {
				jumping = false;
				anim.enabled = true;
				dir = (transform.position.x > simon.transform.position.x);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "platform")
			grounded = false;
	}
	
	void die(){
		Destroy (this.gameObject);
	}
}
