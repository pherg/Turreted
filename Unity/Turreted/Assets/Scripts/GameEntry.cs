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
		Instantiate(Resources.Load("Actors/SpawnController"));
	}
	
	void BootstrapScene()
	{
		//Camera.mainCamera.transform.position = new Vector3(0, 50, 0);
		//Camera.mainCamera.transform.up = new Vector3(0, 0, 1);
		
		Instantiate(Resources.Load("SimpleParticleEffect"));
		
		Instantiate(Resources.Load("Floor"));
	}
}
