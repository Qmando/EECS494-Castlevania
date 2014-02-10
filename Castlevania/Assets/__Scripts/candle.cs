using UnityEngine;
using System.Collections;
using System.Linq;


public class candle : MonoBehaviour {

	GameObject[] collectables;
	GameObject drop;

	void Start () {
		collectables = Resources.LoadAll("Collectables",typeof(GameObject)).Cast<GameObject>().ToArray();
		print ("Collectables start");
		foreach (GameObject c in collectables){
			print (c.name);
		}
		print ("Collectables end");
		int i = Random.Range (0, collectables.Length - 1);
		drop = collectables [i];
		print (drop.name);
	}

	void die(){
		Instantiate (drop, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}