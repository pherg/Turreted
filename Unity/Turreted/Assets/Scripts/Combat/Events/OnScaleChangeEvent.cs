using UnityEngine;
using System.Collections;

public class OnScaleChangeEvent 
{
	public float Scale = 1;
	public float AdditionalScale = 0;
	public float MultiplierScale = 1;
	
	public OnScaleChangeEvent(float scale, float additionalScale, float multiplierScale)
	{
		Scale = scale;
		AdditionalScale = additionalScale;
		MultiplierScale = multiplierScale;
	}
}
