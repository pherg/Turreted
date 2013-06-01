using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour 
{
	public float Speed = 2.0f;
	public float RandomizedHeading = 45.0f;
	
	private ActorModel mActorModel;
	
	private Vector3 mDirection;
	
	void Start () 
	{
		mActorModel = gameObject.GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find actor model on gameObject.");
		}
	}
	
	void FixedUpdate () 
	{
		rigidbody.AddForce(mDirection * Speed);
	}
	
	public void SetTarget(Vector3 target)
	{
		Vector3 initDirection = target - transform.position;
		initDirection.y = 0;
		initDirection.Normalize();

		Vector3 direction = Quaternion.AngleAxis(Random.Range(-RandomizedHeading, RandomizedHeading), Vector3.up) * initDirection;
		mDirection = direction;
	}
}
