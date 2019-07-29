using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour {

	public string dataToSave;


	private float startTime;
	List <DataPoint> dataPoints;
	int total = 0;

	Dictionary<string, int> frequencyTable;

	DataCollectorView dataView;

	void Start(){
		float startTime = Time.time;
		dataPoints = new List<DataPoint> ();
		frequencyTable = buildFrequencyTable ();
		dataView = GetComponent<DataCollectorView> ();
		//Debug.LogWarning (frequencyTable);
	}

	/*
	public DataCollector(){
		float startTime = Time.time;
		dataPoints = ne	w List<DataPoint> ();
		frequencyTable = buildFrequencyTable ();
		//Debug.LogWarning (frequencyTable);
	}
	*/

	/*
	FrequencyDataPoint[][] FrequencyDataPoints;

	private class FrequencyDataPoint{
		private string itemName;
		private int itemScore;
		float percentOfTotal;

		FrequencyDataPoint(string itemName, int itemScore){
			this.itemName = itemName;
			this.itemScore = itemScore;
			percentOfTotal = this.itemScore/total;
		}

		void addOneToScore(){
			itemScore += 1;
		}
		void setName(string name){
			this.itemName = name;
		}
	}*/

	private class DataPoint
	{
		float time;
		string name;

		public DataPoint(float t, string n){
			time = t;
			name = n;
		}

		public string getName(){
			return this.name;
		}
	}


	public void AddDataPoint(RaycastHit hit){
		if (hit.transform.CompareTag("Item")){

			total += 1;
			frequencyTable["total"] = total;
			frequencyTable [hit.transform.gameObject.name] += 1;

			Debug.LogWarning (frequencyTable[hit.transform.gameObject.name] + " = " + (float)frequencyTable[hit.transform.gameObject.name] / (float)frequencyTable["total"]);
			//Debug.LogWarning ("total from the table " + frequencyTable ["total"]);

			float deltaTime = Time.time - startTime;
			//Debug.Log ("Time = " + deltaTime + " | object name = " + hit.transform.gameObject.name);
			//dataPoints.Add( new DataPoint(deltaTime, hit.transform.gameObject.name));
			DataPoint newDataPoint = new DataPoint(deltaTime, hit.transform.gameObject.name);
			int itemNumber = extractItemNumber(newDataPoint.getName());
			//dataPoints.Add(newDataPoint);

			dataView.UpdateDisplay (frequencyTable);

		}
	}

	int extractItemNumber(string itemName){
		int result;
		string stringToConvert = itemName;
		Debug.Log ("Length = " + stringToConvert.Length);
		int newStringLength = stringToConvert.Length - 4; // - 4 for Item
		string extractedNumber = stringToConvert.Substring (4, newStringLength);
		Debug.Log ("extractedNumber = " + extractedNumber);
		result = int.Parse (extractedNumber);
		return result;
	}

	Dictionary<string, int> buildFrequencyTable(){
		GameObject[] allItems = GameObject.FindGameObjectsWithTag ("Item");

		Dictionary<string, int> frequencyDataPoints = new Dictionary<string, int> ();

		frequencyDataPoints.Add ("total", 0);

		for (int k = 0; k < allItems.Length; k++){
			frequencyDataPoints.Add (allItems [k].name, 0);
		}

		return frequencyDataPoints;
	}

	public void SaveFromTheButton(){
		SaveData ();
	}

	public void SaveData(){
		SaveToCSV dataSaver = new SaveToCSV ();
	}
}
