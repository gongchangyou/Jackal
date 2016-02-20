//#define DEBUG
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoldierGroup : MonoBehaviour {

	float elapseTime = 0.0f;
	private Object soldierObject;
	private float soldierTime = 5.0f;

	public int killEnemyCount = 0;
	public int enemyCount = 0;

	public Text enemyText;
	public Text killEnemyText;
	// Use this for initialization
	void Start () {
		soldierObject = Resources.Load ("prefabs/Soldier/Soldier");
#if DEBUG
		createSoldier();
#endif
	}
	
	// Update is called once per frame
	void Update () {

		enemyText.text = enemyCount.ToString();
		killEnemyText.text = killEnemyCount.ToString();
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

		int count = Random.Range(1, 5);
		enemyCount += count;
		for (int i=0; i<count; i++) {
						Vector3 pos = Vector3.zero;
						pos.x = Random.Range (8, 230);
						pos.z = Random.Range (8, 230);
						GameObject SoldierGO = (GameObject)Instantiate (soldierObject, pos, Quaternion.identity);
						SoldierGO.transform.parent = transform;
			SoldierGO.GetComponent<Soldier>().sg = this;
		}
	}
}
