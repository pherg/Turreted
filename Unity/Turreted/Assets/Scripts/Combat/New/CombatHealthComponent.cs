using UnityEngine;
using System.Collections;

public class CombatHealthComponent : HealthComponent 
{
	public void ReceiveDamage(float damage)
	{
		AlterHealthPoints(-damage);
		//Debug.Log ("Current health: " + CurrentHealth);
		if (CurrentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
