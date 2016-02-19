using UnityEngine;
using System.Collections;

// 追加.
// パラメータのBool値の状態を調べる.
namespace AISystem{
	[Category("Parameter")]
	[System.Serializable]
	public class GetParameterInt : BaseCondition {
		[StoreParameter(true,typeof(IntParameter))]
		public string parameterName;
		public IntComparer comparer;
		public int value;
		private IntParameter mTarget;

		public override void OnEnter ()
		{
			mTarget = ((IntParameter)owner.GetParameter(parameterName));
		}

		public override bool Validate ()
		{
			switch (comparer)
			{
			case IntComparer.Equal:
				return mTarget.Value == value;
			case IntComparer.NotEqual:
				return mTarget.Value != value;
			case IntComparer.Greater:
				return mTarget.Value > value;
			case IntComparer.Less:
				return mTarget.Value < value;
			}
			return false;
		}
	}
}