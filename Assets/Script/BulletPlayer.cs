using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {
	SpriteRenderer sr;
	public float x, speed;
	Vector2 post;
	public int bulletDmg;

	Manager manager;
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer> ();
		x = gameObject.transform.position.x;
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();
		post = gameObject.transform.position;
			
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!manager.pause) {
			if (Vector2.Distance (post, gameObject.transform.position) > 14)
				Destroy (gameObject);
			if (sr.flipX)
				transform.position = new Vector2 (x -= speed, transform.position.y);
			else
				transform.position = new Vector2 (x += speed, transform.position.y);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy") {
			col.gameObject.GetComponent<EnemyBehaviour> ().ReduceHP (bulletDmg);
		}
		if (col.transform.tag == "Core") {
			col.gameObject.GetComponent<CoreBehaviour> ().ReduceHP (bulletDmg);
		}
		if (col.transform.tag == "Projectile") {
			Debug.Log("BenterB");
			Destroy (col.gameObject);
		}
		Destroy (gameObject);
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.transform.tag == "Projectile") {
			Debug.Log("BstayB");
			Destroy (col.gameObject);
		}
		Destroy (gameObject);
	}
}
