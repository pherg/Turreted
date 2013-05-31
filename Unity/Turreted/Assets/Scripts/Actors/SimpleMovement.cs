using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour 
{
	public float Speed = 2.0f;
	public float RandomizedHeading = 45.0f;
	public float Damage = 10;
	
	private Vector3 mDirection;
	
	void Start () 
	{
	}
	
	void Update () 
	{
		rigidbody.AddForce(mDirection * Speed);
	}
	
	public void SetDirection(Vector3 target)
	{
		Vector3 initDirection = Vector3.zero - transform.position;
		initDirection.y = 0;
		initDirection.Normalize();

		Vector3 direction = Quaternion.AngleAxis(Random.Range(-RandomizedHeading, RandomizedHeading), Vector3.up) * initDirection;
		mDirection = direction;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		PlayerController pc = collision.collider.gameObject.GetComponent("PlayerController") as PlayerController;
		if (pc)
		{
			pc.AlterHealthPoints(Damage);
		}
	}
}
