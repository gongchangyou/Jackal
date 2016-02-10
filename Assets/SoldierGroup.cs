using UnityEngine;
using System.Collections;

public class SoldierGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var soldierObject = Resources.Load ("prefabs/Soldier/Soldier");

		Vector3 pos = Vector3.zero;
		pos.x = Random.Range(0, 230);
		pos.z = Random.Range (0, 230);
		GameObject SoldierGO = (GameObject)Instantiate (soldierObject, pos, Quaternion.identity);
		SoldierGO.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
