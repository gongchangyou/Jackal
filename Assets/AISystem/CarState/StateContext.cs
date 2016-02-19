using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateContext<T> : IStateContext where T : MonoBehaviour
{
	public IState CurrentState { get; set; }
	
	private bool isChanged;
	
	public virtual void SetState(IState state, bool isRequiredSync = true )
	{
		IState prevState = CurrentState;
		CurrentState = state;
		if (prevState != null)
		{
			prevState.OnExit( CurrentState );
		}
		if (CurrentState != null)
		{
			CurrentState.OnEnter(prevState);
		}

		isChanged = true;

		if ( state == null ) {
			return;
		}
	}
	
	public virtual void Update()
	{
		if (CurrentState == null) return;
		
		while ( isChanged )
		{
			IState c = CurrentState;
			CurrentState.Start();
			if ( c == CurrentState )
			{
				isChanged = false;
			}
		}
		IState nextState = CurrentState.Update();
		if (isChanged == false && nextState != CurrentState)
		{
			SetState( nextState );
		}
	}
	
	public virtual void OnDestroy()
	{
		if (CurrentState != null)
		{
			CurrentState.OnExit(null);
			CurrentState = null;
		}
	}

	public virtual void OnDrawGizmos()
	{
		if (CurrentState != null) {
			CurrentState.OnDrawGizmos();
		}
	}
	
	protected virtual bool isSyncState(IState state)
	{
		return false;
	}
}

