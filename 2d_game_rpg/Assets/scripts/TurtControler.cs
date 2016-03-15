using UnityEngine;
using System.Collections;

public class TurtControler : MonoBehaviour {

	private Animator anim;


	public float speed = 1f;
	private Rigidbody2D myRigidBody;
	public GameObject wayPoint;

	private bool moving; 
	private bool rotating;
	private bool isStill;

	public float MaxRestTime = 1f;
	private float restTime;
	private float restTimeCounter;
	
	private float moveTime;
	private float moveTimeCounter;

	public float rad = 1f;
	public Transform wanderPoint;
	private Vector3 target;
	private Vector3 newVel;

	private bool dead;
	private float deadCount;
	public float respawn = 1;
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		myRigidBody = GetComponent<Rigidbody2D> ();

		moving = false;
		rotating = false;
		isStill = false;

		restTime = MaxRestTime; 
		restTimeCounter = restTime;

		moveTime = rad * 3;
		moveTimeCounter = moveTime;

		target = wanderPoint.position;
		//Instantiate (wayPoint, new Vector2 (target.x, target.y), Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {




		if (moving) {

			moveTimeCounter -= Time.deltaTime;

			myRigidBody.velocity = newVel;  

			if( moveTimeCounter < 0 || isStill){
				moving = false;
				isStill = false;
				anim.SetBool("walking", false);
				moveTimeCounter = moveTime;


			}


		} else {
			restTimeCounter -= Time.deltaTime;
			myRigidBody.velocity = new Vector2(0f,0f);


			if(restTimeCounter < 0){
				restTimeCounter = restTime;
				moving = true;

				target = new Vector3(Random.Range(-rad,rad), Random.Range(-rad,rad), 0f) + wanderPoint.position;

				newVel = (target - transform.position).normalized*speed;

				wayPoint.transform.position = target;
				wayPoint.SetActive(true);
				anim.SetBool("walking", true);

			}

		}

	}
	void OnCollisionEnter2D (Collision2D other){
		isStill=true;
		wayPoint.SetActive(false);
	}
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.name == wayPoint.name) {
			isStill = true;
			wayPoint.SetActive (false);
		}

	}
}