using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DataCollectorView : MonoBehaviour {
	public Text displayText;


	public void UpdateDisplay(Dictionary<string, int> frequencyData){
		string newStringToDisplay = "";
		foreach (KeyValuePair<string,int> entry in frequencyData) {
			newStringToDisplay += entry.Key + " = " + entry.Value + "\n";
		}
		displayText.text = newStringToDisplay;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
