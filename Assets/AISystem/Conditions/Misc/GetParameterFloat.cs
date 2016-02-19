using UnityEngine;
using System.Collections;

// 追加.
// パラメータのBool値の状態を調べる.
namespace AISystem
{
	[Category("Parameter")]
	[System.Serializable]
	public class GetParameterFloat : BaseCondition
	{
		[StoreParameter(true,typeof(FloatParameter))]
		public string parameterName;
		public FloatComparer comparer;
		public float value;
		private FloatParameter mTarget;
		
		public override void OnEnter()
		{
			mTarget = ((FloatParameter)owner.GetParameter(parameterName));
		}
		
		public override bool Validate()
		{
			switch (comparer)
			{
			case FloatComparer.Greater:
				return mTarget.Value > value;
			case FloatComparer.Less:
				return mTarget.Value < value;
			case FloatComparer.GreaterEqual:
				return mTarget.Value >= value;
			case FloatComparer.LessEqual:
				return mTarget.Value <= value;
			}
			return false;
		}
	}
}