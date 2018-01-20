using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

    public enum WeatherType
    {
        Sunny, Cloudy, Overcast, Raining, HeavyRain, Thunderstorm, 
    }

    public Transform player;

    [Header("Weather Objects")]
    public GameObject rain;
    public GameObject dayNight;
    public GameObject cloud;
    public GameObject nightSky;
    public GameObject thunder;
    public GameObject wind;

    [Header("Current Weather")]
    public WeatherType weatherType;
    public float transitionSpeed;

    [Header("Weather Definitions")]
    public WeatherDefinition sunnyWeather;
    public WeatherDefinition cloudyWeather;
    public WeatherDefinition overcastWeather;
    public WeatherDefinition rainingWeather;
    public WeatherDefinition heavyRainWeather;
    public WeatherDefinition thunderstormWeather;

    // Day and dight
    private DayNight dayNightScript;
    private Light sun;

    // DownFall
    private ParticleSystem rainSystem;
    private AudioSource rainSound;

    // Sky
    private Renderer cloudR;
    private Renderer nightSkyR;

    // Thunder
    private Light thunderLight;
    private AudioSource thunderSound;
    private bool isThundering;

    // Wind
    private WindZone windZone;

    private void Start()
    {
        dayNightScript = dayNight.GetComponentInChildren<DayNight>();
        sun = dayNight.GetComponentInChildren<Light>();

        rainSystem = rain.GetComponentInChildren<ParticleSystem>();
        rainSound = rain.GetComponentInChildren<AudioSource>();

        cloudR = cloud.GetComponent<Renderer>();
        nightSkyR = nightSky.GetComponent<Renderer>();

        thunderLight = thunder.GetComponent<Light>();
        thunderSound = thunder.GetComponent<AudioSource>();

        windZone = wind.GetComponent<WindZone>();
    }

    private void Update()
    {
        switch (weatherType)
        {
            case WeatherType.Sunny:
                UpdateWeather(sunnyWeather);
                break;
            case WeatherType.Cloudy:
                UpdateWeather(cloudyWeather);
                break;
            case WeatherType.Overcast:
                UpdateWeather(overcastWeather);
                break;
            case WeatherType.Raining:
                UpdateWeather(rainingWeather);
                break;
            case WeatherType.HeavyRain:
                UpdateWeather(heavyRainWeather);
                break;
            case WeatherType.Thunderstorm:
                UpdateWeather(thunderstormWeather);
                break;
        }

        // Place at player
        Vector3 newPos = player.position;
        //newPos.y = 0.0f;
        transform.position = newPos;
    }

    private void UpdateWeather(WeatherDefinition wd)
    {
        // Rain
        ParticleSystem.EmissionModule em = rainSystem.emission;
        em.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Lerp(em.rateOverTime.constant, wd.rainAmount, Time.deltaTime * transitionSpeed));
        rainSound.volume = Mathf.Lerp(rainSound.volume, Mathf.Clamp(wd.rainAmount / 3000.0f, 0.0f, 1.0f), Time.deltaTime * transitionSpeed);

        // Fog
        RenderSettings.fogDensity= Mathf.Lerp(RenderSettings.fogDensity, wd.fog, Time.deltaTime * transitionSpeed);
        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, wd.fogColor, Time.deltaTime * transitionSpeed);

        // Sun
        sun.color = Color.Lerp(sun.color, wd.sunColor, Time.deltaTime * transitionSpeed);

        // Thunder
        if(wd.thunderIntensity > 0)
        {
            if (!isThundering)
            {
                isThundering = true;
                StartCoroutine("Thunder_C", wd.thunderIntensity);
            }
        }
        else
        {
            if (isThundering)
            {
                StopCoroutine("Thunder_C");
                isThundering = false;
            }
        }

        // Wind


        // Clouds
        int index = 1;
        for (int i= 0; i < cloudR.materials.Length; i++)
        {
            Color c = cloudR.materials[i].color;
            if(index == wd.cloudType)
            {
                c.a = Mathf.Lerp(c.a, 1.0f, Time.deltaTime * transitionSpeed);
                if (c.a > 0.98f)
                    c.a = 1.0f;
            }
            else
            {
                c.a = Mathf.Lerp(c.a, 0.0f, Time.deltaTime * transitionSpeed);
                if (c.a < 0.02f)
                    c.a = 0.0f;
            }
            cloudR.materials[i].color = c;
            index++;
        }

        // Nightsky
        index = 1;
        for (int i = 0; i < nightSkyR.materials.Length; i++)
        {
            Color c = nightSkyR.materials[i].color;
            if (index == wd.nightType)
            {
                c.a = Mathf.Lerp(c.a, Mathf.Abs(sun.intensity-1), Time.deltaTime * transitionSpeed);
                if (c.a > 0.98f)
                    c.a = 1.0f;
            }
            else
            {
                c.a = Mathf.Lerp(c.a, 0.0f, Time.deltaTime * transitionSpeed);
                if(c.a < 0.02f)
                    c.a = 0.0f;
            }
            nightSkyR.materials[i].color = c;
            index++;
        }
    }

    IEnumerator Thunder_C(float intensity)
    {
        while (isThundering)
        {
            yield return new WaitForSeconds(Random.Range(intensity, intensity * 10.0f));
            thunderLight.intensity = intensity / 4.0f;
            yield return new WaitForSeconds(0.07f);
            thunderLight.intensity = intensity / 2.0f;
            yield return new WaitForSeconds(0.05f);
            thunderLight.intensity = intensity;
            yield return new WaitForSeconds(0.03f);
            thunderLight.intensity = 0.0f;
            yield return new WaitForSeconds(0.5f);
            thunderSound.Play();
        }
    }

}

[System.Serializable]
public class WeatherDefinition {
    [Header("Downfall")]
    [Range(0, 5000)]
    public float rainAmount;

    [Header("Sky")]
    public int cloudType;
    public int nightType;

    [Header("Sun")]
    public Color sunColor;

    [Header("Fog")]
    public float fog;
    public Color fogColor;

    [Header("Thunder")]
    [Range(0, 10)]
    public float thunderIntensity;
}
