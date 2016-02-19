using UnityEngine;
using System.Collections;

[System.Serializable]
public class MinMax {
	public float min;
	public float max;

	MinMax(){
		min = 0f;
		max = 0f;
	}
	public MinMax( float mn, float mx ){
		min = mn;
		max = mx;
	}
	public float GetRandom(){
		return Random.Range(min,max);
	}
}
