using UnityEngine;
using System.Collections;

public class ActorModelV2 : MonoBehaviour 
{
	public string Name = "Unamed Actor";
	
	public UnityEngine.Object DeathEffect;
	
	// Scale controls
	private Vector3 mInitialTransformScale;
	private float mScale = 1;
	
	// Model
	private ActorModel mParentActor;
	
	void Awake () 
	{
		mInitialTransformScale = transform.localScale;
	}
	
	public Vector3 TransformScale
	{
		get { return mInitialTransformScale; }
	}
	
	public float Scale
	{
		get { return mScale; }
		set 
		{ 
			mScale = value;
			SendMessage ("OnScaleChange", mScale, SendMessageOptions.DontRequireReceiver); 
		} 
	}
}
