using UnityEngine;
using System.Collections;

public class tank : MonoBehaviour {
	public float next_bomb=0;
	public float shoot_time = 2;
	public GameObject bomb_obj;
	public GameObject simon;
	// Use this for initialization
	void Start () {
		next_bomb = 9999f;
	}
	
	// Update is called once per frame
	void Update () {
		// Start shooting after simon hits a certain pos
		if (next_bomb == 9999f
		    && simon.transform.position.x < 5
		    && simon.transform.position.y > 12){
			next_bomb = 0;
		}

		if (Time.time > next_bomb) {
			next_bomb = Time.time + shoot_time;
			GameObject bomb = (GameObject) Instantiate( bomb_obj );
			bomb.transform.position = transform.position;
			bomb.SendMessage ("shoot");
		}
	}


}
