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
	
	void Update()
	{
		float newScale = (mActorModel.HealthPoints/mActorModel.StartingHealth 
						* (mActorModel.InitialScale - mActorModel.MinScale)) + mActorModel.MinScale;
		
		transform.localScale = new Vector3(newScale, newScale, newScale);
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
		ActorModel am = collision.collider.gameObject.GetComponent("ActorModel") as ActorModel;
		if (am && mActorModel.Team != am.Team)
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
			}
		}
	}
	
	void Death()
	{
		GameObject effect = Instantiate(Resources.Load(DeathEffect)) as GameObject;
		effect.transform.position += transform.position;
		Destroy(gameObject);
	}
}
