using UnityEngine;
using System.Collections;

// 追加.
// Actionで作成したタイムカウンタの値を調べる.
namespace AISystem{
	[Category("Misc")]
	[System.Serializable]
	public class TimeCounter : BaseCondition {
		public string valueName;
		public FloatComparer comparer = FloatComparer.Greater;
		public float value;

		public override bool Validate ()
		{
			if ( owner.timeCounters.ContainsKey( valueName ) )
			{
				switch(comparer){
				case FloatComparer.Greater:
					return owner.timeCounters[ valueName ] > value;
				case FloatComparer.Less:
					return owner.timeCounters[ valueName ] < value;
				case FloatComparer.GreaterEqual:
					return owner.timeCounters[ valueName ] >= value;
				case FloatComparer.LessEqual:
					return owner.timeCounters[ valueName ] <= value;
				}
			}
			return false;
		}
	}
}