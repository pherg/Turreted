using UnityEngine;
using System.Collections;

public class ScoreTextfield : MonoBehaviour 
{
	private string mScoreString = "";
	private string mScorePrefixString = "AGE: ";
	
	public Rect mScorePosition = new Rect(0, 0, 100, 100);
	
	private float mTimeSpent = 0;
	
	private bool mTrackScore = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(mTrackScore)
		{
			mTimeSpent += Time.deltaTime;
			mScoreString = mScorePrefixString + mTimeSpent.ToString("f2");
		}
	}
	
	public void TrackScore()
	{
		mTrackScore = true;
	}
	
	public void StopTrackingScore()
	{
		mTrackScore = false;
	}
	
	void OnGUI()
	{
		GUI.TextField(mScorePosition, mScoreString);
	}
}
