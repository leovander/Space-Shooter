using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
	//Number of rotations
	public float tumble;

	void Start() {
		//Change the angular velocity to something random within a given radius
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}