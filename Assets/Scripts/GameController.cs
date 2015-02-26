using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {	
	public GameObject hazard; //Enemy gameobject
	public Vector3 spawnValues; //Where we can potentially an enemy
	public int hazardCount; //How many enemies per wave
	public float spawnWait; //How much to wait before next spawn
	public float startWait; //How much to wait for enemies to spawn
	public float waveWait; //Time between waves

	public GUIText scoreText; //Display text for score
	public GUIText restartText; //Display text for restart
	public GUIText gameOverText; //Display text for game over

	private bool gameOver; //Bool to check if the game is over
	private bool restart; //Bool to see if we are restarting the game
	private int score; //

	void Start() {
		score = 0;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		UpdateScore ();
		//Runs in the background not interrupting the rest of the game
		StartCoroutine (SpawnWaves ());
	}

	void Update() {
		//Check after every frame if restart is set and reload the scene
		if(restart) {
			//Check to see if 'R' is pressed
			if(Input.GetKeyDown(KeyCode.R)) {
				//Load the scene again to start from scratch
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves() {
		//Pause the spawns at the start of the scene
		yield return new WaitForSeconds (startWait);
		//While the game is not over
		while(!gameOver) {
			for (int i = 0; i < hazardCount; i++) {
				//The location of the new object with a random x position between the -x and x as boundaries
				//a y that is 0 and z being 16 outside the view of the boundary
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//Perfectly align the new object to the world
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				//Pause for a bit before sending the next enemy
				yield return new WaitForSeconds (spawnWait);
			}
			//Time to wait between sets of spawns
			yield return new WaitForSeconds (waveWait);
		}

		restartText.text = "Press 'R' for Restart";
		restart = true;
	}

	//Increases score of player by a given point value associated with an enemy
	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}