using UnityEngine;
using System.Collections;

public class damangebox : MonoBehaviour {
	public GameObject simon; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = simon.transform.position;
	}

	void OnTriggerEnter2D(Collider2D hit) {
		if (hit.gameObject.tag == "killable"
		    || hit.gameObject.tag == "collectable") {
			simon.SendMessage ("hitby", hit);
		}
	}
}
