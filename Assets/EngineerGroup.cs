//#define DEBUG
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EngineerGroup : MonoBehaviour {

	float elapseTime = 0.0f;
	private Object engineerObject;
	private float engineerTime = 5.0f;

	public int rescueCount = 0;
	public Text rescueText;
	// Use this for initialization
	void Start () {
		engineerObject = Resources.Load ("prefabs/Engineer/Engineer");
#if DEBUG
		createEngineer();
#endif
	}
	
	// Update is called once per frame
	void Update () {
		rescueText.text = rescueCount.ToString ();
		#if DEBUG
		return;
#endif
		elapseTime += Time.deltaTime;

		if (elapseTime > engineerTime && transform.childCount < 10) {
			elapseTime = 0.0f;
			createEngineer();
		}


	}

	private void createEngineer(){

		int count = Random.Range(1, 2);

		for (int i=0; i<count; i++) {
						Vector3 pos = Vector3.zero;
						pos.x = Random.Range (10, 200);
						pos.z = Random.Range (10, 200);
						GameObject engineerGO = (GameObject)Instantiate (engineerObject, pos, Quaternion.identity);
						engineerGO.transform.parent = transform;
						engineerGO.GetComponent<Engineer>().eg = this;
		}
	}
}
