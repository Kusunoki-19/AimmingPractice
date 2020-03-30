using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimSens : MonoBehaviour
{
	public GameObject minusBtnObj;
	public GameObject plusBtnObj;
	public GameObject valDispObj;
	
	private Button pBtn;
	private Button mBtn;
	
	string itemVal = "";
	
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
        //this.itemVal = PadControl.AIM_SENS.ToString();
		this.valDispObj.GetComponent<Text>().text = itemVal;
    }
	void increaseVal(){
		//PadControl.AIM_SENS += 1;
	}
	void decreaseVal(){
		//PadControl.AIM_SENS -= 1;
	}
}
