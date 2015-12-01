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

	public Text HighScoreLable;
	public Text VersionLable;

	string bundleVersion = "1.5.5";

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		_SM = Score.ScoreManager.GetInstance();
		
		string getdata_string = _SM.GetData_txt(true);
		SetScore(getdata_string);

		SetVersion(bundleVersion);

		Debug.Log (" levelCount ? " + Application.levelCount);
		
		debugPrint("MainMenuManager:Awake()");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetScore( string n_data_txt ){
		//do init text
		HighScoreLable.text = n_data_txt;
		debugPrint("SetScore() : text ? " + n_data_txt);
	}

	public void SetVersion(string n_version){
		VersionLable.text = 
//			"Version : " + 
				n_version;
	}

	public void StartGame(){

		debugPrint("StartGame() Called!");

//		Application.LoadLevel(1);
		Application.LoadLevel("Game");

	}

	public void QuitGame(){
		Application.Quit();
	}

	void debugPrint( string n_msg ){
//		Debug.Log("  MainMenuManager::debugPrint() ");
		if( GameObject.FindWithTag("UIDebug") ){
			GameObject.FindWithTag("UIDebug").GetComponent<DebugUI>().PrintDebug(n_msg);
		}else{
//			Debug.Log("  MainMenuManager::debugPrint() > DebugUI class not found ");
		}
	}

}

