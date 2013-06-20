using UnityEngine;
using System.Collections;

public class CombatGod : MonoBehaviour 
{
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
}
