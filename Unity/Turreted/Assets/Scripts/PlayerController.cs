using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float ShrinkagePerTick = 0.01f;
	public float MinScaleForDeath = 0.0f;
	
	void Start () 
	{
		Debug.Log ("PLAYER START");
	}
	
	void Update () 
	{
	
		Vector3 newScale = transform.localScale;
		newScale.x -= ShrinkagePerTick;
		newScale.z = newScale.x;
		
		transform.localScale = newScale;
		
		if (transform.localScale.x <= MinScaleForDeath)
		{
			PlayerDeath();
		}
	}
	
	void PlayerDeath()
	{
		Instantiate(Resources.Load("Effects/SimpleParticleEffect"));
		Debug.Log("DEATH");
		Destroy(gameObject);
	}
}
