using UnityEngine;
using System.Collections;

// 追加.
// Actionで作成したカウントダウンタイマーの値を調べる.
namespace AISystem{
	[Category("Misc")]
	[System.Serializable]
	public class CountDownTimer : BaseCondition {
		public string valueName;
		public FloatComparer comparer = FloatComparer.Greater;
		public float value;

		public override bool Validate ()
		{
			if ( owner.timers.ContainsKey( valueName ) )
			{
				switch(comparer){
				case FloatComparer.Greater:
					return owner.timers[ valueName ] > value;
				case FloatComparer.Less:
					return owner.timers[ valueName ] < value;
				case FloatComparer.GreaterEqual:
					return owner.timers[ valueName ] >= value;
				case FloatComparer.LessEqual:
					return owner.timers[ valueName ] <= value;
				}
			}
			return false;
		}
	}
}