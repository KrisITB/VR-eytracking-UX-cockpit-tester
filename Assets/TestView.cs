using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestView : MonoBehaviour {

	public Text InstructionDisplay1;
	public Text InstructionDisplay2;
	public Text TestNumberText;
	public Dropdown TestOption;
	public Text Console;
	public Button StartTestButton;
	public Button SaveToCSVbutton;

	private bool testCallibrationCompleted = false;
	public void SetTestCallibrationCompleted(bool b){
		testCallibrationCompleted = b;
		updateButtons ();
	}

	private bool testIDassigned = false;
	public void SetTestIDassigned(bool b){
		testIDassigned = b;
		updateButtons ();
	}

	private void updateButtons(){
		if (testCallibrationCompleted && testIDassigned) {
			Debug.LogWarning (12);
			if (!StartTestButton.IsInteractable ()) {
				StartTestButton.interactable = true;
			}
		} else {
			Debug.LogWarning ("testCallibrationCompleted: " + testCallibrationCompleted);
			Debug.LogWarning ("testIDassigned: " + testIDassigned);
			if (StartTestButton.IsInteractable ()) {
				StartTestButton.interactable = false;
			}
		}
	}

	public delegate void UIcontrolDelagate(int i);
	UIcontrolDelagate uiControlDelagate;

	public int GetTestNumber(){
		return System.Int32.Parse(TestNumberText.text);
	}

	public int GetTestOption(){
		return TestOption.value;
	}

	public string GetTestOptionString(){
		if (TestOption.value == 0) {
			return "Throttle";
		} else {
			return "Flight stick";
		}
	}
}
