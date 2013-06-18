using UnityEngine;
using System.Collections;

public class CombatReceiverModel : MonoBehaviour 
{
	// Used to enable/disable collision.  Useful for collision arbitration.
	public bool IsActive = true;
	// Used to disable death.
	public bool GodMode = false;
	// Flag allow death events to fire outside of physics arbitration, at beginning of next frame.
	public bool MarkedForDeath = false;
	
	public int CollisionMask = 0;
	
	public float InitialHealthPoints = 100;
	private float mHealthPoints;
	
	public float DamageToAttacker = 0;
	
	public void Awake()
	{
		mHealthPoints = InitialHealthPoints;
	}
	
	public void AlterHealthPoints(float delta)
	{
		mHealthPoints += delta;
	}
	
	public float HealthPoints
	{
		get { return mHealthPoints; }
	}
}
