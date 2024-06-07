using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Forest_puzzle : MonoBehaviour
{
    public List<Light> lights;
    public Light helicopter;
    public Light directionalLight;
    //public int type;
    public float lightsIntensity;
    public float directionalLightIntensity, helicopterIntensity;
    public Slider lightsSlider, directionalSlider, helicopterSlider;
    //public Dropdown lightsDrop, ambientDrop, helicopterDrop;
    [SerializeField]
    private bool lightTypeCorrect = false, lightTpyeCorrectAmbiente = true, lightTypeCorrectHelicopter = false;
    public float tries = 0;
    public float lightsExampleIntensity = 1.75f, helicompterExample = 2.88f, ambientExample = 0.39f;
    public GameObject panel;
    public TMP_Text calificacion;


    // Start is called before the first frame update

    private void Start()
    {
        foreach (GameObject light in GameObject.FindGameObjectsWithTag("Light"))
        {
            lights.Add(light.GetComponent<Light>());
        }
    }

    public void ReturnToMaiNScene()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckAnswers()
    {
        float errorAbsolutoLuces = Mathf.Abs(lightsExampleIntensity - lightsIntensity);
        // Calcula el error relativo
        float errorRelativoLuces = errorAbsolutoLuces / lightsExampleIntensity;
        // Calcula el porcentaje de precisión
        float porcentajePrecision = 100f - (errorRelativoLuces * 100f);

        float errorAbsolutoHeli = Mathf.Abs(helicompterExample - helicopterIntensity);
        // Calcula el error relativo
        float errorRelativoHeli = errorAbsolutoHeli / helicompterExample;
        // Calcula el porcentaje de precisión
        float porcentajePrecisionHeli = 100f - (errorRelativoHeli * 100f);

        float errorAbsolutoLucesAmbiente = Mathf.Abs(ambientExample - directionalLightIntensity);
        // Calcula el error relativo
        float errorRelativoLucesAmbiente = errorAbsolutoLucesAmbiente / ambientExample;
        // Calcula el porcentaje de precisión
        float porcentajePrecisionAmbiente = 100f - (errorRelativoLucesAmbiente * 100f);

        Debug.Log("Porcentaje de precision de Luces: " + porcentajePrecision);
        Debug.Log("Porcentaje de precision de Helicoptero: " + porcentajePrecisionHeli);
        Debug.Log("Porcentaje de precision de Ambiente: " + porcentajePrecisionAmbiente);

        if (porcentajePrecision > 91 && porcentajePrecisionHeli > 91 && porcentajePrecisionAmbiente > 91 && lightTypeCorrect && lightTpyeCorrectAmbiente && lightTypeCorrectHelicopter)
        {
            panel.SetActive(true);
            calificacion.text = "Califiación: " + (10 - (tries / 2)).ToString();
            Debug.Log("Todo bien");
        }
        else
        {
            tries++;
        }

    }


    public void ChangeLights(int type)
    {
        Debug.Log(type);
        foreach (Light light in lights)
        {
            switch (type)
            {
                case 0:
                    light.type = LightType.Directional;
                    lightTypeCorrect = false;
                    break;
                case 1:
                    light.type = LightType.Spot;
                    lightTypeCorrect = false;
                    break;
                case 2:
                    light.type = LightType.Point;
                    lightTypeCorrect = true;
                    break;
                default:
                    light.type = LightType.Directional;
                    lightTypeCorrect = false;
                    break;
            }

            light.intensity = lightsIntensity;
        }
    }

    public void ChangeAmbientLight(int type)
    {
        switch (type)
        {
            case 0:
                directionalLight.type = LightType.Directional;
                lightTpyeCorrectAmbiente = true;
                break;
            case 1:
                directionalLight.type = LightType.Spot;
                lightTpyeCorrectAmbiente = false;
                break;
            case 2:
                directionalLight.type = LightType.Point;
                lightTpyeCorrectAmbiente = false;
                break;
            default:
                directionalLight.type = LightType.Directional;
                lightTpyeCorrectAmbiente = true;
                break;
        }

        directionalLight.intensity = directionalLightIntensity;

    }

    public void ChangeHelicopterLight(int type)
    {
        switch (type)
        {
            case 0:
                helicopter.type = LightType.Directional;
                lightTypeCorrectHelicopter = false;
                break;
            case 1:
                helicopter.type = LightType.Spot;
                lightTypeCorrectHelicopter = true;
                break;
            case 2:
                helicopter.type = LightType.Point;
                lightTypeCorrectHelicopter = false;
                break;
            default:
                helicopter.type = LightType.Directional;
                lightTypeCorrectHelicopter = false;
                break;
        }

        helicopter.intensity = helicopterIntensity;

    }

    public void ChangeLightsIntensity()
    {
        lightsIntensity = lightsSlider.value * 3;
        //directionalLightIntensity = directionalSlider.value;
        foreach (Light light in lights)
        {
            light.intensity = lightsIntensity;
        }
    }

    public void ChangeHelicopterIntensity()
    {
        Debug.Log(helicopterSlider.value);
        Debug.Log(directionalSlider.value);
        Debug.Log(lightsSlider.value);
        helicopterIntensity = helicopterSlider.value * 3;
        helicopter.intensity = helicopterIntensity;
    }

    public void ChangeDirectional()
    {
        Debug.Log(directionalSlider.value);
        directionalLightIntensity = directionalSlider.value * 2;
        directionalLight.intensity = directionalLightIntensity;
    }

    // Update is called once per frame
}
