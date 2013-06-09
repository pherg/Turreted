//This sets up the bullets basic properties on creation
//Properties are inherited from the player object

using UnityEngine;
using System.Collections;

public class BulletCreation : MonoBehaviour 
{
	//Percentage of size the bullets light object uses as its range
	private const float LIGHT_PERCENTAGE = 5.0f;
	
	//Percentage of size the bullets inherit from the player object
	private const float SIZE_PERCENTAGE = 0.4f;
	
	//Percentage of mass the bullets inherit from the player object
	private const float MASS_PERCENTAGE = 2.0f;
	
	// Use this for initialization
	void Start () 
	{
		//Get the ActorModel so we can get the parent object
		ActorModel am = this.GetComponent("ActorModel") as ActorModel;
		if( !am )
		{
			throw new MissingComponentException("Unable to find model for this actor.");
		}
		
		//Set up the bullets data based on the parent object
		
		//Set the projectiles color, this will need to be adjusted if/when we get inner/core colors added
		renderer.material.SetColor("_Color", am.ParentActor.renderer.material.GetColor("_Color"));
		light.color = am.ParentActor.renderer.material.GetColor("_Color");
		light.range = am.ParentActor.GetScale() * LIGHT_PERCENTAGE;
		
		//@TODO:
		//Do all the fun calculations for decrementing health on bullet creation based on bullet size
		
		//Change bullet size based on player size
		//Make size potentially based on color value and not player size?
		am.InitialScale = am.ParentActor.GetScale() * SIZE_PERCENTAGE;
		
		//Bullet momentum based on player size
		rigidbody.mass = am.ParentActor.GetScale() * MASS_PERCENTAGE;
		
		//Change bullet damage based on player color
		//Change bullet speed based on player color
	}
	
	/*  We do not need to update the bullets properties here
	void Update () 
	{
	
	}
	*/
}
