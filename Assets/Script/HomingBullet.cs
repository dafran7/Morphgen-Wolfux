using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour {
	public GameObject explosion;

	bool wait=true;
	Manager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();

		StartCoroutine (Homing ());

	}
	
	// Update is called once per frame
	void Update () {
		if (!manager.pause) {
			if (!wait)
				gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, GameObject.FindGameObjectWithTag ("Player").transform.position, speed);
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Player") {
				col.gameObject.GetComponent<Player> ().Hit ();
		}
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
	public float speed;

	IEnumerator Homing(){
		yield return new WaitForSeconds (2f);
		wait = false;
		yield return new WaitForSeconds (3f);
		//Instantiate (explosion, transform.position, Quaternion.identity);
		//Destroy (gameObject);
	}
}
