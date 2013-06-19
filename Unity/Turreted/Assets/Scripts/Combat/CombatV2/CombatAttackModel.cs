using UnityEngine;
using System.Collections;

public class CombatAttackModel : MonoBehaviour 
{
	public bool IsActive = true;
	
	public int CollisionMask = 0;
	
	public float Damage = 10;
	
	// Used for reporting combat results to the owner of the attack.
	public GameObject Owner;
}
