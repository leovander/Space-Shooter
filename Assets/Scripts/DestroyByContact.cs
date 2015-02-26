using UnityEngine;
using System.Collections;

//Applied to enemies for when they collide with other objects
public class DestroyByContact : MonoBehaviour {
	//The explosion animations
	public GameObject explosion;
	public GameObject playerExplosion;

	//How much an enemy is worth
	public int scoreValue;
	//The main gameController associated in the scene
	private GameController gameController;

	void Start() {
		//Find the gameObject that we assigned with the GameController Tag
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		//If it was found, get the GameController
		if(gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();	
		}
		//Shouldn't hit this but it is here just in case
		if(gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	//When an other object enters the enemy
	void OnTriggerEnter(Collider other) {
		//If it is just enteriing the bondary, ignore and just return
		if(other.tag == "Boundary") {
			return;
		} else if(other.tag == "Player") {
			//If it is the player, also play the player explosion animation
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			//Set the game to over
			gameController.GameOver();
		} else {
			//It most likely is a bullet so add to player score
			gameController.AddScore (scoreValue);
		}

		//Call the explosion for the asteroid
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}