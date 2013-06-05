using UnityEngine;
using System.Collections;

public class BulletMovement : SimpleMovement 
{
	
	public void FixedUpdate()
	{
		// Do nothing on update.  Simply shoot.
	}
	
	public override void SetTarget(Vector3 target)
	{
		base.SetTarget(target);
		rigidbody.AddForce(mDirection * Speed, ForceMode.Impulse);
	}
}
