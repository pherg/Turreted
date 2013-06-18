using UnityEngine;
using System.Collections;

public class ScaleOverTime : MonoBehaviour 
{
	public float LengthOfTime = 1;
	
	public float FinalScale = 2;
	
	private ActorModel mActorModel;
	
	private float mTimeSpent = 0;
	
	void Awake()
	{
		mActorModel = GetComponent("ActorModel") as ActorModel;
		if (mActorModel == null)
		{
			throw new MissingComponentException("Unable to find ActorModel on GameObject.");
		}
	}
	
	void Start()
	{
		StartCoroutine("Scale");
	}
	
	IEnumerator Scale()
	{
		while(true)
		{
			// Run until LenghtOfTime then destroy.
			if (mTimeSpent >= LengthOfTime)
			{
				StopCoroutine("Scale");
				Destroy (this.gameObject);
			}
		
			// Set scale on the model. 
			// May turn into a problem if multiple entities are fighting over scale.
			mActorModel.Scale = mTimeSpent / LengthOfTime * FinalScale + 1;

			mTimeSpent += Time.deltaTime;
			// Waiting one frame and run again.
			yield return 1;
		}
	}
}
