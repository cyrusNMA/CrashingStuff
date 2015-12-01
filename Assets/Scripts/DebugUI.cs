using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugUI : MonoBehaviour {

	Text DebugLable = null;
	int debugTextLenght = 0;
	string[] split_case = new string[10];
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
		PrintDebug(logString
//		           + " : " 
//		           + stackTrace
		           );
	}


	public void PrintDebug(string n_debugText){

		//Shift the first row out.
		if(debugTextLenght > 10){
			for(int i = 0 ; i < split_case.Length - 1 ; i++){
				split_case[ i ] = split_case [ i + 1 ];
			}
			split_case[0] = n_debugText;
		}else{
			split_case[debugTextLenght] = n_debugText;
		}
		
		DebugLable.text = "";
		for(int i = 0 ; i < split_case.Length ; i++){
			DebugLable.text += "\n " + split_case[i];
		}
		
		debugTextLenght++;


	}
}
