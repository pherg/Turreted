using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour 
{
	public float GravitationalForce = 10;
	
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.rigidbody)
		{
			ApplyGravitationalForce(collision.gameObject);
		}
	}
	
	public void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.rigidbody)
		{
			ApplyGravitationalForce(collision.gameObject);
		}
	}
	
	public void OnTriggerStay(Collider collider)
	{
		if (collider.gameObject.rigidbody)
		{
			ApplyGravitationalForce(collider.gameObject);
		}
	}
	
	public void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.rigidbody)
		{
			ApplyGravitationalForce(collider.gameObject);
		}
	}
	
	private void ApplyGravitationalForce(GameObject orbiter)
	{
		Vector3 positionOfOrbiter = orbiter.transform.position;
		Vector3 direction = new Vector3(transform.position.x - positionOfOrbiter.x,
										transform.position.y - positionOfOrbiter.y,
										transform.position.z - positionOfOrbiter.z);
		
		float gravityStrength = GravitationalForce * orbiter.rigidbody.mass / direction.magnitude;
		orbiter.rigidbody.AddForce(	direction.x * gravityStrength,
									direction.y * gravityStrength,
									direction.z * gravityStrength,
									ForceMode.Acceleration);
		Debug.Log ("Applying gravitation force.");
	}
}
