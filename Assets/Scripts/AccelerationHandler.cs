using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccelerationHandler : MonoBehaviour {

	public Text UIDebugLabel;
	public GameObject tester = null;

	void Awake(){
		if( tester == null )
			tester = new GameObject("AccelerationCube");

	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){

		

		
//		tester.transform.position = GetAcceleration();
	}

	public Vector2 GetAcceleration(){
		Vector3 acc = new Vector3( RoundUp(Input.acceleration.x - 0.1f), Input.acceleration.y + 0.4f , RoundUp(Input.acceleration.z + 1f) );
		Vector2 reculca = new Vector2( acc.x , acc.y);

		// on Screen Debug.Log
		if( UIDebugLabel != null ){
			UIDebugLabel.text = acc.ToString()
				+ "\n" + reculca.ToString();
		}

		return reculca;
	}

	float RoundUp( float _val ){
		int offset = 8;
		float _pow = 100f;
		float n_val = _val * _pow ;


		if( n_val % offset == 0){
			return n_val / _pow;
		}else{
			n_val -= n_val % offset;
		}

		return n_val / _pow;
	}
}
