using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour 
{
	protected ActorModel mActorModel;
	void Awake () 
	{
		mActorModel = gameObject.GetComponent("ActorModel") as ActorModel;
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
	
	public void OnScaleChange(OnScaleChangeEvent scaleEvt)
	{
		float scale = scaleEvt.Scale * (scaleEvt.AdditionalScale + scaleEvt.MultiplierScale);
		transform.localScale = new Vector3(	mActorModel.TransformScale.x * scale,
											mActorModel.TransformScale.y * scale,
											mActorModel.TransformScale.z * scale);
	}
	
	public void OnMassChange(OnMassChangeEvent massEvt)
	{
		if (rigidbody)
		{
			rigidbody.mass = rigidbody.mass * (massEvt.AdditionalScale + massEvt.MultiplierScale);
		}
	}
}
	