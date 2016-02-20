using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class StatePatrol : StateMove {
	public StatePatrol(Soldier soldier, Vector3[] list) : base( soldier ){
		pointList = new Vector3[list.Count()];
		List<Vector3> tmp = new List<Vector3> (list);
		pointList = tmp.Select (point => point + owner.ComponentTransform.position).ToArray ();
		pathLength = pointList.Length;

	}

}
