using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	[Category("Sound")]
	[System.Serializable]
	public class PlayBGM : BaseAction {

		public int bgmId;

		public override void OnEnter ()
		{
//			SoundManager.I.Play((SoundManager.BGM)bgmId);//play bgm
			Finish ();
		}
	}
}