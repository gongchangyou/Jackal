using UnityEngine;
using System.Collections;

namespace AISystem
{
	[Category("GameObject")]
	[System.Serializable]
	public class Angle2Target : BaseCondition
	{
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string target;
		public float minAngle;
		public float maxAngle;
		private GameObject mTarget;

		public override void OnEnter ()
		{
			mTarget = ((GameObjectParameter)owner.GetParameter (target)).Value;
		}

		public override bool Validate ()
		{
			if (null != mTarget)
			{
				Vector3 targetDir = mTarget.transform.position - owner.transform.position;
				targetDir = owner.transform.InverseTransformDirection(targetDir);
				Vector3 eulerAngle = Quaternion.LookRotation(targetDir).eulerAngles;

				if (minAngle <= eulerAngle.y && maxAngle >= eulerAngle.y)
				{
					return true;
				}
			}
			else
			{
				mTarget =owner.GetGameObject(target);
			}
			return false;
		}
	}
}
