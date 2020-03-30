using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadControl : MonoBehaviour
{
	public static Dictionary<string, float> AXISES = new Dictionary<string, float>();
	public static Dictionary<string, bool> BTNS = new Dictionary<string, bool>();
	public static Dictionary<string, bool> PRE_BTNS = new Dictionary<string,bool>();
	Dictionary<string, string> AXIS_SETTINGS = new Dictionary<string, string>() {
		{"RStickHoz"		,"RStickHoz"},
		{"RStickVer"		,"RStickVer"},
		{"LStickHoz"		,"LStickHoz"},
		{"LStickVer"		,"LStickVer"},
		{"ArrowUpDown"		,"ArrowUpDown"},
		{"ArrowRightLeft"	,"ArrowRightLeft"}
	};
	Dictionary<string, string> BTN_SETTINGS = new Dictionary<string, string>() {
		{"Square"	,"joystick button 0"},
		{"Cross"	,"joystick button 1"},
		{"Round"	,"joystick button 2"},
		{"Triangle"	,"joystick button 3"},
		{"R1"		,"joystick button 5"},
		{"R2"		,"joystick button 7"},
		{"R3"		,"joystick button 11"},
		{"L1"		,"joystick button 4"},
		{"L2"		,"joystick button 6"},
		{"L3"		,"joystick button 10"},
		{"Option"	,"joystick button 9"},
		{"Track"	,"joystick button 13"},
	};
	
	float dt = 0; //Time.deltaTime
	
    // Start is called before the first frame update
    void Start()
    {
		Camera.main.fieldOfView = P_PARAM["FIELD_OF_VIEW"];
		axisHoz = Vector3.up;
		axisVer = this.transform.right;
		foreach(KeyValuePair<string, string> axisSetting in AXIS_SETTINGS) {
			PAD_AXISES.Add(axisSetting.Key, Input.GetAxis(axisSetting.Value));
		}
		foreach(KeyValuePair<string, string> btnSetting in BTN_SETTINGS) {
			PAD_BTNS.Add(btnSetting.Key, Input.GetKey(btnSetting.Value));
			_PRE_BTNS.Add(btnSetting.Key, Input.GetKey(btnSetting.Value));
		}
		RenewPadAxises();
		RenewPadBtns();
    }

	void Update(){
		RenewPadAxises();
		RenewPadBtns();
	}
	
	void RenewPadAxises() 
	{
		foreach(KeyValuePair<string, string> axisSetting in AXIS_SETTINGS) {
			PAD_AXISES[axisSetting.Key] = Input.GetAxis(axisSetting.Value);
		}
	}
	
	void RenewPadBtns() {
		foreach(KeyValuePair<string, string> btnSetting in BTN_SETTINGS) {
			PAD_BTNS[btnSetting.Key] = Input.GetKey(btnSetting.Value);
		}
	}
	
}
