using UnityEngine;
using System.Collections;

public class ScaleBasedOnHealth : MonoBehaviour 
{
	private CombatReceiverModel mCombatReceiverModel;
	
	public float MinScale = 1;
	
	
	public void Awake()
	{
		mCombatReceiverModel = GetComponent("CombatReceiverModel") as CombatReceiverModel;
		if (mCombatReceiverModel == null)
		{
			throw new MissingComponentException("Unable to find CombatReceiverModel.");
		}
	}
	
	public void OnHealthPointChange(OnHealthPointChange hpEvent)
	{
		Debug.Log(hpEvent.CurrentHealthPoints);
	}
}
