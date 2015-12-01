/* Copyright (C) Cyrus Lam , Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Cyrus Lam <cyrus1127@gmail.com>, Noverber 2015
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameSetting;

public class GameManager : MonoBehaviour {

	int initial_cube_count = 0;
	float timerTime_ready = 3f;
	float timerTime = 60f;
	float timeCountTime = 0f;
	Vector2 spawerArea = new Vector2(40, 40);
	List<Cube.CubeProp> cubeProp_list;

	int hit_type_b4 = -1; //initially is in null, so set it to -1
	int continue_bonus = 0;

	public GameObject HintsLabel = null;
	public GameObject FinishLabel = null;
	public Text TimerLabel = null;
	public Text ScoreLabel = null;
	public Text FinalScoreLabel = null;
	public int cude_rand_cnt = 0;
	public GameObject prefab_cube = null;
	public ShpereController sphere = null;
	public Animation anim_countDown = null;
	
	Score.ScoreManager _SM;

	
	enum GameState{ready , onplay , end};
	GameState state;

	// Use this for initialization
	void Start () {

		_SM = Score.ScoreManager.GetInstance();

		//Hide textLables
		TimerLabel.gameObject.SetActive(false);
		ScoreLabel.gameObject.SetActive(false);

		//Load csv data file
		TextAsset data_text = Resources.Load("Data") as TextAsset;
		string[] datas;
		cubeProp_list = new List<Cube.CubeProp>();

		//Parse datas
		if( data_text.text != "" ){
			datas = data_text.ToString().Split("\n"[0]);
			foreach( string data in datas ){
				string[] split_data = data.ToString().Split(","[0]);

				switch(split_data[0]){
				case "cude_rand_cnt":
					initial_cube_count = int.Parse(split_data[1]);
					break;
				case "cube": 
					int hex_decimal = int.Parse(split_data[2]);
					Color hex_ = ToColor(hex_decimal);

					cubeProp_list.Add ( new Cube.CubeProp() );
					cubeProp_list[cubeProp_list.Count-1].type_id = cubeProp_list.Count-1;
					cubeProp_list[cubeProp_list.Count-1].score = int.Parse(split_data[3]);
					cubeProp_list[cubeProp_list.Count-1]._color = hex_;
					break;
				}
			}

			//Set Animation just play once time.
//			anim_countDown.wrapMode = WrapMode.Once;

			//As sphere should able to be control when game is start. set as disable.
			sphere.SetEnable(false);

			//pre-set the gameState on ready
			state = GameState.ready;
			
			//Do 3 secounds count down before game start
			timeCountTime = timerTime_ready;
			
			//Pre-gen the 10 cubes
			for(int i = 0 ; i < initial_cube_count ; i++)
				creat_cude();

		}else{
				Debug.LogError( "Data file is missing!!" );
		}


	}

	// Update is called once per frame
	void Update () {

		switch(state){
		case GameState.ready:

			//Time count down
			if( timeCountTime - Time.deltaTime > 0 ){
				timeCountTime -= Time.deltaTime;
			}else{
				timeCountTime = timerTime;
				state = GameState.onplay;
				sphere.SetEnable(true);

				TimerLabel.gameObject.SetActive(true);
				ScoreLabel.gameObject.SetActive(true);
				HintsLabel.SetActive(false);
			}

			break;
		case GameState.onplay:

			//Time count down
			if( timeCountTime - Time.deltaTime > 0 ){
				timeCountTime -= Time.deltaTime;
			}else{
				timeCountTime = 0;
				state = GameState.end;
			}

			//update UI Timer
			UpdateTimer(timeCountTime);

			break;
		case GameState.end:

			FinishLabel.SetActive(true);
			FinalScoreLabel.gameObject.SetActive(true);

			//Set Score
			PlayerPrefs.SetString( GameSetting.ID.SCORE_ID ,_SM.GetData_txt());
			sphere.SetEnable(false);
			_SM.SaveData();

			StartCoroutine(EndGame_BackToMenu());
			break;
		}
	}

//	public void StartGame(){
//		//get data file
//		state = GameState.onplay;
//
//	}
	

	void UpdateTimer( float n_time ){
		TimerLabel.text = (int)n_time + "s";
	}

	void UpdateScore( int n_score ){

		//update text Lable
		ScoreLabel.text = "Score : " + _SM.ScoreUpdate(n_score) + ( _SM.IsTheHighestScore()? "\nNew Record!!" : "" );

		UpdateFinalScore();
	}

	void UpdateFinalScore(){
		FinalScoreLabel.text = "Score You Get:\n" + _SM.GetCurrentScore();
	}

	public void CudeDistoried( int n_type , int n_score ){

		if( state == GameState.end )
			return;

		//Do culcate with combom
		if(hit_type_b4 == n_type){
			continue_bonus += 1;
		}else{
			hit_type_b4 = n_type;
			continue_bonus = 0;
		}

		n_score = n_score + (n_score * continue_bonus);

		//Get score datas
		UpdateScore( n_score );

		//call 
		StartCoroutine(Grand_Cude());

	}

	void creat_cude(){
		if( prefab_cube != null ){
			GameObject n_cude = (GameObject)GameObject.Instantiate(prefab_cube , new Vector3(Random.value * spawerArea.x - (spawerArea.x/2) , 2 , Random.value * spawerArea.y  - (spawerArea.y/2)),Quaternion.identity );

			//Set Detail
			Cube.CubeControl cube_ctl = n_cude.AddComponent<Cube.CubeControl>();
			int rand_idx = Random.Range(0 ,cubeProp_list.Count);
			cube_ctl.Setup_cube(cubeProp_list[rand_idx].type_id ,
			                    cubeProp_list[rand_idx]._color ,
			                    cubeProp_list[rand_idx].score ,
			                    this);
		}
	}

	//Do random generate more cude
	IEnumerator Grand_Cude(){
		int rand_dice = Random.Range(0,50);

		if( rand_dice > 45 ){
			yield return new WaitForSeconds(1);
			creat_cude();
		}

		yield return new WaitForSeconds(1);
		creat_cude();
	}

	//Do random generate more cude
	IEnumerator EndGame_BackToMenu(){
		yield return new WaitForSeconds(3);
//		Application.LoadLevel(0);
		Application.LoadLevel("MainMenu");
	}

	//convert the Decimal Hex value to color for use
	Color ToColor(int HexVal)
	{
		byte R = (byte)((HexVal >> 16) & 0xFF);
		byte G = (byte)((HexVal >> 8) & 0xFF);
		byte B = (byte)((HexVal) & 0xFF);
		return new Color(R, G, B, 255);
	}

}



