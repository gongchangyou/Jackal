using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class ChaseAction : BaseAction {
		public override void OnEnter ()
		{
			Person person = owner.GetComponent<Person> ();
			person.Chase ();
			Finish ();
		}
		
	}
}