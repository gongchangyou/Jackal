using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour {
	private IStateContext stateContext;
	public IStateContext StateContext{
		get{return stateContext;}
		set{stateContext = value;}
	}

	public GameObject bullet;
	
	private Transform model;
	private Transform gun;

	private Transform playerTransform;

	public float health = 100.0f;
	private bool bDead = false;

	private float power = 3000.0f;
	private float radius = 40.0f;

	private float shootElapseTime = 0.0f;
	public Transform ComponentTransform;

 	public GameObject TargetObj;
	
	// Use this for initialization
	void Start () {
		model = transform.FindChild("Model").transform;	
		gun = transform.FindChild("Gun").transform;	

		GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
		playerTransform = objPlayer.transform;

		ComponentTransform = transform;
//		model.animation.CrossFade("Run");
//		model.animation.CrossFade("idle");

		//add airuntimecontroller

		AIRuntimeController runtimeController = gameObject.AddComponent<AIRuntimeController> ();
		AISystem.AIController aic = null;
		aic = (AISystem.AIController)Resources.Load ("AISystem/SoldierAI");
		runtimeController.originalController = aic;
		runtimeController.SetAIController ();

		StateContext = new StateContext<Soldier> ();

		//add Target
		var targetObj = (AISystem.GameObjectParameter) runtimeController.controller.GetParameter("Target");
		targetObj.Value = objPlayer;
		TargetObj = objPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		if (bDead)
			return;

		if (StateContext == null)
			return;
		if (StateContext.CurrentState == null)
			return;
		StateContext.Update ();
		if (StateContext.CurrentState == null)
			return;
//		if (Vector3.Distance (playerTransform.localPosition, transformComponent.localPosition) < 50.0f) {
//			Vector3 target = playerTransform.localPosition;
//			target.y = transformComponent.position.y;
//			transformComponent.LookAt (target);
////			transform.localRotation = Quaternion.LookRotation(playerTransform.localPosition - transform.localPosition);
////			transform.rotation = Quaternion.LookRotation(playerTransform.localPosition - transform.localPosition);
//			shootElapseTime += Time.deltaTime;
//			if(shootElapseTime > 2.0f){
//				Fire ();
//				shootElapseTime = 0.0f;
//			}
//		} else {
//			model.animation.CrossFade("idle");
//		}
	}

	void Fire(){
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
		}
	}

	public virtual StatePatrol Patrol(Vector3[] pointList){
		StatePatrol state = new StatePatrol (this, pointList);
		StateContext.SetState (state);
		return state;
	}

	public virtual StateChase Chase(){
		StateChase state = new StateChase (this);
		StateContext.SetState (state);
		return state;
	}

	public void run(){
		model.animation.CrossFade("Run");
	}
}
