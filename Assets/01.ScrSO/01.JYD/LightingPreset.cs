using UnityEngine;

[CreateAssetMenu(menuName = "SO/LightPreset")]
public class LightingPreset : ScriptableObject
{
    public Gradient AembientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
    
}
