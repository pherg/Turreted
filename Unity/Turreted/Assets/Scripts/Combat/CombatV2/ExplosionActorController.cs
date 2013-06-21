using UnityEngine;
using System.Collections;

public class ExplosionActorController : ActorControllerV2 
{	
	public void Awake()
	{
		mActorModel = GetComponent("ActorModelV2") as ActorModelV2;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModelV2");
		}
	}
	
//	public void OnScaleChange(OnScaleChangeEvent scaleEvt)
//	{
//		base.OnScaleChange(scaleEvt);
//		float scale = scaleEvt.Scale * (scaleEvt.AdditionalScale + scaleEvt.MultiplierScale);
//		Debug.Log("SCALE: " + scale + " scaleEvt.Scale: " + scaleEvt.Scale + "scaleEvt.MultiplierScale: " + scaleEvt.MultiplierScale + " scaleEvt.AdditionalScale: " + scaleEvt.AdditionalScale);
//	}
	
	public void OnExplosionSizeScaleChange(float scale)
	{
		//Debug.Log ("OnExplosionSizeScaleChange: " + scale);
		mActorModel.AlterScaleMultiplier(scale);
	}
}
