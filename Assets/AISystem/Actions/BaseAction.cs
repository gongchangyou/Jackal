using UnityEngine;
using System.Collections;

namespace AISystem.Actions{
	
	[System.Serializable]
	public class BaseAction:ScriptableObject  {
		[System.NonSerialized]
		public AIRuntimeController owner;

		public bool queue=false;
		public bool oneShot=false;
		[System.NonSerialized]
		public bool finished;
		
		private void OnEnable(){
			hideFlags = HideFlags.HideInHierarchy;
		}
		
		public void Finish(){
			this.finished = true;
		}

		public void Reset(){
			if ( !oneShot )
				this.finished = false;
		}
		
		public virtual void OnAwake(){}
		
		public virtual void OnEnter(){}
		
		public virtual void OnExit(){}
		
		public virtual void OnFixedUpdate(){}
		
		public virtual void OnUpdate(){}
		
		public virtual void OnLateUpdate(){
		}
		
		public virtual void OnAnimatorIK(int layerIndex){}
	}
}
