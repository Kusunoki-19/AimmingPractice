using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSetParam : MonoBehaviour
{
	public GameObject minusBtnObj;
	public GameObject plusBtnObj;
	public GameObject valDispObj;
	public string pParamName;
	public float addUpVal;
	
	private Button pBtn;
	private Button mBtn;
	
	
    // Start is called before the first frame update
    void Start()
    {
		this.pBtn = this.plusBtnObj.GetComponent<Button>();
		this.mBtn = this.minusBtnObj.GetComponent<Button>();
		pBtn.onClick.AddListener(increaseVal);
		mBtn.onClick.AddListener(decreaseVal);
    }

    // Update is called once per frame
    void Update()
    {
        //display parameter value
		this.valDispObj.GetComponent<Text>().text = AimControl.AIM_PARAM[this.pParamName].ToString();
    }
	void increaseVal(){
		UIControl.ADD_P_PARAM_VAL(this.pParamName, this.addUpVal);
	}
	void decreaseVal(){
		UIControl.ADD_P_PARAM_VAL(this.pParamName, -this.addUpVal);
	}
}
