using UnityEngine;
using System.Collections;

public class DeathAfterMilliseconds : MonoBehaviour 
{
	public float MillisecondsUntilDeathEvent=1000;
	
	public void Start()
	{
		StartCoroutine(DeathCountdown());
	}
	
	public IEnumerator DeathCountdown()
	{
		yield return new WaitForSeconds(MillisecondsUntilDeathEvent*0.001f);
		SendMessage("OnDeathEvent", SendMessageOptions.DontRequireReceiver);
	}
}
