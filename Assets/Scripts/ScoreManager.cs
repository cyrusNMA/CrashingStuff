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
		
		public void SetData( string txt_data ){
			Debug.Log ("ScoreManager > SetData() : txt_data ? " + txt_data);

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
					datas[i] = new ScoreRecord("Player"+i , 0);
				}
			}
		}

		public string GetData_txt(){
			string txt_data = "";

			if( datas != null ){
				foreach(ScoreRecord sr in datas){
					txt_data += sr.GetText() + ";";
				}
			}

			return txt_data;
		}

		public int ScoreUpdate(int n_score){
			current_score += n_score;
			return current_score;
		}

	}
	
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

		public string GetText(){
			return holder_name + "," + holder_score;
		}
	}
}
