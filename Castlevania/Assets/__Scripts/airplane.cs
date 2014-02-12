using UnityEngine;
using System.Collections;

public class airplane : MonoBehaviour {
	public float next_bomb=0;
	public GameObject bomb_obj;
	public GameObject simon;
	// Use this for initialization
	void Start () {
		next_bomb = 9999f;
	}
	
	// Update is called once per frame
	void Update () {
		if (next_bomb == 9999f
		    && simon.transform.position.x < 5
		    && simon.transform.position.y > 12){
			next_bomb = 0;
		}

		// Drop bomb!
		if (Time.time > next_bomb) {
			next_bomb = Time.time + Random.Range (1, 4);
			GameObject bomb = (GameObject) Instantiate( bomb_obj );
			bomb.transform.position = transform.position;
		}

		// Move
		Vector2 pos = transform.position;
		pos.x += 7 * Time.deltaTime;
		//Wrap
		if (pos.x > 55) {
			pos.x = -5;
		}
		transform.position = pos;


	}
}
