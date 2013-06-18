using UnityEngine;
using System.Collections;

public class BulletMovementNew : MonoBehaviour
{
	//private BulletModel mBulletModel;
	
	public float Speed = 10;
	
	private Vector3 mDirection;
	
	public void Awake()
	{
	}
	
	public void Start()
	{
		rigidbody.AddForce(mDirection * Speed, ForceMode.VelocityChange);
	}
	
	public virtual void SetTarget(Vector3 target)
	{
		Vector3 initDirection = target - transform.position;
		initDirection.y = 0;
		initDirection.Normalize();

		Vector3 direction = Quaternion.AngleAxis(0, Vector3.up) * initDirection;
		mDirection = direction;
	}
}
