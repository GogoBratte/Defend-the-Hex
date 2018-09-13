using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	public AudioClip[] music;
	public bool checkForVolumeChange;

	private AudioSource audioSource;
	private int index;
	private GameController gameController;
	private bool mute;
	// Use this for initialization
	void Start ()
	{

		if(!PlayerPrefs.HasKey("MusicVol"))
			PlayerPrefs.SetFloat ("MusicVol", 1f);

		if(!PlayerPrefs.HasKey("HostilesVol"))
			PlayerPrefs.SetFloat ("HostilesVol", 1f);
		
		if(!PlayerPrefs.HasKey("ShootingVol"))
			PlayerPrefs.SetFloat ("ShootingVol", 1f);

		if(!PlayerPrefs.HasKey("CrystalsVol"))
			PlayerPrefs.SetFloat ("CrystalsVol", 1f);

		mute = false;
		try {
			gameController = FindObjectOfType<GameController> ();
		} catch {
		}
		index = 0;
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = music [index];
		audioSource.volume = PlayerPrefs.GetFloat ("MusicVol");
	

		if (!mute)
			audioSource.Play ();


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (checkForVolumeChange) {
				audioSource.volume = PlayerPrefs.GetFloat ("MusicVol");
		}

		try {
			if (!gameController.gameOver) {
				if (!audioSource.isPlaying && !mute) {
					index++;
					if (index >= music.Length)
						index = 0;
			
					audioSource.clip = music [index];
					audioSource.Play ();
				}
			} else {
				audioSource.Stop ();
			}
		} catch {
		}

	}

	public void Mute ()
	{
	
		mute = !mute;
		if (mute == true) {
			audioSource.Pause ();
		} else {
			audioSource.UnPause ();
		}
	}
}
