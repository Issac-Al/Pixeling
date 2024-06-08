using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class colonial_puzzle : MonoBehaviour
{
    public List<Light> lights;
    public Light directionalLight;
    //public int type;
    public float lightsIntensity;
    public float directionalLightIntensity;
    public Slider lightsSlider, directionalSlider;
    public Dropdown lightsDrop;
    public bool lightTypeCorrect = false, lightTpyeCorrectAmbiente = true;
    public float tries = 0;
    private float lightsExampleIntensity = 2.4f, ambientExample = 1.2f;
    public GameObject panel;
    public TMP_Text calificacion;
    public DataManager dataManager;


    // Start is called before the first frame update

    private void Start()
    {
        tries = dataManager.playerDataSO.tries;
        foreach(GameObject light in GameObject.FindGameObjectsWithTag("Light"))
        {
            lights.Add(light.GetComponent<Light>());
        }
    }

    public void CheckAnswers()
    {
        float errorAbsolutoLuces = Mathf.Abs(lightsExampleIntensity - lightsIntensity);
        // Calcula el error relativo
        float errorRelativoLuces = errorAbsolutoLuces / lightsExampleIntensity;
        // Calcula el porcentaje de precisión
        float porcentajePrecision = 100f - (errorRelativoLuces * 100f);

        float errorAbsolutoLucesAmbiente = Mathf.Abs(ambientExample - directionalLightIntensity);
        // Calcula el error relativo
        float errorRelativoLucesAmbiente = errorAbsolutoLucesAmbiente / ambientExample;
        // Calcula el porcentaje de precisión
        float porcentajePrecisionAmbiente = 100f - (errorRelativoLucesAmbiente * 100f);

        Debug.Log("Porcentaje de precision de Luces: " + porcentajePrecision);
        Debug.Log("Porcentaje de precision de Ambiente: " + porcentajePrecisionAmbiente);

        if (porcentajePrecision > 91 && porcentajePrecisionAmbiente > 91 && lightTypeCorrect && lightTpyeCorrectAmbiente)
        {
            panel.SetActive(true);
            //calificacion.text = "Califiación: " + (10 - (tries / 2)).ToString();
        }
        else
        {
            dataManager.playerDataSO.tries++;
            tries++;
            dataManager.SaveData();
        }

    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(2);
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

        directionalLight.intensity = lightsIntensity;
        
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

    public void ChangeDirectional()
    {
        directionalLightIntensity = directionalSlider.value * 2;
        directionalLight.intensity = directionalLightIntensity;
    }

    // Update is called once per frame
    
}
