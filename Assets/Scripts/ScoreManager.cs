using UnityEngine;
using System.Collections;

namespace Score{
	public class ScoreManager : MonoBehaviour {

		private int current_score = 0;
		private int array_lenght = 10;
		private ScoreRecord[] datas;
		private static ScoreManager instance;

		public static ScoreManager GetInstance()
		{
			if (!instance)
			{
				GameObject get_inst = ((GameObject)GameObject.FindObjectOfType(typeof(ScoreManager)));
				if (!get_inst){
					get_inst = new GameObject("ScoreManager");
					instance = get_inst.AddComponent<ScoreManager>();
					GameObject.DontDestroyOnLoad(get_inst);
				}
			}
			
			return instance;
		}

		void Awake(){

			//Init the score 
			string data = PlayerPrefs.GetString(GameSetting.ID.SCORE_ID);
			SetData(data);
		}
		
		public void SetData( string txt_data ){

			//new array
			datas = new ScoreRecord[array_lenght];

			//parse txt
			if( txt_data != "" ){
				string[] split_txt = txt_data.Split(new string[]{";"} , System.StringSplitOptions.RemoveEmptyEntries);
				for(int i = 0 ; i < array_lenght ; i ++){
					datas[i] = new ScoreRecord(split_txt[i]);
				}
			}else{
				//init the data
				for(int i = 0 ; i < array_lenght ; i ++){
					datas[i] = new ScoreRecord("Player"+i , 1000 + array_lenght - i);
				}
			}
		}

		public string GetData_txt( bool forView = false ){
			string txt_data = "";

			if( datas != null ){
				foreach(ScoreRecord sr in datas){
					txt_data += sr.GetText(forView) + ( forView ? "\n" : ";");
				}
			}

			return txt_data;
		}

		public int ScoreUpdate(int n_score){
			current_score += n_score;
			return current_score;
		}

		public bool TheHighestScore(){

			if( current_score > datas[0].holder_score)
				return true;

			return false;
		}

		public void SaveData(){
			//Do Sorting 
			for(int i = datas.Length-1  ; i >= 0 ; i--){
				if(current_score > datas[i].holder_score){
					int temp = datas[i].holder_score;
					datas[i].holder_score = current_score;
					//Do interchange
					if( i < datas.Length-1 ){
						datas[i+1].holder_score = temp;	
					}
				}
			}

			PlayerPrefs.SetString(GameSetting.ID.SCORE_ID , GetData_txt());

			//reset
			current_score = 0;
		}

	}

	//A data class to holder a player's data
	public class ScoreRecord{
		public string holder_name = "";
		public int holder_score = 0 ;

		public ScoreRecord( string n_name , int n_score ){
			holder_name = n_name;
			holder_score = n_score;
		}

		public ScoreRecord( string txt_data ){
			string[] split_txt = txt_data.Split(new string[] { ","} , System.StringSplitOptions.RemoveEmptyEntries);
			holder_name = split_txt[0];
			holder_score = int.Parse(split_txt[1]);
		}

		public string GetText( bool forView = false ){
			return holder_name + ( forView ? "\t" : ",") + holder_score;
		}
	}
}
