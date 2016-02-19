using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class FloatParameter : NamedParameter {
		[SerializeField]
		private float value;
		
		public float Value
		{
			get
			{
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