using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class PatrolAction : BaseAction {
		[SerializeField]
		public Vector3[] pointList;
		public override void OnEnter ()
		{
			Soldier soldier = owner.GetComponent<Soldier> ();
			soldier.Patrol (pointList);
			Finish ();
		}
		
	}
}