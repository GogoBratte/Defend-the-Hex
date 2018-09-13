using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Color color;
	public Crystal crystal;
	public Enemy enemyPrefab;
	public float rotationZ;
	public float rotationY;

	private Transform target;
	private Quaternion rotation;
	private GameController gameController;
	// Use this for initialization
	void Start () {

		target = crystal.gameObject.transform;
		rotation = Quaternion.Euler (0f, rotationY, rotationZ);
		gameController = FindObjectOfType<GameController> ();
	}

	public void SpawnEnemy(){
	
		GameObject newEnemy = Instantiate (enemyPrefab.gameObject, this.transform.position, rotation, this.transform);
		newEnemy.GetComponent<Enemy> ().target = target;
		newEnemy.GetComponent<Enemy> ().color = color;
		gameController.enemies.Add (newEnemy.gameObject);
	}
}
