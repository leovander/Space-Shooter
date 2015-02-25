using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	//Once an object leaves the boundary object destroy it
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
