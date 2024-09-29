using System;
using System.Collections;
using UnityEditor.Presets;
using UnityEngine;

public class DayManager : MonoSingleton<DayManager>
{
    [Header("unityGameObject")]
    [SerializeField] private Light directionalLight;

    [Header("Data")]
    [SerializeField] private LightingPreset lightingPreset;
    [SerializeField] [Range(0, 24)] private float timeOfDay;
    [SerializeField] private float period;
    private float initialY;
    public static bool CanProcess { get; set; } = false;
    public static int Multiplier { get; set; } = 1;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        if (directionalLight != null)
            initialY = directionalLight.transform.eulerAngles.y;
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
    private void Update()
    {
        if (lightingPreset == null)
        {
            return;
        }
        if (Application.isPlaying && CanProcess)
        {
            timeOfDay += (Time.deltaTime / period) * Multiplier;
            timeOfDay %= 24;
        }
        UpdateLighting(timeOfDay /24f);
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
}