using UnityEngine;
using System.Collections;

public class CombatAttackComponent : MonoBehaviour 
{
	// Receiver combat model is necessary to take damage.
	private CombatAttackModel mCombatAttackModel;
	
	void Awake () 
	{
		// Grab the receiver off the object.
		// If no receiver then this object will not have attacks enacted upon it.
		mCombatAttackModel = gameObject.GetComponent("CombatAttackModel") as CombatAttackModel;
		
		if (mCombatAttackModel == null)
		{
			throw new MissingComponentException("Unable to find CombatAttackModel.  Spawning a default.");
		}
	}
	
	// Interact with physics collisions.
	public void OnCollisionEnter(Collision collision)
	{
		Collide(collision.collider);
	}
	
	// Interact with triggers.
	public void OnTriggerEnter(Collider collider)
	{
		Collide(collider);
	}
	
	public void Collide(Collider collider)
	{
		// Collide when active.
		if (mCombatAttackModel.IsActive)
		{
			// Only collide with CombatReceivers that are active.
			CombatReceiverModel receiverModel = collider.gameObject.GetComponent("CombatReceiverModel") as CombatReceiverModel;
			if (receiverModel != null && receiverModel.IsActive)
			{
				// Resolve a combat result and move on.
				CombatResult combatResult = ResolveAttackCollision(mCombatAttackModel, receiverModel);
				if(combatResult != null)
				{
					receiverModel.SendMessage("ReceiveCombatResult", combatResult, SendMessageOptions.DontRequireReceiver);
					SendMessage("AttackCombatResult", combatResult, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	// This is the main controller for combat.  It arbitrates the interaction, records what took place
	// into a combat result, and sends out a message to both parties about the combat result.  This contorller
	// will set all the model values, and send out the message, which would be picked up by view controllers
	// to do things like "flash on receiving damage".  Perform an attack on hit.
	private CombatResult ResolveAttackCollision(CombatAttackModel attack, CombatReceiverModel receiver)
	{
		// Calculate collision based on masks
		CombatResult result = null;
		if(attack.CollisionMask == receiver.CollisionMask)
		{
			result = new CombatResult();
			// Record models
			result.Attack = attack;
			result.Receiver = receiver;
			
			// Arbitrate Damage and Record
			receiver.AlterHealthPoints(-attack.Damage);
			result.DamageToReceiver = attack.Damage;
			
			// TODO Make a call to the ownerReceiver that they took damage
			result.DamageToAttacker = receiver.DamageToAttacker;
		}
		
		return result;
	}
}
