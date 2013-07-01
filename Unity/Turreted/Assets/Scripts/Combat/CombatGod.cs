using UnityEngine;
using System.Collections;

public class CombatGod : MonoBehaviour 
{
	
	private const float SCALER = 0.33f;
	private static int BulletLayer = 15;
	
	// Spawns an actor and sets up properties.  If provided with a CombatAttackModel it will
	// copy the properties from the model.
	// Otherwies it will attempt to add properties from the owner.
	public static GameObject SpawnCombatActor(UnityEngine.Object objectToSpawn, Vector3 position, GameObject owner=null, CombatAttackModel spawnAttackModel=null)
	{
		GameObject actorGO = Instantiate (objectToSpawn) as GameObject;
		
		// If this is an attack object we need to set up the attack model
		CombatAttackModel attackCombatModel = actorGO.GetComponent("CombatAttackModel") as CombatAttackModel;
		if (attackCombatModel != null)
		{
			// Set up attack model based off parent.
			attackCombatModel.Owner = owner;
			
			// Copy properties of parent attack mods if it exists.
			if (spawnAttackModel)
			{
				CopyAttackProperties(spawnAttackModel, ref attackCombatModel);
			}
			// Otherwise if there is an owner try to copy their properties.
			else if (owner != null)
			{
				ColorCombatComponent ownerColorComponent = owner.GetComponent("ColorCombatComponent") as ColorCombatComponent;
				if (ownerColorComponent)
				{
					CalculateAttackPropertiesFromColorComponent(ref attackCombatModel, ownerColorComponent);
				}
			}
		}
		
		actorGO.transform.position = position;
		
		return actorGO;
	}
	
	private static void CalculateAttackPropertiesFromColorComponent(ref CombatAttackModel attackCombatModel, ColorCombatComponent ownerColorComponent)
	{
		attackCombatModel.BulletSpeedScale		=	ownerColorComponent.GetBulletSpeedScale();
		attackCombatModel.DamageScale 			=	ownerColorComponent.GetDamageScale();
		attackCombatModel.BulletSizeScale 		=	ownerColorComponent.GetBulletSizeScale();
		attackCombatModel.BulletMassScale 		=	ownerColorComponent.GetBulletMassScale();
		attackCombatModel.ExplosionScale		=	ownerColorComponent.GetExplosionSizeScale();
		attackCombatModel.HealthLossRateScale	=	ownerColorComponent.GetHealthLossRateScale();
	}
	
	private static void CopyAttackProperties(CombatAttackModel fromData, ref CombatAttackModel toData)
	{
		toData.BulletSpeedScale		=	fromData.BulletSpeedScale;
		toData.DamageScale 			=	fromData.DamageScale;	
		toData.BulletSizeScale 		=	fromData.BulletSizeScale;
		toData.BulletMassScale 		=	fromData.BulletMassScale;
		toData.ExplosionScale		=	fromData.ExplosionScale;
		toData.HealthLossRateScale	=	fromData.HealthLossRateScale;
	}
	
	public static GameObject SpawnBullet(UnityEngine.Object bulletPrefab, GameObject owner=null, Vector3 position= new Vector3(), Vector3 direction= new Vector3())
	{
		GameObject bulletGO = SpawnCombatActor(bulletPrefab, position, owner);
		CombatAttackModel attackCombatModel = bulletGO.GetComponent("CombatAttackModel") as CombatAttackModel;
		if (attackCombatModel != null && owner != null)
		{
			// Set up attack model based off parent.
			attackCombatModel.Owner = owner;
		}
		
		BulletMovementNew bulletMovement = bulletGO.GetComponent("BulletMovementNew") as BulletMovementNew;

		// manually set the layer you want to use for the bullet (prevents chicken and egg issue with shield)
		attackCombatModel.gameObject.layer = BulletLayer;
		
		attackCombatModel.transform.position += owner.transform.position;
		
		if (bulletMovement != null)
		{
			bulletMovement.SetTarget(direction);
		}
		
		// TODO: Move this to a higher level object to do once.
		// Ignore collision with player
		Physics.IgnoreLayerCollision(bulletGO.layer, owner.layer);
		// Ignore collision with other bullets.
		Physics.IgnoreLayerCollision(bulletGO.layer, bulletGO.layer);
		
		return bulletGO;
	}
	
	// This is the main controller for combat.  It arbitrates the interaction, records what took place
	// into a combat result, and sends out a message to both parties about the combat result.
	public static void ResolveAttackCollision(CombatAttackModel attack, CombatReceiverModel receiver)
	{
		CombatResult result;

		result = new CombatResult();
		// Record models
		result.Attack = attack;
		result.Receiver = receiver;
		
		result.DamageToReceiver = attack.Damage;
		
		result.DamageToAttacker = receiver.DamageToAttackerOnHit;
		
		if (receiver.AllowColorLeech)
		{
			result.ColorChangeAttacker = DetermineColorLeech(attack, receiver, result.DamageToReceiver/receiver.InitialHealthPoints);
		}
		else
		{
			result.ColorChangeAttacker = Vector3.zero;
		}
	
		// Determine if attack will kill the receiver.
		if ( (receiver.HealthPoints - result.DamageToReceiver) <= 0)
		{
			result.DamageToAttacker += receiver.DamageToAttackerOnKill;
		}
		// Send result back to the receiver.
		receiver.SendMessage("ReceiveCombatResult", result, SendMessageOptions.DontRequireReceiver);
		attack.SendMessage("AttackCombatResult", result, SendMessageOptions.DontRequireReceiver);
		// Inform the owner of the attack success.
		if (attack.Owner)
		{
			attack.Owner.SendMessage("AttackCombatResult", result, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	private static Vector3 DetermineColorLeech(CombatAttackModel attack, CombatReceiverModel receiver, float percentDamageToTotalHealthDealt)
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
