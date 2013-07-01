using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour 
{
	private GameObject mSpawnController;
	private GameObject mPlayer;
	
	private ScoreTextfield mScoreTextfield;
	
	private int mScene = 0;
	private UnityEngine.Object mPlayerEngineObject;
	private UnityEngine.Object mSpawnControllerEngineObject;
	
	private GameObject mReplayGUI;
	
	public void Constructor (UnityEngine.Object player, UnityEngine.Object spawnController)
	{
		mScene = UnityEngine.Application.loadedLevel;
		
		mPlayerEngineObject = player;
		mSpawnControllerEngineObject = spawnController;
		
		BootstrapScene();
		
		StartGame();
	}
	
	private float stupidCounterForDelayedGameOver = 0;
	private float mStupidCounter = 100;
	// Update is called once per frame
	void Update () 
	{
		if (mPlayer == null && mScoreTextfield != null && mReplayGUI == null)
		{
			stupidCounterForDelayedGameOver++;
			if ( stupidCounterForDelayedGameOver > mStupidCounter && mReplayGUI == null)
			{
				mReplayGUI = Object.Instantiate(Resources.Load ("GUI/GameOverGUI")) as GameObject;
			}
			mScoreTextfield.StopTrackingScore();
		}
	}
	
	void StartGame()
	{
		mPlayer = Object.Instantiate(mPlayerEngineObject) as GameObject;
		mSpawnController = Object.Instantiate(mSpawnControllerEngineObject) as GameObject;
		if(mSpawnController) {}// THis is only here to make hte compiler stop bitching about that warning..
		GameObject score = Object.Instantiate(Resources.Load ("GUI/ScoreGUI")) as GameObject;
		mScoreTextfield = score.GetComponent("ScoreTextfield") as ScoreTextfield;
		mScoreTextfield.TrackScore();
		
		
	}
	
	void BootstrapScene()
	{		
		Object.Instantiate(Resources.Load("Actors/Floor"));
	}
}
