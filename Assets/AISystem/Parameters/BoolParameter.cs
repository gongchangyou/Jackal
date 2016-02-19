using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class BoolParameter : NamedParameter {
		[SerializeField]
		private bool value;
		
		public bool Value
		{
			get{
				return this.value;
			}
			set
			{
				bool isChange = (this.value != value);

				this.value = value;

				if (isChange)
				{
					OnCallback();
				}
			}
		}
	}
}