using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
	public float MinSpawnTime = 0.0f;
	public float MaxSpawnTime = 10.0f;
	
	public float DistanceFromCenter = 10.0f;
	
	public UnityEngine.Object[] EnemyList;
	
	private float mTimeSinceLastSpawn = 0.0f;
	private float mNextSpawnTime = 0.0f;
	
	// Update is called once per frame
	void Update () 
	{
		mTimeSinceLastSpawn += Time.deltaTime;
		if (mTimeSinceLastSpawn >= mNextSpawnTime)
		{
			Vector2 randPosition = Random.insideUnitCircle;
			randPosition.Normalize();
			randPosition *= DistanceFromCenter;
			SpawnActor(new Vector3(randPosition.x, 0, randPosition.y));
			ResetSpawnTimers();
		}
	}
	
	void SpawnActor(Vector3 position)
	{
		int randVal = (int)(Random.value * 100);
		int index = randVal % EnemyList.Length;
		
		GameObject newActor = Instantiate(EnemyList[index]) as GameObject;
		//position.y += newActor.transform.localScale.y;
		newActor.transform.position = position;
		
		SimpleMovement moveScript = newActor.GetComponent("SimpleMovement") as SimpleMovement;
		if (moveScript == null)
		{
			throw new MissingComponentException("TODO: Refactor MoveScripts.");
		}
		moveScript.SetTarget(new Vector3(0, 0, 0));
		
		float rand = 1;//Random.Range(1, 5);
		// Just doing some goofy randomization.
		ActorModel am = newActor.GetComponent("ActorModel") as ActorModel;
		if (am)
		{
			am.Scale *= rand;
		}
		if (rigidbody) 
		{
			rigidbody.mass *= rand;
		}
		CombatReceiverModel crm = newActor.GetComponent("CombatReceiverModel") as CombatReceiverModel;
		if (crm)
		{
			crm.InitialHealthPoints *= rand;
		}
	}
	
	void ResetSpawnTimers()
	{
		mTimeSinceLastSpawn = 0;
		mNextSpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
	}
}
