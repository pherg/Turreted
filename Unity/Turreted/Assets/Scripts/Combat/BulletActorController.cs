using UnityEngine;
using System.Collections;

public class BulletActorController : ActorController 
{
	public void Awake()
	{
		mActorModel = GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModel");
		}
	}
	
	public void OnBulletSizeScaleChange(float scale)
	{
		mActorModel.AlterScaleMultiplier(scale);
	}
	
	public void OnBulletMassScaleChange(float scale)
	{
		mActorModel.AlterMassMultiplier(scale);
	}
}
