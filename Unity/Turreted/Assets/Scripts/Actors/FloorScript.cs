/* Background object for the scene.
 * Simple colored rectangle.
 * Desired functionality:
 * 1) Parses the scene and finds the average color of the entities (players, bullets, and enemies)
 * 2) Calculates the complementary color of that average
 * 3) Renders as the complementary color found
*/

using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour 
{
	//Color of the rectangle
	//r,g,b,a
	Color mColor;

	// Use this for initialization
	void Start() 
	{
		//default color values
		mColor.r = 0;
		mColor.g = 0;
		mColor.b = 0;
		mColor.a = 0;
	}
	
	// Update is called once per frame
	void Update() 
	{
		renderer.material.SetColor("_Color", mColor );
	}
	
	//@TODO
	// Finds the Complementary color of c
	private Color GetComplementary( Color c )
	{
		Color comp = Color.gray;
		
		//DOING THANGS
		
		return comp;
	}
}
