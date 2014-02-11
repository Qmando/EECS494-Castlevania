using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform 	poi; // Point of Interest
	public float		u;
	public Vector3		offset = new Vector3(0,2,-5);
	public bool follow_y;
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = poi.position;
		Vector3 curpos = transform.position;
		curpos.x = pos.x;
		if (follow_y) 
			curpos.y = pos.y;
		transform.position = curpos;
	}
}
