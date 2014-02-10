using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class attackbox : MonoBehaviour {
	public GameObject simon;
	public GameObject thisobj;
	public bool right;
	public List<GameObject> in_trigger;
	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.layer = LayerMask.NameToLayer("ghost");
		transform.position = simon.transform.position;
		//print (LayerMask.LayerToName (gameObject.layer));
	}

	// Because we move the transform every frame, it always triggers as an "enter" action
	void OnTriggerEnter2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		
		if (collided_with.tag == "killable"){
			if (!in_trigger.Contains (collided_with)) {
				in_trigger.Add (collided_with);
			}
		}

	}
	void OnTriggerExit2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		
		if (collided_with.tag == "killable"){
			if (in_trigger.Contains (collided_with)) {
				in_trigger.Remove (collided_with);
			}
		}
	}

	void hit_stuff() {
		foreach (GameObject obj in in_trigger) {
			obj.SendMessage("die");
		}
		in_trigger.Clear ();
	}
}
