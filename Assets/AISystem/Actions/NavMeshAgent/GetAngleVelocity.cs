﻿using UnityEngine;
using System.Collections;

namespace AISystem.Actions.NavMeshAgent{
	[Category("NavMeshAgent")]
	[System.Serializable]
	public class GetAngleVelocity : BaseAction {

		[StoreParameter(true,typeof(FloatParameter))]
		public string store;
		private UnityEngine.NavMeshAgent agent;
		
		public override void OnAwake ()
		{
			agent = owner.GetComponent<UnityEngine.NavMeshAgent> ();
		}
		
		public override void OnUpdate ()
		{
			float angle = FindAngle(owner.transform.forward, agent.desiredVelocity, owner.transform.up);
			float angularSpeed = angle / 0.6f;
			((FloatParameter)owner.GetParameter(store)).Value=angularSpeed;
		}

		private float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
		{
			// If the vector the angle is being calculated to is 0...
			if(toVector == Vector3.zero)
				// ... the angle between them is 0.
				return 0f;
			// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
			float angle = Vector3.Angle(fromVector, toVector);
			// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
			Vector3 normal = Vector3.Cross(fromVector, toVector);
			// The dot product of the normal with the upVector will be positive if they point in the same direction.
			angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
			// We need to convert the angle we've found from degrees to radians.
			angle *= Mathf.Deg2Rad;
			return angle;
		}
	}
}