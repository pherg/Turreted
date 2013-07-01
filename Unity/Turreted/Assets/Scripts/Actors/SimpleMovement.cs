using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour 
{
	public float Speed = 2.0f;
	public float RandomizedHeadingMax = 45.0f;
	public float RandomizedHeadingMin = 0.0f;
	
	private ActorModel mActorModel;
	
	protected Vector3 mDirection;
	
	void Awake () 
	{
		mActorModel = gameObject.GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find actor model on gameObject.");
		}
	}
	
	void FixedUpdate () 
	{
		rigidbody.AddForce(mDirection * Speed, ForceMode.VelocityChange);
	}
	
	public virtual void SetTarget(Vector3 target)
	{
		Vector3 initDirection = target - transform.position;
		initDirection.y = 0;
		initDirection.Normalize();
		
		float randomizedHeading = Random.Range(RandomizedHeadingMin, RandomizedHeadingMax);
		if (Random.value < 0.5f)
		{
			randomizedHeading *= -1;
		}
		
		Vector3 direction = Quaternion.AngleAxis(randomizedHeading, Vector3.up) * initDirection;
		mDirection = direction;
		
		//rigidbody.AddForce(mDirection * Speed, ForceMode.VelocityChange);
	}
}
