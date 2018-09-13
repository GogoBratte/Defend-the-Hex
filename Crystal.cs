using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{

	public float health;
	public Color color;
	public GameObject healthBar;
	public Color[] textGradient;
	public AudioClip breakingSound;
	public AudioClip brokenSound;
	public float shakeMagnitude;
	public float shakeDuration;

	private SpriteRenderer sprRend;
	private float fullHealth;
	private AudioSource audioSource;
	private bool playedSound;
	private GameController gameController;
	private Camera cam;
	// Use this for initialization
	void Start ()
	{

		cam = Camera.main;
		
		playedSound = false;
		gameController = FindObjectOfType<GameController> ();
		audioSource = GetComponent<AudioSource> ();
		sprRend = GetComponent<SpriteRenderer> ();
		color = sprRend.color;
		fullHealth = health;

		audioSource.clip = breakingSound;
		audioSource.volume = PlayerPrefs.GetFloat ("CrystalsVol");

		for (int i = 0; i < textGradient.Length; i++) {
		
			textGradient[i].a = 255f;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

			
		if (gameController.gameOver) {
			audioSource.Stop ();
		}

		if (health <= 0) {

			StartCoroutine ("Shake");
			audioSource.clip = brokenSound;
			sprRend.sprite = null;
			healthBar.gameObject.SetActive (false);
			GetComponent<PolygonCollider2D> ().enabled = false;

			if (!audioSource.isPlaying && !playedSound) {
				audioSource.Play ();
				playedSound = true;
			}
			if (!audioSource.isPlaying && playedSound) {
				Destroy (this.gameObject);
				Destroy (healthBar.gameObject);
			}
		}


		if (healthBar.GetComponent<Text> () != null) {
		
			healthBar.GetComponent<Text> ().text = "" + (int)health;

			if (health / fullHealth >= 0.7f)
				healthBar.GetComponent<Text> ().color = textGradient [0];
			else if (health / fullHealth < 0.7f && health / fullHealth >= 0.4f)
				healthBar.GetComponent<Text> ().color = textGradient [1];
			else if (health / fullHealth < 0.4f && health / fullHealth >= 0.1f)
				healthBar.GetComponent<Text> ().color = textGradient [2];
			else if (health / fullHealth < 0.1f)
				healthBar.GetComponent<Text> ().color = textGradient [3];
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
	
		if (other.tag == "Enemy") {
		
			if (other.GetComponent<Enemy> ().color == color)
				health -= 0.1f;
			else
				health -= 0.05f;

			if(!audioSource.isPlaying)
				audioSource.Play ();

		}
	}

	void OnTriggerExit2D(Collider2D other){
	
		if (other.CompareTag ("Enemy")) {
		
			if(audioSource.isPlaying)
				audioSource.Stop ();
		}
	}

	IEnumerator Shake() {

		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < shakeDuration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / shakeDuration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= shakeMagnitude * damper;
			y *= shakeMagnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}
}
