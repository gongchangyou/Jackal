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
			Person person = owner.GetComponent<Person> ();
			person.Patrol (pointList);
			Finish ();
		}
		
	}
}