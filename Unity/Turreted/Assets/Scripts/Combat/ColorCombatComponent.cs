using UnityEngine;
using System.Collections;

public class ColorCombatComponent : MonoBehaviour 
{
	public float DamageMultiplierMax = 5;
	public float SpeedMultiplierMax = 5;
	public float SizeMultiplierMax = 5;
	public float HealthLossRateMultiplierMax = 5;
	public float BulletMassMultiplierMax = 5;
	public float ExplosionSizeMultiplierMax = 5;
	public float BulletCostReductionMinMultiplier = 0.1f;
	
	private Color mColor;
	
	public void Awake()
	{
		if ( renderer == null)
		{
			throw new MissingComponentException("Unable to find Renderer for ColorComponent.");
		}
		if (renderer.material == null)
		{
			throw new MissingComponentException("Unable to find Material for ColorComponent.");
		}
		
		mColor = renderer.material.color;
	}
	
	public void SetColor(Color color)
	{
		mColor = color;
		renderer.material.color = mColor;
		//Debug.Log ("Final color: " + rFinal + ", " + gFinal + ", " + bFinal);
	}
	
	public void AttackCombatResult(CombatResult combatResult)
	{
		AlterColor(combatResult.ColorChangeAttacker);
	}
	
	private void AlterColor(Vector3 colorDelta)
	{
		//Debug.Log ("Alter Color: " + colorDelta + " Pre Delta color: " + renderer.material.color.ToString("n3"));
		// Clamp between 0-1 because that's how colors roll.
		float rFinal = Mathf.Clamp(mColor.r + colorDelta.x, 0, 1);
		float gFinal = Mathf.Clamp(mColor.g + colorDelta.y, 0, 1);
		float bFinal = Mathf.Clamp(mColor.b + colorDelta.z, 0, 1);
		
		SetColor(new Color(rFinal, gFinal, bFinal));
	}
	
	public Color CurrentColor()
	{
		return mColor;
	}
	
	// Color dominance variables
	public float GetBulletSpeedScale()
	{
		return GreenDominance() * SpeedMultiplierMax + 1;
	}
	
	public float GetBulletSizeScale()
	{
		return BlueDominance() * SizeMultiplierMax + 1;
	}
	
	public float GetExplosionSizeScale()
	{
		return RedDominance() * ExplosionSizeMultiplierMax + 1;
	}
	
	// Raw Color scale variables
	public float GetHealthLossRateScale()
	{
		return renderer.material.color.g * HealthLossRateMultiplierMax;
	}
	
	public float GetBulletMassScale()
	{
		return renderer.material.color.b * BulletMassMultiplierMax + 1;
	}
	
	public float GetDamageScale()
	{
		return renderer.material.color.r * DamageMultiplierMax + 1;
	}
	
	public float GetBulletCostReductionScale()
	{
		return 1 - (BlueDominance() * BulletCostReductionMinMultiplier);
	}
	
	private float RedDominance()
	{
		return GetColorDominanceVector()[0];
	}
	
	private float GreenDominance()
	{
		return GetColorDominanceVector()[1];
	}
	
	private float BlueDominance()
	{
		return GetColorDominanceVector()[2];
	}
	
	private Vector3 GetColorDominanceVector()
	{
		Vector3 colorVector = new Vector3(	renderer.material.color.r, 
											renderer.material.color.g, 
											renderer.material.color.b);
		colorVector.Normalize();
		//Debug.Log ("COLOR: " + renderer.material.color.ToString());
		//Debug.Log ("COLOR DOMINANCE VECTOR: " + colorVector.ToString());
		return colorVector;	
	}
}
