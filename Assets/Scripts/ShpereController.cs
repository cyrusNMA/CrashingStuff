/* Copyright (C) Cyrus Lam , Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Cyrus Lam <cyrus1127@gmail.com>, Noverber 2015
 */

using UnityEngine;
using System.Collections;

public class ShpereController : MonoBehaviour {

	bool isEnabled;
	Rigidbody rig = null;

	public AccelerationHandler AccelHandler = null;
	public int move_speed = 100;


	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody>();
		isEnabled = false;
	}

	// Update is called once per frame
	void Update () {

		if( isEnabled ){
			//Movement handling
			if( Input.GetKey(KeyCode.UpArrow) ){
				move_z(move_speed);
			}else if(  Input.GetKey(KeyCode.DownArrow) ){
				move_z(-move_speed);
			}
			
			if( Input.GetKey(KeyCode.LeftArrow) ){
				move_x(move_speed);
			}else if( Input.GetKey(KeyCode.RightArrow) ){
				move_x(-move_speed);
			}

			//Mobile Support
#if UNITY_IOS 
			Debug.Log( "current device is UNITY_IOS" );
			if(AccelHandler != null){

				move_x( (int)(-move_speed * (AccelHandler.GetAcceleration().x * 10f)) );
				move_z( (int)(move_speed * (AccelHandler.GetAcceleration().y * 10f)) );
			}
#elif UNITY_ANDROID
			Debug.Log( "current device is UNITY_ANDROID" );
			if(AccelHandler != null){
				
				move_x( (int)(-move_speed * (AccelHandler.GetAcceleration().x * 10f)) );
				move_z( (int)(move_speed * (AccelHandler.GetAcceleration().y * 10f)) );
			}
#endif
		}
	}

	void move_x( int n_speed ){

		Vector3 n_force = Vector3.left * n_speed;
		Vector3 n_pos = this.transform.position + ( n_speed > 0 ? Vector3.left : Vector3.right  );
		rig.AddForceAtPosition( n_force ,  n_pos);

	}

	void move_z( int n_speed ){
		
		Vector3 n_force = Vector3.forward * n_speed;
		Vector3 n_pos = this.transform.position + ( n_speed > 0 ? Vector3.forward : Vector3.back  );
		rig.AddForceAtPosition( n_force ,  n_pos);
		
	}

	public void SetEnable(bool isEnable){
		this.isEnabled = isEnable;
	}

}
