using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	private IStateContext stateContext;
	public IStateContext StateContext{
		get{return stateContext;}
		set{stateContext = value;}
	}

	public float health = 100.0f;
	protected bool bDead = false;
	
	public Transform ComponentTransform;

 	public GameObject TargetObj;

	AIRuntimeController runtimeController;
	// Use this for initialization
	protected void Start () {

		ComponentTransform = transform;

		StateContext = new StateContext<Soldier> ();

		//add Target
		GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
		var targetObj = (AISystem.GameObjectParameter) runtimeController.controller.GetParameter("Target");
		targetObj.Value = objPlayer;
		TargetObj = objPlayer;
	}

	protected void setAiController(string aiFilePath){
		runtimeController = gameObject.AddComponent<AIRuntimeController> ();
		AISystem.AIController aic = null;
		aic = (AISystem.AIController)Resources.Load (aiFilePath);
		runtimeController.originalController = aic;
		runtimeController.SetAIController ();
	}
	
	// Update is called once per frame
	protected void Update () {
		if (bDead)
			return;

		if (StateContext == null)
			return;
		if (StateContext.CurrentState == null)
			return;
		StateContext.Update ();
		if (StateContext.CurrentState == null)
			return;
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

	protected virtual void OnDrawGizmos(){
		if (StateContext != null)
			StateContext.OnDrawGizmos ();
	}

	public virtual void run(){}
}
