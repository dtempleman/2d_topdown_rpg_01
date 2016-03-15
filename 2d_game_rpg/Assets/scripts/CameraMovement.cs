using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	Camera myCam;

	void Start () {
		myCam = GetComponent<Camera> ();

	}

	void Update () {
		myCam.orthographicSize = (Screen.height / 100f) / 4f;
		if (target) {

			transform.position = Vector3.Lerp(transform.position, target.position, 1f) + new Vector3 (0,0,-10);
		}
	}
}
