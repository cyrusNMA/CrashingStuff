using UnityEngine;
using System.Collections;

public class GameBoardController : MonoBehaviour {

	public bool enable = true;

	float max_angle = 35f;
	float axie_x = 0;
	float axie_z = 0;
	Vector3 ray_move;

	// Use this for initialization
	void Start () {
		//do reset
		axie_x = 0;
		axie_z = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(!enable)
			return;

		if( Input.GetMouseButtonDown(0) ){
			ray_move = Input.mousePosition;
			Debug.Log ( "OnMouseMove > mousePosition :  " +  ray_move);
		}

		//console control
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			Rotate_front();

		}else if(Input.GetKeyDown(KeyCode.DownArrow)){
			Rotate_back();

		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			Rotate_left();

		}else if(Input.GetKeyDown(KeyCode.RightArrow)){
			Rotate_right();

		}
	}

	void Rotate_left(){this.transform.Rotate( Vector3.forward , this.transform.rotation.z + 1 );}
	void Rotate_right(){this.transform.Rotate( Vector3.forward , this.transform.rotation.z - 1 );}
	void Rotate_front(){this.transform.Rotate( Vector3.left , this.transform.rotation.x - 1 );}
	void Rotate_back(){this.transform.Rotate( Vector3.left , this.transform.rotation.x + 1 );}
}
