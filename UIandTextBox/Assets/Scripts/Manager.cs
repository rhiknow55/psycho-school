using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Manager : MonoBehaviour{

	public GameObject txtScene;
	public GameObject txtOption1;
	public GameObject txtOption2;
	public GameObject txtOption3;
	public GameObject txtOption4;

	GameObject[] textOptions;

	private static Dictionary<string, List<optionNode>> optionsByScenes;
	public static Dictionary<string, List<string>> dialogueByScenes;
	public List<string> dialogueLines;

	private string endNode;

	private optionNode oN;

	void Start () {
		textOptions = new GameObject[] {txtOption1, txtOption2, txtOption3, txtOption4};
		//GameObject.Find("Object Click Manager").GetComponent<ObjectClickManager>().canClick = true;
		ObjectClickManager.canClick = true;
	}
	
	public class optionNode{

		private string optionText;
		private string gotoID;

		public optionNode(string text, string ID){
			optionText = text;
			gotoID = ID;
		}

		public string getText(){
			return optionText;
		}

		public string getID(){
			return gotoID;
		}
	}
	

	public void LoadOptionsData(string ID){
		//if at end of options

		optionsByScenes = new Dictionary<string, List<optionNode>>();
		dialogueByScenes = new Dictionary<string, List<string>>();

		//```````~~~~~MOVE THESE TO START, AND MAKE THEM PRIVATE VARIABLES-----
		TextAsset xmlData = (TextAsset)Resources.Load("practice"); //casts the xml file as text
		XmlDocument xmlDoc = new XmlDocument(); //creates a new xml doc
		xmlDoc.LoadXml(xmlData.text); //loads the text into the xml doc
		/////////////

		foreach (XmlNode scene in xmlDoc["scenes"].ChildNodes) //going through scene nodes
		{	
			string sceneName = scene.Attributes["name"].Value;
			//string sceneChoiceText = scene.Attributes["choiceText"].Value;
			string sceneID = scene.Attributes["ID"].Value;

			if (sceneID == ID){
				Debug.Log(sceneID + ", " + ID);
				endNode = scene["dialogue"].Attributes["endNode"].Value;

				//THE DIALOGUE
			
				dialogueLines = new List<string>();
				foreach (XmlNode line in scene["dialogue"].ChildNodes){
					dialogueLines.Add(line.InnerText);
				}

				dialogueByScenes[sceneName] = dialogueLines;
			}



			List<optionNode> options = new List<optionNode>();
			if (sceneID == ID & endNode == "0") {
				foreach (XmlNode option in scene["options"].ChildNodes){
					oN = new optionNode(option.InnerText, option.Attributes["gotoID"].Value);
					options.Add(oN);
				}
				optionsByScenes[sceneID] = options; //sceneName is key and list of text (innertext) is the value*/

				OptionsText();
			}
			
			if (sceneID == ID & endNode == "-1"){
				Debug.Log(scene["gotoScene"].InnerText);
				SceneManagerScript.sceneID = int.Parse(scene["gotoScene"].InnerText);
				GameObject.Find("Scene Manager").GetComponent<SceneManagerScript>().changeScene();
			}
		}
	}

	private void OptionsText(){
		foreach (KeyValuePair<string, List<optionNode>> optionsByScene in optionsByScenes)
		{
			txtScene.GetComponent<Text>().text = optionsByScene.Key;

			for(int i = 0; i < textOptions.Length; i++){
				textOptions[i].GetComponentInChildren<Text>().text = optionsByScene.Value[i].getText();
			}
		}
		
		
	}

	public void OptionsManage(int buttonNum){
		//if certain option is clicked
		Debug.Log("going to new dialogue");
		foreach (KeyValuePair<string, List<optionNode>> optionsByScene in optionsByScenes){
			//go to new scene
			if (optionsByScene.Value[buttonNum].getID() != null){
				LoadOptionsData(optionsByScene.Value[buttonNum].getID());
				//currently, will repeat the same scene if button without a existant id is clicked 
				//one solution is to have existant scenes for all ids
			}

			GameObject.Find("Text Box Manager").GetComponent<TextBoxManager>().EnableTextBox();
        	GameObject.Find("Options Panel Manager").GetComponent<OptionsPanelManager>().isActive = false;
		}
	}

	public bool canDisplayOptions(){
		if (endNode == "0"){
			return true;
		}else{
			ObjectClickManager.canClick = true;
			return false;
		}
	}
}
