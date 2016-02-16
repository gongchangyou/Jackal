using UnityEngine;
using System.Collections;

public class Soldier : Person {
	public GameObject bullet;
	
	public Transform model;
	private Transform gun;

	private float power = 3000.0f;
	private float radius = 40.0f;

	private float shootElapseTime = 0.0f;

	public SoldierGroup sg;
	// Use this for initialization
	void Start () {

		setAiController ("AISystem/SoldierAI");
		model = transform.FindChild("Model").transform;	
		gun = transform.FindChild("Gun").transform;	
		base.Start();
	}

	public void Fire(){
		//if (buttonName=="Fire"){
		Instantiate( bullet, gun.transform.position, gun.rotation);
		//}		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (bDead)
			return;
//		Debug.Log ("collision.gameObject.tag  = " + collision.gameObject.tag );
		if (collision.gameObject.tag == "Bullet")
		{
			print("AICar Hit with Bullet");
			health -= 30;
		}
		else if (collision.gameObject.tag == "Missile")
		{
			print("AICar Hit with Missile");
			health -= 50;
		}
		
		if (health <= 0 || collision.gameObject.tag == "Player")
		{
			bDead = true;
			float rndX = Random.Range(5.0f, 10.0f);
			float rndZ = Random.Range(5.0f, 10.0f);
			
			rigidbody.velocity = transform.TransformDirection(new Vector3(5.0f, 15.0f, 0.0f));
			rigidbody.AddExplosionForce(power, transform.position - 3.0f * transform.right, radius, 40.0F);
			Destroy(gameObject, 2.0f);

			sg.killEnemyCount += 1;
			sg.enemyCount -= 1;

		}
	}

	public virtual StateAttack Attack(){
		StateAttack state = new StateAttack (this);
		StateContext.SetState (state);
		return state;
	}

	public override void run(){
		model.animation.CrossFade("Run");
	}

}
