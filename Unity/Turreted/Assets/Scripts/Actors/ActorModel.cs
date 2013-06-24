using UnityEngine;
using System.Collections;

public class ActorModel : MonoBehaviour 
{
	public string Name = "Unamed Actor";
	
	public UnityEngine.Object DeathEffect;
	
	// Scale controls
	private Vector3 mInitialTransformScale;
	private float mScale = 1;
	
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
			SendScaleChangeHelper();
		} 
	}
	
	private float mScaleAddition = 0;
	public void AlterScaleAddition(float delta)
	{
		mScaleAddition += delta;
		SendScaleChangeHelper();
	}
	
	private float mScaleMultiplier = 1;
	public void AlterScaleMultiplier(float delta)
	{
		mScaleMultiplier += delta;
		SendScaleChangeHelper();
	}
	
	private void SendScaleChangeHelper()
	{
		SendMessage ("OnScaleChange", new OnScaleChangeEvent(mScale, mScaleAddition, mScaleMultiplier), SendMessageOptions.DontRequireReceiver); 
	}
}
