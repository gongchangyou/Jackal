using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class StateMove : StateBase<Person> {
	float elapseTime = 0.0f;

	protected Vector3[] pointList;
	protected int currentPathIndex;
	protected int pathLength;
	Vector3 velocity;

	private float speed = 10.0f;
	private float mass = 5.0f;
	private float curSpeed;
	private Vector3 targetPoint;
	private float radius = 2.0f;
	public StateMove(Person person) : base( person ){
		currentPathIndex = 0;
		velocity = owner.ComponentTransform.forward;
//		curSpeed = speed * Time.deltaTime;//因为Time.deltaTime是个变量 帧数变低就变大了
		curSpeed = 0.1f;



		owner.run ();

	}
	
	// Update is called once per frame
	public override IState Update () {

		targetPoint = pointList [currentPathIndex];

//		Debug.Log ("currentPathIndex=" + currentPathIndex + " targetPoint=" + targetPoint);
		if (Vector3.Distance (owner.ComponentTransform.localPosition, targetPoint) < radius) {
//			Debug.Log ("distance=" + Vector3.Distance (owner.ComponentTransform.position, targetPoint) + "pathLength=" + pathLength);
			if(currentPathIndex < pathLength - 1) currentPathIndex++;
			else currentPathIndex = 0;
		}

		if (currentPathIndex >= pathLength) {
			Debug.LogError("impossible currentPathIndex=" + currentPathIndex);
			return this;
		}

		var dir = targetPoint - owner.ComponentTransform.localPosition;
		dir.y = 0;
		dir.Normalize ();
		var velocity = dir * curSpeed;
		owner.ComponentTransform.localPosition += velocity;

		if(dir.magnitude > 0.001f)
			owner.ComponentTransform.localRotation = Quaternion.LookRotation(dir);

		return this;
	}

	private Vector3 Steer(Vector3 target){
		Vector3 desiredVelocity = target - owner.ComponentTransform.position;
		float dist = desiredVelocity.magnitude;
		desiredVelocity.Normalize ();
		if (dist < 10.0f)
			desiredVelocity *= curSpeed * dist / 10.0f;
		else
			desiredVelocity *= curSpeed;
		Vector3 steeringForce = desiredVelocity - velocity;
		Vector3 acceleration = steeringForce / mass;
		return acceleration;
	}

	public override void OnDrawGizmos(){
		if (targetPoint != Vector3.zero) 

		Debug.DrawLine (targetPoint, owner.ComponentTransform.localPosition);
	}
}
