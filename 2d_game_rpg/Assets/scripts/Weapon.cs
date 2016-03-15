using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	private Animator anim;
	private Animation swing;
	public int power = 1;
	private float time;
	private float prevCounter;
	private float counter;

	public GameObject weapon;




	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		swing = GetComponent<Animation> ();
		weapon.SetActive (false);
		time = Time.time;

		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		counter -= Time.deltaTime;
		if(counter <= 0){
			weapon.SetActive(false);
		}

		if (Input.GetMouseButton (0)) {
			weapon.SetActive(true);
			counter = swing.clip.length;

			float newTime = Time.time;
			if (newTime - time > 1 && power < 10) {
				power++;
				time = newTime;
			}

		}
		if (Input.GetMouseButtonUp (0)) {
			anim.SetBool ("attack", true);
			print(power);
			power = 1;

		} else {
			anim.SetBool("attack",false);		
		}


	}
}
