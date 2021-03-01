using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyControlScript : MonoBehaviour {

	// Reference to Rigidbody2D component of the ball game object
	Rigidbody2D rb;

	// Range option so moveSpeedModifier can be modified in Inspector
	// this variable helps to simulate objects acceleration
	[Range(0.2f, 2f)]
	public float moveSpeedModifier = 0.5f;

	// Direction variables that read acceleration input to be added
	// as velocity to Rigidbody2d component
	float dirX, dirY;

	// Reference to Balls Animator component to control animaations transition
	Animator anim;


	// Variable to allow or disallow movement when ball is alive or dead
	static bool moveAllowed;

	// Variable to allow or disallow movement when ball is alive or dead
	public static bool RotateLeft;
	// Variable to allow or disallow movement when ball is alive or dead
	public static bool RotateRight;

	// Variable to allow or disallow movement when ball is alive or dead
	public int MovementXTest;
	// Variable to allow or disallow movement when ball is alive or dead
	public int MovementYTest;

	// Use this for initialization
	void Start () {


		// Movement is allowed at the start
		moveAllowed = true;

		
		transform.Rotate(new Vector3(0f , 0f , Random.Range(0f, 360f)));
		// Getting Rigidbody2D component of the ball game object
		rb = GetComponent<Rigidbody2D> ();

		// Getting Animator component of the ball game object
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		// Getting devices accelerometer data in X and Y direction
		// multiplied by move speed modifier
		dirX = Input.acceleration.x * moveSpeedModifier;
		dirY = Input.acceleration.y * moveSpeedModifier;
		//dirX = MovementXTest * moveSpeedModifier;
		//dirY = MovementYTest * moveSpeedModifier;

		if (RotateLeft){
			transform.Rotate(new Vector3(0f , 0f , 0.5f));
		}
		if (RotateRight){
			transform.Rotate(new Vector3(0f , 0f , -0.5f));
		}

		

	}

	void FixedUpdate()
	{
		// Setting a velocity to Rigidbody2D component according to accelerometer data
		if (moveAllowed)
		rb.velocity = new Vector2 (rb.velocity.x + dirX, rb.velocity.y + dirY);
	}



	public static void setMoveAllowedToFalse()
	{
		moveAllowed = false;
	}

	// Method to restart current scene
	void RestartScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Scene01");
	}
}
