using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Dead ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Player") {
			col.gameObject.GetComponent<Player> ().Hit ();
		}
	}
	IEnumerator Dead(){
		yield return new WaitForSeconds (2f);
		Destroy (gameObject);
	}
}
