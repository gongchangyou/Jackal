using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class StateAttack : StateBase<Soldier> {
	public StateAttack(Soldier soldier) : base( soldier ){

	}

	private float shootElapseTime = 1.5f;
	public override IState Update () {

		Vector3 target = owner.TargetObj.transform.localPosition;
					target.y = owner.ComponentTransform.position.y;
		owner.ComponentTransform.LookAt (target);
		//			transform.localRotation = Quaternion.LookRotation(playerTransform.localPosition - transform.localPosition);
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
