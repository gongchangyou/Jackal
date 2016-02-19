using UnityEngine;
using System.Collections;

// 追加.
// ランダム値を準備する.
// 指定した名前のフィールドが無ければ、新たに作成する。
namespace AISystem.Actions{
	[Category("Misc")]
	[System.Serializable]
	public class SetupRandomValue : BaseAction {
		public string valueName;
		[MinMax(0,100)]
		public MinMax range = new MinMax( 0, 100 );

		public override void OnEnter ()
		{
			if ( !owner.randomValues.ContainsKey( valueName ) )
			{
				owner.randomValues.Add ( valueName, range.GetRandom() );
			}
			else
			{
				owner.randomValues[ valueName ] = range.GetRandom();
			}
			Finish();
		}
	}
}