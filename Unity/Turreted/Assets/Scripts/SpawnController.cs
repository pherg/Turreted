using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
	private float mTimeSinceLastSpawn = 0;
	private float spawnTimer = 0;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		mTimeSinceLastSpawn += Time.deltaTime;
		if (mTimeSinceLastSpawn >= mNextSpawnTime)
	}
}
