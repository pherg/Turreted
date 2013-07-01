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
	
	public float DamageToAttackerOnHit = 0;
	public float DamageToAttackerOnKill = 0;
	
	public Color BaseColor;
	public bool AllowColorLeech = true;
	
	public void AlterHealthPoints(float delta)
	{
		mHealthPoints += delta;
		SendMessage("OnHealthPointChange", new OnHealthPointChange(delta, mHealthPoints), SendMessageOptions.DontRequireReceiver);
	}
	
	public float HealthPoints
	{
		get { return mHealthPoints; }
		set { mHealthPoints = value; }
	}
}
