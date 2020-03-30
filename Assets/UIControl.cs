using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    static GameObject PadInputUI;
    static GameObject OptionUI;

    static Dictionary<string, bool> UI_STAT = new Dictionary<string, bool>() {
		{"PadInputUI", true},
		{"OptionUI", false},
	};

    // Start is called before the first frame update
    void Start()
    {
        PadInputUI = GameObject.Find("PadInputUI");
        OptionUI = GameObject.Find("OptionUI");

        PadInputUI.SetActive(UI_STAT["PadInputUI"]);
        OptionUI.SetActive(UI_STAT["OptionUI"]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void TOGGLE_PAD_INPUT_UI()
    {
        UI_STAT["PadInputUI"] = !UI_STAT["PadInputUI"];
        PadInputUI.SetActive(UI_STAT["PadInputUI"]);
    }
    public static void TOGGLE_OPTION_UI()
    {
        UI_STAT["OptionUI"] = !UI_STAT["OptionUI"];
        OptionUI.SetActive(UI_STAT["OptionUI"]);
    }
    public static void ADD_P_PARAM_VAL(string paramName, float increaseVal)
    {
        AimControl.AIM_PARAM[paramName] += increaseVal;
    }
    public static void START_SPAWN_TARGET()
    {
        TargetSpawn.SPAWN_TARGET();
    }
}
