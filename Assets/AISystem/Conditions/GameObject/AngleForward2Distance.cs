using UnityEngine;
using System.Collections;

// 追加.
// 対象の向きと、指定したキャラ間のベクトルの向きをチェックする.
namespace AISystem{
	[Category("GameObject")]
	[System.Serializable]

	public class  AngleForward2Distance : BaseCondition {
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string forward;
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string distanceFrom;
		[StoreParameter(false,"Owner",typeof(GameObjectParameter))]
		public string distanceTo;

		public FloatComparer comparer;
		public float angle;
		private GameObject mForward;
		private GameObject mDistanceFrom;
		private GameObject mDistanceTo;

		public override void OnEnter ()
		{
			mForward =		(forward == "Owner" || string.IsNullOrEmpty (forward) ? owner.gameObject : ((GameObjectParameter)owner.GetParameter (forward)).Value);
			mDistanceFrom	=	(distanceFrom	== "Owner" || string.IsNullOrEmpty (distanceFrom)	? owner.gameObject : ((GameObjectParameter)owner.GetParameter (distanceFrom)).Value);
			mDistanceTo		=	(distanceTo		== "Owner" || string.IsNullOrEmpty (distanceTo)		? owner.gameObject : ((GameObjectParameter)owner.GetParameter (distanceTo)).Value);
		}

		
		public override bool Validate ()
		{
			if ( mForward != null && mDistanceFrom != null && mDistanceTo != null )
			{
				Vector3 forward=mForward.transform.forward;
				Vector3 distance = mDistanceTo.transform.position - mDistanceFrom.transform.position;
				float angleDis = Mathf.Abs( Vector3.Angle (forward, distance) );
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
				if ( mForward == null )
				{
					mForward = ((GameObjectParameter)owner.GetParameter (forward)).Value;
				}
				if ( mDistanceFrom == null )
				{
					mDistanceFrom = ((GameObjectParameter)owner.GetParameter (distanceFrom)).Value;
				}
				if ( mDistanceTo == null )
				{
					mDistanceTo = ((GameObjectParameter)owner.GetParameter (distanceTo)).Value;
				}
			}
			return false;
		}
	}
}