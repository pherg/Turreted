using UnityEngine;
using System.Collections;

public class LoadLevelButton : MonoBehaviour 
{
	public string Level;
	
	void OnMouseEnter ()
	{
		renderer.material.color = Color.green;
	}
	
	void OnMouseExit ()
	{
		renderer.material.color = Color.white;
	}
	
	void OnMouseUp () 
	{
		Application.LoadLevel(Level);
	}
}
