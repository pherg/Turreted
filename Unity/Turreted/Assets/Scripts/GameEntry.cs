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
	

}
