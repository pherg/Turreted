using UnityEngine;
using System.Collections;

public class ActorControllerV2 : MonoBehaviour 
{
	protected ActorModelV2 mActorModel;
	void Awake () 
	{
		mActorModel = gameObject.GetComponent("ActorModelV2") as ActorModelV2;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find model for this actor.");
		}
	}
	
	void OnDeathEvent()
	{
		// Immediately turn off collision to avoid colliding with the effects you are spawning.
		this.collider.enabled = false;
		if (mActorModel.DeathEffect)
		{
			// Instantiate death effect.
			// Place at center of model that just died.
			GameObject effect = Instantiate(mActorModel.DeathEffect) as GameObject;
			effect.transform.position = transform.position;
		}
		Destroy(gameObject);
	}
	
	public void OnScaleChange(float scale)
	{
		transform.localScale = new Vector3(	mActorModel.TransformScale.x * scale,
											mActorModel.TransformScale.y * scale,
											mActorModel.TransformScale.z * scale);
	}
}


	