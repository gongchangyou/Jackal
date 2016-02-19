using UnityEngine;
using System.Collections;

// 追加.
// タイムカウンターを準備する.　0　→　加算　へ.
// 指定した名前のタイムカウンターが無ければ、新たに作成する。
namespace AISystem.Actions{
	[Category("Misc")]
	[System.Serializable]
	public class SetupTimeCounter : BaseAction {
		public string valueName;

		public override void OnEnter ()
		{
			if ( !owner.timeCounters.ContainsKey( valueName ) )
			{
				owner.timeCounters.Add ( valueName, 0f );
			}
			else
			{
				owner.timeCounters[ valueName ] = 0f;
			}
			Finish();
		}
	}
}