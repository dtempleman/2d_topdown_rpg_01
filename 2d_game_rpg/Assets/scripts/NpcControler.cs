using UnityEngine;
using System.Collections;

public class NpcControler : MonoBehaviour {
	private Animator anim;

	public bool still;
	public bool wandering;
	public bool patrolling;

	public float chaceDist = 1f;
	public float speed = 1f;
	public float rotSpeed = 4f;
	private Rigidbody2D myRigidBody;
	public GameObject wayPoint;
	
	private bool moving; 
	private bool rotating;
	private bool isStill;
	private bool inCombat;

	private float combatTime = 10;
	private GameObject oponent;

	public float MaxRestTime = 1f;
	private float restTime = 1f;
	private float restTimeCounter;
	public bool fixedRest;

	public float rad = 1f;
	public Transform wanderPoint;
	private Vector3 target;
	private Vector3 newVel;

	private Quaternion rot;


	public Transform[] path = new Transform[100];
	private int pathCount = 0;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		restTime = MaxRestTime;
		moving = true;
		rotating = true;
		isStill = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(inCombat){
			Combat();
	
		}else if (still) {

		} else if (wandering) {
			Wander ();
		} else if (patrolling) {
			Patrol();
		}
	}

	void Wander(){
		if (rotating) {
			Rotate();

		}else if (moving) {

			myRigidBody.velocity = newVel;  
			
			if(isStill){
				moving = false;
				isStill = false;
				anim.SetBool("walking", false);
		
			}
				
		} else {
			restTimeCounter -= Time.deltaTime;
			myRigidBody.velocity = new Vector2(0f,0f);
						
			if(restTimeCounter < 0){
				restTimeCounter = restTime;
				if(!fixedRest){
					restTime = Random.Range(1,MaxRestTime);
				}


				moving = true;
				
				target = new Vector3(Random.Range(-rad,rad), Random.Range(-rad,rad), 0f) + wanderPoint.position;
				
				newVel = (target - transform.position).normalized*speed;

				Vector3 dir = (target - transform.position);

				float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg -90f;
				rot = Quaternion.AngleAxis(angle, Vector3.forward);

				rotating = true;

				wayPoint.transform.position = target;
				wayPoint.SetActive(true);
				
			}
			
		}

	}

	void Rotate(){
		Quaternion prev = transform.rotation;
		transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
		Quaternion after = transform.rotation;

		if(prev == after){
			Vector3 dir = (target - transform.position);
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg -90f;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			anim.SetBool("walking", true);
			rotating = false;
		}
	}

	void Patrol(){
		if (rotating) {
			Rotate();
			
		}else if (moving) {
			
			myRigidBody.velocity = newVel;  
			
			if(isStill){
				moving = false;
				isStill = false;
				anim.SetBool("walking", false);

			}
			
			
		} else {
			restTimeCounter -= Time.deltaTime;
			myRigidBody.velocity = new Vector2(0f,0f);
			
			
			if(restTimeCounter < 0){
				restTimeCounter = restTime;
				restTime = Random.Range(1,MaxRestTime);
				
				moving = true;
				
				target = path[pathCount].position;
				pathCount = (pathCount+1)%path.Length;

				newVel = (target - transform.position).normalized*speed;
	
				Vector3 dir = (target - transform.position);
				
				float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg -90f;
				rot = Quaternion.AngleAxis(angle, Vector3.forward);
				
				rotating = true;
				
				wayPoint.transform.position = target;
				wayPoint.SetActive(true);
				
			}
			
		}
	
	}

	void Combat(){
		combatTime -= Time.deltaTime;
		target = oponent.transform.position;
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
		Vector3 dir = target - transform.position;

		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg -90f;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		if(combatTime < 0){
			inCombat = false;
		}
	}






	void OnCollisionEnter2D (Collision2D other){
		if(!rotating || !inCombat){
			isStill=true;
			wayPoint.SetActive(false);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject == wayPoint) {
			isStill = true;
			wayPoint.SetActive (false);
		}else if(other.gameObject.name == "Sword"){
			inCombat = true;
			combatTime = 10;
			oponent = other.gameObject.transform.parent.transform.parent.gameObject;
		}
		
	}
}
