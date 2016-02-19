using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("Sound")]
	[System.Serializable]
	public class StopBGM : BaseAction {
		public override void OnEnter ()
		{
//			SoundManager.I.StopBGM();//stop BGM
			Finish ();
		}
	}
}