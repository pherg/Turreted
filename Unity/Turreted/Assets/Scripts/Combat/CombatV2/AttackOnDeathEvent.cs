using UnityEngine;
using System.Collections;

public class AttackOnDeathEvent : MonoBehaviour 
{
	public UnityEngine.Object DeathAttack;
	
	public void OnDeathEvent()
	{
		GameObject parent = gameObject;
		CombatAttackModel combatAttackModel = GetComponent("CombatAttackModel") as CombatAttackModel;
		if (combatAttackModel)
		{
			parent = combatAttackModel.Owner;
		}
		CombatGod.SpawnCombatActor(DeathAttack, this.transform.position, parent, combatAttackModel);
	}
}
