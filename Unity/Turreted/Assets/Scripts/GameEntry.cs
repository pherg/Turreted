using UnityEngine;
using System.Collections;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject foo = Instantiate(Resources.Load("SimpleParticleEffect")) as GameObject;
		 foo.transform.position.Set(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
