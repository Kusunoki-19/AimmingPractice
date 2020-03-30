using System;
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
		{"AIM_SHIFT_TIME" , 0.25f}
	};
	float aimShiftingTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
		axisHoz = Vector3.up;
		axisVer = this.transform.right;
    }

    // Update is called once per frame
    void Update()
    {
		RotateCamera();
		BtnFunctions();
    }
    
	float responseCurve(float input) {
		double temp = (double)input;
						
		//deadzone
		if (Math.Abs(temp) < AIM_PARAM["DEADZONE"]){
			temp = 0; //deadzone範囲内のとき
		} else if (temp > 0) {
			temp -= AIM_PARAM["DEADZONE"];
		} else {
			temp += AIM_PARAM["DEADZONE"];
		}		
			
		//aim responsivity mapping
		if (temp > 0) {
			temp = -1 + Math.Exp(AIM_PARAM["RES_CURVE_COEF"] * temp);
		} else {
			temp = 1 - Math.Exp(AIM_PARAM["RES_CURVE_COEF"] * -temp);
        }
        //scaring by max input(=1 - DEADZONE) 
        //(感度係数*aim感度)倍
        //積分(時間差[s]を掛けて差分[deg]に変換)
        temp = temp
        * (AIM_PARAM["AIM_SENS"] * AIM_PARAM["SENS_COEF"])
        / (-1 + Math.Exp(AIM_PARAM["RES_CURVE_COEF"] * (1 - AIM_PARAM["DEADZONE"])))
        * Time.deltaTime;


        if (Pad.BTNS["R1"]) temp /= AIM_PARAM["AIM_MAGNIF"];

        //Debug.Log(dt);
        return (float)temp;
    }

    void RotateCamera()
    {
			
        this.qRotVer = Quaternion.AngleAxis( responseCurve(Pad.AXISES["RStickVer"]), this.axisVer);
        this.qRotHoz = Quaternion.AngleAxis( responseCurve(Pad.AXISES["RStickHoz"]), this.axisHoz);
		
		this.transform.rotation = this.qRotVer * this.transform.rotation;
		this.transform.rotation = this.qRotHoz * this.transform.rotation;
		axisVer = this.transform.right ;//rotation axisVer with the horizontal rotation 
	}
	void controlAimmingView() {
	}
	void BtnFunctions() {
		if (!Pad.PRE_BTNS["R1"] && Pad.BTNS["R1"]) {
			//button falling down
			Pad.PRE_BTNS["R1"] = true;
			UIControl.TOGGLE_PAD_INPUT_UI();
		}
		else if(Pad.PRE_BTNS["R1"] && !Pad.BTNS["R1"]) {
			//button rising up 
			Pad.PRE_BTNS["R1"] = false;
		}
		
	}
}
