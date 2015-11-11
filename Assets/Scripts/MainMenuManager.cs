using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameSetting;


public class MainMenuManager : MonoBehaviour {

	Score.ScoreManager _SM;

	public Text HighScoreLable;

	void Awake(){
		_SM = Score.ScoreManager.GetInstance();

		string data = PlayerPrefs.GetString(GameSetting.ID.SCORE_ID);
		_SM.SetData(data);
		string getdata_string = _SM.GetData_txt();
		Debug.Log (" MainMenuManager > Awake() : getdata_string ? " + getdata_string);


//		//parse string


	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {}

	public void SetScore( string[] n_datas ){
		//do init text
		HighScoreLable.text = "";
	}

	public void StartGame(){
		Application.LoadLevel(1);
	}	
}

