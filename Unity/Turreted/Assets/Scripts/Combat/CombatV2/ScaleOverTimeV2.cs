using UnityEngine;
using System.Collections;

public class ScaleOverTimeV2 : MonoBehaviour 
{
	public float ScalePerTick = 0.1f;
	public float Ticks = 24;
	private float mTotalScale = 0;
	public bool RevertChangeOnFinish=false;
	
	private float mTicks = 0;
	
	private ActorModelV2 mActorModel;
	
	public void Awake()
	{
		mActorModel = GetComponent("ActorModelV2") as ActorModelV2;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModel.");
		}
	}
	
	public void FixedUpdate()
	{
		if (mTicks >= Ticks)			
		{
			CleanUpAndDisable();
			return;
		}
		mActorModel.Scale += ScalePerTick;
		mTotalScale+=ScalePerTick;
		mTicks++;
	}
	
	private void CleanUpAndDisable()
	{
		if (RevertChangeOnFinish)
		{
			mActorModel.Scale -= mTicks*ScalePerTick;
		}
		this.enabled = false;
	}
}
