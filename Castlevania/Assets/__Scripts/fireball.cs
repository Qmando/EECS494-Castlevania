using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D with){
		print (with.gameObject.tag);
		if (with.gameObject.tag == "Player")
			Destroy (this.gameObject);
	}
}