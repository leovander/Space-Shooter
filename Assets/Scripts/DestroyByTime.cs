using UnityEngine;
using System.Collections;

//Used to destroy explosions after a given lifeTime
public class DestroyByTime : MonoBehaviour {

	public float lifeTime;

	void Start () {
		Destroy (gameObject, lifeTime);	
	}
}