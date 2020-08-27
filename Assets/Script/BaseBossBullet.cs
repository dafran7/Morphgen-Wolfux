using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossBullet : MonoBehaviour {
	public float y, speed;
	Manager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();

		y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (!manager.pause) {
			transform.position = new Vector2 (transform.position.x, y -= speed);
			if (y <= -30)
				Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Player") {
			bool playerState = col.gameObject.GetComponent<Player>().invi;
			if(!playerState)
				col.gameObject.GetComponent<Player> ().Hit ();
		}
		Destroy (gameObject);
	}

}
