using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PadInputUI : MonoBehaviour
{
    [SerializeField] GameObject dispObj;
    StringBuilder sb = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pad.AXIS_SETTINGS != null && Pad.BTN_SETTINGS != null)
        {
            sb = new StringBuilder();
            foreach (string inputName in Pad.AXIS_SETTINGS.Keys)
            {
                sb.Append(inputName);
                sb.Append(" : ");
                sb.Append(Pad.AXISES[inputName].ToString());
                sb.Append("\n");
            }
            foreach (string inputName in Pad.BTN_SETTINGS.Keys)
            {
                sb.Append(inputName);
                sb.Append(" : ");
                sb.Append(Pad.BTNS[inputName].ToString());
                sb.Append("\n");
            }
            this.dispObj.GetComponent<Text>().text = sb.ToString();
        }
    }
}
