using UnityEngine;
using System.Collections;

public class ghost : MonoBehaviour {
	public GameObject simon;
	public GameObject health;
	public int moving;
	private Animator animator;

	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
		float simon_x = simon.transform.position.x;
		if (transform.position.x - simon_x < 0)
			moving = 4;
		else
			moving =-4;
		animator = this.GetComponent<Animator>();
			
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 vel = new Vector2 (0, 0);
		vel.x = moving;
		rigidbody2D.velocity = vel;

		animator.SetBool ("dir", (moving > 0));

	}

	void die(){
		Destroy (this.gameObject);
	}

	void OnTriggerExit2D(Collider2D collided) {
		if (collided.gameObject.tag == "platform") 
			moving *= -1;
	}
}
