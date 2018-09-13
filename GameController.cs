using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public Ball[] balls;
	public float rotationSpeed;
	public float maxRotationSpeed;
	public float crystalRotateSpeed;
	public float enemySpawnTime;
	public int enemiesPerSpawn;
	public int maxEnemiesPerSpawn;

	public GameObject heart;
	public GameObject[] crystals;
	public Color[] colors;

	public int score;
	public int highScore;
	public Text scoreTxt;
	public Text highScoreTxt;
	public Text finalScoreTxt;
	public Text newBestTxt;
	public Transform centerTransform;
	public Transform crystalCenter;

	public GameObject gameOverScreen;

	public bool gameOver;
	public AudioClip gameOverSound;

	public List<GameObject> enemies;
	public int maxEnemiesSpawned;

	public GameObject pauseMenu;
	public GameObject settingsMenu;
	public bool paused;

	private int time;
	private Color obstacleColor;
	private EnemySpawner[] enemySpawners;
	private AudioSource audioSource;
	private bool playedSound;
	private bool difficultyIncreased;

	// Use this for initialization
	void Start ()
	{

		pauseMenu.SetActive (false);
		settingsMenu.SetActive (false);
		paused = false;

		difficultyIncreased = false;
		playedSound = false;
		Time.timeScale = 1f;
		score = 0;
		enemySpawners = FindObjectsOfType<EnemySpawner> ();
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = gameOverSound;
		gameOver = false;
		gameOverScreen.SetActive (false);
		newBestTxt.gameObject.SetActive (false);
		enemies = new List<GameObject> ();




	
	}

	// Update is called once per frame
	void Update ()
	{


		if (enemiesPerSpawn < maxEnemiesPerSpawn) {
			if (score % 100 == 0 && score != 0 && !difficultyIncreased) {
				enemiesPerSpawn++;
				difficultyIncreased = true;
			} else if (score % 100 != 0) {
				difficultyIncreased = false;
			}
		}

		if (heart == null) {
			gameOver = true;
		}



		if (!gameOver) {
			if (!paused) {
				if (Input.touchCount > 0) {
					if (Input.GetTouch (0).position.x > Camera.main.pixelWidth / 2)
						centerTransform.Rotate (Vector3.back * rotationSpeed);
					else
						centerTransform.Rotate (Vector3.forward * rotationSpeed);
				} else if (Input.GetMouseButton (1)) {
					centerTransform.Rotate (Vector3.back * rotationSpeed);
				} else if (Input.GetMouseButton (0)) {
					centerTransform.Rotate (Vector3.forward * rotationSpeed);
				}
			
				crystalCenter.Rotate (Vector3.forward * crystalRotateSpeed);

					
				if (enemies.Count < maxEnemiesSpawned) {

					if (enemySpawnTime != 0) {
						if ((int)Time.fixedTime % enemySpawnTime == 0 && time != (int)Time.fixedTime) {
							for (int i = 0; i < enemiesPerSpawn; i++) {
								enemySpawners [(int)Random.Range (0, enemySpawners.Length)].SpawnEnemy ();

							}
							time = (int)Time.fixedTime;
						} else if ((int)Time.fixedTime % enemySpawnTime != 0) {
							time = (int)Time.fixedTime;
						}
					}
				}
				scoreTxt.text = "" + score;
			}
		} else {
		
			if (!audioSource.isPlaying && !playedSound) {
				audioSource.Play ();
				playedSound = true;
			}
			
			gameOverScreen.SetActive (true);
			finalScoreTxt.text = "" + score;
		
			if (!PlayerPrefs.HasKey ("HighScore"))
				PlayerPrefs.SetInt ("HighScore", 0);

			if (score > PlayerPrefs.GetInt ("HighScore")) {
				PlayerPrefs.SetInt ("HighScore", score);
				newBestTxt.gameObject.SetActive (true);
			} 

			highScoreTxt.text = "" + PlayerPrefs.GetInt ("HighScore");

			Time.timeScale = 0f;
		}
	}

	public void Restart ()
	{

		SceneManager.LoadScene ("Game");

	}

	public void Pause ()
	{

		Time.timeScale = 0f;
		pauseMenu.SetActive (true);
		paused = true;
	}

	public void OpenSettings ()
	{

		pauseMenu.SetActive (false);
		settingsMenu.SetActive (true);
	}

	public void CloseSettings ()
	{

		settingsMenu.SetActive (false);
		pauseMenu.SetActive (true);
	}

	public void UnPause ()
	{

		pauseMenu.SetActive (false);
		Time.timeScale = 1f;
		paused = false;
	}
}
