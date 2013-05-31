using UnityEngine;
using System.Collections;

public class ActorGameObjectModel : MonoBehaviour 
{
	public float HealthDrainPerTick = 0.00f;
	public float StartingHealth = 100.0f;
	public float InitialScale = 5.0f;
	public float MinScale = 1.0f;
	
	private float mHealthPoints;
	
	void Start () 
	{
		mHealthPoints = StartingHealth;
	}
	
	void Update () 
	{
		mHealthPoints -= HealthDrainPerTick;
		
		float newScale = (mHealthPoints/StartingHealth * (InitialScale-MinScale)) + MinScale;
		
		transform.localScale = new Vector3(newScale, 1.0f, newScale);
		
		if (mHealthPoints <= 0)
		{
			PlayerDeath();
		}
	}
	
	void PlayerDeath()
	{
		Instantiate(Resources.Load("Effects/DeathEffect"));
		Destroy(gameObject);
	}
	
	public void AlterHealthPoints(float damage)
	{
		mHealthPoints -= damage;
	}
}
