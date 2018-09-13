using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health;
	public Color color;
	public float speed;
	public Transform target;
	public Transform heart;
	public AudioClip deathSound;

	private GameController gameController;
	private SpriteRenderer sprRend;
	private float dirX;
	private float dirY;
	private bool eating;
	private AudioSource audioSource;
	private bool playedSound;
	private ParticleSystem deathEffect;
	private bool playedEffect;

	// Use this for initialization
	void Start () {

		playedSound = false;
		playedEffect = false;
		gameController = FindObjectOfType<GameController> ();
		audioSource = GetComponent<AudioSource> ();
		sprRend = GetComponentInChildren<SpriteRenderer> ();
		deathEffect = GetComponentInChildren<ParticleSystem> ();

		var mainEfx = deathEffect.main;
		mainEfx.startColor = color;

		eating = false;

		audioSource.clip = deathSound;
		audioSource.volume = PlayerPrefs.GetFloat ("HostilesVol");

	}
	
	// Update is called once per frame
	void Update () {

		float step = speed * Time.deltaTime;

		if(!eating && !gameController.gameOver){
			if(target == null){

				target = heart;
			}
			transform.position = Vector2.MoveTowards(transform.position, target.position, step);
		}

		if (health <= 0) {

			var em = deathEffect.emission;

			sprRend.enabled = false;
			GetComponent<PolygonCollider2D> ().enabled = false;
			if (!playedEffect) {
				deathEffect.Play ();
				em.enabled = true;
				playedEffect = true;
			}

			if (!audioSource.isPlaying && !playedSound) {
				gameController.score++;
				audioSource.Play ();
				playedSound = true;
			}
			if (!audioSource.isPlaying && playedSound) {
				em.enabled = false;
				deathEffect.Stop ();
				gameController.enemies.Remove (this.gameObject);
				Destroy (this.gameObject);
			}

		}
		
	}


	void OnTriggerStay2D(Collider2D other){
	
		if (other.tag == "Bullet") {

			if (other.GetComponent<SpriteRenderer> ().color == color) {

				health -= other.GetComponent<Bullet> ().damage;
				other.GetComponent<Bullet> ().health--;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
	
		 if (other.tag == "Crystal") {
			eating = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
	
		if (other.tag == "Crystal") {
			eating = false;
		}
	}
}
