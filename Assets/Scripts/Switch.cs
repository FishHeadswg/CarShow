/*
Author: Trevor Richardson
Switch.cs
02-11-2015

	Controls the state of switch 1 using references to its individual
	thumb and light. The thumb is rotated 44 degrees and the light
	switches from green to red according to the state.
	
 */

using UnityEngine;
using System;
using System.Collections;

public class Switch : MonoBehaviour {

	public GameObject thumb, switchLight; // renamed due to component naming conflict
	public enum SwitchState {ON = -22, OFF = 22};
	public SwitchState state;

	void Start () {
		state = SwitchState.OFF;
	}

	void Update(){
		setSwitchState(state);
	}

	void setSwitchState (SwitchState toSet){
		state = toSet;
		thumb.transform.localEulerAngles = new Vector3((float)state, 0,90);
		switchLight.renderer.material.color = (state == SwitchState.OFF) ? Color.red : Color.green;
	}

	public void set(SwitchState val){
		state = val;
	}

	public SwitchState get(){
		return state;
	}

	public void setAndDo(SwitchState toSet, Action toDo){
		state = toSet;
		toDo();
	}

	public static SwitchState getOppositeState(SwitchState s){
		return (s == SwitchState.OFF) ? SwitchState.ON : SwitchState.OFF;
	}
}
