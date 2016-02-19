using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class NamedParameter:ScriptableObject {
		[SerializeField]
		private string parameterName;

		public delegate void ChangeDelegater(string name);
		public ChangeDelegater OnChangeParameter;

		public string Name
		{
			get
			{
				return parameterName;
			}
			set
			{
				parameterName=value;
			}
		}
		
		private void OnEnable()
		{
			hideFlags = HideFlags.HideInHierarchy;
		}

		protected void OnCallback()
		{
			if (null != OnChangeParameter)
			{
				OnChangeParameter(parameterName);
			}
		}
	}
}