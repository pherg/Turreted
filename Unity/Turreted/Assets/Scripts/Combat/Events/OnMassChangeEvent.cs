using UnityEngine;
using System.Collections;

public class OnMassChangeEvent 
{
	public float AdditionalScale = 0;
	public float MultiplierScale = 1;
	
	public OnMassChangeEvent(float additionalScale, float multiplierScale)
	{
		AdditionalScale = additionalScale;
		MultiplierScale = multiplierScale;
	}
}
