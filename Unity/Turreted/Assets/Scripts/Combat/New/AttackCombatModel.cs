using UnityEngine;
using System.Collections;

public class AttackCombatModel : MonoBehaviour 
{
	// Which gameObject owns this attack.
	// Used for communicating result back up the chain.
	public GameObject OwnerGameObject;
	// Which Receiver mask to collide with
	public int CollisionMask = 0;
	// Should this attack continue to hit receivers.
	public bool IsActive = true;
}
