using UnityEngine;
using System.Collections;

public class CombatAttackModel : MonoBehaviour 
{
	public bool IsActive = true;
	
	public int CollisionMask = 0;
	
	public float Damage = 10;
	
	// Used for reporting combat results to the owner of the attack.
	public GameObject Owner;
	
	// Attack buffs.
	private float mBulletSpeedScale=1.0f;
	private float mDamageScale=1.0f;
	private float mBulletSizeScale=1.0f;
	private float mBulletMassScale=1.0f;
	private float mExplosionScale=1.0f;
	private float mHealthLossRateScale=1.0f;
	
	public float BulletSpeedScale
	{
		get { return mBulletSpeedScale; }
		set { mBulletSpeedScale = value; }
	}
	public float DamageScale
	{
		get { return mDamageScale; }
		set { mDamageScale = value; }
	}
	public float BulletSizeScale
	{
		get { return mBulletSizeScale; }
		set 
		{ 
			mBulletSizeScale = value; 
			SendMessage("OnBulletSizeScaleChange", mBulletSizeScale, SendMessageOptions.DontRequireReceiver);
		}
	}
	public float BulletMassScale
	{
		get { return mBulletMassScale; }
		set 
		{ 
			mBulletMassScale = value; 
			SendMessage("OnBulletMassScaleChange", mBulletMassScale, SendMessageOptions.DontRequireReceiver);
		}
	}
	public float ExplosionScale
	{
		get { return mExplosionScale; }
		set 
		{ 
			mExplosionScale = value; 
			SendMessage("OnExplosionSizeScaleChange", mBulletMassScale, SendMessageOptions.DontRequireReceiver);
		}
	}
	public float HealthLossRateScale
	{
		get { return mHealthLossRateScale; }
		set { mHealthLossRateScale = value; }
	}
}
