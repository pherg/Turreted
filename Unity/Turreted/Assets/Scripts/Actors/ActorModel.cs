using UnityEngine;
using System.Collections;

public class ActorModel : MonoBehaviour 
{
	public float HealthDrainPerTick = 0.00f;
	public float StartingHealth = 100.0f;
	
	// Player is currently set to 1.
	// Enemies are currently 2.
	public float Team = 2;
	
	public float InitialScale = 5.0f;
	public float MinScale = 1.0f;
	
	public float OnCollisionDamage = 1.0f;
	
	private float mHealthPoints;
	
	void Awake () 
	{
		mHealthPoints = StartingHealth;
		Debug.Log ("Setting starting health to: " + StartingHealth);
	}
	
	void Update () 
	{
		// MODELS HAVE NO UPDATE THEY ARE JUST DATUMS
		// Code in Models should only be transactional 
		// data manipulation, getters and setters
		// E.G.
		// Health Points have been altered.
		// Set Weapon X as ative Weapon.
	}
	
	public float HealthPoints
	{
		get { return this.mHealthPoints; }
	}
	
	// Changes health points by delta.
	public void AlterHealthPoints(float delta)
	{
		mHealthPoints += delta;
	}
}
