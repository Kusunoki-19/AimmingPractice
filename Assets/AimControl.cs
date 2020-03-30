using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
	Vector3 axisHoz;
	Vector3 axisVer;
	Quaternion qRotHoz;
	Quaternion qRotVer;
	
	public static Dictionary<string, float> AIM_PARAM = new Dictionary<string, float> () {
		{"AIM_SENS" , 18f},
		{"SENS_COEF" , 30f},
		{"DEADZONE" , 0.04f},
		{"RES_CURVE_COEF" , 2f},
		{"AIM_MAGNIF" , 2f},
		{"AIM_SHIFT_TIME" , 0.25f},
		{"ELAPSED_TIME" , 0f},
		{"FIELD_OF_VIEW" , 90f}
	};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//aim button
		if(PAD_BTNS[_aimBtn]) {
			P_PARAM["ELAPSED_TIME"] += this.dt;
			Camera.main.fieldOfView = P_PARAM["FIELD_OF_VIEW"] 
			/ ((P_PARAM["AIM_SHIFT_TIME"] - P_PARAM["ELAPSED_TIME"]) * P_PARAM["AIM_MAGNIF"]);
			//Debug.Log("start");
		} else {
			P_PARAM["ELAPSED_TIME"] -= this.dt;
			Camera.main.fieldOfView = P_PARAM["FIELD_OF_VIEW"];
			//Debug.Log("finish");
		}
		
    }
	void AimControl() {
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
	void controlAimmingView() {
		
	}
	void BtnFunctions() {
		if (!PRE_BTNS["R1"] && BTNS["R1"]) {
			//button falling down
			PRE_BTNS["R1"] = true;
			UIControl.TOGGLE_PAD_INPUT_UI();
		}
		else(PRE_BTNS["R1"] && !BTNS["R1"]) {
			//button rising up 
			PRE_BTNS["R1"] = false;
		}
		
	}
}
