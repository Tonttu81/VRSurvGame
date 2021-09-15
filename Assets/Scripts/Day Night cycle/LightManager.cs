using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime/24;
            TimeOfDay %= 24; //clamp between 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePrecent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePrecent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePrecent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePrecent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePrecent * 360f) - 90f, 170f, 0));
        }
    }


    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
