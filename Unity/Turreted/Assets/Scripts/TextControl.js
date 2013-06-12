var isQuitButton = false;

public var Level;

function OnMouseEnter ()
{
	renderer.material.color = Color.green;
}

function OnMouseExit ()
{
	renderer.material.color = Color.white;
}

function OnMouseUp () 
{
	if( isQuitButton )
	{
		Application.Quit();
	}
	else
	{
		Application.LoadLevel(Level);
	}
}
