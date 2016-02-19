using UnityEngine;
using System.Collections;

namespace AISystem{
	[System.Serializable]
	public class AudioClipParameter : NamedParameter {
		[SerializeField]
		private AudioClip value;
		
		public AudioClip Value
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