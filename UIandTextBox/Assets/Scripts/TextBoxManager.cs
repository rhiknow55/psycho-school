using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public GameObject mouseObject;

    public Text theText;

    //public TextAsset textFile;
    public List<string> textLines;

    public int currentLine; //what line we're reading
    public int endAtLine; //where to stop reading

    public bool isActive;

    private GameObject ManagerObject;
    private Manager manager;
    private GameObject OPM;
    private OptionsPanelManager opm;

    void Start()
    {
        //import of other gameobjects
        ManagerObject = GameObject.Find("Manager");
        manager = ManagerObject.GetComponent<Manager>();
        textLines = manager.dialogueLines;

        OPM = GameObject.Find("Options Panel Manager");
        opm = OPM.GetComponent<OptionsPanelManager>();

        isActive = false;

        endAtLine = textLines.Count - 1;

        if(isActive){
            EnableTextBox();
        }else{
            DisableTextBox();
        }

    }

    void Update()
    {
        if(!isActive) return;

        if(isActive){
            EnableTextBox();
        }else{
            DisableTextBox();
        }

        if(currentLine <= endAtLine) theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.K))
        {
            currentLine++;
        }

        if (currentLine > endAtLine)
        {
            isActive = false;
            if(manager.canDisplayOptions()){
                opm.isActive = true;
            }
        }

        if(isActive){
            EnableTextBox();
        }else{
            DisableTextBox();
        }
    }

    public void EnableTextBox(){
        //this is called from manager, and so it changes the list of dialogue lines to be read
        isActive = true;
        textLines = manager.dialogueLines;
        endAtLine = textLines.Count - 1;
        textBox.SetActive(true);
    }

    private void DisableTextBox(){
        textBox.SetActive(false);
        currentLine = 0;
    }
}
