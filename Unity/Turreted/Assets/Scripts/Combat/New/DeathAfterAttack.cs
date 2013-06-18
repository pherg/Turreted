using UnityEngine;
using System.Collections;

public class DeathAfterAttack : MonoBehaviour 
{
	public void AttackHit(AttackCollision attackCollision)
	{
		Destroy(gameObject);
	}
}
