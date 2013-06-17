using UnityEngine;
using System.Collections;

public class GameEntry : MonoBehaviour 
{
	public UnityEngine.Object SpawnController;
	public UnityEngine.Object Player;
	
	private GameObject mPlayer;
	private ScoreTextfield mScoreTextfield;
	
	// Use this for initialization
	void Start () 
	{
		BootstrapScene();
		StartGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mPlayer == null && mScoreTextfield != null)
		{
			mScoreTextfield.StopTrackingScore();
		}
	}
	
	void StartGame()
	{
		mPlayer = Instantiate(Player) as GameObject;
		
		// hey - we can always load levels directly
		// GameObject level = Instantiate(Resources.Load ("SpawnControllers/LevelOne")) as GameObject;		
		
		Instantiate(SpawnController);
		GameObject score = Instantiate(Resources.Load ("GUI/ScoreGUI")) as GameObject;
		mScoreTextfield = score.GetComponent("ScoreTextfield") as ScoreTextfield;
		mScoreTextfield.TrackScore();
	}
	
	void BootstrapScene()
	{		
		Instantiate(Resources.Load("Actors/Floor"));
	}
}
