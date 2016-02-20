﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class AttackAction : BaseAction {
		public override void OnEnter ()
		{
			Soldier soldier = owner.GetComponent<Soldier> ();
			soldier.Attack ();
			Finish ();
		}

		
	}
}