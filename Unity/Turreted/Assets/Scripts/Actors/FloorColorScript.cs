/* Background object for the scene.
 * Simple colored rectangle.
 * Desired functionality:
 * 1) Parses the scene and finds the average color of the entities (players, bullets, and enemies)
 * 2) Calculates the complementary color of that average
 * 3) Renders as the complementary color found
*/

using UnityEngine;
using System.Collections;

//Hue, saturation, lightness color component
//h = 0 - 360
//s = 0-1
//l = 0-1
public struct HSL
{
	public float h;
	public float s;
	public float l;
};

public class FloorColorScript : MonoBehaviour 
{
	//Number of frames that it takes to interpolate from mPreviousColor to mNextColor
	private const float INTERPOLATE_RATE = 20;
	
	//Colors we're interpolating between
	Color mPreviousColor, mNextColor;
	
	//Frame of the interpolation, 0 to 20
	uint mFrame;

	// Use this for initialization
	void Start() 
	{
		//default color values
		mPreviousColor = renderer.material.GetColor("_Color");
		mNextColor     = GetComplementary( AverageColorOfScene() ); 
		
		mFrame = 0;
	}
	
	// Update is called once per frame
	void Update() 
	{
		mFrame++;
		Color currentColor = Color.blue;
		
		currentColor.r = (((float)mFrame*(1/INTERPOLATE_RATE))*(mNextColor.r - mPreviousColor.r)) + mPreviousColor.r;
		currentColor.g = (((float)mFrame*(1/INTERPOLATE_RATE))*(mNextColor.g - mPreviousColor.g)) + mPreviousColor.g;
		currentColor.b = (((float)mFrame*(1/INTERPOLATE_RATE))*(mNextColor.b - mPreviousColor.b)) + mPreviousColor.b;
		
		renderer.material.SetColor("_Color", currentColor );
		
		//If we're gone up 10 frames, its time to get a new color to interpolate to
		if ( mFrame >= INTERPOLATE_RATE )
		{
			mPreviousColor = mNextColor;
			
			mNextColor = GetComplementary( AverageColorOfScene() ); 
			
			mFrame = 0;
		}
	}
	
	// Finds the Complementary color of c
	private Color GetComplementary( Color c )
	{
		Color comp = Color.red;
		
		//Step one:  Convert color to HSL.
		HSL _hsl = RGBtoHSL(c);
		
		//Step two:  Change the Hue value to that of the Hue opposite (0-360 degrees, so +180 degrees and clamped)
		_hsl.h += 180;
		if ( _hsl.h > 360 )
		{
			_hsl.h -= 360;
		}
		
		//Step three:  Leave the Saturation and Lightness values as they were for true complementary color
		//			   We may want to dial down the saturation or lightness since this is the background object
		
		//Step four:  Convert color back to RGB
		comp = HSLtoRGB(_hsl);
		
		return comp;
	}
	
	//Given HSL, returns RGB
	private Color HSLtoRGB( HSL _hsl )
	{
		//default value
		Color rgb = Color.green;
		
		//number wizardry
		//from http://www.rapidtables.com/convert/color/hsl-to-rgb.htm
		float c = (1 - (Mathf.Abs(2*_hsl.l) - 1)) * _hsl.s,
			  x =  c * ( 1 - Mathf.Abs (((_hsl.h/60)%2) - 1)),
			  m = _hsl.l - c/2;
		
		//default value
		Color rgbDelta = Color.black;
		
		//To find what rgbDelta is, we need
		if ( 0 <= _hsl.h  && _hsl.h < 60 )
		{
			rgbDelta.r = c;
			rgbDelta.g = x;
			rgbDelta.b = 0;
		}
		else if ( 60 <= _hsl.h  && _hsl.h < 120 )
		{
			rgbDelta.r = x;
			rgbDelta.g = c;
			rgbDelta.b = 0;
		}
		else if ( 120 <= _hsl.h  && _hsl.h < 180 )
		{
			rgbDelta.r = 0;
			rgbDelta.g = c;
			rgbDelta.b = x;
		}
		else if ( 180 <= _hsl.h  && _hsl.h < 240 )
		{
			rgbDelta.r = 0;
			rgbDelta.g = x;
			rgbDelta.b = c;
		}
		else if ( 240 <= _hsl.h  && _hsl.h < 300 )
		{
			rgbDelta.r = x;
			rgbDelta.g = 0;
			rgbDelta.b = c;
		}
		else //300 <= _hsl.h  && _hsl.h < 360
		{
			rgbDelta.r = c;
			rgbDelta.g = 0;
			rgbDelta.b = x;
		}
		
		rgb.r = rgbDelta.r + m;
		rgb.g = rgbDelta.g + m;
		rgb.b = rgbDelta.b + m;
		
		return rgb;
	}
	
	//Given RGB, returns HSL
	private HSL RGBtoHSL( Color c )
	{
		HSL _hsl;
		_hsl.h = 0;
		_hsl.l = 0;
		_hsl.s = 0;
		
		//RGB is already in range of 0 to 1
		//number wizardry from:  http://www.rapidtables.com/convert/color/rgb-to-hsl.htm
		float max = Mathf.Max( c.r, Mathf.Max( c.g, c.b ) ),
		      min = Mathf.Min( c.r, Mathf.Min( c.g, c.b ) ),
			  delta = max - min; 
		
		//Lightness calculation
		_hsl.l = (max+min)/2;
		
		//Calculate hue based on what the largest rgb component was
		if ( max == c.r )
		{
			_hsl.h = 60 * (((c.g - c.b)/delta)%6);
		}
		else if ( max == c.g )
		{
			_hsl.h = 60 * (((c.b - c.r)/delta)+2);
		}
		else //max = c.b
		{
			_hsl.h = 60 * (((c.r - c.g)/delta)+4);
		}
		
		//saturation calculation
		if ( delta == 0 )
		{
			_hsl.s = 0;
		}
		else
		{
			_hsl.s = delta / ( 1 - Mathf.Abs(2*_hsl.l - 1) );
		}
		
		return _hsl;
	}
	
	//Returns the average color of all objects in the scene
	//Because the objects in the scene have variable color, optimizing a running average of the color is a bit tricky
	//We shouldn't have too many objects in this game so a brute force average calculation every frame should suffice
	//This is potentially slow as balls though
	private Color AverageColorOfScene()
	{
		Color c = Color.blue;
		
		GameObject[] actors = GameObject.FindGameObjectsWithTag("ColorObject");
		//ColorObject[] actors = FindObjectsOfType(typeof(ColorObject));
		
		float r = 0, g = 0, b = 0;
		
		for( uint i = 0; i < actors.Length ; i++ )
		{
			Color temp = actors[i].renderer.material.GetColor("_Color");
			
			//This is just a rough way to scale an objects impact on the scenes average color
			//This probably requires some tuning
			float scale = actors[i].transform.localScale.x / 2;
			
			r += temp.r * scale;
			g += temp.g * scale;
			b += temp.b * scale;
		}
		
		c.r = r / actors.Length;
		c.g = g / actors.Length;
		c.b = b / actors.Length;
		
		return c;
	}
}
