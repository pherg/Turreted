using UnityEngine;
using System.Collections;

public class ScaleByHealth : MonoBehaviour 
{
	public float InitialScale = 5.0f;
	public float MinScale = 1.0f;
	
	private ActorModel mActorModel;
	
	void Start()
	{
		mActorModel = GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModel on GameObject.");
		}
	}
	
	void Update()
	{
		float newScale = (mActorModel.HealthPoints/mActorModel.StartingHealth 
						* (InitialScale - MinScale + MinScale));
		newScale = Mathf.Max (newScale, MinScale);
		transform.localScale = new Vector3( mActorModel.TransformScale.x * newScale, 
											mActorModel.TransformScale.y * newScale,
											mActorModel.TransformScale.z * newScale);
	}
}
