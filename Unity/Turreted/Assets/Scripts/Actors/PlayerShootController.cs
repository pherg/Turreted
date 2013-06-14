using UnityEngine;
using System.Collections;

public class PlayerShootController : MonoBehaviour 
{
	public UnityEngine.Object Bullet;
	
	public UnityEngine.Object Shield;
	
	private ActorModel mPlayerModel;
	
	void Start () 
	{
		mPlayerModel = gameObject.GetComponent("ActorModel") as ActorModel;
		if (mPlayerModel == null)
		{
			throw new MissingComponentException("Unable to find model for this actor.");
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
                GameObject bullet = Instantiate (Bullet) as GameObject;
				ActorModel bulletModel = bullet.GetComponent("ActorModel") as ActorModel;
				bulletModel.ParentActor = GetComponent("ActorModel") as ActorModel;
				if (bulletModel == null)
				{
					throw new MissingComponentException("ActorModel not found on bullet");
				}
				bullet.transform.position += transform.position;
				// Ignore collision with player
				Physics.IgnoreLayerCollision(bullet.layer, mPlayerModel.gameObject.layer);
				// Ignore collision with other bullets.
				Physics.IgnoreLayerCollision(bullet.layer, mPlayerModel.gameObject.layer);
				
				Vector3 worldPointFromScreenPoint = Camera.mainCamera.ScreenToWorldPoint(
					new Vector3 (Input.mousePosition.x, Input.mousePosition.y,Camera.mainCamera.nearClipPlane));
				
				Vector3 direction = new Vector3(worldPointFromScreenPoint.x, 0, worldPointFromScreenPoint.z);
				
				SimpleMovement simpleMovement = bullet.GetComponent("SimpleMovement") as SimpleMovement;
				simpleMovement.SetTarget(direction);
				
				// Decrease health on shooter
				mPlayerModel.AlterHealthPoints(-bulletModel.HealthCostPerBullet);
            }
        }
		
		if (Input.GetButtonDown ("Fire2"))
		{
			GameObject shield = Instantiate (Shield) as GameObject;
			
			ActorModel shieldModel = shield.GetComponent("ActorModel") as ActorModel;
			shieldModel.ParentActor = GetComponent("ActorModel") as ActorModel;
			
			Physics.IgnoreLayerCollision(shield.layer, mPlayerModel.gameObject.layer);
			
			if (shieldModel == null)
			{
				throw new MissingComponentException("ActorModel not found on shield");
			}
			
			//shield.transform.position += transform.position;			
		}
    }
}
