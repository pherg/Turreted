using UnityEngine;
using System.Collections;

public class HpChangePerTick : MonoBehaviour 
{
	public float HpChangeEveryTick = 1;
	
	private CombatReceiverModel mCombatReceiverModel;
	
	public void Awake()
	{
		mCombatReceiverModel = GetComponent("CombatReceiverModel") as CombatReceiverModel;
		if (mCombatReceiverModel == null)
		{
			throw new MissingComponentException("Unable to find CombatReceiverModel.");
		}
	}
	
	public void FixedUpdate()
	{
		mCombatReceiverModel.AlterHealthPoints(HpChangeEveryTick);
	}
}
