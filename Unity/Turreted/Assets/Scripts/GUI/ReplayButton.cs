using UnityEngine;
using System.Collections;

public class ReplayButton : Button 
{	
	void OnMouseUp () 
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
