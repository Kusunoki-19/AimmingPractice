using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnPracticeStart : MonoBehaviour
{
	private Button startBtn;
    // Start is called before the first frame update
    void Start()
    {
		this.startBtn = this.GetComponent<Button>();
		startBtn.onClick.AddListener(practiceStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void practiceStart() {
		UIControl.START_SPAWN_TARGET();
	}
}
