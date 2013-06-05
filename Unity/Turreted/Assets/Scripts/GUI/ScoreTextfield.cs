using UnityEngine;
using System.Collections;

public class ScoreTextfield : MonoBehaviour 
{
	private string mScoreString;
	private string mScorePrefixString = "AGE: ";
	
	public Rect mScorePosition = new Rect(0, 0, 100, 100);
	
	private float mTimeSpent = 0;
	
	// Update is called once per frame
	void Update () 
	{
		mTimeSpent += Time.deltaTime;
		mScoreString = mScorePrefixString + mTimeSpent.ToString("f2");
	}
	
	void OnGUI()
	{
		GUI.TextField(mScorePosition, mScoreString);
	}
}
