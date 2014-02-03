using UnityEngine;
using System.Collections;

public class Simon : MonoBehaviour {
	public float		speed = 8;
	public float		jumpSpeed = 7;
	public float		jumpAcc = 4;

	public bool			grounded = true;
	public bool			disable = false;

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
		if (disable) {
			Vector2 vel2 = rigidbody2D.velocity;
			vel2.x = 4;
			vel2.y = 0;
			animator.Play ("walk_right");
			rigidbody2D.velocity = vel2;
			return;
		}

		// adjust collider to match crouch sprite
		AnimatorStateInfo x = animator.GetCurrentAnimatorStateInfo(0);
		BoxCollider2D collider = this.GetComponents<BoxCollider2D> ()[1];
		if (x.IsName ("jump_left") || x.IsName ("jump_right")) {
			Vector2 size = collider.size;
			Vector2 center = collider.center;
			size.y = 1.4f;
			center.y = .7f;
			collider.size = size;
			collider.center = center;
		}
		else {
			Vector2 size = collider.size;
			size.y = 1.9f;
			Vector2 center = collider.center;
			center.y = 1;
			collider.size = size;
			collider.center = center;
		}

		// CONTROLS
		float h = Input.GetAxis ("Horizontal");

		Vector2 vel = rigidbody2D.velocity;
		if (grounded && !animator.GetBool ("whipping")) {
			vel.x = h * speed;
		}

		if (Input.GetKeyDown(KeyCode.Z)) {
			if (grounded && !animator.GetBool ("crouch")) {
				vel.y = jumpSpeed;
			}
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			if (grounded) {
				vel.x = 0;
				animator.SetBool ("crouch", true);
			}
		}
		else {
			animator.SetBool ("crouch", false);
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			animator.SetBool("whipping", true);
			whip_end = Time.time + whip_time;
			if (grounded) {
				vel.x = 0;
			}
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

		animator.SetBool ("air", !grounded);


		rigidbody2D.velocity = vel;

	}

	public void next_level() {
		this.disable = true;
		Vector2 vel = rigidbody2D.velocity;
		vel.x = 4;
		vel.y = 0;
		animator.Play ("walk_right");
		rigidbody2D.velocity = vel;
	}

	void OnTriggerEnter2D(Collider2D other) {
		grounded = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		grounded = false;
	}

	void whip_hit(GameObject hit_obj){
		if (animator.GetBool ("whipping")){
			if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .66) {
				hit_obj.SendMessage ("die");
			}
		}
	}
	


}


















