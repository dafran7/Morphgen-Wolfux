using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {
	public int hp,score, id;
	public float dist;
	public GameObject player;
	public bool attack=false;
	public bool faceLeft = true;
	public bool active;
	public float speed = 0.8f;

	public SpriteRenderer sr;
	public GameObject bulletL;
	public GameObject bulletR;
	public GameObject blt;
	Manager manager;

	public RectTransform hpfill;
	public Canvas hpthing;
	public GameObject scoreText;
	public int maxhp;
	Text scrTxt;
	// Use this for initialization
	void Start () {
		maxhp = hp;

		scoreText = GameObject.Find("ScoreText");
		scrTxt = scoreText.GetComponent<Text> ();
		score = hp * 10;
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		sr = gameObject.GetComponentInChildren<SpriteRenderer> ();
		//gameObject.transform.position = new Vector2 (10, -2.25f);
		//blt = GameObject.Find ("bullet");
	}

	bool run = false;
	bool walk = false;
	bool initiate = true;
	float post;
	// Update is called once per frame
	void Update () {
		if (active) {
			if (initiate) {
				Vector2 insPos = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 1.5f);
				Canvas x = Instantiate (hpthing, insPos, Quaternion.identity);
				x.transform.SetParent (gameObject.transform);
				hpfill = x.transform.GetChild (0).GetChild(0).GetComponent<RectTransform> ();
				initiate = false;
			}
			if (!manager.pause) {
				dist = Vector2.Distance (gameObject.transform.position, player.transform.position);
				post = gameObject.transform.position.x - player.transform.position.x;
				if (post > 0)
					faceLeft = true;
				else
					faceLeft = false;
				if (id > 0) {
					if (dist <= 5 && !attack) {
						if (faceLeft)
							gameObject.GetComponent<Animator> ().Play ("enemy1-shoot");
						else
							gameObject.GetComponent<Animator> ().Play ("enemy1-shootright");
					} else {
						if (dist > 5) {
							walkRun ();
						} else {
							if (faceLeft)
								gameObject.GetComponent<Animator> ().Play ("enemy1-idle");
							else
								gameObject.GetComponent<Animator> ().Play ("enemy1-idleright");
							run = false;
							walk = false;
						}
					}
				} else {
					if (dist <= 10 && !attack) {
						if (faceLeft)
							gameObject.GetComponent<Animator> ().Play ("enemy1-shoot");
						else
							gameObject.GetComponent<Animator> ().Play ("enemy1-shootright");
					} else {
						if (dist > 10) {
							walkRun ();
						} else {
							if (faceLeft)
								gameObject.GetComponent<Animator> ().Play ("enemy1-idle");
							else
								gameObject.GetComponent<Animator> ().Play ("enemy1-idleright");
							run = false;
							walk = false;
						}
					}
				}
			}
		} 
	}

	void walkRun(){
		if (id > 0) {
			if (dist > 7.01f) {
				walk = false;
				if (!run) {
					if (faceLeft)
						gameObject.GetComponent<Animator> ().Play ("enemy1-run");
					else
						gameObject.GetComponent<Animator> ().Play ("enemy1-runright");
					run = true;
				}
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, 0.09f*speed/2);
				Debug.Log ("Movetoward-Run");
			} else {
				run = false;
				if (!walk) {
					if (faceLeft) {
						gameObject.GetComponent<Animator> ().Play ("enemy1-walk");
					} else {
						gameObject.GetComponent<Animator> ().Play ("enemy1-walkright");
					}
					walk = true;
				}
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, 0.03f*speed/2);
				Debug.Log ("Movetoward-Walk");
			}
		} else {
			if (dist > 12.6f) {
				walk = false;
				if (!run) {
					if (faceLeft)
						gameObject.GetComponent<Animator> ().Play ("enemy1-run");
					else
						gameObject.GetComponent<Animator> ().Play ("enemy1-runright");
					run = true;
				}
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, 0.09f * speed / 2);
				Debug.Log ("Movetoward-Run");
			} else {
				run = false;
				if (!walk) {
					if (faceLeft) {
						gameObject.GetComponent<Animator> ().Play ("enemy1-walk");
					} else {
						gameObject.GetComponent<Animator> ().Play ("enemy1-walkright");
					}
					walk = true;
				}
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, 0.03f * speed / 2);
				Debug.Log ("Movetoward-Walk");
			}
		}
	}

	GameObject x;
	void BulletRelease(){
		if (!attack) {
			attack = true;
			if (!faceLeft)
				x = Instantiate (bulletR, blt.transform.position, Quaternion.identity);
			else
				x = Instantiate (bulletL, blt.transform.position, Quaternion.identity);
			StartCoroutine (Shoot ());
		}
	}

	IEnumerator Shoot(){
		//attack = true;
		//x.GetComponent<Collider2D> ().enabled = false;
		//yield return new WaitForSeconds (0.256f);
		x.GetComponent<Collider2D> ().enabled = true;
		yield return new WaitForSeconds (1.89f);
		attack = false;

	}

	IEnumerator HitNotice(){
		sr.color = Color.red;
		yield return new WaitForSeconds (.2f);
		sr.color = Color.white;
	}

	public void ReduceHP(int dmg){
		hp-=dmg;
		float newsize = 1.5f-1.5f * (hp / maxhp);
		hpfill.sizeDelta=new Vector2(hp*0.5f,hpfill.sizeDelta.y);
		//hpfill.rect.width=newsize;
		StartCoroutine (HitNotice());
		if(hp<=0){
			PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + score);
			scrTxt.text = "Score : " + PlayerPrefs.GetInt ("Score");
			Dead ();
		}
	}

	public GameObject Item;
	void Dead(){
		float rand = Random.Range (1, 100);
		if (rand < 70 )//&& rand > 50)
			Instantiate (Item, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);

	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.name == "bawah") {
			Dead ();
		}
	}
}
