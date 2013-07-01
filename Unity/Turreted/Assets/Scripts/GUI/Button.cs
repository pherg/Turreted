using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	void OnMouseEnter ()
	{
		renderer.material.color = Color.green;
	}
	
	void OnMouseExit ()
	{
		renderer.material.color = Color.white;
	}
}
