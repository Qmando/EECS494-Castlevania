using UnityEngine;
using System.Collections;

public class Simon : MonoBehaviour {
	public float		speed = 8;
	public float		jumpSpeed = 7;
	public float		jumpAcc = 4;

	public bool			grounded = true;

	private Animator animator;
	private AnimatorStateInfo anim_state;
	public float whip_time = .5f;
	public float whip_end = 0;
	
	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		animator.speed = .5f;
	}

	void Update () { // Every Frame

		// CONTROLS
		float h = Input.GetAxis ("Horizontal");

		Vector2 vel = rigidbody2D.velocity;
		if (grounded) {
			vel.x = h * speed;
		}
		rigidbody2D.velocity = vel;

		if (Input.GetKeyDown(KeyCode.Z)) {
			if (grounded) vel.y = jumpSpeed;
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			animator.SetBool("whipping", true);
			whip_end = Time.time + whip_time;
		}

		// ANIMATION CONTROL
		if (grounded && vel.x == 0) {
			animator.SetBool ("idle", true);
		}
		else {
			animator.SetBool ("idle", false);
		}

		if (vel.x > 0) {
			animator.SetInteger ("direction", 0);
		} else if (vel.x < 0) {
			animator.SetInteger ("direction", 1);
		}

		if (Time.time >= whip_end && animator.GetBool ("whipping")) {
			animator.SetBool ("whipping", false);
		}


		

	}

	void OnTriggerEnter2D(Collider2D other) {
		grounded = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		grounded = false;
	}
}


















