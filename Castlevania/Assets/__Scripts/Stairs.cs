using UnityEngine;
using System.Collections;

public struct stair_info {
	public Vector2 pos;
	public int dir;
}

public class Stairs : MonoBehaviour {

	public stair_info info;
	public int stair_dir;

	void Start(){
		info.pos = transform.position;
		info.dir = stair_dir;
	}

	void OnTriggerEnter2D(Collider2D other) {
		print (other.gameObject.name);
		if (other.gameObject.name == "Simon") {
			other.gameObject.SendMessage ("near_stairs", info);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "Simon") {
			other.gameObject.SendMessage ("not_near_stairs", info);
		}
	}
}
