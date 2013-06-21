using UnityEngine;
using System.Collections;

public class BulletActorController : ActorControllerV2 
{
	public void Awake()
	{
		mActorModel = GetComponent("ActorModelV2") as ActorModelV2;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModelV2");
		}
	}
	
	public void OnBulletSizeScaleChange(float scale)
	{
		mActorModel.AlterScaleMultiplier(scale);
	}
}
