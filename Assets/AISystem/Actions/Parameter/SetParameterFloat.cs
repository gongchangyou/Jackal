using UnityEngine;
using System.Collections;

// 追加.
// 用意されたFloatフィールドに値を設定する.
namespace AISystem.Actions
{
	[Category("Parameter")]
	[System.Serializable]
	public class SetParameterFloat : BaseAction
	{
		[StoreParameter(true, typeof(FloatParameter))]
		public string store;
		public float value;
		
		public override void OnEnter()
		{
			owner.SetFloat(store, value);
			Finish();
		}
	}
}