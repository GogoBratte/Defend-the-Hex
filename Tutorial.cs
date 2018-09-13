using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public GameObject[] tutorialScreens;
	public bool[] tutorialPhases;

	private int currentTutorialIndex;

	// Use this for initialization
	void Start () {

		currentTutorialIndex = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if (tutorialPhases [currentTutorialIndex]) {

			NextTutorial ();
		}
	}


	public void NextTutorial(){
	
		tutorialScreens [currentTutorialIndex].gameObject.SetActive (false);
		currentTutorialIndex++;
		tutorialScreens [currentTutorialIndex].gameObject.SetActive (true);
	}
}
