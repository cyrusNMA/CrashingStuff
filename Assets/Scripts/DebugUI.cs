using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugUI : MonoBehaviour {

	Text DebugLable = null;
	const int logLimit = 15;
	int debugTextLenght = 0;
	string[] split_case = new string[logLimit];
	string tmp_MultiSignal_msg = "";

	void Awake(){
		Application.logMessageReceived += HandleLog;

		DebugLable = this.GetComponent<Text>();
		if(DebugLable == null){
			Debug.LogError( "DebugUI::Awake() > DebugLable is null" );
		}else{
			DebugLable.text = "";
			for(int i = 0 ; i < split_case.Length ; i++){
				split_case[ i ] = "";
			}
		}
//		Debug.Log( "DebugUI::Awake()" );
	}

	// Use this for initialization
	void Start () {
//		Debug.Log( "DebugUI::Start()" );
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void HandleLog (  string logString ,  string stackTrace ,  LogType type ) {
		PrintDebug(logString);
//		           + " : " 
//		           + stackTrace

	}


	public void PrintDebug(string n_debugText){

//		Debug.Log ("debugTextLenght ? " + debugTextLenght ); 

		//Shift the first row out.
		if(debugTextLenght >= logLimit){
			for(int i = 0 ; i < split_case.Length - 1 ; i++){
				split_case[ i ] = split_case [ i + 1 ];
			}
			split_case[split_case.Length - 1] = n_debugText;
		}else{
			split_case[debugTextLenght] = n_debugText;
		}
		
		DebugLable.text = "";
		for(int i = 0 ; i < split_case.Length ; i++){
			DebugLable.text += "\n " + split_case[i];
		}
		
		debugTextLenght += 1;


	}
}
