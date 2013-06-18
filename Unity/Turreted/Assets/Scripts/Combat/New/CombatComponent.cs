using UnityEngine;
using System.Collections;

public class CombatComponent : MonoBehaviour 
{
	// Receiver combat model is necessary to take damage.
	private ReceiverCombatModel mReceiverCombatModel;
	// Attack combat model is needed to act on receivers.
	private AttackCombatModel mAttackCombatModel;
	
	void Awake () 
	{
		// Grab the receiver off the object.
		// If no receiver then this object will not have attacks enacted upon it.
		mReceiverCombatModel = gameObject.GetComponent("ReceiverCombatModel") as ReceiverCombatModel;
		
		// Get a reference to the attack model.
		// If no attack model then there will be no combat initiated on a receiver.
		mAttackCombatModel = gameObject.GetComponent("AttackCombatModel") as AttackCombatModel;
		
		if (mAttackCombatModel == null && mReceiverCombatModel == null)
		{
			throw new MissingComponentException("Unable to find AttackCombatModel or ReceiverCombatModel.  CombatComponents need one of these to function.");
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
		// Determine if these two objects sohuld interact.
		// If this object has an attack combat model it can initiate combat.
		// Make sure the attack is still active.
		if (mAttackCombatModel && mAttackCombatModel.IsActive)
		{
			// If the other object is an Active ReceiverCombatModel, then Resolve Combat.
			
			ReceiverCombatModel rcm = collider.gameObject.GetComponent("ReceiverCombatModel") as ReceiverCombatModel;
			if (rcm != null && rcm.IsActive)
			{
				AttackCollision attackCollision = ResolveAttackCollision(mAttackCombatModel, rcm);
				if(attackCollision != null)
				{
					rcm.SendMessage("ReceiveAttackCollision", attackCollision, SendMessageOptions.DontRequireReceiver);
					SendMessage("AttackHit", attackCollision, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	private AttackCollision ResolveAttackCollision(AttackCombatModel attacker, ReceiverCombatModel receiver)
	{
		// Calculate collision based on masks
		AttackCollision result = null;
		if(attacker.CollisionMask == receiver.CollisionMask)
		{
			result = new AttackCollision();
			result.Attacker = attacker;
			result.Receiver = receiver;
		}
		
		return result;
	}
}
