using UnityEngine;
using System.Collections;
using System;

public class Simon : MonoBehaviour {
	public float		speed = 8;
	public float		jumpSpeed = 25;
	public float		jumpAcc = 4;

	public bool			grounded = true;
	public bool			disable = false;

	private Animator animator;
	private AnimatorStateInfo anim_state;
	public float whip_time = .5f;
	public float whip_end = 0;
	public stair_info cur_stairs;
	public stair_info on_stair_info;
	public bool on_stairs = false;
	public bool gettingOffStairs = false;
	public bool gettingOnStairs = false;
	
	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		animator.speed = .5f;
		cur_stairs.pos.x = 0;
		cur_stairs.pos.y = 0;
		cur_stairs.dir = 0;
		on_stair_info.pos.x = 0;
		on_stair_info.pos.y = 0;
		on_stair_info.dir = 0;
	}

	void Update () { // Every Frame
		// Timed actions
		//-------------------------------------------------------------
		if (disable) {
			Vector2 vel2 = rigidbody2D.velocity;
			vel2.x = 4;
			vel2.y = 0;
			animator.Play ("walk_right");
			rigidbody2D.velocity = vel2;
			return;
		}

		if (gettingOnStairs){
			rigidbody2D.velocity=getOnStairs();
			updateAnimation ();
			return;
		}

		if (gettingOffStairs) {
			rigidbody2D.velocity = getOffStairs ();
			updateAnimation ();
			return;
		}

		// adjust collider to match crouch sprite
		// -----------------------------------------------------------
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
		//--------------------------------------------------------------------------
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

		if (Input.GetKey (KeyCode.DownArrow) && !gettingOnStairs) {
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

		// Get on stairs if we are close
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			// If we are on the ground by the bottom of a staircase
			if (cur_stairs.pos.x != 0 && cur_stairs.dir == 0 && !on_stairs) {
				gettingOnStairs = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)){
			if (cur_stairs.pos.x != 0 && cur_stairs.dir == 1 && !on_stairs) {
				gettingOnStairs=true;
			}
		}

		// Stairs movement
		if (on_stairs){
			if (Input.GetKey (KeyCode.UpArrow) 
			    || Input.GetKey (KeyCode.RightArrow)) {
				if (cur_stairs.pos.x != 0 && cur_stairs.dir == 1) {
					if (transform.position.y < cur_stairs.pos.y-.2) {
						vel.x = 2;
						vel.y = 2;
					}
					else {
						gettingOffStairs = true;
					}
				}
				else {
					vel.x = 2;
					vel.y = 2;
				}
			}
			else if (Input.GetKey(KeyCode.DownArrow) 
			    	|| Input.GetKey(KeyCode.LeftArrow)) {
				if (cur_stairs.pos.x != 0 && cur_stairs.dir == 0){
					if (transform.position.y > cur_stairs.pos.y+.2) {
						vel.x=-2;
						vel.y=-2;
					}
					else {
						gettingOffStairs = true;
					}
				}
				else {
					vel.x=-2;
					vel.y=-2;
				}

			}
			else {
				vel.x=0;
				vel.y=0;
			}
		}
		rigidbody2D.velocity = vel;

		updateAnimation ();




	}

	void updateAnimation(){
		Vector2 vel = rigidbody2D.velocity;
		if (vel.x == 0 && vel.y == 0) {
			animator.SetBool ("idle", true);
		}
		else {
			animator.SetBool ("idle", false);
		}
		
		if (vel.x > 0) {
			animator.SetInteger ("direction_x", 0);
		} else if (vel.x < 0) {
			animator.SetInteger ("direction_x", 1);
		}

		if (vel.y > 0) {
			animator.SetInteger ("direction_y", 0);
		} else if (vel.y < 0) {
			animator.SetInteger ("direction_y", 1);
		}

		
		if (Time.time >= whip_end && animator.GetBool ("whipping")) {
			animator.SetBool ("whipping", false);
		}

		if (on_stairs) {
			animator.SetBool ("stairs", true);
		}
		else {
			animator.SetBool("stairs", false);
		}
		if (!grounded && on_stairs == false){
			animator.SetBool ("air", true);
		}
		else {
			animator.SetBool ("air", false);
		}

		if (gettingOnStairs) {
			animator.SetBool ("crouch", false);
		}
	}

	Vector2 getOnStairs(){
		Vector2 vel = new Vector2 (0, 0);
		rigidbody2D.gravityScale = 0;
		// If we are within a tolerable distance to the center of the stairs
		if (.05 > Math.Abs(cur_stairs.pos.x-transform.position.x) || on_stairs) {
			on_stairs = true;
			// Walk up onto the stairs a little bit
			if (cur_stairs.dir == 0 && transform.position.y < cur_stairs.pos.y+.35) {
				vel.y=2;
				vel.x=2;
			}
			else if (cur_stairs.dir == 1 && transform.position.y > cur_stairs.pos.y-.35) {
				vel.y=-2;
				vel.x=-2;
			}
			else {
				// Get on!
				vel.y=0;
				vel.x=0;
				on_stair_info = cur_stairs;
				gettingOnStairs = false;
			}
		}
		
		else if (transform.position.x > cur_stairs.pos.x) {
			// Walk towards stairs
			vel.x = -4;
		}
		
		else {
			// Walk towards stairs
			vel.x = 4;
		}
		
		return vel;
		
	}
	
	Vector2 getOffStairs(){
		Vector2 vel = new Vector2 (0, 0);
		if (cur_stairs.dir == 1 && transform.position.y < cur_stairs.pos.y){
			vel.x=2;
			vel.y=2;
		}
		else if (cur_stairs.dir == 1 && transform.position.x < cur_stairs.pos.x+1.3) {
			vel.x=4;
		}
		else {
			gettingOffStairs = false;
			on_stairs = false;
			rigidbody2D.gravityScale = 8;
		}
		return vel;
	}
		
		
	public void next_level() {
		this.disable = true;
		Vector2 vel = rigidbody2D.velocity;
		vel.x = 3;
		vel.y = 0;
		animator.Play ("walk_right");
		rigidbody2D.velocity = vel;
	}

	void OnTriggerStay2D(Collider2D other) {
		grounded = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		grounded = false;
	}

	void whip_hit(GameObject hit_obj){
		// For weird reasons, we check both whether the animation is > 2/3 done
		// AND check to see we are at least half way through the "whip time"
		if (animator.GetBool ("whipping") 
			&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .66
		    && Time.time > whip_end-(whip_time/2)) { 
				hit_obj.SendMessage ("die");

		}
	}

	void near_stairs(stair_info info) {
		print ("NEW STAIRS");
		this.cur_stairs = info;
	}

	void not_near_stairs() {
		Vector2 pos;
		pos.x = 0;
		pos.y = 0;
		cur_stairs.pos = pos;
		print ("exiting stairs");
	}


}


















