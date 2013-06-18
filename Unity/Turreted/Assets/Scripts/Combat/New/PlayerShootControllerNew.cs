using UnityEngine;
using System.Collections;

public class PlayerShootControllerNew : MonoBehaviour 
{
	public UnityEngine.Object Bullet;
	
	public UnityEngine.Object Shield;
	
	private const float COLOR_MAX_VALUE = 255;
	public float MaxShotCostReductionMultiplierFromBlue = 0.5f;
	
	private int BulletLayer = 69;
	
	// Update is called once per frame
    void Update () 
	{
        if (Input.GetButtonDown ("Fire1")) 
		{
            // Construct a ray from the current mouse coordinates
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray)) 
			{
                GameObject bullet = Instantiate (Bullet) as GameObject;
				AttackCombatModel attackCombatModel = bullet.GetComponent("AttackCombatModel") as AttackCombatModel;
				if (attackCombatModel == null)
				{
					throw new MissingComponentException("AttackCombatModel not found on bullet");
				}
				
				// Setup bullet.
				// Set parent for bullet
				attackCombatModel.OwnerGameObject = gameObject;
				// manually set the layer you want to use for the bullet (prevents chicken and egg issue with shield)
				attackCombatModel.gameObject.layer = BulletLayer;
				
				attackCombatModel.transform.position += transform.position;
				
				// TODO: Move this to a higher level object to do once.
				// Ignore collision with player
				Physics.IgnoreLayerCollision(bullet.layer, gameObject.layer);
				// Ignore collision with other bullets.
				Physics.IgnoreLayerCollision(bullet.layer, bullet.layer);
				
				// Determine direction of bullet flight.
				Vector3 worldPointFromScreenPoint = Camera.mainCamera.ScreenToWorldPoint(
					new Vector3 (Input.mousePosition.x, Input.mousePosition.y,Camera.mainCamera.nearClipPlane));
				Vector3 direction = new Vector3(worldPointFromScreenPoint.x, 0, worldPointFromScreenPoint.z);
				BulletMovementNew bulletMovement = bullet.GetComponent("BulletMovementNew") as BulletMovementNew;
				if (bulletMovement == null)
				{
					throw new MissingComponentException("Unable to find BulletMovementNew on bullet.");
				}
				bulletMovement.SetTarget(direction);
				
				// Decrease health on shooter
				// Shot cost is reduced depending on what percent of the total color is blue:
				// First build a vector, then normalize it, this will give us the amount that 
				// blue contributes to the total color value.
//				Vector3 colorVector = new Vector3(	mPlayerActorModel.ColorOfActor.r/COLOR_MAX_VALUE, 
//													mPlayerActorModel.ColorOfActor.g/COLOR_MAX_VALUE, 
//													mPlayerActorModel.ColorOfActor.b/COLOR_MAX_VALUE);
//				colorVector.Normalize();
//				float shotCostReduction = 1 - (colorVector.z * MaxShotCostReductionMultiplierFromBlue);
				//Debug.Log ("Color.b: " + mPlayerModel.ColorOfActor.b + " shotCostReduction: " + shotCostReduction + " final cost: " + -bulletModel.ShotCost * shotCostReduction);
				//mPlayerCombatModel.AlterHealthPoints(-bulletCombatModel.TotalHealthCostPerBullet() -mPlayerCombatModel.TotalHealthCostPerBullet());
            }
        }
		
		if (Input.GetButtonDown ("Fire2"))
		{
			GameObject shield = Instantiate (Shield) as GameObject;
			
			ActorModel shieldModel = shield.GetComponent("ActorModel") as ActorModel;
			shieldModel.ParentActor = GetComponent("ActorModel") as ActorModel;
			
			// ignore the dude
			Physics.IgnoreLayerCollision(shield.layer, gameObject.layer);
			
			//... and his bullets
			Physics.IgnoreLayerCollision(shield.layer, BulletLayer);
			
			if (shieldModel == null)
			{
				throw new MissingComponentException("ActorModel not found on shield");
			}
			
			//shield.transform.position += transform.position;			
		}
    }
}
