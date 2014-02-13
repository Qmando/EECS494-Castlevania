using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Simon : MonoBehaviour {
	public float	speed = 8;
	public float	jumpSpeed = 25;
	public float	jumpAcc = 4;

	public bool		grounded = true;
	public bool		disable = false;

	public int			health = 10;
	public int			hearts = 0;
	public GameObject	sub_weapon;

	private Animator animator;
	private AnimatorStateInfo anim_state;

	public float whip_time = .5f;
	public float whip_end = 0;

	public stair_info cur_stairs;
	public stair_info on_stair_info;
	public bool on_stairs = false;
	public bool gettingOffStairs = false;
	public bool gettingOnStairs = false;

	public int whip_dir=0;
	public List<GameObject> in_trigger;
	public GameObject right_box;
	public GameObject left_box;
	public float next_damage=0;
	
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
		whip_hit ();

		if (disable) {
			Vector2 vel2 = rigidbody2D.velocity;
			vel2.x = 4;
			vel2.y = 0;
			animator.Play ("walk_right");
			rigidbody2D.velocity = vel2;
			return;
		}

		else if (gettingOnStairs){
			rigidbody2D.velocity=getOnStairs();
			updateAnimation ();
			return;
		}

		else if (gettingOffStairs) {
			rigidbody2D.velocity = getOffStairs ();
			updateAnimation ();
			return;
		}

		else if (Time.time < next_damage-1.7) {
			animator.SetTrigger ("hurt");
			return;
		}


		input ();
		updateAnimation ();





	}

	void updateAnimation(){
		// If this gets called we arent "hurt"
		animator.SetTrigger ("unhurt");
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

	void input() {
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
			whip_dir = animator.GetInteger ("direction_x");
			if (grounded) {
				vel.x = 0;
			}
		}
		
		// Get on stairs if we are close
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			// If we are on the ground by the bottom of a staircase
			if (grounded && cur_stairs.pos.x != 0 
			    && (cur_stairs.dir == 0 || cur_stairs.dir == 3)
			    && !on_stairs) {
				gettingOnStairs = true;
				on_stair_info = cur_stairs;
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)){
			if (grounded && cur_stairs.pos.x != 0 
			    && (cur_stairs.dir == 1 || cur_stairs.dir == 2)
			    && !on_stairs) {
				gettingOnStairs=true;
				on_stair_info = cur_stairs;
			}
		}
		
		// Stairs movement
		if (on_stairs){
			if (on_stair_info.dir == 0 || on_stair_info.dir == 1) {
				// We are on a up/right stair
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
			else {
				// We are on a down/left stair
				if (Input.GetKey (KeyCode.UpArrow) 
				    || Input.GetKey (KeyCode.LeftArrow)) {
					if (cur_stairs.pos.x != 0 && cur_stairs.dir == 2) {
						if (transform.position.y < cur_stairs.pos.y-.2) {
							vel.x = -2;
							vel.y = 2;
						}
						else {
							gettingOffStairs = true;
						}
					}
					else {
						vel.x = -2;
						vel.y = 2;
					}
				}
				else if (Input.GetKey(KeyCode.DownArrow) 
				         || Input.GetKey(KeyCode.RightArrow)) {
					if (cur_stairs.pos.x != 0 && cur_stairs.dir == 3){
						if (transform.position.y > cur_stairs.pos.y+.2) {
							vel.x=2;
							vel.y=-2;
						}
						else {
							gettingOffStairs = true;
						}
					}
					else {
						vel.x=2;
						vel.y=-2;
					}
					
				}
				else {
					vel.x=0;
					vel.y=0;
				}
			}
		}
		rigidbody2D.velocity = vel;
	}

	Vector2 getOnStairs(){
		Vector2 vel = new Vector2 (0, 0);

		// If we are within a tolerable distance to the center of the stairs
		if ((.05 > Math.Abs(cur_stairs.pos.x-transform.position.x)) 
		    				|| on_stairs) {
			on_stairs = true;
			rigidbody2D.gravityScale = 0;
			// Walk up onto the stairs a little bit
			if (on_stair_info.dir == 0 && on_stair_info.pos.x != 0
			    && transform.position.y < on_stair_info.pos.y+.35) {
				vel.y=2;
				vel.x=2;
			}
			else if (on_stair_info.dir == 1 && transform.position.y > on_stair_info.pos.y-.35) {
				vel.y=-2;
				vel.x=-2;
			}
			else if (on_stair_info.dir == 2 && transform.position.y > on_stair_info.pos.y-.35) {
				vel.y=-2;
				vel.x=2;
			}
			else if (on_stair_info.dir == 3 && transform.position.y < on_stair_info.pos.y+.35) {
				vel.y=2;
				vel.x=-2;
			}
			else {
				// Get on!
				vel.y=0;
				vel.x=0;

				gettingOnStairs = false;
				if (on_stair_info.dir == 0 || on_stair_info.dir == 1) {
					animator.SetBool("stairdir", false);
				}
				else {
					animator.SetBool("stairdir", true);
				}
				animator.SetTrigger ("getonstairs");
			}
		}
		
		else if (transform.position.x > on_stair_info.pos.x) {
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
		else if (cur_stairs.dir == 1 && transform.position.x < cur_stairs.pos.x+1) {
			vel.x=4;
		}
		else if (cur_stairs.dir == 2 && transform.position.y < cur_stairs.pos.y){
			vel.x=-2;
			vel.y=2;
		}
		else if (cur_stairs.dir == 2 && transform.position.x > cur_stairs.pos.x-1) {
			vel.x=-4;
		} 
		else {
			gettingOffStairs = false;
			on_stairs = false;
			animator.SetTrigger ("getoffstairs");
			rigidbody2D.gravityScale = 4;
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
		if (other.gameObject.tag == "platform")
			grounded = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "platform")
			grounded = false;
	}

	void whip_hit(){
		// For weird reasons, we check both whether the animation is > 2/3 done
		// AND check to see we are at least half way through the "whip time"
		if (animator.GetBool ("whipping") 
			&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .66
		    && Time.time > whip_end-(whip_time/2)
		    && whip_dir == 0) { 
				hit_stuff (true);

		}
		else if (animator.GetBool ("whipping") 
		    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .66
		    && Time.time > whip_end-(whip_time/2)
		    && whip_dir == 1) { 
			hit_stuff (false);
			
		}
	}


	void near_stairs(stair_info info) {

		this.cur_stairs = info;
	}

	void not_near_stairs(stair_info info) {
		// If we overlap stairs, dont "exit" the new stairs
		if (this.cur_stairs.pos.x == info.pos.x){
			print("leaving stairs");
			Vector2 pos;
			pos.x = 0;
			pos.y = 0;
			cur_stairs.pos = pos;
		}
		else {
			print ("overlapping stairs");
		}
	}

	void hitby(Collider2D collider){
		// Collectable items
		if (collider.gameObject.tag == "collectable"){
			if (collider.gameObject.name == "heart") {
				increase_hearts(2); // Fix this to get heart_amnt
			}
			Destroy( collider.gameObject );
		}

		// Enemies!
		if (Time.time < next_damage)
			return;
		GameObject collided_with = collider.gameObject;
		if (collided_with.layer != 0)
			print ("Collided with object in layer " + collided_with.layer.ToString ());
		if (collided_with.layer == 9) {
			print ("Starting damage coroutine");
			take_damage(collided_with);
		}
	}

	void take_damage(GameObject obj){
		health -= 2;
		next_damage = Time.time + 2;
		// Bounce back if we take damage (but not on stairs!)
		if (!on_stairs && !gettingOnStairs && !gettingOffStairs) {
			Vector2 pos = transform.position;
			Vector2 vel = new Vector2 (0, 15);
			if (obj.transform.position.x > pos.x){
				vel.x -= 8;
				animator.SetInteger("direction_x", 0);
			}
			else {
				vel.x = 8;
				animator.SetInteger("direction_x", 1);
			}
			rigidbody2D.velocity = vel;
		}
		// Get knocked off stairs by bombs
		if (obj.name.Length > 3
		    && obj.name.Substring (0, 4) == "bomb") {
			on_stairs = false;
			animator.SetTrigger ("getoffstairs");
			rigidbody2D.gravityScale = 4;
		}
		else {
			print("Hit by "+obj.name);
		}

	}

	void increase_hearts(int amt){
		hearts += amt;
	}

	void equip_subweapon(GameObject sw){
		sub_weapon = sw;
	}

	void hit_stuff(bool right) {
		foreach (GameObject obj in objects_in_whip(right)){
			print ("Killing " + obj.name);
			obj.SendMessage("die");
		}
	}
	
	List<GameObject> objects_in_whip(bool right){
		List<GameObject> objs = new List<GameObject> ();
		Vector2 pos = transform.position;
		Vector2 size = new Vector2 (2.5f, 1.8f);
		Vector2 center = pos;
		if (right) 
			center = pos + new Vector2(1f, .1f);
		else 
			center = pos + new Vector2(-1f, .1f);
		
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("killable")){
			if (within(center, obj.transform.position, size)){
				objs.Add (obj);
			}
		}
		return objs;
	}

	void bomb(GameObject bomb){
		if (Math.Abs (transform.position.x-bomb.transform.position.x) < 1
		    && Math.Abs (transform.position.y-bomb.transform.position.y) < 1) {
			take_damage(bomb);
		}
	}

	bool within(Vector2 center, Vector2 comp, Vector2 range){
		if (Math.Abs(center.x-comp.x) < range.x 
		    && Math.Abs(center.y-comp.y) < range.y) {
			return true;
		}
		return false;
	}
}