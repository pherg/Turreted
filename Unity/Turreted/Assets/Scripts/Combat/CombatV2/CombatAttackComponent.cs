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
			throw new MissingComponentException("Unable to find CombatAttackModel.");
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
				// Check masks to see if these should interact.
				if(mCombatAttackModel.CollisionMask == receiverModel.CollisionMask)
				{
					// An attack collision has occured, resolve it.
					CombatGod.ResolveAttackCollision(mCombatAttackModel, receiverModel);
				}
			}
		}
	}
}
