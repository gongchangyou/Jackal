using UnityEngine;
using System.Collections;

/// <summary>
/// State base.
/// </summary>
public abstract class StateBase< T > : IState
	where T : MonoBehaviour
{
	private T	_owner;
	public virtual T owner { get{ return _owner; } set{ _owner = value; } }
	protected StateBase( T owner ){ _owner = owner; }
	public virtual int GetID() { return -1; }
	
	public virtual void Start(){}
	public virtual IState Update(){return this;}
	public virtual void FixedUpdate(){}
	public virtual void OnEnter( IState prev_state ){}
	public virtual void OnExit( IState next_state ){}
	public virtual void OnDrawGizmos(){
	}
}


