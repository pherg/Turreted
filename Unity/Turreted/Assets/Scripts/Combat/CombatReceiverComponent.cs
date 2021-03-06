using UnityEngine;
using System.Collections;

public class CombatReceiverComponent : MonoBehaviour 
{
	
	private CombatReceiverModel mCombatReceiverModel;
	void Awake () 
	{
		// Grab the receiver off the object.
		// If no receiver then this object will not have attacks enacted upon it.
		mCombatReceiverModel = gameObject.GetComponent("CombatReceiverModel") as CombatReceiverModel;
		if (mCombatReceiverModel == null)
		{
			throw new MissingComponentException("Unable to find CombatAttackModel.  Spawning a default.");
		}
		
		mCombatReceiverModel.HealthPoints = mCombatReceiverModel.InitialHealthPoints;
		if (renderer && renderer.material)
		{
			renderer.material.color = mCombatReceiverModel.BaseColor;
			ColorCombatComponent colorComp = GetComponent("ColorCombatComponent") as ColorCombatComponent;
			if (colorComp)
			{
				colorComp.SetColor(mCombatReceiverModel.BaseColor);
			}
		}
	}
	
	public void Start()
	{
		mCombatReceiverModel.HealthPoints = mCombatReceiverModel.InitialHealthPoints;
	}
	
	public void FixedUpdate()
	{
		// IF object is marked for death and not in GodMode send the death event.
		if ((mCombatReceiverModel.MarkedForDeath || mCombatReceiverModel.HealthPoints <= 0) && !mCombatReceiverModel.GodMode)
		{
			SendMessage("OnDeathEvent", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}
	
	public void ReceiveCombatResult(CombatResult combatResult)
	{
		mCombatReceiverModel.AlterHealthPoints(-combatResult.DamageToReceiver);
		
		// Monitor health for killing actor.
		if (mCombatReceiverModel.HealthPoints <= 0)
		{
			mCombatReceiverModel.MarkedForDeath = true;
		}
	}
	
	public void AttackCombatResult(CombatResult combatResult)
	{
		mCombatReceiverModel.AlterHealthPoints(-combatResult.DamageToAttacker);
	}
}
