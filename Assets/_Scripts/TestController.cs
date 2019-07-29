using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {
	int CurrentTestID = 0;
	private TestView testView;
	private int testOption;

	//public DataCollector dataCollectorGO;

	public string stringDataToSave = "";

	private bool callibrationTestingCompleted = false;

	public bool CallibrationTestingCompleted {
		get{return callibrationTestingCompleted;}
		set{callibrationTestingCompleted = value;}
	}

	private GameObject EyeTargetGameObject; 

	public void passingHitData(GameObject hitTarget){
		Debug.Log (hitTarget.name);
		EyeTargetGameObject = hitTarget;
	}

	public IEnumerator StartCallibrationTest(){
		testView.InstructionDisplay1.text = "Look at this screen";
		testView.InstructionDisplay2.text = "<- Look at the screen on your left";

		while (EyeTargetGameObject.name != "InstructionDisplay1Col") {
			yield return null;
			if (testView.Console.text != "Waiting for screen 1") {
				testView.Console.text = "Waiting for screen 1";
			}
		}
		testView.InstructionDisplay1.text = "Done!\nLook at the screen on your right ->";
		testView.InstructionDisplay2.text = "Look at this screen";

		while (EyeTargetGameObject.name != "InstructionDisplay2Col") {
			yield return null;
			if (testView.Console.text != "Waiting for screen 2") {
				testView.Console.text = "Waiting for screen 2";
			}
		}
		testView.InstructionDisplay1.text = "Eyetracking callibration complete!\nAwait further instructions here";
		testView.InstructionDisplay2.text = "Done!\nLook at the screen on your left for further instruction";
		testView.Console.text = "Eyetracking callibration successful";
		CallibrationTestingCompleted = true;
		testView.SetTestCallibrationCompleted(CallibrationTestingCompleted);
	}

	public void StartThrottleTest(){
		Debug.Log ("Throttle test started");
		StartCoroutine ("throttleTest");
	}

	IEnumerator throttleTest(){
		Debug.Log ("Throttle test in progress");
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text = "test starts in...\n3";
		yield return new WaitForSeconds (1f);
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "test starts in...\n2";
		yield return new WaitForSeconds (1f);
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "test starts in...\n1";
		yield return new WaitForSeconds (1f);
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "find the throttle";
		float startTime = Time.time;
		float totalTime;
		while (EyeTargetGameObject.name != "Throttle") {
			totalTime = Time.time - startTime;
			testView.Console.text = "searching throttle for = " + totalTime.ToString() + " s";
			yield return null;
			//counting time here?
		}
		totalTime = Time.time - startTime;

		stringDataToSave = "";
		stringDataToSave = testView.TestNumberText.text + "," + testView.GetTestOptionString() + "," + totalTime.ToString() + "," + System.DateTime.Now.ToString("dd/MM/yyyy"); 

		testView.Console.text = "Total time to find throttle = " + totalTime.ToString()+ " s";

		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "Well done! \nPlease wait further instructions";
	}

	public void StartFlightStickTest(){
		Debug.Log ("Flight stick test started");
		StartCoroutine ("flightStickTest");
	}

	IEnumerator flightStickTest(){
		Debug.Log ("Flight stick test in progress");
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text = "test starts in...\n3";
		yield return new WaitForSeconds (1f);
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "test starts in...\n2";
		yield return new WaitForSeconds (1f);
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "test starts in...\n1";
		yield return new WaitForSeconds (1f);
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "find the flight stick";
		float startTime = Time.time;
		float totalTime;
		while (EyeTargetGameObject.name != "FlightStick") {
			totalTime = Time.time - startTime;
			testView.Console.text = "searching for flight stick for = " + totalTime.ToString() + " s";
			yield return null;
			//counting time here?
		}
		totalTime = Time.time - startTime;

		stringDataToSave = "";
		stringDataToSave = testView.TestNumberText.text + "," + testView.GetTestOptionString() + "," + totalTime.ToString() + "," + System.DateTime.Now.ToString("dd/MM/yyyy"); 

		testView.Console.text = "Total time to find the flight stick = " + totalTime.ToString() + " s";
		testView.InstructionDisplay1.text = testView.InstructionDisplay2.text= "Well done! \nPlease wait further instructions";
	}

	public void StartRandomTest(){
		float randomFloat = Random.Range (0, 1);
		if (randomFloat <= .5) {
			StartThrottleTest ();
		} else {
			StartFlightStickTest ();
		}
	}

	public void StartCallibrationTesting(){
		StartCoroutine("StartCallibrationTest");
	}

	public void StartTest(){
		if(initialSetup()){
			if (!callibrationTestingCompleted) {
				StartCallibrationTesting ();
			}

			switch(testOption){
			case 0:
				Debug.Log ("option: throttle");
				StartThrottleTest ();
				break;
			case 1:
				Debug.Log ("option: flight stick");
				StartFlightStickTest ();
				break;
			case 2:
				Debug.Log ("option: random");
				StartRandomTest ();
				break;
			default:
				Debug.LogError ("can't recognize this option");
				break;
			}
		}else{
			Debug.LogError("InitialSetupFailed");
		}
	}

	public void ChangeTestID(int i){
		CurrentTestID = testView.GetTestNumber ();
		Debug.LogWarning ("CurrentTestID: " + CurrentTestID);
		testView.SetTestIDassigned (true);
	}

	private bool initialSetup(){
		CurrentTestID = testView.GetTestNumber ();
		//Debug.Log ("test ID: " + CurrentTestID);

		testOption = testView.GetTestOption();
		//Debug.Log ("test option: " + testOption);

		return true;
	}

	// Update is called once per frame
	void Start () {
		testView = GetComponentInChildren<TestView> ();
		testView.SetTestCallibrationCompleted(callibrationTestingCompleted);
	}
}
