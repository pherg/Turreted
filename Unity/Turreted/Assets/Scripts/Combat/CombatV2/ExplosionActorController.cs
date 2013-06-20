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
	
	public void OnExplosionSizeScaleChange(float scale)
	{
		Debug.Log ("OnExplosionSizeScaleChange: " + scale);
		mActorModel.Scale *= scale;
	}
}
