using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int cude_rand_cnt = 0;
	public GameObject prefab_cube = null;

	static string SCORE_ID = "score_id";

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(this.gameObject);
		PlayerPrefs.GetString(SCORE_ID);
	}
	
	// Update is called once per frame
	void Update () {


		//update score
		if(false)
			UpdateScore(0);

	
	}

	public void StartGame(){
		//get data file
		TextAsset data_text = Resources.Load("Data") as TextAsset;
		string[] datas;

		if( data_text.text != "" ){
			datas = data_text.ToString().Split(new string[] { ","} , System.StringSplitOptions.RemoveEmptyEntries);
			foreach( string data in datas){
				Debug.Log ( " data_text ? " + data );
			}
		}else{
			Debug.LogError( "Data file is missing!!" );
		}
	}

	void SetScore(){

	}
	void UpdateScore( int n_score ){

		//sort the score

	}
	
}

public class ScoreRecord{
	string holder_name = "";
	int holder_score = 0 ;

	public string Holder_name{ 
		get{return holder_name;} 
		set{holder_name = value;} 
	}
	public int Holder_score{ 
		get{return  holder_score;} 
		set{holder_score = value;} 
	}
}

