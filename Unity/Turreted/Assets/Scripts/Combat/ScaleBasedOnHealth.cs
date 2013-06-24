using UnityEngine;
using System.Collections;

public class ScaleBasedOnHealth : MonoBehaviour 
{
	private CombatReceiverModel mCombatReceiverModel;
	private ActorModelV2 mActorModel;
	
	public float MinScale = 1;
	public float MaxScale = 2;
	
	
	public void Awake()
	{
		mCombatReceiverModel = GetComponent("CombatReceiverModel") as CombatReceiverModel;
		if (mCombatReceiverModel == null)
		{
			throw new MissingComponentException("Unable to find CombatReceiverModel.");
		}
		
		mActorModel = GetComponent("ActorModelV2") as ActorModelV2;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModel.");
		}
	}
	
	public void OnHealthPointChange(OnHealthPointChange hpEvent)
	{
		mActorModel.Scale = (hpEvent.CurrentHealthPoints / mCombatReceiverModel.InitialHealthPoints * MaxScale) + MinScale;
	}
}
