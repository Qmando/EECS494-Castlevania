using UnityEngine;
using System.Collections;

public class spawnGhosts : MonoBehaviour {

	public GameObject simon;
	public bool from_behind = false;
	Object ghost;

	void Start () {
		simon = GameObject.Find ("Simon");
		ghost = Resources.Load("ghost");
		InvokeRepeating ("instantiate_ghosts", 0.0f, 5.0f);
	}
	
	void instantiate_ghosts() {
		Vector3 pos = new Vector3 (simon.transform.position.x, 0.2912f, 0.0f);
		pos.x += 10;
		Instantiate (ghost, pos, Quaternion.identity);
		pos.x += 2;
		Instantiate (ghost, pos, Quaternion.identity);
		pos.x += 3;
		Instantiate (ghost, pos, Quaternion.identity);
	}
}
