using UnityEngine;
using System.Collections;

public class DeathEffect : MonoBehaviour 
{
	public float RotationSpeed = 0.01f;
	public float ScaleSpeed = 0.05f;
	public float LifeTime = 10.0f;
	
	private float mTimeLapsed = 0.0f;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		mTimeLapsed += Time.deltaTime;
		if (mTimeLapsed >= LifeTime)
		{
			Destroy (gameObject);
			return;
		}
		
		transform.RotateAround(new Vector3(0, 1, 0), -RotationSpeed);
		transform.localScale = new Vector3(
			transform.localScale.x + ScaleSpeed,
			transform.localScale.y + ScaleSpeed,
			transform.localScale.z + ScaleSpeed
			);
		
	}
}
