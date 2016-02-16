using UnityEngine;
using System.Collections;

public class Engineer : Person {
	public EngineerGroup eg;
	// Use this for initialization
	void Start () {
		setAiController ("AISystem/EngineerAI");
		base.Start();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (bDead)
			return;
//		Debug.Log ("collision.gameObject.tag  = " + collision.gameObject.tag );
		if (collision.gameObject.tag == "Player")
		{
			eg.rescueCount += 1;
//			Debug.LogError("add engineer");
			Destroy(gameObject);

			// add power to car	TargetObj
		}
	}

	public override void run(){
		animation.CrossFade("run");
	}

}
