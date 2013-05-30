using UnityEngine;
using System.Collections;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		BootstrapScene();
		StartGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void StartGame()
	{
		Instantiate(Resources.Load("Actors/Player"));
		Instantiate(Resources.Load("Actors/SpawnController"));
	}
	
	void BootstrapScene()
	{		
		Instantiate(Resources.Load("Actors/Floor"));
	}
}
