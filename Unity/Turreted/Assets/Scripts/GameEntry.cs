using UnityEngine;
using System.Collections;

public class GameEntry : MonoBehaviour 
{
	public UnityEngine.Object SpawnController;
	public UnityEngine.Object Player;
	
	void Start () 
	{
		GameObject levContGO = Instantiate(Resources.Load ("Utility/LevelController")) as GameObject;//(Player, SpawnController);
		LevelController lc = levContGO.GetComponent("LevelController") as LevelController;
		lc.Constructor(Player, SpawnController); //LevelController(null, null);
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
		// GameObject level = Instantiate(Resources.Load("SpawnControllers/LevelOne")) as GameObject;		
		
		// which means we can also do this
		// mPlayer = Instantiate(Resources.Load("Actors/Player")) as GameObject;
		
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
