using UnityEngine;
using System.Collections;

public class KillboxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerExit(Collider collider)
	{
		GameObject go = collider.gameObject;
		if (go)
		{
			Destroy(go);
		}
	}
}
