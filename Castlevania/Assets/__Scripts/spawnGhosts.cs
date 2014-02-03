using UnityEngine;
using System.Collections;

public class spawnGhosts : MonoBehaviour {

	public GameObject simon;
	public bool from_behind = false;

	void Start () {
		simon = GameObject.Find ("Simon");
		InvokeRepeating ("spawnGhost", 0.0f, 5.0f);
	}
	
	void spawnGhost() {
		Object ghost = Resources.Load("ghost");
		Vector3 pos = new Vector3 (simon.transform.position.x, 0.2912f, 0.0f);
		if (from_behind) {
			pos.x -= 10;
			from_behind = false;
		}
		else{
			pos.x += 10;
			from_behind = true; 
		}
		Instantiate (ghost, pos, Quaternion.identity);
	}
}
