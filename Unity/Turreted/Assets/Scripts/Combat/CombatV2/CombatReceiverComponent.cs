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
	}
	
	public void FixedUpdate()
	{
		// IF object is marked for death and not in GodMode send the death event.
		if (mCombatReceiverModel.MarkedForDeath && !mCombatReceiverModel.GodMode)
		{
			SendMessage("DeathEvent", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}
	
	public void ReceiveCombatResult(CombatResult combatResult)
	{
		Debug.Log ("RECEIVER: ReceiveCombatResult");
		// Monitor health for killing actor.
		if (mCombatReceiverModel.HealthPoints <= 0)
		{
			mCombatReceiverModel.MarkedForDeath = true;
		}
	}
}
