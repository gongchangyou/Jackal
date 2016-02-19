using UnityEngine;
using System.Collections;

namespace AISystem
{
	[Category("GameObject")]
	[System.Serializable]
	public class EscapeDistance : BaseCondition
	{
		[StoreParameter(true,typeof(GameObjectParameter))]
		public string			target;
		public FloatComparer	comparer;
		public float			value = 1f;

		private GameObject		mTarget;

		public override void OnEnter ()
		{
			mTarget = ((GameObjectParameter)owner.GetParameter (target)).Value;
		}

		public override bool Validate ()
		{
			if ( string.IsNullOrEmpty( target ) )
				return false;
			if ( mTarget == null )
				return false;


			Vector3 pos = owner.transform.position - (mTarget.transform.position - owner.transform.position).normalized * value;
			Vector3 dis = pos - owner.transform.position;

			float distance = 0f;
			Ray r = new Ray(owner.transform.position, dis);
			RaycastHit hitInfo;
			if (Physics.Raycast(r, out hitInfo, dis.magnitude))
			{
				distance = Vector3.Distance(hitInfo.point, owner.transform.position);
			}
			else
			{
				return true;
			}

			switch (comparer)
			{
			case FloatComparer.Greater:
				return distance > value;
			case FloatComparer.Less:
				return distance < value;
			case FloatComparer.GreaterEqual:
				return distance >= value;
			case FloatComparer.LessEqual:
				return distance <= value;
			}

			return false;
		}
	}
}
