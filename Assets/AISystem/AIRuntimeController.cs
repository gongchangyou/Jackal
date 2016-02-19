using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AISystem;
using AISystem.States;

public class AIRuntimeController : MonoBehaviour {

	public AIController originalController;
	public AIController controller;
	public int level=1;
	private int currentStateIndex;
	public State CurrentState{
		get
		{
			if (controller == null)
			{
				return null;
			}
			else
			{
				return controller.states[currentStateIndex];
			}
		}
	}
	private AnyState anyState;
	private List<BaseTrigger>	triggers;
	private List<BaseAttribute>	attributes;
	public	Dictionary<string,float> randomValues = new Dictionary<string, float>();
	public	Dictionary<string,float> timers = new Dictionary<string, float>();
	public	Dictionary<string,float> timeCounters = new Dictionary<string, float>();

	private void Awake(){
		SetAIController();
	}
	public void SetAIController()
	{
		enabled = (originalController != null && originalController.states.Count > 0);
		if (enabled) {
			triggers= new List<BaseTrigger>();
			attributes= new List<BaseAttribute>();
			controller=(AIController)ScriptableObject.Instantiate(originalController);
			for(int i=0;i<controller.parameters.Count;i++)
			{
				controller.parameters[i]=(NamedParameter)ScriptableObject.Instantiate(controller.parameters[i]);
				controller.parameters[i].OnChangeParameter = OnChangeParameter;
			}
			for(int i=0;i< controller.states.Count;i++)
			{
				controller.states[i]=(State)ScriptableObject.Instantiate(controller.states[i]);
				controller.states[i].Initialize(this);
				if(controller.states[i] is BaseTrigger){
					triggers.Add(controller.states[i] as BaseTrigger);
				}
				if(controller.states[i] is OnAttributeChanged){
					attributes.Add((controller.states[i] as OnAttributeChanged).attribute);
				}
			}
			
			currentStateIndex= controller.states.FindIndex (state => state.isDefaultState == true);
			anyState = (AnyState)controller.states.Find (state => state.GetType() == typeof(AnyState));
		}
	}

	private void Start(){
		anyState.DoEnter ();
		CurrentState.DoEnter ();
	}

	private void Update(){
		anyState.DoUpdate ();
		CurrentState.DoUpdate ();
//		Debug.Log (CurrentState.name);
		State state = CurrentState.ValidateTransitions ();
		if (state == null) {
		 	state = anyState.ValidateTransitions ();
		}

		if (state != null) {
			CurrentState.DoExit();
			currentStateIndex = controller.states.FindIndex (x => x.id == state.id);
			CurrentState.DoEnter();
		}

		List< string > timerNames = new List<string>( timers.Keys );
		float dt = Time.deltaTime;
		foreach( var timerName in timerNames )
		{
			timers[ timerName ] -= dt;
		}
		List< string > timeCounterNames = new List<string>( timeCounters.Keys );
		foreach( var timeCounterName in timeCounterNames )
		{
			timeCounters[ timeCounterName ] += dt;
		}

	}

	private void LateUpdate(){
		CurrentState.OnLateUpdate ();
	}

	private void FixedUpdate(){
		CurrentState.OnFixedUpdate ();
	}

	private void OnTriggerEnter(Collider other){
		for (int i=0; i<triggers.Count; i++) {
			if(triggers[i] is OnTriggerEnter){
				OnTriggerEnter trigger=(OnTriggerEnter)triggers[i];
				if(other.CompareTag(other.tag)){
					State toState=trigger.ValidateTransitions();
					if (toState != null) {
						CurrentState.DoExit();
						currentStateIndex = controller.states.FindIndex (x => x.id == toState.id);
						CurrentState.DoEnter();
					}
				}
			}
		}
	}

	private void OnChangeParameter(string parameterName)
	{
		for (int i=0; i<triggers.Count; i++)
		{
			if (!(triggers[i] is OnParameterChanged)) {continue ;}

			OnParameterChanged trigger=(OnParameterChanged)triggers[i];
			if (trigger.IsTarget(parameterName))
			{
				trigger.OnEnter();
				State toState = trigger.ValidateTransitions();
				if (null != toState)
				{
					CurrentState.DoExit();
					currentStateIndex = controller.states.FindIndex (x => x.id == toState.id);
					CurrentState.DoEnter();
					CurrentState.DoUpdate();
				}
			}
		}
	}

