using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public Vector2 pos;
	public Color color;
	public GameObject bulletPrefab;
	public Transform bulletParent;
	public AudioClip bulletSound;


	public float bulletSpawnTime;
	public float minBulletSpawnTime;
	public float bulletSpeed;
	public float maxBulletSpeed;
	public int bulletDamage;
	public int maxBulletDamage;
	public int bulletHealth;
	public int maxBulletHealth;

	private SpriteRenderer sprRend;
	private GameController gameController;
	private float bulletTime; 
	private Bullet bullet;
	private AudioSource audioSource;
	private Animator anim;
	private bool canFire;

	// Use this for initialization
	void Start ()
	{
		canFire = true;
		gameController = FindObjectOfType<GameController> ();
		sprRend = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();

		bullet = bulletPrefab.GetComponent<Bullet> ();
		audioSource.clip = bulletSound;
		audioSource.volume = PlayerPrefs.GetFloat ("ShootingVol");
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		sprRend.color = color;
		pos = transform.position;



		if (bulletSpawnTime != 0 && !gameController.gameOver) {

			if (canFire) {
				StartCoroutine ("fireBullet");
				canFire = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
	}

	public IEnumerator fireBullet(){
	
		Instantiate (bulletPrefab, this.transform.position, transform.rotation, this.gameObject.transform);
		anim.SetTrigger ("Fire");
		audioSource.Play ();
		bullet.color = color;
		bullet.speed = bulletSpeed;
		bullet.damage = bulletDamage;
		bullet.health = bulletHealth;

		yield return new WaitForSeconds (bulletSpawnTime);

		canFire = true;
	} 
}
