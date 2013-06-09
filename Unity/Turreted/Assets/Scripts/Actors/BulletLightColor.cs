//DEPRECATED BEHAVIOR!  
//BulletCreation.cs handles this now!
//Sets the light components color to the whatever the color of the bullet is

using UnityEngine;
using System.Collections;

public class BulletLightColor : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		this.light.color = this.renderer.material.GetColor("_Color");
	}
	
	/*  Do not need to update this component every frame *currently*
	// Update is called once per frame
	void Update () 
	{
	
	}
	*/
}
