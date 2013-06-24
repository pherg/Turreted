using UnityEngine;
using System.Collections;

public class DeathOnCombatColision : MonoBehaviour 
{
	public void AttackCombatResult()
	{
		SendMessage("OnDeathEvent", SendMessageOptions.DontRequireReceiver);
	}
	
	public void ReceiveCombatResult()
	{
		SendMessage("OnDeathEvent", SendMessageOptions.DontRequireReceiver);
	}
}
