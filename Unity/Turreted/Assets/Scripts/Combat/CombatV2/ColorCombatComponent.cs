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
	}
	public void AttackCombatResult(CombatResult combatResult)
	{
		AlterColor(combatResult.ColorChangeAttacker);
	}
	
	private void AlterColor(Vector3 colorDelta)
	{
		float rFinal = Mathf.Clamp(renderer.material.color.r + colorDelta.x, 0, 255);
		float gFinal = Mathf.Clamp(renderer.material.color.g + colorDelta.y, 0, 255);
		float bFinal = Mathf.Clamp(renderer.material.color.b + colorDelta.z, 0, 255);
		//Debug.Log ("Alter color: " + rFinal + ", " + gFinal + ", " + bFinal);
		renderer.material.color = new Color(rFinal, gFinal, bFinal);
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
