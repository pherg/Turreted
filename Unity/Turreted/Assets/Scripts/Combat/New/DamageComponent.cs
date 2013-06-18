using UnityEngine;
using System.Collections;

public class DamageComponent : MonoBehaviour 
{
	public float BaseDamage = 10;
	
	private float mDamage;
	
	public void Awake()
	{
		mDamage = BaseDamage;
	}
	
	public void AttackHit(AttackCollision attackCollision)
	{
		attackCollision.Receiver.SendMessage("ReceiveDamage", mDamage, SendMessageOptions.DontRequireReceiver);
	}
}
