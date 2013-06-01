using UnityEngine;
using System.Collections;

public class PlayerShootController : MonoBehaviour 
{
	public UnityEngine.Object Bullet;
	
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
				bullet.transform.position += transform.position;
				Physics.IgnoreCollision(bullet.collider, mPlayerModel.collider);
				
				Vector3 worldPointFromScreenPoint = Camera.mainCamera.ScreenToWorldPoint(
					new Vector3 (Input.mousePosition.x, Input.mousePosition.y,Camera.mainCamera.nearClipPlane));
				
				Vector3 direction = new Vector3(worldPointFromScreenPoint.x, 0, worldPointFromScreenPoint.z);
				
				SimpleMovement simpleMovement = bullet.GetComponent("SimpleMovement") as SimpleMovement;
				simpleMovement.SetTarget(direction);
            }
        }
    }
}
