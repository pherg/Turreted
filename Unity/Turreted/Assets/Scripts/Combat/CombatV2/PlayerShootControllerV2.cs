using UnityEngine;
using System.Collections;

public class PlayerShootControllerV2 : MonoBehaviour 
{
	public UnityEngine.Object Bullet;
	
	public UnityEngine.Object Shield;
	
	private const float COLOR_MAX_VALUE = 255;
	public float MaxShotCostReductionMultiplierFromBlue = 0.5f;
	
	private ColorCombatComponent mColorCombatComponent;
	
	private int BulletLayer = 15;
	
	public void Awake()
	{
		mColorCombatComponent = GetComponent("ColorCombatComponent") as ColorCombatComponent;
	}
	
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
				CombatAttackModel attackCombatModel = bullet.GetComponent("CombatAttackModel") as CombatAttackModel;
				if (attackCombatModel == null)
				{
					throw new MissingComponentException("CombatAttackModel not found on bullet");
				}
				
				BulletMovementNew bulletMovement = bullet.GetComponent("BulletMovementNew") as BulletMovementNew;
				if (bulletMovement == null)
				{
					throw new MissingComponentException("Unable to find BulletMovementNew on bullet.");
				}
				
				ActorModel bulletActorModel = bullet.GetComponent("ActorModel") as ActorModel;
				if (bulletActorModel == null)
				{
					throw new MissingComponentException("Unable to find ActorModel on bullet.");
				}
				
				
				// Setup bullet.
				// Set parent for bullet
				attackCombatModel.Owner = gameObject;
				// manually set the layer you want to use for the bullet (prevents chicken and egg issue with shield)
				attackCombatModel.gameObject.layer = BulletLayer;
				
				attackCombatModel.transform.position += transform.position;
				
				// Use color component to alter properties of bullet
				if (mColorCombatComponent)
				{
					// Size
					Debug.Log ("Pre Scale: " + bulletActorModel.Scale);
					Debug.Log ("Scale scale: " + mColorCombatComponent.GetSizeScale());
					Debug.Log ("Pre Transform Scale: " + bulletActorModel.transform.localScale.ToString());
					bulletActorModel.Scale *= mColorCombatComponent.GetSizeScale();
					Debug.Log ("Post Transform Scale: " + bulletActorModel.transform.localScale.ToString());
					Debug.Log ("Post Scale: " +  bulletActorModel.Scale);
					
					// Speed
					bulletMovement.Speed *= mColorCombatComponent.GetSpeedScale();
				}
				
				// TODO: Move this to a higher level object to do once.
				// Ignore collision with player
				Physics.IgnoreLayerCollision(bullet.layer, gameObject.layer);
				// Ignore collision with other bullets.
				Physics.IgnoreLayerCollision(bullet.layer, bullet.layer);
				
				// Determine direction of bullet flight.
				Vector3 worldPointFromScreenPoint = Camera.mainCamera.ScreenToWorldPoint(
					new Vector3 (Input.mousePosition.x, Input.mousePosition.y,Camera.mainCamera.nearClipPlane));
				Vector3 direction = new Vector3(worldPointFromScreenPoint.x, 0, worldPointFromScreenPoint.z);
				
				bulletMovement.SetTarget(direction);
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
