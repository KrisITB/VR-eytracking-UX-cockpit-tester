using UnityEngine;
using System.Collections;

public class FOVE3DCursor : MonoBehaviour
{
	public enum LeftOrRight
	{
		Left,
		Right
	}

	public DataCollector DataCollector;
	public TestController TestController;

	[SerializeField]
	public LeftOrRight whichEye;
	public FoveInterfaceBase foveInterface;


	// Latepdate ensures that the object doesn't lag behind the user's head motion
	//void Update() {
	void FixedUpdate() {

		//Debug.Log (1.0f / Time.deltaTime);

		FoveInterfaceBase.EyeRays rays = foveInterface.GetGazeRays();

		Ray r = whichEye == LeftOrRight.Left ? rays.left : rays.right;

		RaycastHit hit;
		Physics.Raycast(r, out hit, Mathf.Infinity);
		if (hit.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
		{
			transform.position = hit.point;
			/*
			//Calling AddDataPoint on DataCollector
			if (whichEye == LeftOrRight.Left) {
				DataCollector.AddDataPoint (hit);
			}
			*/
			if (whichEye == LeftOrRight.Left) {
				TestController.passingHitData(hit.transform.gameObject);
			}
		}
		else
		{
			transform.position = r.GetPoint(3.0f);
		}


	}
}
