using UnityEngine;
using System.Collections;

// 追加.
// Actionで作成したランダム値の値を調べる.
namespace AISystem{
	[Category("Misc")]
	[System.Serializable]
	public class RandomValue : BaseCondition {
		public string valueName;
		public FloatComparer comparer = FloatComparer.Greater;
		public float value;

		public override bool Validate ()
		{
			if ( owner.randomValues.ContainsKey( valueName ) )
			{
				switch(comparer){
				case FloatComparer.Greater:
					return owner.randomValues[ valueName ] > value;
				case FloatComparer.Less:
					return owner.randomValues[ valueName ] < value;
				case FloatComparer.GreaterEqual:
					return owner.randomValues[ valueName ] >= value;
				case FloatComparer.LessEqual:
					return owner.randomValues[ valueName ] <= value;
				}
			}
			return false;
		}
	}
}