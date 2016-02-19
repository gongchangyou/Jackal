using UnityEngine;
using System.Collections;

// 追加.
// パラメータのBool値の状態を調べる.
namespace AISystem{
	[Category("Parameter")]
	[System.Serializable]
	public class GetParameterBool : BaseCondition {
		[StoreParameter(true,typeof(BoolParameter))]
		public string parameterName;
		public bool check;
		private BoolParameter mTarget;

		public override void OnEnter ()
		{
			mTarget = ((BoolParameter)owner.GetParameter(parameterName));
		}

		public override bool Validate ()
		{
			return mTarget.Value == check;
		}
	}
}