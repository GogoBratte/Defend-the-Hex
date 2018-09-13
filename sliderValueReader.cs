using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderValueReader : MonoBehaviour {

	public enum Setting
	{
		Music,
		Hostiles,
		Shooting,
		Crystals
	};

	public Setting setting;
	public Slider slider;
	public float sliderValue;

	private Text sliderValueTxt;

	// Use this for initialization
	void Start () {


		if (setting == Setting.Music) {
		
			if (PlayerPrefs.HasKey ("MusicVol")) {
				slider.value = PlayerPrefs.GetFloat ("MusicVol") * 100f;
			} else {
				slider.value = slider.maxValue;
			}
		} else if (setting == Setting.Hostiles) {
		
			if (PlayerPrefs.HasKey ("HostilesVol")) {
				slider.value = PlayerPrefs.GetFloat ("HostilesVol") * 100f;
			} else {
				slider.value = slider.maxValue;
			}
		} else if (setting == Setting.Shooting) {

			if (PlayerPrefs.HasKey ("ShootingVol")) {
				slider.value = PlayerPrefs.GetFloat ("ShootingVol") * 100f;
			} else {
				slider.value = slider.maxValue;
			}
		} else if (setting == Setting.Crystals) {

			if (PlayerPrefs.HasKey ("CrystalsVol")) {
				slider.value = PlayerPrefs.GetFloat ("CrystalsVol") * 100f;
			} else {
				slider.value = slider.maxValue;
			}
		}
			
		sliderValueTxt = GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {

		sliderValue = slider.value;

		sliderValueTxt.text = "" + sliderValue + "%";
		
	}
}
