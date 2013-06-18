using UnityEngine;
using System.Collections;

public class ReceiverCombatModel : MonoBehaviour 
{
	// Which Receiver mask to collide with
	public int CollisionMask = 0;
	// Should this attack continue to hit receivers.
	public bool IsActive = true;
}
