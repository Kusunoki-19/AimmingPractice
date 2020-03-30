using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public static float FIELD_OF_VIEW = 90f;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.fieldOfView = FIELD_OF_VIEW;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        BtnFunctions();
    }
    void CharacterMove()
    {
        double theta = Math.PI * this.transform.rotation.eulerAngles.y / 180f;
        float cosT = (float)Math.Cos(theta);
        float sinT = (float)Math.Sin(theta);

        Vector3 pos = this.transform.position;
        pos.x +=
        (float)(2 * Time.deltaTime * (cosT * Pad.AXISES["LStickHoz"] + sinT * (-Pad.AXISES["LStickVer"])));
        pos.z +=
        (float)(2 * Time.deltaTime * (-sinT * Pad.AXISES["LStickHoz"] + cosT * (-Pad.AXISES["LStickVer"])));

        //Debug.Log(180 * theta / Math.PI);
        this.transform.position = pos;
    }
    void fire()
    {
    }
    void jump()
    {
    }
    void BtnFunctions()
    {

        if (!Pad.PRE_BTNS["Track"] && Pad.BTNS["Track"])
        {
            //button falling down
            Pad.PRE_BTNS["Track"] = true;
            UIControl.TOGGLE_PAD_INPUT_UI();
        }
        else if (Pad.PRE_BTNS["Track"] && !Pad.BTNS["Track"])
        {
            //button rising up 
            Pad.PRE_BTNS["Track"] = false;
        }

        if ((!Pad.PRE_BTNS["Option"]) && (Pad.BTNS["Option"]))
        {
            //button falling down
            Pad.PRE_BTNS["Option"] = true;
            UIControl.TOGGLE_OPTION_UI();
        }
        else if (Pad.PRE_BTNS["Option"] && !Pad.BTNS["Option"])
        {
            //button rising up 
            Pad.PRE_BTNS["Option"] = false;
        }
    }
}
