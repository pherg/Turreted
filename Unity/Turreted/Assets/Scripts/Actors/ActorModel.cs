using UnityEngine;
using System.Collections;

public class ActorModel : MonoBehaviour 
{
	public string Name = "Unamed Actor";
	public float HealthDrainPerTick = 0.00f;
	public float StartingHealth = 100.0f;
	
	// Player is currently set to 1.
	// Enemies are currently 2.
	public float Team = 2;
	
	public float InitialScale = 5.0f;
	public float MinScale = 1.0f;
	
	public float OnCollisionDamage = 1.0f;
	
	public float HealthCostPerBullet = 10.0f;
	
	public float HealthGainPerKill = 20.0f;
	
	private float mHealthPoints;
	
	private ActorModel mParentActor;
	
	void Awake () 
	{
		mHealthPoints = StartingHealth;
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
	
	public ActorModel ParentActor
	{
		set { mParentActor = value; }
		get { return mParentActor; }
	}
	
	// Changes health points by delta.
	public void AlterHealthPoints(float delta)
	{
		//Debug.Log (Name + " altering health points by : " + delta);
		mHealthPoints += delta;
	}
	
	public void InformOfKill(ActorModel killedActor)
	{
		AlterHealthPoints(killedActor.HealthGainPerKill);
	}
}
