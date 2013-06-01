using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour 
{
	public string DeathEffect = "Effects/DeathEffect";
	private ActorModel mActorModel;
	void Start () 
	{
		mActorModel = gameObject.GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find model for this actor.");
		}
	}
	
	void FixedUpdate () 
	{
		Debug.Log ("Actor health: " + mActorModel.HealthPoints + " DrainPerTick" + -mActorModel.HealthDrainPerTick);
		mActorModel.AlterHealthPoints(-mActorModel.HealthDrainPerTick);
		
		float newScale = (mActorModel.HealthPoints/mActorModel.StartingHealth 
						* (mActorModel.InitialScale - mActorModel.MinScale)) + mActorModel.MinScale;
		
		transform.localScale = new Vector3(newScale, 1.0f, newScale);
		
		if (mActorModel.HealthPoints <= 0)
		{
			Death();
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		ActorModel am = collision.collider.gameObject.GetComponent("ActorModel") as ActorModel;
		if (am && mActorModel.Team != am.Team)
		{
			mActorModel.AlterHealthPoints(-am.OnCollisionDamage);
		}
	}
	
	void Death()
	{
		GameObject effect = Instantiate(Resources.Load(DeathEffect)) as GameObject;
		effect.transform.position += transform.position;
		Destroy(gameObject);
	}
}
