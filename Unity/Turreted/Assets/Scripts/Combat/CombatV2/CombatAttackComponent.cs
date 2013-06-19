using UnityEngine;
using System.Collections;

public class CombatAttackComponent : MonoBehaviour 
{
	private const float SCALER = 0.33f;
	
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
					// Inform the owner of the attack success.
					if (mCombatAttackModel.Owner)
					{
						mCombatAttackModel.Owner.SendMessage("AttackCombatResult", combatResult, SendMessageOptions.DontRequireReceiver);
					}
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
			
			result.DamageToReceiver = attack.Damage;
			
			result.DamageToAttacker = receiver.DamageToAttacker;
			
			result.ColorChangeAttacker = DetermineColorLeech(attack, receiver, result.DamageToReceiver/receiver.InitialHealthPoints);
		}
		
		return result;
	}
	
	private Vector3 DetermineColorLeech(CombatAttackModel attack, CombatReceiverModel receiver, float percentDamageToTotalHealthDealt)
	{
		Color attackerColor = new Color();
		Color receiverColor = new Color();
		// Make sure the attacker and receiver have colors
		if (attack.Owner)
		{
			try
			{
				attackerColor = attack.Owner.renderer.material.color;
				receiverColor = receiver.renderer.material.color;
			}
			catch
			{
				return new Vector3();
			}
		}
	
		//Player Property += ((Experience Property - Player Property) * SCALER) * ( Damage / Experiences Health)
		float scale = SCALER * percentDamageToTotalHealthDealt;
		//Debug.Log ("PercentDamageDealtToTotalHealth: " + percentDamageToTotalHealthDealt + " Scaler: " + SCALER + " Final: " + scale);
		//Debug.Log("Receiver Color: " + receiverColor + " Attacker Color: " + attackerColor);
		float rDifference = (receiverColor.r - attackerColor.r) * scale;
		float gDifference = (receiverColor.g - attackerColor.g) * scale;
		float bDifference = (receiverColor.b - attackerColor.b) * scale;
		Vector3 colorChange = new Vector3( rDifference, gDifference, bDifference);
		//Debug.Log ("Color Change: " + colorChange);
		
		return colorChange;
	}
}
