using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	
	void Start () {
		rigidbody2D.velocity = new Vector2 (15,0);;
	}
	
	void OnTriggerEnter2D(Collider2D with){
		if (with.tag == "Player")
			Destroy (this.gameObject);
	}
}