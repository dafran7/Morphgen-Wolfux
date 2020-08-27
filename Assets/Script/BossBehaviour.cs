using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
	public int phase = 1;
	public float x, speed;
	public Vector2 basePos;
	public bool goLeft = true, wait=false;

	public GameObject destroyPlatform;
	public GameObject player;

	public GameObject baseSpawner;
	public GameObject baseBullet;

	public GameObject[] secondarySpawners;
	public GameObject secondaryBullet;
	public GameObject homingBullet;
	Manager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();

		basePos = transform.position;
		player = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine (BaseSpawn ());
	}

	public bool newphase = true;
	public bool newphasesecond = true;
	public bool attack = false;
	// Update is called once per frame
	void FixedUpdate () {
		if (!manager.pause) {
			x = gameObject.transform.position.x;
			if (!wait) {
				if (phase == 1) {
					if (goLeft)
						gameObject.transform.position = new Vector2 (x -= speed, transform.position.y);
					else
						gameObject.transform.position = new Vector2 (x += speed, transform.position.y);
				} else if (phase >= 2) {
					gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, new Vector2 (player.transform.position.x, transform.position.y), speed);
					if (phase == 3) {
						if (newphase) {
							

							StartCoroutine (SecondarySpawn (1));
							newphase = false;
						}
					} else if (phase == 4) {
						if (newphasesecond) {
							StopCoroutine (SecondarySpawn (1));
							StartCoroutine (SecondarySpawn (Random.Range (1, 3)));
							newphasesecond = false;
						}
					}
				}
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.transform.tag == "Platform") {
			Debug.Log ("Coll");
			if (goLeft)
				goLeft = false;
			else
				goLeft = true;
		}
	}

	IEnumerator BaseSpawn(){
		if (!manager.pause) {
			Instantiate (baseBullet, baseSpawner.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (1.1f);
		}
		StartCoroutine (BaseSpawn ());
	}

	IEnumerator SecondarySpawn(int i){
		if (i == 1) {
			Debug.Log ("Spawn");
			if (!manager.pause) {
				foreach (GameObject secondarySpawner in secondarySpawners) {
					Instantiate (secondaryBullet, secondarySpawner.transform.position, Quaternion.identity);
				}
				yield return new WaitForSeconds (6f);
			}
			StartCoroutine (SecondarySpawn (Random.Range(1,3)));
		}else{
			if (!manager.pause) {
				foreach (GameObject secondarySpawner in secondarySpawners) {
					Instantiate (homingBullet, secondarySpawner.transform.position, Quaternion.identity);
				}
				yield return new WaitForSeconds (10f);
			}
			StartCoroutine (SecondarySpawn (Random.Range(1,3)));
		}

	}

}
