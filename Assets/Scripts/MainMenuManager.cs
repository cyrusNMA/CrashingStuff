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
}

