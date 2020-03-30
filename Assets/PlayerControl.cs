using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		padContorl = this.GameObject.GetComponent<PadContorl>();
		aimContorl = this.GameObject.GetComponent<AimContorl>();
		this.dt = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
		aimContorl.AimControl();
		padControl.detectPadChangeAndAction();
		CharacterMove();
    }
	void CharacterMove() {
		double theta = Math.PI * this.transform.rotation.eulerAngles.y / 180f;
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
	void fire() {
	}
	void jump() {
	}
	void BtnFunctions() {
		if (!PRE_BTNS["Track"] && BTNS["Track"]) {
			//button falling down
			PRE_BTNS["Track"] = true;
			UIControl.TOGGLE_PAD_INPUT_UI();
		}
		else(PRE_BTNS["Track"] && !BTNS["Track"]) {
			//button rising up 
			PRE_BTNS["Track"] = false;
		}
		
		if (!PRE_BTNS["Option"] && BTNS["Option"]) {
			//button falling down
			PRE_BTNS["Option"] = true;
			UIControl.TOGGLE_OPTION_UI();
		}
		else(PRE_BTNS["Option"] && !BTNS["Option"]) {
			//button rising up 
			PRE_BTNS["Option"] = false;
		}
	}
}
