using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class LayerMaskParameter : NamedParameter {
		[SerializeField]
		private LayerMask value;
		
		public LayerMask Value
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