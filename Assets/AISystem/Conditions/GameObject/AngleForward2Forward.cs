using UnityEngine;
using System.Collections;

// 追加.
// 指定した２キャラの向きをチェックする.
namespace AISystem{
	[Category("GameObject")]
	[System.Serializable]
	public class  AngleForward2Forward : BaseCondition {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string forward1;
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string forward2;
		public FloatComparer comparer;
		public float angle;
		private GameObject mForward1;
		private GameObject mForward2;

		public override void OnEnter ()
		{
			mForward1 = (forward1 == "Owner" || string.IsNullOrEmpty (forward1) ? owner.gameObject : ((GameObjectParameter)owner.GetParameter (forward1)).Value);
			mForward2 = (forward2 == "Owner" || string.IsNullOrEmpty (forward2) ? owner.gameObject : ((GameObjectParameter)owner.GetParameter (forward2)).Value);
		}

		
		public override bool Validate ()
		{
			if ( mForward1 != null && mForward2 != null )
			{
				Vector3 ownerForward=mForward1.transform.forward;
				Vector3 targetForward=mForward2.transform.forward;
				float angleDis = Mathf.Abs( Vector3.Angle (ownerForward, targetForward) );
				//Debug.Log ("Distance to: " + mTarget.name + " " + distance);
				switch (comparer) {
				case FloatComparer.Greater:
					return angleDis > angle;
				case FloatComparer.Less:
					return angleDis < angle;
				case FloatComparer.GreaterEqual:
					return angleDis >= angle;
				case FloatComparer.LessEqual:
					return angleDis <= angle;
				}
			}
			else
			{
				if ( mForward1 == null )
				{
					mForward1 = ((GameObjectParameter)owner.GetParameter (forward1)).Value;
				}
				if ( mForward2 == null )
				{
					mForward2 = ((GameObjectParameter)owner.GetParameter (forward2)).Value;
				}
			}
			return false;
		}
	}
}