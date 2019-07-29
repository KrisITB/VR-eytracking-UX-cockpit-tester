using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class SaveToCSV {



	/*
	public SaveToCSV (Dictionary<string, int> dataToSave){
   */
	public SaveToCSV (){

		TestController testControllerGO = GameObject.Find("TestController").GetComponent<TestController>();

		string stringDataToSave = testControllerGO.stringDataToSave;

		string filePath = Application.dataPath + "/CSV/" + "SavedSession1.csv";

		StreamWriter streamWriter = new StreamWriter (filePath);

		StringBuilder sb = new StringBuilder ();

		streamWriter.WriteLine ("TestNumber, TestItem, Score, Date");

		sb.AppendLine (stringDataToSave);

		/*
		foreach (KeyValuePair<string, int> entry in dataToSave) {
			
			sb.AppendLine (entry.Key +","+ entry.Value.ToString ());
		}
		*/
		streamWriter.WriteLine (sb);


		stringDataToSave = "";
		streamWriter.Flush ();
		streamWriter.Close ();
	}
}
