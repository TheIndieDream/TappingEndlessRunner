using UnityEngine;

/// <summary>
/// Scriptable object variable to represent an integer.
/// </summary>
[CreateAssetMenu(fileName = "new Color Variable", 
	menuName = "Variables.../Color")]
public class ColorVariable : ScriptableObject
{
	[Tooltip("Color value of the variable.")]
	public Color Value;
}
