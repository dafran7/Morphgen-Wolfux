using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    Rigidbody2D rb2d;
	Manager gamemanager;
	public float atkMove;
	public float jumpPower;
	public float xforce, yforce;
	public float speed = 0.8f;
	public bool attack =false;
	public bool isJumping=false;
	public bool dbJump = false;
	public bool faceLeft=false;
	public bool invi=false;
	bool waitActive = false;
	public GameObject success, failed;
	public GameObject LifeMan;
	public BoxCollider2D RHitbox;
	public BoxCollider2D LHitbox;

	public SpriteRenderer sr;

	public GameObject mEffectL;
	public GameObject mEffectR;
	public GameObject rEffectL;
	public GameObject rEffectR;


	public bool movel=false,mover=false;

	public Manager manager;
	public int hp;
	public int maxhp;
	public GameObject tx;
	public Text txt;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Score", 0);
		LifeMan = GameObject.Find ("LifeManager");
		txt = tx.GetComponent<Text> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager> ();
		sr = gameObject.GetComponentInChildren<SpriteRenderer> ();
		gamemanager = GameObject.FindObjectOfType<Manager>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();	//Ngambil component rigidbody
		gameObject.GetComponent<Animator> ().enabled = true;
		maxhp = hp;
		StartCoroutine (TimeCount());
	}
	
	// Update is called once per frame
	void Update () {
		if (!manager.pause) {
			if (gameObject.transform.position.y < -10)		//Kalogame object udh dibawah arena main di Destroy
			Dead ();
			if (!waitActive) {
				if (!gamemanager.pause) {
					if (Input.GetKeyDown (KeyCode.Z)) {
						MeleeAttack ();
					} else if (Input.GetKeyDown (KeyCode.X)) {
						RangeAttack ();
					} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
						Jump ();
					} else if (Input.GetKey (KeyCode.LeftArrow)) {
						faceLeft = true;
						MoveLeft (); 
					} else if (Input.GetKey (KeyCode.RightArrow)) {
						faceLeft = false;
						MoveRight ();
					} else if (mover) {		//Buat gerakin object & kontrolnya
						faceLeft = false;
						MoveRight ();

					} else if (movel) {
						faceLeft = true;
						MoveLeft ();

					} else  {
						if (!faceLeft)
							gameObject.GetComponent<Animator> ().Play ("player-idle");
						else
							gameObject.GetComponent<Animator> ().Play ("player-idleleft");
					}
				}
			} else {
				if (Input.GetKeyDown (KeyCode.UpArrow) && (dbJump == false)) {
					StopAllCoroutines ();
					waitActive = false;
					Jump ();
				} else if (mover) {		//Buat gerakin object & kontrolnya
					faceLeft = false;
					transform.Translate (Vector3.right * speed / 20);
				} else if (movel) {
					faceLeft = true;
					transform.Translate (Vector3.left * speed / 20);
				}
			}
		}
    }

	public void ChangeLeft(){
		if (movel)
			movel = false;
		else if (!movel)
			movel = true;
	}
	public void ChangeRight(){
		if (mover)
			mover = false;
		else if (!mover)
			mover = true;
	}

	public void Jump(){										//Fungsi Jump
		if (!dbJump&&isJumping) {
			rb2d.AddForce(new Vector2(rb2d.velocity.x,yforce*jumpPower));
			StartCoroutine(DBJumping());
			dbJump = true;
		}
		if(!isJumping){
			//transform.Translate(Vector3.up * 2.8f);
			rb2d.AddForce(new Vector2(rb2d.velocity.x,yforce*jumpPower));	//Ngasil gaya ke y
			StartCoroutine(Jumping());
			isJumping=true;										//Cek sudah jump apa belum biar ga lompat terus2an
		}
	}

	IEnumerator Jumping()
	{
		waitActive = true;
		if (faceLeft)
			gameObject.GetComponent<Animator>().Play("player-jump");
		else gameObject.GetComponent<Animator>().Play("player-jumpright");
		yield return new WaitForSeconds(0.3f);
		waitActive = false;
	}

	IEnumerator DBJumping()
	{
		waitActive = true;
		if (faceLeft)
			gameObject.GetComponent<Animator>().Play("player-doublejump");
		else gameObject.GetComponent<Animator>().Play("player-doublejumpright");
		yield return new WaitForSeconds(0.68f);
		waitActive = false;
	}

	public void MoveLeft(){
		gameObject.GetComponent<Animator> ().Play ("player-run");
		transform.Translate(Vector3.left * speed/8);
		//rb2d.velocity = new Vector2(-3f, rb2d.velocity.y);		//Move
	}
	public void MoveRight(){
		gameObject.GetComponent<Animator> ().Play ("player-runright");
		transform.Translate(Vector3.right * speed/8);
		//rb2d.velocity = new Vector2(3f, rb2d.velocity.y);		//Move
	}

	public int meleeDmg;

	public void MeleeAttack(){
		StartCoroutine (MAttackEffect ());
		if (faceLeft) {
			rb2d.AddForce (new Vector2 (-atkMove, rb2d.velocity.y));
			faceLeft = false;
		} else {
			rb2d.AddForce (new Vector2 (atkMove, rb2d.velocity.y));
			faceLeft = true;
		}
		StartCoroutine (MAttack ());
	}

	public void RangeAttack(){
		StartCoroutine (RAttack());
	}

	IEnumerator RAttack(){
		
		waitActive = true;
		GameObject x;
		if (faceLeft) {
			gameObject.GetComponent<Animator> ().Play ("player-attackleft");
			x = Instantiate (rEffectL, new Vector2 (gameObject.transform.position.x-2f, gameObject.transform.position.y-.5f), Quaternion.identity);
		} else {
			gameObject.GetComponent<Animator> ().Play ("player-attack");
			x = Instantiate (rEffectR, new Vector2 (gameObject.transform.position.x+2f, gameObject.transform.position.y-.5f), Quaternion.identity);
		}
		yield return new WaitForSeconds (0.52f);


		waitActive = false;
	}

	IEnumerator MAttack(){
		attack = true;
		waitActive = true;
		invi = true;
		if (!faceLeft) {
			LHitbox.enabled = true;
			gameObject.GetComponent<Animator> ().Play ("player-attackleft");
			faceLeft = false;
		} else {
			RHitbox.enabled = true;
			gameObject.GetComponent<Animator> ().Play ("player-attack");
			faceLeft = true;
		}
		yield return new WaitForSeconds (0.585f);

		LHitbox.enabled = false;
		RHitbox.enabled = false;

		waitActive = false;
		attack = false;
		invi = false;

	}

	IEnumerator MAttackEffect(){
		GameObject x;
		if(faceLeft)
			x = Instantiate (mEffectL, new Vector2(gameObject.transform.position.x,gameObject.transform.position.y-.6f), Quaternion.identity);
		else 
			x = Instantiate (mEffectR, new Vector2(gameObject.transform.position.x,gameObject.transform.position.y-.6f), Quaternion.identity);
		

		yield return new WaitForSeconds (0.5f);
		Destroy (x);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Platform"||col.transform.tag=="DPlatform") {
            //StopAllCoroutines();
            dbJump = false;
            isJumping = false;									//Matiin kondisi jumpung kl kena collider dengan tag "Platform"
		}
		if (col.transform.tag == "Goal") {
			success.SetActive (true);							//Muncullin window success
			gamemanager.GamePause();
		}
		if (col.transform.tag == "Apalah") {
			Hit();									//Matiin kondisi jumpung kl kena collider dengan tag "Platform"
		}
		if (col.transform.tag == "Enemy") {
			if (!invi) {
				if (col.gameObject.GetComponent<EnemyBehaviour>().active == true)
				Hit ();
			}
			if (attack) {
				col.gameObject.GetComponent<EnemyBehaviour> ().ReduceHP (meleeDmg);
			}
		}
		if (col.transform.tag == "Core") {
			if (attack) {
				col.gameObject.GetComponent<CoreBehaviour> ().ReduceHP (meleeDmg);
			}
		}
		if (col.gameObject.name == "bawah") {
			Hit ();
			gameObject.transform.position = GameObject.Find ("trg1").transform.position;
		}
	}

	public GameObject[] Enemy;
	void OnTriggerEnter2D(Collider2D col){
		Enemy = GameObject.FindGameObjectsWithTag ("Enemy");

		if (col.transform.tag == "item") {
			if (hp < maxhp) {
				LifeMan.GetComponent<LifeManager> ().re = true;
				Destroy (col.gameObject);
			}
		}
		if (col.gameObject.name == "trg4") {
			foreach (GameObject en in Enemy) {
				Debug.Log ("TES4");
				if (en.GetComponent<EnemyBehaviour> ().id == 4) {
					en.GetComponent<EnemyBehaviour> ().active = true;
					en.GetComponent<EnemyBehaviour> ().sr.enabled = true;
					en.GetComponent<BoxCollider2D> ().enabled = true;
					en.GetComponent<Rigidbody2D> ().isKinematic = false;
				}
			}

		}
		if (col.gameObject.name == "trg1") {
			foreach (GameObject en in Enemy) {
				if (en.GetComponent<EnemyBehaviour> ().id == 1) {
					Debug.Log ("TES1");
					en.GetComponent<EnemyBehaviour> ().active = true;
					en.GetComponent<EnemyBehaviour> ().sr.enabled = true;
					en.GetComponent<BoxCollider2D> ().enabled = true;
					en.GetComponent<Rigidbody2D> ().isKinematic = false;
				}
			}
		}
		if (col.gameObject.name == "trg3") {
			foreach (GameObject en in Enemy) {
				Debug.Log ("TES3");
				if (en.GetComponent<EnemyBehaviour> ().id == 3) {
					en.GetComponent<EnemyBehaviour> ().active = true;
					en.GetComponent<EnemyBehaviour> ().sr.enabled = true;
					en.GetComponent<BoxCollider2D> ().enabled = true;
					en.GetComponent<Rigidbody2D> ().isKinematic = false;
				}
			}
		}
		if (col.gameObject.name == "trg2") {
			foreach (GameObject en in Enemy) {
				Debug.Log ("TES2");
				if (en.GetComponent<EnemyBehaviour> ().id == 2) {
					en.GetComponent<EnemyBehaviour> ().active = true;
					en.GetComponent<EnemyBehaviour> ().sr.enabled = true;
					en.GetComponent<BoxCollider2D> ().enabled = true;
					en.GetComponent<Rigidbody2D> ().isKinematic = false;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.transform.tag == "Platform"||col.transform.tag=="DPlatform") {
			isJumping = true;									//Biar ga bisa jump kalo jatoh dr platform
		}
	}

	void Dead(){
		failed.SetActive (true);
		Destroy (gameObject);
		Time.timeScale = 0f;
	}

	IEnumerator HitNotice(){
		sr.color = Color.red;
		invi = true;
		yield return new WaitForSeconds (.2f);
		invi = false;
		sr.color = Color.white;
	}

	public void Hit(){
		hp--;
		LifeMan.GetComponent<LifeManager> ().hit = true;
		StartCoroutine (HitNotice ());
		if (hp <= 0)
			Invoke ("Dead", 0.3f);
	}

	public bool CheckState(){
		return invi;
	}


	public int sec=0, min=0;
	IEnumerator TimeCount(){
		yield return new WaitForSeconds (1f);
		sec += 1;
		if (sec > 60) {
			min += 1; sec = 0;
		}
		txt.text = min.ToString () + ":" + sec.ToString ();
		StartCoroutine (TimeCount());
	}
}
