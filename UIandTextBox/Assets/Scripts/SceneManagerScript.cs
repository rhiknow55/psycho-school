using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

	public static int sceneID;

	void Start()
	{
		
	}

	public void changeScene(){
		SceneManager.LoadScene(sceneID);
	}
}
