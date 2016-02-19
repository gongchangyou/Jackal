using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class ColorParameter : NamedParameter {
		[SerializeField]
		private Color value;
		
		public Color Value
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