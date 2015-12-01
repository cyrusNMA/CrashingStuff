/* Copyright (C) Cyrus Lam , Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Cyrus Lam <cyrus1127@gmail.com>, Noverber 2015
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameSetting;


public class MainMenuManager : MonoBehaviour {

	Score.ScoreManager _SM;

	public bool Enable_OnScreenDebugLog = false;
	public Text HighScoreLable;
	public Text VersionLable;

	public string bundleVersion = "1.5.7";

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		_SM = Score.ScoreManager.GetInstance();
		
		string getdata_string = _SM.GetData_txt(true);
		SetScore(getdata_string);

		SetVersion(bundleVersion);

		Debug.Log (" levelCount ? " + Application.levelCount);
		for(int i = 0 ; i < Application.levelCount ; i++){
			Debug.Log ("check is CanStreamedLevelBeLoaded "+i+" ? " + Application.CanStreamedLevelBeLoaded(i));
		}

		//Switch on/off on Screen Debug log.
		if( GameObject.FindWithTag("UIDebug") ){
			GameObject.FindWithTag("UIDebug").SetActive(Enable_OnScreenDebugLog);
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetScore( string n_data_txt ){
		//do init text
		HighScoreLable.text = n_data_txt;
		Debug.Log("SetScore() : text ? " + n_data_txt);
	}

	public void SetVersion(string n_version){
		VersionLable.text = 
//			"Version : " + 
				n_version;
	}

	public void StartGame(){

		Debug.Log("StartGame() Called!");

		Application.LoadLevel("Game");
	}

	public void QuitGame(){
		Application.Quit();
	}



}

