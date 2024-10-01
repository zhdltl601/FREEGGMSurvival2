using System;
using System.Collections;
using UnityEditor.Presets;
using UnityEngine;
public enum EDayState
{
    None = 0,
    Morning,
    Night
}
public class DayManager : MonoSingleton<DayManager>
{
    [Header("unityGameObject")]
    [SerializeField] private Light directionalLight;

    [Header("Data")]
    [SerializeField] private LightingPreset lightingPreset;
    [SerializeField] [Range(0, 24)] private float timeOfDay;
    [SerializeField] private float period;
    private float initialY;
    public float GetTimeOfDay => timeOfDay;
    public static bool CanProcess { get; set; } = false;
    public static bool Is2D { get; set; } = false;
    public static int Multiplier { get; set; } = 1;

    private static EDayState _currentState = EDayState.None;
    public static event Action<EDayState> OnChangeState;
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
        void CalcCurrentState()
        {

            bool IsMorning(float time)
            {
                bool isDay = 6 <= timeOfDay && timeOfDay <= 19;
                return isDay;// ? EDayState.Day : EDayState.Night;
            }
            EDayState _updatedState = IsMorning(timeOfDay) ? EDayState.Morning : EDayState.Night;
            bool onChange = _currentState != _updatedState;
            if (onChange)
            {
                _currentState = _updatedState;// isDay ? EDayState.Morning : EDayState.Night;
                OnChangeState?.Invoke(_updatedState);
            }
        }
        CalcCurrentState();
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