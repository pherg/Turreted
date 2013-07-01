using UnityEngine;
using System.Collections;

public class LoadLevelButton : Button 
{
	public string Level;
	
	void OnMouseUp () 
	{
		Application.LoadLevel(Level);
	}
}
