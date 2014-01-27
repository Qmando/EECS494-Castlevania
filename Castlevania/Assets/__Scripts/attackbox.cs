using UnityEngine;
using System.Collections;

public class attackbox : MonoBehaviour {
	public GameObject simon;
	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = simon.transform.position;
	}

	void OnTriggerStay2D(Collider2D collider) {
		GameObject collided_with = collider.gameObject;
		if (collided_with.name == "ghost") {
			simon.SendMessage ("whip_hit", collided_with);
		}
	}
}
