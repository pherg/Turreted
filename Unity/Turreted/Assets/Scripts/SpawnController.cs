using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
	public float MinSpawnTime = 0.0f;
	public float MaxSpawnTime = 10.0f;
	
	public float DistanceFromCenter = 10.0f;
	
	private float mTimeSinceLastSpawn = 0;
	private float mNextSpawnTime = 0;
	
	// Use this for initialization
	void Start () 
	{
		Debug.Log("START!");
	}
	
	// Update is called once per frame
	void Update () 
	{
		mTimeSinceLastSpawn += Time.deltaTime;
		if (mTimeSinceLastSpawn >= mNextSpawnTime)
		{
			Debug.Log ("Attempting to spawn an actor.");
			Vector2 randPosition = Random.insideUnitCircle;
			randPosition.Normalize();
			randPosition *= DistanceFromCenter;
			SpawnActor(new Vector3(randPosition.x, 1, randPosition.y));
			ResetSpawnTimers();
		}
	}
	
	void SpawnActor(Vector3 position)
	{
		Instantiate(Resources.Load("Actors/Actor1"), position, Quaternion.identity);
		//GameObject newActor = Instantiate(Resources.Load("Actor1"), position, Quaternion.identity) as GameObject;
	}
	
	void ResetSpawnTimers()
	{
		mTimeSinceLastSpawn = 0;
		mNextSpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
	}
}
