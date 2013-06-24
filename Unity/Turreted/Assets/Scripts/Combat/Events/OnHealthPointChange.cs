using UnityEngine;
using System.Collections;

public class OnHealthPointChange
{
	private float mChangeInHealthPoints = 0;
	public float ChangeInHealthPoints
	{
		get { return mChangeInHealthPoints; }
	}
	
	private float mCurrentHealthPoints = 0;
	public float CurrentHealthPoints
	{
		get { return mCurrentHealthPoints; }
	}
	
	public OnHealthPointChange(float changeInHealthPoints, float currentHealthPoints)
	{
		mChangeInHealthPoints = changeInHealthPoints;
		mCurrentHealthPoints = currentHealthPoints;
	}
}
