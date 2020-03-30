using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{	
	StringBuilder sb = new StringBuilder();
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if (PadControl.AXIS_SETTINGS != null && PadControl.BTN_SETTINGS != null ) {
			sb = new StringBuilder();
			foreach(string inputName in PadControl.AXIS_SETTINGS.Keys){
				sb.Append(inputName);
				sb.Append(" : ");
				sb.Append(PadControl.PAD_AXISES[inputName].ToString());
				sb.Append("\n");
			}
			foreach(string inputName in PadControl.BTN_SETTINGS.Keys){
				sb.Append(inputName);
				sb.Append(" : ");
				sb.Append(PadControl.PAD_BTNS[inputName].ToString());
				sb.Append("\n");
			}
			this.GetComponent<TextMesh>().text = sb.ToString();
		}
    }
}
