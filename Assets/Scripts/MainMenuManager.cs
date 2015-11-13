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

	void Awake(){
		_SM = Score.ScoreManager.GetInstance();

		string getdata_string = _SM.GetData_txt(true);
		SetScore(getdata_string);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {}

	public void SetScore( string n_data_txt ){
		//do init text
		HighScoreLable.text = n_data_txt;
	}

	public void StartGame(){
		Application.LoadLevel(1);
	}

	public void QuitGame(){
		Application.Quit();
	}
}

