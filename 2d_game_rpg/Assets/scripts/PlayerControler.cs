using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	//for movement
	public float speed = 1.0f;
	
	private Rigidbody2D myRigidBody;


	//for rotate to mouse
	public enum SpriteRotation
	{
		Up = -90, 
		Right = 0, 
		Down = 90, 
		Left = 180
	}
	public Camera currentCamera;
	public SpriteRotation initialRotation;
	
	private Vector2 _direction;
	private Vector2 _mousePosition;
	
	private Transform _transform;
	private float _angle;

	//
	void Start () {
		//movement
		myRigidBody = GetComponent<Rigidbody2D> ();

		//rotation
		_transform = transform;
		
		if (!currentCamera)
			currentCamera = Camera.main;
		//
	}
	
	
	void Update () {
		//movement
		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) {
			myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*speed, myRigidBody.velocity.y);
		
		}
		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) {
			myRigidBody.velocity = new Vector2(myRigidBody.velocity.x ,Input.GetAxisRaw("Vertical")*speed);
			
		}
		if(Input.GetAxisRaw("Horizontal")==0 && Input.GetAxisRaw("Vertical")==0){
			myRigidBody.velocity = new Vector2(0f,0f);
		}
		//rotate to mouse
		_mousePosition = currentCamera.ScreenToWorldPoint (Input.mousePosition);
		_direction = (_mousePosition - (Vector2)_transform.position).normalized;
		
		_angle = Mathf.Atan2 (_direction.y, _direction.x) * Mathf.Rad2Deg + (int)initialRotation -90f;
		_transform.rotation = Quaternion.AngleAxis (_angle, Vector3.forward);

	}
}
