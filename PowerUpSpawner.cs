using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	public GameObject[] powerUps;
	public float powerUpSpawnInterval;


	private bool canSpawn;

	// Use this for initialization
	void Start () {

		canSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (canSpawn) {
			StartCoroutine ("SpawnPowerUp");
			canSpawn = false;
		}
	}

	public IEnumerator SpawnPowerUp(){

		Vector2 pos = new Vector2 (Random.Range(0, Camera.main.pixelWidth), transform.position.y);
		pos.x = Camera.main.ScreenToWorldPoint (pos).x;

		Instantiate (powerUps[(int)Random.Range(0,powerUps.Length)], pos, Quaternion.identity, this.transform);

		yield return new WaitForSeconds (powerUpSpawnInterval);

		canSpawn = true;
	}
}
