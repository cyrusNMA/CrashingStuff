using UnityEngine;
using System.Collections;

public class ShpereController : MonoBehaviour {

	Rigidbody rig = null;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if( rig != null )
			if (rig.IsSleeping() ) {
				rig.WakeUp();
			}
	}
}
