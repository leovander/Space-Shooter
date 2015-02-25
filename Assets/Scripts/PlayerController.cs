using UnityEngine;
using System.Collections;

//This allows for the Boundary class to be accesible by the front end Unity
[System.Serializable]

//The boundary of the game area and it coordinates
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	//Runs after every frame
	void Update() {
		//When Left Mouse Button or Space Bar is clicked and the time is greater than time plus previous fire rate
		if ((Input.GetButton ("Fire1") || Input.GetKey(KeyCode.Space)) && Time.time > nextFire) {
			//Add current time plus the fireRate
			nextFire = Time.time + fireRate;
			//Create a new shot at the given position and rotation
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			//Play audio file associated with Player Ojbect
			audio.Play ();
		}
	}

	//Function runs after every fixed frame, used for rigid body
	void FixedUpdate() {
		//Gets the inputs for left/right and up/down
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//The direction in which we want to move in with the Y-axis set to 0
		//since we don't want to move up or down the Y-axis
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		//Uses the new coordinates created and moves the character by speed
		rigidbody.velocity = movement * speed;

		//Check to make sure that the new position is within the game boundaries
		rigidbody.position = new Vector3 (
			//This sweet function locks in the value by checking if its between min and max,
			//if its not within it returns the one it is closest to i.e. 10, 1, 3 would return 3
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		//Quaternion helps create rotations, so we rotate in the direction we are going but tilt in the opposite direction
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}