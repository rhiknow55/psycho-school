using UnityEngine;
using System.Collections;

public class ClickScreen : MonoBehaviour {

	

	void Start () {

	}

	void Update () {

		if(Input.GetMouseButtonDown(0)){
			
			Debug.Log("clicked");
		}
	}
}
