using UnityEngine;
using System.Collections;

public class CubeSwaperHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetColor( Color n_color ){
		this.GetComponent<Renderer>().material.color = n_color;
	}
}
