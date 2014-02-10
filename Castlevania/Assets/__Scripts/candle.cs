using UnityEngine;
using System.Collections;
using System.Linq;


public class candle : MonoBehaviour {

	GameObject[] collectables;
	GameObject drop;

	void Awake(){
		//Candles can't be moved
		Physics2D.IgnoreLayerCollision (11,8);
		Physics2D.IgnoreLayerCollision (11,9);

		//Dropped items can't be moved
		Physics2D.IgnoreLayerCollision (12,9);
	}

	void Start () {
		//Randomly choose a collectable to put in this candle
		collectables = Resources.LoadAll("Collectables",typeof(GameObject)).Cast<GameObject>().ToArray();
		int i = Random.Range (0, collectables.Length - 1);
		drop = collectables [i];
	}

	void die(){
		//Drop item and self destruct
		Instantiate (drop, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}