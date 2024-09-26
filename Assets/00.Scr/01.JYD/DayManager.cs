using System;
using System.Collections;
using UnityEditor.Presets;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset lightingPreset;

    [SerializeField] [Range(0, 24)] private float timeOfDay;
    [SerializeField] private float period;
    [SerializeField] private float startTime;

    private float initialY;
    public static int Mul { get; set; } = 1;
    private void Awake()
    {
        timeOfDay = startTime;
        if(directionalLight != null)
            initialY = directionalLight.transform.eulerAngles.y;
    }
    private void Update()
    {
        if (lightingPreset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            timeOfDay += (Time.deltaTime / period) * Mul;
            timeOfDay %= 24;
            
            UpdateLighting(timeOfDay /24f);
        }
        else
        {
            UpdateLighting(timeOfDay /24f);
        }
        
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightingPreset.AembientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightingPreset.FogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = lightingPreset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent * 360 - 90f , initialY, 0));

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
        initialY = directionalLight.transform.eulerAngles.y;

    }
}