using UnityEngine;
using System.Collections;

public class merman : MonoBehaviour {

	Vector2 upward_vec;
	bool spawning = true; 
	public float upward_vel;
	Object fireball;
	public GameObject simon;

	void Start() {
		upward_vec = new Vector2 (0, upward_vel);
		fireball = Resources.Load ("fireball");
		simon = GameObject.Find ("Simon");
		InvokeRepeating ("shoot_fireballs", 0.0f, 1.0f);
		InvokeRepeating ("move", 0.0f, 3.0f);
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y < 12 && spawning) {
			rigidbody2D.velocity = upward_vec;
		}
		else {
			GetComponent<BoxCollider2D> ().enabled = true;
			spawning = false;
		}
	}

	void move() {
		if (!spawning) {
			if (simon.transform.position.x > transform.position.x)
				rigidbody2D.velocity = new Vector2 (-2f, 0);
			else
				rigidbody2D.velocity = new Vector2 (2f, 0);
		}
	}

	void shoot_fireballs() {
		if (!spawning) {
			Vector3 pos = new Vector3(transform.position.x,transform.position.y+1.5f, transform.position.z);
			GameObject fb = (GameObject)Instantiate (fireball, transform.position, Quaternion.identity);
		}
	}
}