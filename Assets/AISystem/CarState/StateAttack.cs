using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class StateAttack : StateBase<Soldier> {
	public StateAttack(Soldier soldier) : base( soldier ){

	}

	private float shootElapseTime = 1.5f;
	public override IState Update () {
		owner.rigidbody.velocity = Vector3.zero;
		Vector3 target = owner.TargetObj.transform.localPosition;
//		Debug.Log ("StateAttack target=" + target);
//		owner.ComponentTransform.LookAt (target);
		Vector3 rotation = target - owner.transform.localPosition;
		rotation.y = 0;
		owner.transform.localRotation = Quaternion.LookRotation(rotation);
		//			transform.rotation = Quaternion.LookRotation(playerTransform.localPosition - transform.localPosition);
					shootElapseTime += Time.deltaTime;
					if(shootElapseTime > 2.0f){
						owner.Fire ();
						shootElapseTime = 0.0f;
					}
				
					owner.model.animation.CrossFade("idle");
				
		return this;
	}

}
