using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{
	static GameObject TargetObj = (GameObject)Resources.Load("TargetObj");
	
    // Start is called before the first frame update
    void Start()
    {
        //this.TargetObj = (GameObject)Resources.Load("TargetObj");
    }

    // Update is called once per frame
    void Update()
    {
    }

	public static void SPAWN_TARGET() {
		int spawnNum = 5;
		for(int i = 0; i < spawnNum; i++) {
			Instantiate(TargetObj,createTargetPos(), Quaternion.identity);
		}
	}
	
	static Vector3 createTargetPos() {
		return new Vector3(createRnd(5,-5), createRnd(2, 1), createRnd(5,-5));
	}
	
	static float createRnd(float min ,float max) {
		return Random.Range(max, min);
	}
}
