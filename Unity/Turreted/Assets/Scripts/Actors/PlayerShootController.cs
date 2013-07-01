using UnityEngine;
using System.Collections;

public class PlayerShootController: MonoBehaviour 
{
	public static int BulletLayer = 15;

	public UnityEngine.Object Bullet;
	
	public UnityEngine.Object Shield;
	
	private const float COLOR_MAX_VALUE = 255;
	
	public float BaseBulletCost = 10;
	public float MaxShotCostReductionMultiplierFromBlue = 0.1f;	
	
	private ColorCombatComponent mColorCombatComponent;
	private CombatReceiverModel mCombatReceiverModel;
	
	public void Awake()
	{
		mColorCombatComponent = gameObject.GetComponent("ColorCombatComponent") as ColorCombatComponent;
		if (mColorCombatComponent == null)
		{
			throw new MissingComponentException("Unable to find ColorCombatComponent");
		}
		
		mCombatReceiverModel = gameObject.GetComponent("CombatReceiverModel") as CombatReceiverModel;
		if (mCombatReceiverModel == null)
		{
			throw new MissingComponentException("Unable to find CombatReceiverModel");
		}
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
				// Determine direction of bullet flight.
				Vector3 worldPointFromScreenPoint = Camera.mainCamera.ScreenToWorldPoint(
					new Vector3 (Input.mousePosition.x, Input.mousePosition.y,Camera.mainCamera.nearClipPlane));
				Vector3 direction = new Vector3(worldPointFromScreenPoint.x, 0, worldPointFromScreenPoint.z);
				float damage = GetHealthCostPerShot();
				mCombatReceiverModel.AlterHealthPoints(-damage);
				
                GameObject bulletGO = CombatGod.SpawnBullet(Bullet, gameObject, new Vector3(), direction);
				bulletGO.renderer.material.color = gameObject.renderer.material.color;
            }
        }
		
		if (Input.GetButtonDown ("Fire2"))
		{
			GameObject shield = Instantiate (Shield) as GameObject;
			
			//ActorModel shieldModel = shield.GetComponent("ActorModel") as ActorModel;
			//shieldModel.ParentActor = GetComponent("ActorModel") as ActorModel;
			
			// ignore the dude
			//Physics.IgnoreLayerCollision(shield.layer, gameObject.layer);
			
			//... and his bullets
			//Physics.IgnoreLayerCollision(shield.layer, BulletLayer);
			
			//if (shieldModel == null)
			//{
			//	throw new MissingComponentException("ActorModel not found on shield");
			//}
			
			//shield.transform.position += transform.position;			
		}
    }
	
	private float GetHealthCostPerShot()
	{
		float result = BaseBulletCost * mColorCombatComponent.GetBulletCostReductionScale();
		return result;
	}
}
