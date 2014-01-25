using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform 	poi; // Point of Interest
	public float		u;
	public Vector3		offset = new Vector3(0,2,-5);
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = poi.position + offset;
		transform.position = pos;
	}
}