	//可以通过character.controller.aiRuntimeController.InvokeDamage 进来修改状态
	/*
	public void InvokeDamage( DamageData damage )
	{
		for (int i=0; i<triggers.Count; i++) {
			if ( !( triggers[i] is OnDamage ) )
				continue;
			OnDamage trigger=(OnDamage)triggers[i];
			if ( trigger.CompareDamage( damage ) )
			{
				trigger.OnEnter();
				State toState = trigger.ValidateTransitions();
				if (toState != null)
				{
					CurrentState.DoExit();
					currentStateIndex = controller.states.FindIndex (x => x.id == toState.id);
					CurrentState.DoEnter();
				}
			}
		}
	}

	public void InvokeTiming( DamageData damage )
	{
		for (int i=0; i<triggers.Count; i++) {
			if ( !( triggers[i] is OnTiming ) )
				continue;
			OnTiming trigger=(OnTiming)triggers[i];
			if ( trigger.CompareDamage( damage ) )
			{
				trigger.OnEnter();
				State toState = trigger.ValidateTransitions();
				if (toState != null)
				{
					CurrentState.DoExit();
					currentStateIndex = controller.states.FindIndex (x => x.id == toState.id);
					CurrentState.DoEnter();
				}
			}
		}
	}
*/
	public void InvokeForceCharacterToIdle()
	{
		for (int i = 0; i < triggers.Count; i++)
		{
			if (!(triggers[i] is OnForceCharacterToIdle))
			{
				continue;
			}
			
			OnForceCharacterToIdle trigger = (OnForceCharacterToIdle)triggers[i];
			
			trigger.OnEnter();
			State toState = trigger.ValidateTransitions();
			if (toState != null)
			{
				CurrentState.DoExit();
				currentStateIndex = controller.states.FindIndex ( x => x.id == toState.id );
				CurrentState.DoEnter();
			}
		}
	}

	public void InvokeSetOnGround()
	{
		for (int i = 0; i < triggers.Count; i++)
		{
			if (!(triggers[i] is OnSetOnGround))
			{
				continue;
			}
			
			OnSetOnGround trigger = (OnSetOnGround)triggers[i];
			
			trigger.OnEnter();
			State toState = trigger.ValidateTransitions();
			if (toState != null)
			{
				CurrentState.DoExit();
				currentStateIndex = controller.states.FindIndex ( x => x.id == toState.id );
				CurrentState.DoEnter();
			}
		}
	}

	public void InvokeSetIntoGround()
	{
		for (int i = 0; i < triggers.Count; i++)
		{
			if (!(triggers[i] is OnSetIntoGround))
			{
				continue;
			}
			
			OnSetIntoGround trigger = (OnSetIntoGround)triggers[i];
			
			trigger.OnEnter();
			State toState = trigger.ValidateTransitions();
			if (toState != null)
			{
				CurrentState.DoExit();
				currentStateIndex = controller.states.FindIndex ( x => x.id == toState.id );
				CurrentState.DoEnter();
			}
		}
	}

	public void InvokeIdle()
	{
		for (int i=0; i<triggers.Count; i++)
		{
			if ( !( triggers[i] is OnIdle ) )
				continue;

			OnIdle trigger = ( OnIdle )triggers[ i ];

			trigger.OnEnter();
			State toState = trigger.ValidateTransitions();
			if (toState != null)
			{
				CurrentState.DoExit();
				currentStateIndex = controller.states.FindIndex ( x => x.id == toState.id );
				CurrentState.DoEnter();
			}
		}
	}

	public void InvokeVariableSet()
	{
		for (int i=0; i<triggers.Count; i++) {
			if ( !( triggers[i] is OnVariableSet ) )
				continue;
			var trigger = triggers[i] as OnVariableSet;
			if (trigger == null) continue;

			if ( trigger.IsVariableSet() )
			{
				trigger.OnEnter();
				State toState = trigger.ValidateTransitions();
				if (toState != null)
				{
					CurrentState.DoExit();
					currentStateIndex = controller.states.FindIndex (x => x.id == toState.id);
					CurrentState.DoEnter();
				}
			}
		}
	}

	public void TryEnterState(State fromState){
		State toState=fromState.ValidateTransitions();
		if (toState != null) {
			CurrentState.DoExit();
			currentStateIndex = controller.states.FindIndex (x => x.id == toState.id);
			CurrentState.DoEnter();
		}
	}

	private void OnAnimatorIK(int layerIndex) {
		anyState.DoAnimatorIK (layerIndex);
		CurrentState.DoAnimatorIK (layerIndex);
	}

	private void OnAnimatorMove() {
		CurrentState.OnAnimatorMove ();
	}

	public NamedParameter GetParameter(string name){
		return controller.GetParameter(name);
	}

	public BaseAttribute GetAttribute(string name){
		return attributes.Find(x=>x.name==name);
	}

	public void ApplyAttributeDamage(object[] data){
		if (data.Length > 1) {
			string name=(data[0] != null && data[0].GetType()== typeof(string)?(string)data[0]:string.Empty);
			//Debug.Log(name);
			BaseAttribute attribute=GetAttribute(name);
			if(attribute != null){
				int damage=data[1] != null && data[1].GetType()== typeof(int)?(int)data[1]:0;
				attribute.Consume(damage);
				Debug.Log("CurValue "+attribute.CurValue);
			}
		}
	}
}
