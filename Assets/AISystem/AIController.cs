﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AISystem.States;

namespace AISystem{

	[System.Serializable]
	public class AIController : ScriptableObject {
		public List<State> states;
		public List<NamedParameter> parameters;
		public RuntimeAnimatorController runtimeAnimatorController;

		public virtual void OnEnter(){}
		
		public virtual void OnExit(){}
		
		public virtual void OnFixedUpdate(){}
		
		public virtual void OnUpdate(){}
		
		public virtual void OnLateUpdate(){}
		
		public virtual void OnAnimatorIK(int layerIndex){}

		private Dictionary<string, NamedParameter> nameIndexedParameter = new Dictionary<string, NamedParameter>();

		public NamedParameter[] GetParameters(Type type){
			return parameters.FindAll(x=> x.GetType() == type).ToArray();
		}

		public NamedParameter GetParameter(string name){
			if(nameIndexedParameter.ContainsKey(name)){
				return nameIndexedParameter[name];
			}
			
			var param =  parameters.Find (x => x.Name == name);
			nameIndexedParameter.Add (name, param);
			return param;
		}


		public string[] GetParameterNames(Type type){
			NamedParameter[] mParameters = GetParameters (type);
			List<string> list = new List<string> ();
			foreach (NamedParameter parameter in mParameters) {
				list.Add(parameter.Name);
			}
			return list.ToArray ();
		}
		
		public string[] GetParameterNames(params Type[] types){
			List<string> list = new List<string> ();
			foreach (Type type in types) {
				NamedParameter[] mParameters = GetParameters (type);
				
				foreach (NamedParameter parameter in mParameters) {
					list.Add (parameter.Name);
				}
			}
			return list.ToArray ();
		}
	}
}