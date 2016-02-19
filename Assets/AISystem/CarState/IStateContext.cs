using UnityEngine;
using System.Collections;

public interface IStateContext
{
	IState CurrentState { get; set; }

	void SetState(IState state, bool isRequiredSync = true );
	void Update();
	void OnDestroy();
	void OnDrawGizmos();
}

