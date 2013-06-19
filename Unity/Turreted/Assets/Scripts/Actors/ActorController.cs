using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour 
{
	public UnityEngine.Object DeathEffect;
	private ActorModel mActorModel;
	void Awake () 
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
				Death (am);
			}
		}
	}
	
	void Death(ActorModel killer=null)
	{
		// Immediately turn off collision to avoid colliding with the effects you are spawning.
		this.collider.enabled = false;
		if (DeathEffect)
		{
			// Instantiate death effect.
			// Place at center of model that just died.
			GameObject effect = Instantiate(DeathEffect) as GameObject;
			effect.transform.position += transform.position;
			if (killer != null)
			{
				// This code is used for bombs right now.
				// If the actor the death effect is an actor
				// we are assuming it is a bomb explosion.
				// To ensure the explosion gives health to the right
				// actor we check if the killer has a parent and parent
				// either the parent of the kilelr or the killer.
				// This will make sure whatever the explosion kills 
				// will be contributed to the actor that triggered
				// the explosion.
				ActorModel am = effect.GetComponent("ActorModel") as ActorModel;
				if (am != null)
				{
					if (killer.ParentActor != null)
					{
						am.ParentActor = killer.ParentActor;
					}
					else
					{
						am.ParentActor = killer;
					}
					Debug.Log ("Setting parent to: " + am.ParentActor.Name);
				}
			}
		}
		Destroy(gameObject);
	}
	
	public void ScaleChange(float scale)
	{
		transform.localScale = new Vector3(	mActorModel.TransformScale.x * scale,
											mActorModel.TransformScale.y * scale,
											mActorModel.TransformScale.z * scale);
	}
}


	