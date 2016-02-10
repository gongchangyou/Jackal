using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	private float startTime;
	public float speed = 20.0f;
	// Use this for initialization
	void Start () {
		startTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.realtimeSinceStartup-startTime<2){
			transform.Translate(new Vector3(0,0,1) * speed * Time.deltaTime);
		}
		else{
			Destroy(gameObject);	
		}
	}
}
