using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISystem.States
{
	[CanCreate(true)]
	[System.Serializable]
	public class OnParameterChanged : BaseTrigger
	{
		public List<string> parameterNames;

		public bool IsTarget(string targetName)
		{
			return (-1 != parameterNames.FindIndex(o => o == targetName));
		}

		public override void OnEnter()
		{
			foreach ( var transition in transitions )
			{
				transition.DoEnter();
			}
		}
	}
}
