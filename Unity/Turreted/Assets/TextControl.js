var isQuitButton = false;

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
		Application.LoadLevel(1);
	}
}
