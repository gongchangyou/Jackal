using UnityEngine;
using System.Collections;

namespace AISystem.States{

	[CanCreate(true)]
	[System.Serializable]
	public class OnVariableSet : BaseTrigger {
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string target;
		public bool equals;

		public bool IsVariableSet()
		{
			return (((GameObjectParameter)owner.GetParameter (target)).Value == null) == equals;
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