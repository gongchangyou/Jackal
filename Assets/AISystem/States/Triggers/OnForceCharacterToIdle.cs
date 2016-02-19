using UnityEngine;
using System.Collections;

namespace AISystem.States
{
	[CanCreate(true)]
	[System.Serializable]
	public class OnForceCharacterToIdle : BaseTrigger
	{
		public override void OnEnter()
		{
			foreach (var transition in transitions)
			{
				transition.DoEnter();
			}
		}
	}
	
}