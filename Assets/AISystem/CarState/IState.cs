using UnityEngine;
using System.Collections;

public interface IState
{
	int GetID();
	void Start();
	IState Update();
	void FixedUpdate();
	void OnEnter( IState prev_state );
	void OnExit( IState next_state );
	void OnDrawGizmos();
}