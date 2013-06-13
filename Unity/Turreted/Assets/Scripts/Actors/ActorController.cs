using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour 
{
	public UnityEngine.Object DeathEffect;
	private ActorModel mActorModel;
	void Start () 
	{
		mActorModel = gameObject.GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			Debug.Log ("FOOOOO");
			throw new MissingComponentException("Unable to find model for this actor.");
		}
	}
	
	void FixedUpdate () 
	{
		mActorModel.AlterHealthPoints(-mActorModel.HealthDrainPerTick);
		
		if (mActorModel.HealthPoints <= 0)
		{
			Death();
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Collide(collision.collider);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		Collide(collider);
	}
	
	void Collide(Collider collider)
	{
		ActorModel am = collider.gameObject.GetComponent("ActorModel") as ActorModel;
		if (am != null && mActorModel.Team != am.Team)
		{
			//Debug.Log(am.Name + " dealing " + am.OnCollisionDamage + " damage to " + mActorModel.Name);
			mActorModel.AlterHealthPoints(-am.OnCollisionDamage);
			// If actor was killed report to parent of death.
			if (mActorModel.HealthPoints <= 0)
			{
				if (am.ParentActor)
				{
					am.ParentActor.InformOfKill(mActorModel);
				}
				Destroy (mActorModel);
			}
		}
	}
	
	void Death()
	{
		// Immediately turn off collision to avoid colliding with the effects you are spawning.
		this.collider.enabled = false;
		if (DeathEffect)
		{
			GameObject effect = Instantiate(DeathEffect) as GameObject;
			effect.transform.position += transform.position;
		}
		Debug.Log ("Destroying: " + mActorModel.Name);
		Destroy(gameObject);
	}
}
