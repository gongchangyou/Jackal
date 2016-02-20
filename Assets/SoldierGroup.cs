#define DEBUG
using UnityEngine;
using System.Collections;

public class SoldierGroup : MonoBehaviour {

	float elapseTime = 0.0f;
	private Object soldierObject;
	private float soldierTime = 5.0f;
	// Use this for initialization
	void Start () {
		soldierObject = Resources.Load ("prefabs/Soldier/Soldier");
#if DEBUG
		createSoldier();
#endif
	}
	
	// Update is called once per frame
	void Update () {
		#if DEBUG
		return;
#endif
		elapseTime += Time.deltaTime;

		if (elapseTime > soldierTime) {
			elapseTime = 0.0f;
			createSoldier();
		}
	}

	private void createSoldier(){

		int count = Random.Range(1, 1);
		for (int i=0; i<count; i++) {
						Vector3 pos = Vector3.zero;
						pos.x = Random.Range (8, 230);
						pos.z = Random.Range (8, 230);
						GameObject SoldierGO = (GameObject)Instantiate (soldierObject, pos, Quaternion.identity);
						SoldierGO.transform.parent = transform;
				}
	}
}
