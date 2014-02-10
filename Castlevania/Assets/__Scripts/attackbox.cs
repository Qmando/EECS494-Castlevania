using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class attackbox : MonoBehaviour {
	public GameObject simon;
	public GameObject thisobj;
	public bool right;
	public List<int> in_trigger;
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
			//print ("Killing " + collided_with.name);
			if (!in_trigger.Contains (collided_with.GetInstanceID())) {
				print ("Adding " + collided_with.name);
				in_trigger.Add (collided_with.GetInstanceID());
			}
		}

	}
	void OnTriggerExit2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		
		if (collided_with.tag == "killable"){
			if (in_trigger.Contains (collided_with.GetInstanceID())) {
				print ("Removing " + collided_with.name);
				in_trigger.Remove (collided_with.GetInstanceID());
			}
		}
	}

	void hit_stuff() {
		foreach (int obj_id in in_trigger) {
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("killable")){
				if (obj.GetInstanceID() == obj_id){
					print ("Killing " + obj.name);
					obj.SendMessage("die");
				}
			}
		}
		in_trigger.Clear ();
	}
}
