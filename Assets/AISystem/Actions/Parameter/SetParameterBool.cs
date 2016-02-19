using UnityEngine;
using System.Collections;

// 追加.
// 用意されたBoolフィールドに値を設定する.
namespace AISystem.Actions{
	[Category("Parameter")]
	[System.Serializable]
	public class SetParameterBool : BaseAction {
		[StoreParameter(true,typeof(BoolParameter))]
		public string store;
		public bool value;
		
		public override void OnEnter ()
		{
			owner.SetBool(store,value);
			Finish ();
		}
	}
}