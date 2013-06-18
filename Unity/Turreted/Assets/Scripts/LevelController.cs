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
	
	public void Constructor (UnityEngine.Object player, UnityEngine.Object spawnController)
	{
		mScene = UnityEngine.Application.loadedLevel;
		
		mPlayerEngineObject = player;
		mSpawnControllerEngineObject = spawnController;
		
		BootstrapScene();
		
		StartGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mPlayer == null && mScoreTextfield != null)
		{
			mScoreTextfield.StopTrackingScore();
			Application.LoadLevel(mScene);
		}
	}
	
	void StartGame()
	{
		mPlayer = Object.Instantiate(mPlayerEngineObject) as GameObject;
		mSpawnController = Object.Instantiate(mSpawnControllerEngineObject) as GameObject;
		GameObject score = Object.Instantiate(Resources.Load ("GUI/ScoreGUI")) as GameObject;
		mScoreTextfield = score.GetComponent("ScoreTextfield") as ScoreTextfield;
		mScoreTextfield.TrackScore();
		
		
	}
	
	void BootstrapScene()
	{		
		Object.Instantiate(Resources.Load("Actors/Floor"));
	}
}
