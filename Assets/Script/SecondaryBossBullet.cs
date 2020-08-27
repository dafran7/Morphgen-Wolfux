using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryBossBullet : MonoBehaviour {
	Vector2 direction;
	Vector2 targetPos;
	public float power;
	Rigidbody2D rb;
	Manager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();

		rb = gameObject.GetComponent <Rigidbody2D> ();
		targetPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		direction = targetPos - new Vector2(transform.position.x,transform.position.y);
		StartCoroutine (Shoot ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Player") {
			bool playerState = col.gameObject.GetComponent<Player>().invi;
			if(!playerState)
				col.gameObject.GetComponent<Player> ().Hit ();
		}
		if (col.transform.tag != "Projectile")
			Destroy (gameObject);
	}
		
	IEnumerator Shoot(){
		if (!manager.pause) {
			yield return new WaitForSeconds (0.2f);
			rb.AddForce (direction * power);
		}
	}
}
