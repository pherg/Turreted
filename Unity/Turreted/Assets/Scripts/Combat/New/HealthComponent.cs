using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour 
{
	public float InitialHealth = 100;
	
	private float mCurrentHealth;
	
	public void Awake()
	{
		mCurrentHealth = InitialHealth;
	}
	
	public float CurrentHealth
	{
		get { return mCurrentHealth; }	
	}
	
	public void AlterHealthPoints(float delta)
	{
		mCurrentHealth += delta;
	}
}
