using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class StatePatrol : StateMove {
	public StatePatrol(Person person, Vector3[] list) : base( person ){
		pointList = new Vector3[list.Count()];
		List<Vector3> tmp = new List<Vector3> (list);
		pointList = tmp.Select (point => point + owner.ComponentTransform.localPosition).ToArray ();
		pathLength = pointList.Length;

	}
}
