using UnityEngine;
using System.Collections;

// 追加.
// カウントダウンタイマーを準備する.　指定した時間　→　0　へ.
// 指定した名前のタイマーが無ければ、新たに作成する。
namespace AISystem.Actions{
	[Category("Misc")]
	[System.Serializable]
	public class SetupCountDownTimer : BaseAction {
		public string valueName;
		public float time = 1f;
		public bool isRandom = false;
		[MinMax(0,100)]
		public MinMax startRange = new MinMax( 0f, 100f );

		public override void OnEnter ()
		{
			float startTime = time;
			if ( isRandom )
			{
				startTime = startRange.GetRandom();
			}

			if ( !owner.timers.ContainsKey( valueName ) )
			{
				owner.timers.Add ( valueName, startTime );
			}
			else
			{
				owner.timers[ valueName ] = startTime;
			}
			Finish();
		}
	}
}