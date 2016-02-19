using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class IntParameter : NamedParameter {
		[SerializeField]
		private int value;
		
		public int Value
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