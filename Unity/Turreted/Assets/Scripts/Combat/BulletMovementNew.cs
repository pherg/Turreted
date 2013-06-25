using UnityEngine;
using System.Collections;

public class BulletMovementNew : MonoBehaviour
{	
	public float BaseSpeed = 10;
	
	private Vector3 mDirection;
	
	private CombatAttackModel mCombatAttackModel;
	
	public void Awake()
	{
		mCombatAttackModel = gameObject.GetComponent("CombatAttackModel") as CombatAttackModel;
	}
	
	private float DetermineSpeed()
	{
		float speed = BaseSpeed;
		if (mCombatAttackModel)
		{
			speed *= mCombatAttackModel.BulletSpeedScale;
		}
		return speed;
	}
	
	public void Start()
	{
		rigidbody.AddForce(mDirection * DetermineSpeed(), ForceMode.VelocityChange);
	}
	
	public virtual void SetTarget(Vector3 target)
	{
		Vector3 initDirection = target - transform.position;
		initDirection.y = 0;
		initDirection.Normalize();

		Vector3 direction = Quaternion.AngleAxis(0, Vector3.up) * initDirection;
		mDirection = direction;
		
		
		transform.LookAt(new Vector3(0, -100, 0), initDirection);
	}
}
