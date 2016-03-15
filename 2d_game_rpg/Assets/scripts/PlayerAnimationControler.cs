using UnityEngine;
using System.Collections;

public class PlayerAnimationControler : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("walking", true);


		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
			anim.SetBool("walking",false);
		}

	}
}
