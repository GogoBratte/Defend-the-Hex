using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Color color;
	public float speed;
	public int damage;
	public int health;

	private SpriteRenderer sprRend;
	private Transform parent;
	private Vector2 vel;
	private GameController gameController;

	// Use this for initialization
	void Start () {
	
		sprRend = GetComponent<SpriteRenderer> ();
		parent = gameObject.transform.parent;
		vel = new Vector2 (parent.position.x, parent.position.y);
		color = parent.GetComponent<Ball> ().color;
		transform.parent = parent.GetComponent<Ball> ().bulletParent;
		gameController = FindObjectOfType<GameController> ();



	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0f) {
			Destroy (this.gameObject);
		}

		if (!gameController.gameOver) {
			sprRend.color = color;
			transform.position = new Vector2 (this.transform.position.x + vel.x * speed * Time.deltaTime, this.transform.position.y + vel.y * speed * Time.deltaTime);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
	
		if (other.tag == "Destroy") {

			Destroy (this.gameObject);
		}
	}
}
