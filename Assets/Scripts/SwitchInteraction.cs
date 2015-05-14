/*
Author: Trevor Richardson
SwitchInteraction.cs
02-11-2015

	Turns switches on and off using A, S, and D. These control
	the spotlight, platform rotation, and the rising/lowering
	of the platform. Rotation also ends after a continuous 360
	spin.
	
 */

using UnityEngine;
using System.Collections;

public class SwitchInteraction : MonoBehaviour {

	// switch objects
	public Switch sw;
	public Switch2 sw2;
	public Switch3 sw3;

	public GameObject spot; // spotlight
	public GameObject plat; // platform
	public float platSpeed = 30f; // rotation speed multiplier
	private float platRotation = 0f; // extra credit

	void Start() {
		// using GO's static method Find to locate objects
		plat = GameObject.Find ("Platform");
		Debug.Log (plat.name + " found!");
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.A)){
			Switch.SwitchState toSet = Switch.getOppositeState(sw.get());
			sw.set(toSet);
			if (sw.get() == Switch.SwitchState.OFF)
				spot.SetActive(false);
			else
				spot.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.S)) {
			Switch2.SwitchState toSet2 = Switch2.getOppositeState(sw2.get());
			sw2.set(toSet2);
			platRotation = 0; // reset count
		}
		if(Input.GetKeyUp(KeyCode.D)){
			Switch3.SwitchState toSet3 = Switch3.getOppositeState(sw3.get());
			sw3.set(toSet3);
		}
		// rotate while Switch2 is ON
		if (sw2.get() == Switch2.SwitchState.ON) {
			plat.transform.Rotate ( new Vector3(0f, Time.deltaTime * platSpeed, 0f));
			platRotation += (Time.deltaTime * platSpeed); // track rotation
		}
		// platform has fully rotated
		if (platRotation >= 360f) {
			sw2.set(Switch2.SwitchState.OFF);
			platRotation = 0; // reset tracker
		}
		// Rises/lowers platform using interpolation
		if (sw3.get() == Switch3.SwitchState.ON)
			plat.transform.position = Vector3.Lerp (plat.transform.position, 
			                                        new Vector3(0f, -0.3f, 0f), 
			                                        Time.deltaTime);
		else
			plat.transform.position = Vector3.Lerp (plat.transform.position, 
			                                        new Vector3(0f, 0f, 0f), 
			                                        Time.deltaTime);
	}
}
