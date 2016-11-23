using UnityEngine;
using System.Collections;

public class OptionsPanelManager : MonoBehaviour {

	public bool isActive;
	public GameObject panel;

	void Start()
	{
		isActive = false;
	}

	void OnEnable()
	{	
		
	}
	
	void Update () {
		
		if(isActive){
            EnableOptionsPanel();
        }else{
            DisableOptionsPanel();
        }
	}

	private void EnableOptionsPanel(){
		panel.SetActive(true);
	}

	private void DisableOptionsPanel(){
		panel.SetActive(false);
	}
}
