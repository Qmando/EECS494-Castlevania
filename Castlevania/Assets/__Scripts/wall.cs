using UnityEngine;
using System.Collections;

public class wall : MonoBehaviour {
	void die(){
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}