using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AStarSystem;
public class StateChase : StateMove {
	public StateChase(Soldier soldier) : base( soldier ){

	}

	// Update is called once per frame
	public override IState Update () {

		var target = owner.TargetObj;
//		Debug.Log ("target localPosition=" + target.transform.localPosition);



		var startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(owner.ComponentTransform.localPosition)));
		var goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(target.transform.localPosition)));
		
		var pathArray = AStar.FindPath(startNode, goalNode);
		pointList = new Vector3[pathArray.Count];
		//		pointList.set
		for (int i=0; i<pathArray.Count; i++) {
			pointList[i] = ((Node) pathArray[i]).position;
		}
		pathLength = pointList.Length;
		if (currentPathIndex >= pathLength) {
			Debug.LogError("currentPathIndex =" + currentPathIndex + " pathLength = " + pathLength);
						currentPathIndex = pathLength - 1;

				}
		if (Vector3.Distance (owner.ComponentTransform.position, pointList [0]) < 2.0f) {
			Debug.LogError("currentPathIndex =" + currentPathIndex + "near");
						currentPathIndex = 1;
				}
		if(currentPathIndex < pathLength)
			base.Update ();
		return this;
	}

}
