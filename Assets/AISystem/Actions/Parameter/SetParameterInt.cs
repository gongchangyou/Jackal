using UnityEngine;
using System.Collections;

// 追加.
// 用意されたIntフィールドに値を設定する.
namespace AISystem.Actions
{
	[Category("Parameter")]
	[System.Serializable]
	public class SetParameterInt : BaseAction
	{
		[StoreParameter(true, typeof(IntParameter))]
		public string store;
		public int value;
		
		public override void OnEnter()
		{
			owner.SetInt(store, value);
			Finish();
		}
	}
}
