using UnityEngine;
using System.Collections;

public class spawnMermen : MonoBehaviour {

	public GameObject simon;
	public bool from_behind = false;
	Object merman;

	// Use this for initialization
	void Start () {
		simon = GameObject.Find ("Simon");
		merman = Resources.Load("merman");
		InvokeRepeating ("instantiate_mermen", 0.0f, 5.0f);
	}
	
	void instantiate_mermen () {
		Vector3 pos = new Vector3(Random.Range(118.0f, 138.0f), 1.7f, 0f);
		Instantiate(merman, pos, Quaternion.identity);
	}
}
