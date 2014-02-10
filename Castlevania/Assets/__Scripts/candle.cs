using UnityEngine;
using System.Collections;
using System.Linq;


public class candle : MonoBehaviour {

	GameObject[] collectables;
	GameObject drop;

	void Awake(){
		Physics2D.IgnoreLayerCollision (11,8);
		Physics2D.IgnoreLayerCollision (11,9);
		Physics2D.IgnoreLayerCollision (12,8);
		Physics2D.IgnoreLayerCollision (12,9);
		print ("Now ignoring collisions");
	}

	void Start () {
		collectables = Resources.LoadAll("Collectables",typeof(GameObject)).Cast<GameObject>().ToArray();
		int i = Random.Range (0, collectables.Length - 1);
		drop = collectables [i];
	}

	void die(){
		Instantiate (drop, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}