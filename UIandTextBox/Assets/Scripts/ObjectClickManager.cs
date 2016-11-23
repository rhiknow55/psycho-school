using UnityEngine;
using System.Collections;

public class ObjectClickManager : MonoBehaviour {

	public string sceneID;
	private ObjectClickManager OCM;
	public static bool canClick;

	private GameObject tbm;
	private TextBoxManager tbmCode;

	void Start()
	{	
		//canClick = false;
		tbm = GameObject.Find("Text Box Manager");
		tbmCode = tbm.GetComponent<TextBoxManager>();
	}

	void OnMouseDown()
	{	
		if(!canClick)
			return;
		else{
			Debug.Log("clicked");
			tbmCode.EnableTextBox();
			GameObject.Find("Manager").GetComponent<Manager>().LoadOptionsData(sceneID);
			canClick = false;
		}
	}
}