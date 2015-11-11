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

	public Text TimerLabel = null;
	public Text ScoreLabel = null;
	public int cude_rand_cnt = 0;
	public GameObject prefab_cube = null;
	public ShpereController sphere = null;
	
	Score.ScoreManager _SM;

	
	enum GameState{ready , onplay , end};
	GameState state;

	// Use this for initialization
	void Start () {

		_SM = Score.ScoreManager.GetInstance();
		_SM.GetData_txt();

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
				Debug.Log ( " data_text ? " + data );
				string[] split_data = data.ToString().Split(","[0]);

				switch(split_data[0]){
				case "cude_rand_cnt":
					initial_cube_count = int.Parse(split_data[1]);
					break;
				case "cube": 
					Debug.Log(" split_data[1]  ? " + split_data[1]);
					Color hex_;
					Color.TryParseHexString(split_data[1] , out hex_);
					Debug.Log ( hex_ );
					cubeProp_list.Add ( new Cube.CubeProp() );
					cubeProp_list[cubeProp_list.Count-1].type_id = cubeProp_list.Count;
					cubeProp_list[cubeProp_list.Count-1].score = int.Parse(split_data[2]);
					cubeProp_list[cubeProp_list.Count-1]._color = hex_;
					break;
				}
			}

			sphere.SetEnable(false);

			//pre-set the gameState on ready
			state = GameState.ready;
			
			//Do 3 secounds count down before game start
			timeCountTime = timerTime_ready;
			
			//Pre-grand the cube
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
			}

			//Do Animated Time counter

//			Debug.Log (" GameState.ready : timeCountTime ? " + timeCountTime);

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
			//Set Score
			PlayerPrefs.SetString( GameSetting.ID.SCORE_ID ,_SM.GetData_txt());
			sphere.SetEnable(false);

			Application.LoadLevel(0);
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

		Debug.Log (" GameManager > UpdateScore() : n_score ? " + n_score);
		n_score = _SM.ScoreUpdate(n_score);

		//update text Lable
		ScoreLabel.text = "Score : " + n_score;

	}

	public void CudeDistoried( int type , int n_score ){
		//Get score datas
		UpdateScore( n_score );

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

	IEnumerator Grand_Cude(){
		int rand_dice = Random.Range(0,50);

		if( rand_dice > 35 ){
			yield return new WaitForSeconds(1);
			creat_cude();
		}

		yield return new WaitForSeconds(1);
		creat_cude();
	}

}



