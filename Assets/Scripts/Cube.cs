/* Copyright (C) Cyrus Lam , Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Cyrus Lam <cyrus1127@gmail.com>, Noverber 2015
 */

using UnityEngine;
using System.Collections;

namespace Cube{
	public class CubeProp : MonoBehaviour{
		public int type_id = 0;
		public int score = 0;
		public Color _color = Color.white;
		public AudioClip hitSound;

//		public CubeProp( int n_type_id , int n_score , Color n_color){
//			type_id = n_type_id;
//			score = n_score;
//			_color = n_color;
//
//			Debug.Log ( "CubeProp() : type_id ? " +type_id + " score ? " + score+ " _color ? " + _color.ToString());
//		}
	}

	public class CubeControl : CubeProp {

		private GameManager _GM = null;

		CubeControl(){

		}
		
		public void Setup_cube( int n_type_id , Color n_color , int n_score , GameManager obj){
			type_id = n_type_id;
			score = n_score;
			_GM = obj;

			this.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
			this.GetComponent<Renderer>().material.SetColor("_Color", n_color);
		}
		
		void OnTriggerEnter(Collider other) {

			if(other.name == "Sphere"){

				if( this.GetComponent<AudioSource>().clip != null )
				{
					this.GetComponent<AudioSource>().mute = false;
					this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
					Debug.Log ( "play audioClip" );
				}else{
					Debug.Log ( "No audioClip found" );
				}

				_GM.CudeDistoried(type_id,score);

				DestroyObject(this.gameObject);
			}
		}
	}

}

