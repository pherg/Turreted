using UnityEngine;
using System.Collections;

public class ScaleOverTime : MonoBehaviour 
{
	public float ScalePerTick = 0.1f;
	public float Ticks = 24;
	private float mTotalScale = 0;
	public bool RevertChangeOnFinish=false;
	
	private float mTicks = 0;
	
	private ActorModel mActorModel;
	
	public void Awake()
	{
		mActorModel = GetComponent("ActorModel") as ActorModel;
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
		mActorModel.AlterScaleAddition(ScalePerTick);
		mTotalScale+=ScalePerTick;
		mTicks++;
	}
	
	private void CleanUpAndDisable()
	{
		if (RevertChangeOnFinish)
		{
			mActorModel.AlterScaleAddition( -(mTicks*ScalePerTick));
		}
		this.enabled = false;
	}
}
