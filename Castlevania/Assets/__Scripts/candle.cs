using UnityEngine;
using System.Collections;


public class candle : MonoBehaviour {

	GameObject[] collectables;
	GameObject drop;

	void Start () {
		collectables = GameObject.FindGameObjectsWithTag("collectable");
		int i = Random.Range (0, collectables.Length - 1);
		drop = collectables [i];
	}

	void die(){
		Instantiate (drop, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}