using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadControl : MonoBehaviour
{
	Vector3 axisHoz;
	Vector3 axisVer;
	Quaternion qRotHoz;
	Quaternion qRotVer;
	/*
	public static float AIM_SENS = 18f;
	public static float SENS_COEF = 30f;
	public static float DEADZONE = 0.035f;
	public static float RES_CURVE_COEF = 2f;
	public static float AIM_MAGNIF = 2f;
	public static float FIELD_OF_VIEW = 51f;
	*/
	public static Dictionary<string, float> P_PARAM = new Dictionary<string, float> () {
		{"AIM_SENS" , 18f},
		{"SENS_COEF" , 30f},
		{"DEADZONE" , 0.04f},
		{"RES_CURVE_COEF" , 2f},
		{"AIM_MAGNIF" , 2f},
		{"FIELD_OF_VIEW" , 90f}
	};
	
	public static Dictionary<string, float> PAD_AXISES = new Dictionary<string, float>();
	public static Dictionary<string, bool> PAD_BTNS = new Dictionary<string, bool>();
	public static Dictionary<string, string> AXIS_SETTINGS = new Dictionary<string, string>() {
		{"RStickHoz"		,"RStickHoz"},
		{"RStickVer"		,"RStickVer"},
		{"LStickHoz"		,"LStickHoz"},
		{"LStickVer"		,"LStickVer"},
		{"ArrowUpDown"		,"ArrowUpDown"},
		{"ArrowRightLeft"	,"ArrowRightLeft"}
	};
	public static Dictionary<string, string> BTN_SETTINGS = new Dictionary<string, string>() {
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
	
	static Dictionary<string,bool> _PRE_BTNS = new Dictionary<string,bool>();
	string _fireBtn = "L1";
	string _aimBtn = "R1";
	
	public GameObject bulletObj;
	
	float dt = 0; //Time.deltaTime
	
	GameObject mainCamObj;
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
		this.dt = Time.deltaTime;
		RenewPadAxises();
		RenewPadBtns();
		AimmingSystem();
		CharacterMove();
		detectPadChangeAndAction();
	}
	
    void FixedUpdate()
    {
    }
	
	void AimmingSystem() {
		float responseCurve(float input) {
			double temp = (double)input;
						
			//deadzone
			if (Math.Abs(temp) < P_PARAM["DEADZONE"]){
				temp = 0; //deadzone範囲内のとき
			} else if (temp > 0) {
				temp -= P_PARAM["DEADZONE"];
			} else {
				temp += P_PARAM["DEADZONE"];
			}		
			
			//aim responsivity mapping
			if (temp > 0) {
				temp = -1 + Math.Exp(P_PARAM["RES_CURVE_COEF"] * temp);
			} else {
				temp = 1 - Math.Exp(P_PARAM["RES_CURVE_COEF"] * -temp);
			}
			
			//scaring by max input(=1 - DEADZONE) 
			//(感度係数*aim感度)倍
			//積分(時間差[s]を掛けて差分[deg]に変換)
			temp = temp 
			* (P_PARAM["AIM_SENS"] * P_PARAM["SENS_COEF"]) 
			/ (-1 + Math.Exp(P_PARAM["RES_CURVE_COEF"] * (1 - P_PARAM["DEADZONE"])))
			* dt; 
			
			
			if(PAD_BTNS[_aimBtn])temp /= P_PARAM["AIM_MAGNIF"];
			
			//Debug.Log(dt);
			return (float)temp;
		}
        qRotVer = Quaternion.AngleAxis(responseCurve(PAD_AXISES["RStickVer"]), axisVer);
        qRotHoz = Quaternion.AngleAxis(responseCurve(PAD_AXISES["RStickHoz"]), axisHoz);
		
		this.transform.rotation = qRotVer * this.transform.rotation;
		this.transform.rotation = qRotHoz * this.transform.rotation;
		axisVer = this.transform.right ;//rotation axisVer with the horizontal rotation 
	}
	
	void CharacterMove() {
		double theta = Math.PI * this.transform.rotation.eulerAngles.y / 180;
		float cosT = (float)Math.Cos(theta);
		float sinT = (float)Math.Sin(theta);
		
		Vector3 pos = this.transform.position;
		pos.x += 
		(float)(2 * dt * ( cosT * PAD_AXISES["LStickHoz"] + sinT * (-PAD_AXISES["LStickVer"])));
		pos.z += 
		(float)(2 * dt * (-sinT * PAD_AXISES["LStickHoz"] + cosT * (-PAD_AXISES["LStickVer"])));
		
		//Debug.Log(180 * theta / Math.PI);
		this.transform.position = pos;
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
	
	void detectPadChangeAndAction() {
		bool isBtnChange(string btnName) {
			if(_PRE_BTNS[btnName] != PAD_BTNS[btnName]) {
				_PRE_BTNS[btnName] = PAD_BTNS[btnName];
				return true;
			} else {
				return false;
			}
		}

		//trackpad button push down
		if(isBtnChange("Track") && PAD_BTNS["Track"]) {
			UIControl.TOGGLE_PAD_INPUT_UI();
		}
		
		//option button push down
		if(isBtnChange("Option") && PAD_BTNS["Option"]) {
			UIControl.TOGGLE_OPTION_UI();
		}
		
		//start pushing aim button
		if(isBtnChange(_aimBtn)){
			if(PAD_BTNS[_aimBtn]) {
				Camera.main.fieldOfView = P_PARAM["FIELD_OF_VIEW"] / P_PARAM["AIM_MAGNIF"];
				//Debug.Log("start");
			} else {
				Camera.main.fieldOfView = P_PARAM["FIELD_OF_VIEW"];
				//Debug.Log("finish");
			}
		}
		
		//option button push down
		if(isBtnChange(_fireBtn) && PAD_BTNS[_fireBtn]) {
			createBullet();
		}
		
	}
	void createBullet() {
		//Instantiate(bulletObj, this.transform.forward, Quaternion.identity);
	}
}
