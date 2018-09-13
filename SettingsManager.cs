using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {

	public sliderValueReader[] settings;


	public float musicVol;
	public float hostilesVol;
	public float shootingVol;
	public float crystalsVol;


	private AudioSource audioSource;

	void Update(){

		audioSource = FindObjectOfType<AudioSource> ();

		musicVol = (float)settings [0].sliderValue / 100f;
		hostilesVol = (float)settings [1].sliderValue / 100f;
		shootingVol = (float)settings [2].sliderValue / 100f;
		crystalsVol = (float)settings [3].sliderValue / 100f;

		audioSource.volume = musicVol;

	}

	public void ApplySettings(){

		PlayerPrefs.SetFloat ("MusicVol", musicVol);
		PlayerPrefs.SetFloat ("HostilesVol", hostilesVol);
		PlayerPrefs.SetFloat ("ShootingVol", shootingVol);
		PlayerPrefs.SetFloat ("CrystalsVol", crystalsVol);
	}
}
