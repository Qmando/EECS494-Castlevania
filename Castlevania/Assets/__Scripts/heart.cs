using UnityEngine;
using System.Collections;

public class heart : MonoBehaviour {

	public int increase_amt = 0;
	GameObject simon;

	void Start(){
		simon = GameObject.Find ("Simon");
	}

	void OnCollisionEnter2D(Collision2D collider){
		GameObject collided_with = collider.gameObject;
		print (collided_with.tag);
		if (collided_with.tag == "Player") {
			print ("Picked up");
			simon.SendMessage ("increase_hearts", increase_amt);
			Destroy (this.gameObject);
		}
	}
}