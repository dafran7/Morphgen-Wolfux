using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {
	SpriteRenderer sr;
	public float x, speed;
	Vector2 post;
	Manager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();

		sr = gameObject.GetComponent<SpriteRenderer> ();
		x = gameObject.transform.position.x;
		post = gameObject.transform.position;
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		if (!manager.pause) {
			if (Vector2.Distance (post, gameObject.transform.position) > 27)
				Destroy (gameObject);
			if (sr.flipX)
				transform.position = new Vector2 (x -= speed, transform.position.y);
			else
				transform.position = new Vector2 (x += speed, transform.position.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			bool playerState = col.gameObject.GetComponent<Player>().invi;
			if(!playerState)
				col.gameObject.GetComponent<Player> ().Hit ();
			Destroy (gameObject);
		}
		//Destroy (gameObject);
	}


}
