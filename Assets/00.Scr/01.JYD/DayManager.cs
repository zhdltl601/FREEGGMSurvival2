using System;
using System.Collections;
using UnityEditor.Presets;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset lightingPreset;

    [SerializeField] [Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float period;

    private void Update()
    {
        if (lightingPreset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            TimeOfDay += (Time.deltaTime / period);
            TimeOfDay %= 24;
            
            UpdateLighting(TimeOfDay /24f);
        }
        else
        {
            UpdateLighting(TimeOfDay /24f);
        }
        
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightingPreset.AembientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightingPreset.FogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = lightingPreset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent * 360 - 90f , 170f , 0));

        }
    }
    
    private void OnEnable()
    {
        if (directionalLight != null)
            return;

        if (RenderSettings.sun != null)
            return;
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            
            foreach (var light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}