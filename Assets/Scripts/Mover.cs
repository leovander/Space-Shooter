using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	//How fast we want to move an object across the screen
	//+ going up the screen and - going down the screen
	public float speed;

	void Start() {
		//Where to move the object towards
		rigidbody.velocity = transform.forward * speed;
	}
}
