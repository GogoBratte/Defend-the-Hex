using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

	public enum powerType
	{
		bulletSpeedUp,
		bulletCountUp,
		bulletDamageUp,
		rotationSpeedUp,
		bulletHealthUp
	};

	public powerType power;


	private GameController gameController;
	private Ball[] balls;


	// Use this for initialization
	void Start ()
	{

		balls = GameObject.FindObjectsOfType<Ball> ();
		gameController = FindObjectOfType<GameController> ();

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.touchCount > 0) {
		
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), (Vector2)transform.position) < transform.localScale.x / 2) {
				activatePowerUp (power);
			}
		} else if (Input.GetMouseButtonDown (0)) {
		
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), (Vector2)transform.position) < transform.localScale.x / 2) {

				activatePowerUp (power);
			}
		}

	}


	public void activatePowerUp (powerType power)
	{

		if (power == powerType.bulletCountUp) {

			for (int i = 0; i < balls.Length; i++) {
			
				if(balls[i].bulletSpawnTime > balls[i].minBulletSpawnTime)
					balls [i].bulletSpawnTime -= 0.1f;
			}

		} else if (power == powerType.bulletDamageUp) {

			for (int i = 0; i < balls.Length; i++) {

				if(balls[i].bulletDamage < balls[i].maxBulletDamage)
					balls [i].bulletDamage++;
			}
		
		} else if (power == powerType.bulletSpeedUp) {

			for (int i = 0; i < balls.Length; i++) {

				if (balls [i].bulletSpeed < balls [i].maxBulletSpeed)
					balls [i].bulletSpeed += 0.1f;
			}
			
		} else if (power == powerType.bulletHealthUp) {

			for (int i = 0; i < balls.Length; i++) {

				if (balls [i].bulletHealth < balls [i].maxBulletHealth)
					balls [i].bulletHealth++;
			}

		} else if (power == powerType.rotationSpeedUp) {

			if (gameController.rotationSpeed < gameController.maxRotationSpeed) {
				gameController.rotationSpeed += 0.1f;
			}
		}

		Destroy (this.gameObject);
	}
}
